using LifeEnsure.Data;
using LifeEnsure.Services;
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
