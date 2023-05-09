using Contatos.Aplicacao.InputModels;
using Contatos.Core.Entidades;
using Contatos.Core.Excecoes;
using Contatos.Core.Repositorios;
using Moq;

namespace Contatos.TestesUnitarios.PessoaService
{
    public class AdicionarContatoTeste
    {
        [Fact]
        public async void PessoaComIdDoisExisteEDadosDeEntradaEstaoOk_Executado_RetornarContatoId() // GIVEN_WHEN_THEN
        {
            // Arrange
            var pessoaIdMock = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var pessoaMock = new Pessoa("Muriel");

            var idMock = new Guid();
            var inputModel = new AdicionarContatoInputModel
            {
                Nome = "Mae",
                Tipo = Core.Enums.ContatoTipo.Whatsapp,
                Valor = "48984843232",
                PessoaId = pessoaIdMock
            };

            var pessoaRepositorioMock = new Mock<IPessoaRepositorio>();
            pessoaRepositorioMock.Setup(pr => pr.AdicionarContatoAsync(It.IsAny<Contato>())).ReturnsAsync(idMock);
            pessoaRepositorioMock.Setup(pr => pr.ObterPorIdAsync(It.Is<Guid>(id => id == pessoaIdMock))).ReturnsAsync(pessoaMock);

            var pessoaService = new Aplicacao.Servicos.Implementacoes.PessoaService(pessoaRepositorioMock.Object);

            // Act
            var id = await pessoaService.AdicionarContatoAsync(inputModel);

            // Assert
            Assert.Equal(idMock, id);

            pessoaRepositorioMock.Verify(pr => pr.AdicionarContatoAsync(It.IsAny<Contato>()), Times.Once);

            pessoaRepositorioMock.Verify(pr => pr.ObterPorIdAsync(It.Is<Guid>(id => id == pessoaIdMock)), Times.Once);
            pessoaRepositorioMock.Verify(pr => pr.ObterPorIdAsync(It.Is<Guid>(id => id != pessoaIdMock)), Times.Never);
        }

        [Fact]
        public async void PessoaComIdQuatroNaoExisteEDadosDeEntradaEstaoOk_Executado_LancarPessoaNaoExisteException() // GIVEN_WHEN_THEN
        {
            // Arrange
            var pessoaIdMock = Guid.Parse("44444444-4444-4444-4444-444444444444");
            Pessoa pessoaMock = null;

            var idMock = new Guid();
            var inputModel = new AdicionarContatoInputModel
            {
                Nome = "Mae",
                Tipo = Core.Enums.ContatoTipo.Whatsapp,
                Valor = "48984843232",
                PessoaId = pessoaIdMock
            };

            var pessoaRepositorioMock = new Mock<IPessoaRepositorio>();
            pessoaRepositorioMock.Setup(pr => pr.AdicionarContatoAsync(It.IsAny<Contato>())).ReturnsAsync(idMock);
            pessoaRepositorioMock.Setup(pr => pr.ObterPorIdAsync(It.Is<Guid>(id => id == pessoaIdMock))).ReturnsAsync(pessoaMock);

            var pessoaService = new Aplicacao.Servicos.Implementacoes.PessoaService(pessoaRepositorioMock.Object);

            // Act
            var act = async () => await pessoaService.AdicionarContatoAsync(inputModel);

            // Assert
            await Assert.ThrowsAsync<PessoaNaoExisteException>(act);

            pessoaRepositorioMock.Verify(pr => pr.AdicionarContatoAsync(It.IsAny<Contato>()), Times.Never);

            pessoaRepositorioMock.Verify(pr => pr.ObterPorIdAsync(It.Is<Guid>(id => id == pessoaIdMock)), Times.Once);
            pessoaRepositorioMock.Verify(pr => pr.ObterPorIdAsync(It.Is<Guid>(id => id != pessoaIdMock)), Times.Never);
        }
    }
}
