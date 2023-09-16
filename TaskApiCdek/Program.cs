using TaskApiCdek.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICdekClient, CdekClient>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapDefaultControllerRoute();

app.Run();
