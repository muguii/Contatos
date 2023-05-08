using Contatos.API.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Contatos.API.Persistencia
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
            builder.Entity<Pessoa>(b =>
            {
                b.HasKey(pessoa => pessoa.Id);

                b.Property(pessoa => pessoa.Nome).HasMaxLength(128);

                b.HasMany(pessoa => pessoa.Contatos)
                 .WithOne()
                 .HasForeignKey(pessoa => pessoa.PessoaId)
                 .OnDelete(DeleteBehavior.Restrict); // EXCECAO CASO EU EXCLUA FISICAMENTE UMA PESSOA QUE POSSUA CONTATOS
            });

            builder.Entity<Contato>(b =>
            {
                b.HasKey(contato => contato.Id);

                b.Property(contato => contato.Nome).HasMaxLength(128);
                b.Property(contato => contato.Valor).HasMaxLength(64);
            });
        }
    }
}
