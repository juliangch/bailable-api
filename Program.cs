using bailable_api.Models;
using bailable_api.Service;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ContextDb>(options => options.UseSqlServer(builder.Configuration["ConnectionString:Default"]));
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<ILocalService, LocalService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IServicioService, ServicioService>();
builder.Services.AddCors();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(
    options =>
      options
        .WithOrigins("http://localhost:8081", "http://localhost:8082")
        .AllowAnyHeader()
        .WithMethods("GET", "POST", "PUT", "DELETE")
        .AllowCredentials()
  );
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
