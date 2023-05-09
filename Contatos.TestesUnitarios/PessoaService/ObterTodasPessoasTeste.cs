using Contatos.Core.Entidades;
using Contatos.Core.Repositorios;
using Moq;

namespace Contatos.TestesUnitarios.PessoaService
{
    public class ObterTodasPessoasTeste
    {
        [Fact]
        public async void DuasPessoasExistem_Executado_RetornarDuasPessoas() // GIVEN_WHEN_THEN
        {
            // Arrange
            var pessoasMock = new List<Pessoa>
            {
                new Pessoa("Muriel"),
                new Pessoa("Aurelio")
            };

            var pessoaRepositorioMock = new Mock<IPessoaRepositorio>();
            pessoaRepositorioMock.Setup(pr => pr.ObterTodosAsync()).ReturnsAsync(pessoasMock);

            var pessoaService = new Aplicacao.Servicos.Implementacoes.PessoaService(pessoaRepositorioMock.Object);

            // Act
            var pessoas = await pessoaService.ObterTodosAsync();

            // Assert
            Assert.NotNull(pessoas);
            Assert.NotEmpty(pessoas);
            Assert.Equal(pessoasMock.Count, pessoas.Count());

            pessoaRepositorioMock.Verify(pr => pr.ObterTodosAsync(), Times.Once);
        }
    }
}
