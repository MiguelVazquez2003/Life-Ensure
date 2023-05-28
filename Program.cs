using LifeEnsure.Data;
using LifeEnsure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using LifeEnsure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


using Microsoft.IdentityModel.Tokens;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlServer<TigreHacksContext>(builder.Configuration.GetConnectionString("Connection"));
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<CarroService>();
builder.Services.AddScoped<AccidenteService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<CsvService>();

// Configuraci�n para token de usuario
builder.Services.AddAuthentication("UsuarioToken")
    .AddJwtBearer("UsuarioToken", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:ClientKey"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


// Pol�tica de autorizaci�n para usuarios
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UsuarioPolicy", policy =>
    {
        policy.AuthenticationSchemes.Add("UsuarioToken");
        policy.RequireAuthenticatedUser();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
