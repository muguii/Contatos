namespace Contatos.Core.Excecoes
{
    public class ContatoNaoExisteException : Exception
    {
        public ContatoNaoExisteException(Guid id) : base($"Não existe um contato com o id {id}")
        {

        }
    }
}
