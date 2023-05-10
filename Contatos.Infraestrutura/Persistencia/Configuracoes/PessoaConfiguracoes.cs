using Contatos.Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contatos.Infraestrutura.Persistencia.Configuracoes
{
    public class PessoaConfiguracoes : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasKey(pessoa => pessoa.Id);

            builder.Property(pessoa => pessoa.Nome).HasMaxLength(128);

            builder.HasMany(pessoa => pessoa.Contatos)
                   .WithOne()
                   .HasForeignKey(pessoa => pessoa.PessoaId)
                   .OnDelete(DeleteBehavior.Restrict); // Excecao caso exclua fisicamente uma pessoa que possua contatos
        }
    }
}