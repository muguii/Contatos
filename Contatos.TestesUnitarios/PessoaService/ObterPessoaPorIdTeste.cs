using Contatos.Core.Entidades;
using Contatos.Core.Excecoes;
using Contatos.Core.Repositorios;
using Moq;

namespace Contatos.TestesUnitarios.PessoaService
{
    public class ObterPessoaPorIdTeste
    {
        [Fact]
        public async void PessoaComIdDoisExiste_Executado_RetornaPessoaComIdDois() // GIVEN_WHEN_THEN
        {
            // Arrange
            var pessoaIdMock = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var pessoaMock = new Pessoa("Muriel");

            var pessoaRepositorioMock = new Mock<IPessoaRepositorio>();
            pessoaRepositorioMock.Setup(pr => pr.ObterPorIdComDetalhesAsync(It.Is<Guid>(id => id == pessoaIdMock))).ReturnsAsync(pessoaMock);

            var pessoaService = new Aplicacao.Servicos.Implementacoes.PessoaService(pessoaRepositorioMock.Object);

            // Act
            var pessoa = await pessoaService.ObterPorIdAsync(pessoaIdMock);

            // Assert
            Assert.NotNull(pessoa);
            Assert.Equal(pessoaMock.Nome, pessoa.Nome);
            Assert.Equal(pessoaMock.CriadoEm, pessoa.CriadoEm);

            pessoaRepositorioMock.Verify(pr => pr.ObterPorIdComDetalhesAsync(It.Is<Guid>(id => id == pessoaIdMock)), Times.Once);
            pessoaRepositorioMock.Verify(pr => pr.ObterPorIdComDetalhesAsync(It.Is<Guid>(id => id != pessoaIdMock)), Times.Never);
        }

        [Fact]
        public async void PessoaComIdQuatroNaoExiste_Executado_LancarPessoaNaoExisteException() // GIVEN_WHEN_THEN
        {
            // Arrange
            var pessoaIdMock = Guid.Parse("44444444-4444-4444-4444-444444444444");
            Pessoa pessoaMock = null;

            var pessoaRepositorioMock = new Mock<IPessoaRepositorio>();
            pessoaRepositorioMock.Setup(pr => pr.ObterPorIdComDetalhesAsync(It.Is<Guid>(id => id == pessoaIdMock))).ReturnsAsync(pessoaMock);

            var pessoaService = new Aplicacao.Servicos.Implementacoes.PessoaService(pessoaRepositorioMock.Object);

            // Act
            var act = async () => await pessoaService.ObterPorIdAsync(pessoaIdMock);

            // Assert
            await Assert.ThrowsAsync<PessoaNaoExisteException>(act);

            pessoaRepositorioMock.Verify(pr => pr.ObterPorIdComDetalhesAsync(It.Is<Guid>(id => id == pessoaIdMock)), Times.Once);
            pessoaRepositorioMock.Verify(pr => pr.ObterPorIdComDetalhesAsync(It.Is<Guid>(id => id != pessoaIdMock)), Times.Never);
        }
    }
}
