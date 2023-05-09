using Contatos.Core.Enums;

namespace Contatos.Aplicacao.InputModels
{
    public class AdicionarContatoInputModel
    {
        public Guid PessoaId { get; set; }
        public string Nome { get; set; }
        public ContatoTipo Tipo { get; set; }
        public string Valor { get; set; }
    }
}
