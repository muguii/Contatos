using Contatos.Aplicacao.InputModels;
using Contatos.Core.Entidades;
using Contatos.Core.Repositorios;
using Moq;

namespace Contatos.TestesUnitarios.PessoaService
{
    public class AdicionarPessoaTeste
    {
        [Fact]
        public async void DadosDeEntradaEstaoOk_Executado_RetornarPessoaId() // GIVEN_WHEN_THEN
        {
            // Arrange
            var idMock = new Guid();
            var inputModel = new AdicionarPessoaInputModel
            {
                Nome = "Muriel"
            };

            var pessoaRepositorioMock = new Mock<IPessoaRepositorio>();
            pessoaRepositorioMock.Setup(pr => pr.AdicionarAsync(It.IsAny<Pessoa>())).ReturnsAsync(idMock);

            var pessoaService = new Aplicacao.Servicos.Implementacoes.PessoaService(pessoaRepositorioMock.Object);

            // Act
            var id = await pessoaService.AdicionarAsync(inputModel);

            // Assert
            Assert.Equal(idMock, id);

            pessoaRepositorioMock.Verify(pr => pr.AdicionarAsync(It.IsAny<Pessoa>()), Times.Once);
        }
    }
}
