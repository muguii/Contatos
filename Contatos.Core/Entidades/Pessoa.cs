namespace Contatos.Core.Entidades
{
    public class Pessoa : EntidadeBase
    {
        public string Nome { get; private set; }

        public List<Contato> Contatos { get; private set; }

        public Pessoa(string nome) : base()
        {
            Nome = nome;
            Contatos = new List<Contato>();
        }

        public void Atualizar(string nome)
        {
            Nome = nome;
        }
    }
}
