using Contatos.Aplicacao.InputModels;
using Contatos.Aplicacao.Servicos.Interfaces;
using Contatos.Core.Entidades;
using Contatos.Core.Excecoes;
using Contatos.Core.Repositorios;

namespace Contatos.Aplicacao.Servicos.Implementacoes
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepositorio _pessoaRepositorio;

        public PessoaService(IPessoaRepositorio pessoaRepositorio)
        {
            _pessoaRepositorio = pessoaRepositorio;
        }

        public async Task<IEnumerable<Pessoa>> ObterTodosAsync()
        {
            return await _pessoaRepositorio.ObterTodosAsync();
        }

        public async Task<Pessoa> ObterPorIdAsync(Guid id)
        {
            return await this.ObterPessoaPorIdIncluindoContatosAsync(id);
        }

        public async Task<Guid> AdicionarAsync(AdicionarPessoaInputModel inputModel)
        {
            return await _pessoaRepositorio.AdicionarAsync(new Pessoa(inputModel.Nome));
        }

        public async Task AtualizarAsync(Guid id, AtualizarPessoaInputModel inputModel)
        {
            var pessoa = await this.ObterPessoaPorIdAsync(id);

            pessoa.Atualizar(inputModel.Nome);

            await _pessoaRepositorio.AtualizarAsync(pessoa);
        }

        public async Task ExcluirAsync(Guid id)
        {
            var pessoa = await this.ObterPessoaPorIdIncluindoContatosAsync(id);

            await _pessoaRepositorio.ExcluirAsync(pessoa);
        }

        public async Task<Guid> AdicionarContatoAsync(AdicionarContatoInputModel inputModel)
        {
            await this.ObterPessoaPorIdAsync(inputModel.PessoaId);

            var contato = new Contato(inputModel.Nome, inputModel.Tipo, inputModel.Valor, inputModel.PessoaId);

            return await _pessoaRepositorio.AdicionarContatoAsync(contato);
        }

        public async Task AtualizarContatoAsync(Guid contatoId, AtualizarContatoInputModel inputModel)
        {
            var contato = await this.ObterContatoPorIdAsync(contatoId);

            contato.Atualizar(inputModel.Nome, inputModel.Tipo, inputModel.Valor);

            await _pessoaRepositorio.AtualizarContatoAsync(contato);
        }

        public async Task ExcluirContatoAsync(Guid id)
        {
            var contato = await this.ObterContatoPorIdAsync(id);

            await _pessoaRepositorio.ExcluirContatoAsync(contato);
        }

        private async Task<Pessoa> ObterPessoaPorIdAsync(Guid id)
        {
            var pessoa = await _pessoaRepositorio.ObterPorIdAsync(id);
            if (pessoa is null)
                throw new PessoaNaoExisteException(id);

            return pessoa;
        }

        private async Task<Pessoa> ObterPessoaPorIdIncluindoContatosAsync(Guid id)
        {
            var pessoa = await _pessoaRepositorio.ObterPorIdComDetalhesAsync(id);
            if (pessoa is null)
                throw new PessoaNaoExisteException(id);

            return pessoa;
        }

        private async Task<Contato> ObterContatoPorIdAsync(Guid id)
        {
            var contato = await _pessoaRepositorio.ObterContatoPorIdAsync(id);
            if (contato is null)
                throw new ContatoNaoExisteException(id);

            return contato;
        }
    }
}
