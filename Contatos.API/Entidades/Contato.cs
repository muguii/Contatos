using Contatos.API.Enums;

namespace Contatos.API.Entidades
{
    public class Contato : EntidadeBase
    {
        public string Nome { get; private set; }
        public ContatoTipo Tipo { get; private set; }
        public string Valor { get; private set; }

        public Guid PessoaId { get; private set; }

        public Contato(string nome, ContatoTipo tipo, string valor, Guid pessoaId) : base()
        {
            Nome = nome;
            Tipo = tipo;
            Valor = valor;
            PessoaId = pessoaId;
        }

        public void Atualizar(string nome, ContatoTipo tipo, string valor)
        {
            Nome = nome;
            Tipo = tipo;
            Valor = valor;
        }
    }
}
