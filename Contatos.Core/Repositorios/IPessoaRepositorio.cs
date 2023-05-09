using Contatos.Core.Entidades;

namespace Contatos.Core.Repositorios
{
    public interface IPessoaRepositorio : IRepositorioSomenteLeitura<Pessoa>, IRepositorioSomenteEscrita<Pessoa>
    {
        Task<Pessoa> ObterPorIdComDetalhes(Guid id);
        Task AdicionarContatoAsync(Contato contato);
        Task AtualizarContatoAsync(Contato contato);
        Task ExcluirContatoAsync(Contato contato);
    }
}
