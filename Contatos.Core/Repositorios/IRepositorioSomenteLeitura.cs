namespace Contatos.Core.Repositorios
{
    public interface IRepositorioSomenteLeitura<T>
    {
        Task<IEnumerable<T>> ObterTodos();
        Task<T> ObterPorId(Guid id);
    }
}
