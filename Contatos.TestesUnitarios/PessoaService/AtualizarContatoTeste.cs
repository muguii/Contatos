using Contatos.Aplicacao.InputModels;
using Contatos.Core.Entidades;
using Contatos.Core.Excecoes;
using Contatos.Core.Repositorios;
using Moq;

namespace Contatos.TestesUnitarios.PessoaService
{
    public class AtualizarContatoTeste
    {
        [Fact]
        public async void ContatoComIdDoisExisteEDadosDeEntradaEstaoOk_Executado_AlterarDadosDoContato() // GIVEN_WHEN_THEN
        {
            // Arrange
            var pessoaIdMock = Guid.Parse("44444444-4444-4444-4444-444444444444");

            var contatoIdMock = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var nomeContatoMock = "Muriel";
            var tipoContatoMock = Core.Enums.ContatoTipo.Whatsapp;
            var valorContatoMock = "48999992323";
            var contatoMock = new Contato(nomeContatoMock, tipoContatoMock, valorContatoMock, pessoaIdMock);

            var inputModel = new AtualizarContatoInputModel
            {
                Nome = "Novo nome",
                Tipo = Core.Enums.ContatoTipo.Email,
                Valor = "48911112222"
            };

            var pessoaRepositorioMock = new Mock<IPessoaRepositorio>();
            pessoaRepositorioMock.Setup(pr => pr.ObterContatoPorIdAsync(It.Is<Guid>(id => id == contatoIdMock))).ReturnsAsync(contatoMock);

            var pessoaService = new Aplicacao.Servicos.Implementacoes.PessoaService(pessoaRepositorioMock.Object);

            // Act
            await pessoaService.AtualizarContatoAsync(contatoIdMock, inputModel);

            // Assert
            Assert.NotEqual(nomeContatoMock, contatoMock.Nome);
            Assert.NotEqual(tipoContatoMock, contatoMock.Tipo);
            Assert.NotEqual(valorContatoMock, contatoMock.Valor);

            pessoaRepositorioMock.Verify(pr => pr.ObterContatoPorIdAsync(It.Is<Guid>(id => id == contatoIdMock)), Times.Once);
            pessoaRepositorioMock.Verify(pr => pr.ObterContatoPorIdAsync(It.Is<Guid>(id => id != contatoIdMock)), Times.Never);

            pessoaRepositorioMock.Verify(pr => pr.AtualizarContatoAsync(It.IsAny<Contato>()), Times.Once);
        }

        [Fact]
        public async void ContatoComIdQuatroNaoExisteEDadosDeEntradaEstaoOk_Executado_LancarContatoNaoExisteException() // GIVEN_WHEN_THEN
        {
            // Arrange
            var contatoIdMock = Guid.Parse("44444444-4444-4444-4444-444444444444");
            Contato contatoMock = null;

            var inputModel = new AtualizarContatoInputModel
            {
                Nome = "Novo nome",
                Tipo = Core.Enums.ContatoTipo.Email,
                Valor = "48911112222"
            };

            var pessoaRepositorioMock = new Mock<IPessoaRepositorio>();
            pessoaRepositorioMock.Setup(pr => pr.ObterContatoPorIdAsync(It.Is<Guid>(id => id == contatoIdMock))).ReturnsAsync(contatoMock);

            var pessoaService = new Aplicacao.Servicos.Implementacoes.PessoaService(pessoaRepositorioMock.Object);

            // Act
            var act = async () => await pessoaService.AtualizarContatoAsync(contatoIdMock, inputModel);

            // Assert
            await Assert.ThrowsAsync<ContatoNaoExisteException>(act);

            pessoaRepositorioMock.Verify(pr => pr.ObterContatoPorIdAsync(It.Is<Guid>(id => id == contatoIdMock)), Times.Once);
            pessoaRepositorioMock.Verify(pr => pr.ObterContatoPorIdAsync(It.Is<Guid>(id => id != contatoIdMock)), Times.Never);

            pessoaRepositorioMock.Verify(pr => pr.AtualizarContatoAsync(It.IsAny<Contato>()), Times.Never);
        }
    }
}
