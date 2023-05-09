using Contatos.Aplicacao.InputModels;
using Contatos.Core.Entidades;
using Contatos.Core.Excecoes;
using Contatos.Core.Repositorios;
using Moq;

namespace Contatos.TestesUnitarios.PessoaService
{
    public class ExcluirContatoTeste
    {
        [Fact]
        public async void ContatoComIdDoisExiste_Executado_ExcluiContatoComIdDois() // GIVEN_WHEN_THEN
        {
            // Arrange
            var contatoIdMock = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var contatoMock = new Contato("Mae", Core.Enums.ContatoTipo.Whatsapp, "48984842222", new Guid());

            var pessoaRepositorioMock = new Mock<IPessoaRepositorio>();
            pessoaRepositorioMock.Setup(pr => pr.ObterContatoPorIdAsync(It.Is<Guid>(id => id == contatoIdMock))).ReturnsAsync(contatoMock);

            var pessoaService = new Aplicacao.Servicos.Implementacoes.PessoaService(pessoaRepositorioMock.Object);

            // Act
            await pessoaService.ExcluirContatoAsync(contatoIdMock);

            // Assert
            pessoaRepositorioMock.Verify(pr => pr.ObterContatoPorIdAsync(It.Is<Guid>(id => id == contatoIdMock)), Times.Once);
            pessoaRepositorioMock.Verify(pr => pr.ObterContatoPorIdAsync(It.Is<Guid>(id => id != contatoIdMock)), Times.Never);
            pessoaRepositorioMock.Verify(pr => pr.ExcluirContatoAsync(It.IsAny<Contato>()), Times.Once);
        }

        [Fact]
        public async void ContatoComIdQuatroNaoExiste_Executado_LancarContatoNaoExisteException() // GIVEN_WHEN_THEN
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
            var act = async () => await pessoaService.ExcluirContatoAsync(contatoIdMock);

            // Assert
            await Assert.ThrowsAsync<ContatoNaoExisteException>(act);

            pessoaRepositorioMock.Verify(pr => pr.ObterContatoPorIdAsync(It.Is<Guid>(id => id == contatoIdMock)), Times.Once);
            pessoaRepositorioMock.Verify(pr => pr.ObterContatoPorIdAsync(It.Is<Guid>(id => id != contatoIdMock)), Times.Never);
            pessoaRepositorioMock.Verify(pr => pr.ExcluirContatoAsync(It.IsAny<Contato>()), Times.Never);
        }
    }
}
