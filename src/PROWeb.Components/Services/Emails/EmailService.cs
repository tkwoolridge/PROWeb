using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PROWeb.Common.Helpers;
using Serilog;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks.Dataflow;

namespace PROWeb.Components.Services.Emails
{
    public class EmailService : IHostedService, IDisposable
    {
        private readonly BufferBlock<MailMessage> _mailMessages;
        private Task? _sendTask;
        private CancellationTokenSource? _cancellationTokenSource;

        private readonly IOptionsMonitor<SmtpSettings> _smtpSettings;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _environment;

        public EmailService(ILogger logger, IOptionsMonitor<SmtpSettings> smtpSettings, IWebHostEnvironment environment)
        {
            _smtpSettings = smtpSettings;
            _logger = logger;
            _environment = environment;

            _mailMessages = new BufferBlock<MailMessage>();
        }

        public async Task SendEmailAsync(string subject, string content, string to)
        {
            SmtpSettings settings = _smtpSettings.CurrentValue;
            MailMessage mail = new MailMessage();

            try
            {

                mail.To.Add(to);
                mail.From = new MailAddress(settings.SenderEmail!);
                mail.Subject = subject;

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(content, null, "text/html");

                if (_environment.WebRootFileProvider.GetFileInfo($"{RazorLibHelpers.GetWebRootPath()}/Templates/EmailLogo.png").PhysicalPath is { } logoPath)
                {
                    LinkedResource imagelink = new LinkedResource(logoPath, "image/png");
                    imagelink.ContentId = "logoId";
                    imagelink.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                    htmlView.LinkedResources.Add(imagelink);
                }

                mail.AlternateViews.Add(htmlView);

                await SendEmailAsync(mail);
            }
            catch (Exception exception)
            {
                _logger.Error(exception.Message);
            }
        }

        private async Task SendEmailAsync(MailMessage mail)
        {
            SmtpSettings settings = _smtpSettings.CurrentValue;

            NetworkCredential credential = CredentialCache.DefaultNetworkCredentials;

            if (!string.IsNullOrEmpty(settings.UserName))
            {
                credential = new NetworkCredential(settings.UserName, settings.Password);
            }

            SmtpClient smtp = new SmtpClient
            {
                Host = settings.Server!,
                Port = settings.Port ?? 25,
                Credentials = credential
            };

            using (smtp)
            {
                await smtp.SendMailAsync(mail);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Information("Starting background e-mail delivery");
            _cancellationTokenSource = new CancellationTokenSource();
            // The StartAsync method just needs to start a background task (or a timer)
            _sendTask = DeliverAsync(_cancellationTokenSource.Token);
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            //Let's cancel the e-mail delivery
            CancelSendTask();
            //Next, we wait for sendTask to end, but no longer than what the web host allows

            if (_sendTask is { } task)
            {
                await Task.WhenAny(_sendTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        private void CancelSendTask()
        {
            try
            {
                if (_cancellationTokenSource != null)
                {
                    _logger.Information("Stopping e-mail background delivery");
                    _cancellationTokenSource.Cancel();
                    _cancellationTokenSource = null;
                }
            }
            catch
            {
            }
        }

        public async Task DeliverAsync(CancellationToken token)
        {
            _logger.Information("E-mail background delivery started");

            while (!token.IsCancellationRequested)
            {
                MailMessage? mail = null;

                try
                {
                    SmtpSettings settings = _smtpSettings.CurrentValue;

                    // Let's wait for a message to appear in the queue
                    // If the token gets canceled, then we'll stop waiting
                    // since an OperationCanceledException will be thrown
                    mail = await _mailMessages.ReceiveAsync(token);

                    await SendEmailAsync(mail);
                }
                catch (OperationCanceledException)
                {
                    _logger.Warning($"Sending email {mail?.To} failed!");
                    break;
                }
            }

            _logger.Information("E-mail background delivery stopped");
        }

        public void Dispose()
        {
            CancelSendTask();
        }
    }
}
