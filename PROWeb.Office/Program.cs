using Mapster;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.SignalR;
using PROWeb.Authentication.DependencyInjection;
using PROWeb.Components.DependencyInjection;
using PROWeb.Data.DependencyInjection;
using PROWeb.Data.Services.Extensions;
using PROWeb.Office;
using PROWeb.Office.Mapper;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .WriteTo.Console());

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("en-GB") };

    // State what the default culture for your application is. This will be used if no specific culture
    // can be determined for a given request.
    options.DefaultRequestCulture = new RequestCulture(culture: "en-GB", uiCulture: "en-GB");

    // You must explicitly state which cultures your application supports.
    // These are the cultures the app supports for formatting numbers, dates, etc.
    options.SupportedCultures = supportedCultures;

    // These are the cultures the app supports for UI strings, i.e. we have localized resources for.
    options.SupportedUICultures = supportedCultures;
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddTelerikBlazor();
builder.AddPROWebDataModule();
builder.AddPROWebDataAuthenticationModule();
builder.AddPROWebComponentsModule();
builder.AddPROWebAuthenticationModule();

var config = TypeAdapterConfig.GlobalSettings;
config.Scan(typeof(OfficeMapper).Assembly);
builder.Services.AddSingleton(config);

// SignalR message size for FileSelect
builder.Services.Configure<HubOptions>(options =>
{
    options.MaximumReceiveMessageSize = 1024 * 1024; // 1MB
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapPROWebAuthenticationEndpoints();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
