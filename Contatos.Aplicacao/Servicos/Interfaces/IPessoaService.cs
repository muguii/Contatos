using Contatos.Aplicacao.InputModels;
using Contatos.Core.Entidades;

namespace Contatos.Aplicacao.Servicos.Interfaces
{
    public interface IPessoaService
    {
        Task<IEnumerable<Pessoa>> ObterTodosAsync();
        Task<Pessoa> ObterPorIdAsync(Guid id);
        Task<Guid> AdicionarAsync(AdicionarPessoaInputModel inputModel);
        Task AtualizarAsync(Guid id, AtualizarPessoaInputModel inputModel);
        Task ExcluirAsync(Guid id);
        Task<Guid> AdicionarContatoAsync(AdicionarContatoInputModel inputModel);
        Task AtualizarContatoAsync(Guid contatoId, AtualizarContatoInputModel inputModel);
        Task ExcluirContatoAsync(Guid id);
    }
}
