using Contatos.Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contatos.Infraestrutura.Persistencia.Configuracoes
{
    public class ContatoConfiguracoes : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.HasKey(contato => contato.Id);

            builder.Property(contato => contato.Nome).HasMaxLength(128);
            builder.Property(contato => contato.Valor).HasMaxLength(64);
        }
    }
}