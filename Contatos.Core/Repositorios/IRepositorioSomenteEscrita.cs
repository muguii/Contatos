namespace Contatos.Core.Repositorios
{
    public interface IRepositorioSomenteEscrita<T>
    {
        Task<Guid> AdicionarAsync(T dados);
        Task AtualizarAsync(T dados);
        Task ExcluirAsync(T dados);
        Task SalvarAlteracoesAsync();
    }
}
