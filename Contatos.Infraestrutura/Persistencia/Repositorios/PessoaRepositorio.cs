using Contatos.Core.Entidades;
using Contatos.Core.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Infraestrutura.Persistencia.Repositorios
{
    public class PessoaRepositorio : IPessoaRepositorio
    {
        private readonly ContatoDbContext _dbContext;

        public PessoaRepositorio(ContatoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Pessoa>> ObterTodosAsync()
        {
            return await _dbContext.Pessoa.Include(pessoa => pessoa.Contatos)
                                          .ToListAsync();
        }

        public async Task<Pessoa> ObterPorIdAsync(Guid id)
        {
            return await _dbContext.Pessoa.SingleOrDefaultAsync(pessoa => pessoa.Id == id);
        }

        public async Task<Pessoa> ObterPorIdComDetalhesAsync(Guid id)
        {
            return await _dbContext.Pessoa.Include(pessoa => pessoa.Contatos)
                                          .SingleOrDefaultAsync(pessoa => pessoa.Id == id);
        }

        public async Task<Guid> AdicionarAsync(Pessoa pessoa)
        {
            await _dbContext.Pessoa.AddAsync(pessoa);
            await SalvarAlteracoesAsync();

            return pessoa.Id;
        }

        public async Task AtualizarAsync(Pessoa pessoa)
        {
            _dbContext.Update(pessoa);
            await SalvarAlteracoesAsync();
        }

        public async Task ExcluirAsync(Pessoa pessoa)
        {
            _dbContext.Contato.RemoveRange(pessoa.Contatos);
            _dbContext.Pessoa.Remove(pessoa);
            await SalvarAlteracoesAsync();
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Contato> ObterContatoPorIdAsync(Guid id)
        {
            return await _dbContext.Contato.SingleOrDefaultAsync(contato => contato.Id == id);
        }

        public async Task<Guid> AdicionarContatoAsync(Contato contato)
        {
            await _dbContext.Contato.AddAsync(contato);
            await SalvarAlteracoesAsync();

            return contato.Id;
        }

        public async Task AtualizarContatoAsync(Contato contato)
        {
            _dbContext.Contato.Update(contato);
            await SalvarAlteracoesAsync();
        }

        public async Task ExcluirContatoAsync(Contato contato)
        {
            _dbContext.Contato.Remove(contato);
            await SalvarAlteracoesAsync();
        }
    }
}
