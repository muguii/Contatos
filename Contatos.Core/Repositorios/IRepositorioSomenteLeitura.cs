namespace Contatos.Core.Repositorios
{
    public interface IRepositorioSomenteLeitura<T>
    {
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<T> ObterPorIdAsync(Guid id);
    }
}
