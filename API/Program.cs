using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Middleware;
using Company.ClassLibrary1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAppServices(builder.Configuration);

builder.Services.AddJWTService(builder.Configuration);


var app = builder.Build();

// config the HTTP request pipeline

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.Run();
