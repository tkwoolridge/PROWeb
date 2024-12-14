using PROWeb.Service.Endpoints;
using PROWeb.Service.Services.DependencyInjections;
using PROWeb.Service.Repositories.DependencyInjections;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddProRepositories();
builder.Services.AddProServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapProWebSiteEndpoints();

app.Run();
