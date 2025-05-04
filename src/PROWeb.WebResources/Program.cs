using PROWeb.WebResources.DependencyInjection;
using PROWeb.WebResources.Endpoints;
using PROWeb.WebService.Authentication.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .WriteTo.Console());

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApiKeyAuthentication(builder.Configuration);
builder.Services.AddWebResourcesOptions(builder.Configuration);
builder.Services.AddTCDPhotosOptions(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapWebResourcesEndpoints();
app.Run();

public partial class Program { }
