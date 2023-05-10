using Contatos.Core.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Contatos.Infraestrutura.Persistencia
{
    public class ContatoDbContext : DbContext
    {
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Contato> Contato { get; set; }

        public ContatoDbContext(DbContextOptions<ContatoDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
