using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ParkingApplication.BL.Extensions;
using ParkingApplication.DAL.Context;
using ParkingApplication.DAL.Extensions;
using ParkingApplication.Extensions;
using ParkingApplication.Middleware;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Inject(configuration);
builder.Services.AddServiceInjection();
builder.Services.AddMapper();

builder.Services.AddSwaggerGen();

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidIssuer = AuthOptions.ISSUER,
//             ValidateAudience = true,
//             ValidAudience = AuthOptions.AUDIENCE,
//             ValidateLifetime = true,
//             IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
//             ValidateIssuerSigningKey = true,
//         };
//     });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.RoutePrefix = "docs";
    config.SwaggerEndpoint("/swagger/v1/swagger.json", "ParkingApplication.PL");
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCustomExceptionHandler();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();