using Contatos.Core.Enums;

namespace Contatos.Aplicacao.InputModels
{
    public class AtualizarContatoInputModel
    {
        public string Nome { get; set; }
        public ContatoTipo Tipo { get; set; }
        public string Valor { get; set; }
    }
}
