using Contatos.Core.Entidades;

namespace Contatos.Core.Repositorios
{
    public interface IPessoaRepositorio : IRepositorioSomenteLeitura<Pessoa>, IRepositorioSomenteEscrita<Pessoa>
    {
        Task<Pessoa> ObterPorIdComDetalhesAsync(Guid id);
        Task<Contato> ObterContatoPorIdAsync(Guid id);
        Task<Guid> AdicionarContatoAsync(Contato contato);
        Task AtualizarContatoAsync(Contato contato);
        Task ExcluirContatoAsync(Contato contato);
    }
}
