namespace Contatos.Core.Excecoes
{
    public class PessoaNaoExisteException : Exception
    {
        public PessoaNaoExisteException(Guid id) : base($"Não existe uma pessoa com o id {id}")
        {

        }
    }
}
