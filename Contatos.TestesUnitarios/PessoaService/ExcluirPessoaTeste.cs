using Contatos.Aplicacao.InputModels;
using Contatos.Core.Entidades;
using Contatos.Core.Excecoes;
using Contatos.Core.Repositorios;
using Moq;

namespace Contatos.TestesUnitarios.PessoaService
{
    public class ExcluirPessoaTeste
    {
        [Fact]
        public async void PessoaComIdDoisExiste_Executado_ExcluiPessoaComIdDois() // GIVEN_WHEN_THEN
        {
            // Arrange
            var pessoaIdMock = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var pessoaMock = new Pessoa("Muriel");

            var pessoaRepositorioMock = new Mock<IPessoaRepositorio>();
            pessoaRepositorioMock.Setup(pr => pr.ObterPorIdComDetalhesAsync(It.Is<Guid>(id => id == pessoaIdMock))).ReturnsAsync(pessoaMock);

            var pessoaService = new Aplicacao.Servicos.Implementacoes.PessoaService(pessoaRepositorioMock.Object);

            // Act
            await pessoaService.ExcluirAsync(pessoaIdMock);

            // Assert
            pessoaRepositorioMock.Verify(pr => pr.ObterPorIdComDetalhesAsync(It.Is<Guid>(id => id == pessoaIdMock)), Times.Once);
            pessoaRepositorioMock.Verify(pr => pr.ObterPorIdComDetalhesAsync(It.Is<Guid>(id => id != pessoaIdMock)), Times.Never);
            pessoaRepositorioMock.Verify(pr => pr.ExcluirAsync(It.IsAny<Pessoa>()), Times.Once);
        }

        [Fact]
        public async void PessoaComIdQuatroNaoExiste_Executado_LancarPessoaNaoExisteException() // GIVEN_WHEN_THEN
        {
            // Arrange
            var pessoaIdMock = Guid.Parse("44444444-4444-4444-4444-444444444444");
            Pessoa pessoaMock = null;

            var inputModel = new AtualizarPessoaInputModel
            {
                Nome = "Novo nome"
            };

            var pessoaRepositorioMock = new Mock<IPessoaRepositorio>();
            pessoaRepositorioMock.Setup(pr => pr.ObterPorIdComDetalhesAsync(It.Is<Guid>(id => id == pessoaIdMock))).ReturnsAsync(pessoaMock);

            var pessoaService = new Aplicacao.Servicos.Implementacoes.PessoaService(pessoaRepositorioMock.Object);

            // Act
            var act = async () => await pessoaService.ExcluirAsync(pessoaIdMock);

            // Assert
            await Assert.ThrowsAsync<PessoaNaoExisteException>(act);

            pessoaRepositorioMock.Verify(pr => pr.ObterPorIdComDetalhesAsync(It.Is<Guid>(id => id == pessoaIdMock)), Times.Once);
            pessoaRepositorioMock.Verify(pr => pr.ObterPorIdComDetalhesAsync(It.Is<Guid>(id => id != pessoaIdMock)), Times.Never);
            pessoaRepositorioMock.Verify(pr => pr.ExcluirAsync(It.IsAny<Pessoa>()), Times.Never);
        }
    }
}
