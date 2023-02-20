global using EFCoreTesting.Data;
global using Microsoft.EntityFrameworkCore;
global using IntegrationLibrary;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
builder.Services.AddDbContext<KDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<Integration>(provider => new Integration(provider.GetService<KDBContext>()));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseFileServer();

app.UseAuthorization();

app.MapControllers();

app.Run();
