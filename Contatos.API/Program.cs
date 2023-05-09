using Contatos.API.Persistencia;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// SQL SERVER
//string connectionString = builder.Configuration.GetConnectionString("ContatoCs");
//builder.Services.AddDbContext<ContatoDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ContatoDbContext>(options => options.UseInMemoryDatabase("ContatoDb"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
