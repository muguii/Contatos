using Contatos.Aplicacao.Servicos.Implementacoes;
using Contatos.Aplicacao.Servicos.Interfaces;
using Contatos.Core.Repositorios;
using Contatos.Infraestrutura.Persistencia;
using Contatos.Infraestrutura.Persistencia.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// SQL SERVER
//string connectionString = builder.Configuration.GetConnectionString("ContatoCs");
//builder.Services.AddDbContext<ContatoDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ContatoDbContext>(options => options.UseInMemoryDatabase("ContatoDb"));

builder.Services.AddScoped<IPessoaService, PessoaService>();
builder.Services.AddScoped<IPessoaRepositorio, PessoaRepositorio>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
