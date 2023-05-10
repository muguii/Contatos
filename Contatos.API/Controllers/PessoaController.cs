using Contatos.Aplicacao.InputModels;
using Contatos.Aplicacao.Servicos.Interfaces;
using Contatos.Core.Excecoes;
using Microsoft.AspNetCore.Mvc;

namespace Contatos.API.Controllers
{
    [ApiController]
    [Route("api/pessoas")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoaController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _pessoaService.ObterTodosAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _pessoaService.ObterPorIdAsync(id));
            }
            catch (PessoaNaoExisteException pessoaNaoExiste)
            {
                return NotFound(pessoaNaoExiste.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdicionarPessoaInputModel inputModel)
        {
            if (inputModel is null)
                return BadRequest();

            var id = await _pessoaService.AdicionarAsync(inputModel);

            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, AtualizarPessoaInputModel inputModel)
        {
            try
            {
                if (inputModel is null)
                    return BadRequest();

                await _pessoaService.AtualizarAsync(id, inputModel);

                return NoContent();
            }
            catch (PessoaNaoExisteException pessoaNaoExiste)
            {
                return NotFound(pessoaNaoExiste.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _pessoaService.ExcluirAsync(id);

                return NoContent();
            }
            catch (PessoaNaoExisteException pessoaNaoExiste)
            {
                return NotFound(pessoaNaoExiste.Message);
            }
        }

        [HttpPost("{id}/contatos")]
        public async Task<IActionResult> PostContato([FromBody] AdicionarContatoInputModel inputModel)
        {
            try
            {
                if (inputModel is null)
                    return BadRequest();

                if (!Enum.IsDefined(typeof(Core.Enums.ContatoTipo), inputModel.Tipo))
                    return BadRequest($"Tipos válidos: {string.Join(", ", (int[])Enum.GetValues(typeof(Core.Enums.ContatoTipo)))}");

                var id = await _pessoaService.AdicionarContatoAsync(inputModel);

                return Created(string.Empty, new { id });
            }
            catch (PessoaNaoExisteException pessoaNaoExiste)
            {
                return BadRequest(pessoaNaoExiste.Message);
            }
        }

        [HttpPut("{id}/contatos/{contatoId}")]
        public async Task<IActionResult> PutContato(Guid contatoId, [FromBody] AtualizarContatoInputModel inputModel)
        {
            try
            {
                if (inputModel is null)
                    return BadRequest();

                if (!Enum.IsDefined(typeof(Core.Enums.ContatoTipo), inputModel.Tipo))
                    return BadRequest($"Tipos válidos: {string.Join(", ", (int[])Enum.GetValues(typeof(Core.Enums.ContatoTipo)))}");

                await _pessoaService.AtualizarContatoAsync(contatoId, inputModel);

                return NoContent();
            }
            catch (ContatoNaoExisteException contatoNaoExiste)
            {
                return NotFound(contatoNaoExiste.Message);
            }
        }

        [HttpDelete("{id}/contatos/{contatoId}")]
        public async Task<IActionResult> DeleteContato(Guid contatoId)
        {
            try
            {
                await _pessoaService.ExcluirContatoAsync(contatoId);

                return NoContent();
            }
            catch (ContatoNaoExisteException contatoNaoExiste)
            {
                return NotFound(contatoNaoExiste.Message);
            }
        }
    }
}
