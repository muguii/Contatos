using Contatos.API.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Contatos.API.Controllers
{
    [ApiController]
    [Route("api/pessoas")]
    public class PessoaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            // return NotFound();

            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Pessoa pessoa)
        {
            return CreatedAtAction(nameof(GetById), new { id = pessoa.Id }, pessoa);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Pessoa pessoa)
        {
            // return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            // return NotFound();

            return NoContent();
        }

        [HttpPost("{id}/contatos")]
        public IActionResult PostContato([FromBody] Contato contato)
        {
            return Created(string.Empty, new { id = contato.Id });
        }

        [HttpPut("{id}/contatos/{contatoId}")]
        public IActionResult PutContato(Guid contatoId, [FromBody] Contato contato)
        {
            // return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}/contatos/{contatoId}")]
        public IActionResult DeleteContato(Guid contatoId)
        {
            // return NotFound();

            return NoContent();
        }
    }
}
