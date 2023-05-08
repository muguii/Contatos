namespace Contatos.API.Entidades
{
    public class EntidadeBase
    {
        public Guid Id { get; protected set; }
        public DateTime CriadoEm { get; private set; }
        public bool Deletado { get; protected set; }

        public EntidadeBase()
        {
            CriadoEm = DateTime.Now;
            Deletado = false;
        }

        // Exclusão lógica
        public void Excluir() => Deletado = true;
    }
}
