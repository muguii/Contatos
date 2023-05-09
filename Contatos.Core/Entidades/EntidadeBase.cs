namespace Contatos.Core.Entidades
{
    public class EntidadeBase
    {
        public Guid Id { get; protected set; }
        public DateTime CriadoEm { get; private set; }

        public EntidadeBase()
        {
            CriadoEm = DateTime.Now;
        }
    }
}
