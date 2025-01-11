using PROWeb.WebSiteService.Endpoints;
using PROWeb.WebSiteService.Services.DependencyInjections;
using PROWeb.WebSiteService.Repositories.DependencyInjections;
using Serilog;
using PROWeb.WebSiteService.Authentication.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .WriteTo.Console());

// Add services to the container.
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

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapProWebSiteEndpoints();

app.Run();

public partial class Program { } 
