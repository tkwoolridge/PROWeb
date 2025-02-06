using PROWeb.WebSiteService.Endpoints;
using PROWeb.WebSiteService.Services.DependencyInjections;
using PROWeb.WebSiteService.Repositories.DependencyInjections;
using Serilog;
using PROWeb.WebSiteService.Authentication.DependencyInjection;
using PROWeb.WebSiteService;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .WriteTo.Console());

builder.WebHost.UseSentry();

builder.Services.AddExceptionHandler<SentryExceptionHandler>();
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddProAuthentication(builder.Configuration);
builder.Services.AddProRepositories();
builder.Services.AddProServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
else
{
    app.UseExceptionHandler();
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapProWebSiteEndpoints();

app.Run();


public partial class Program { } 
