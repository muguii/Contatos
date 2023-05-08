using Contatos.API.Entidades;
using Contatos.API.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contatos.API.Controllers
{
    [ApiController]
    [Route("api/pessoas")]
    public class PessoaController : ControllerBase
    {
        private readonly ContatoDbContext _dbContext;

        public PessoaController(ContatoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pessoas = await _dbContext.Pessoa.Include(pessoa => pessoa.Contatos)
                                                 .ToListAsync();

            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var pessoa = await _dbContext.Pessoa.Include(pessoa => pessoa.Contatos)
                                                .SingleOrDefaultAsync(pessoa => pessoa.Id == id);
            if (pessoa is null)
                return NotFound();

            return Ok(pessoa);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pessoa input)
        {
            await _dbContext.Pessoa.AddAsync(input);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = input.Id }, input);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Pessoa input)
        {
            var pessoa = await _dbContext.Pessoa.SingleOrDefaultAsync(pessoa => pessoa.Id == id);
            if (pessoa is null)
                return NotFound();

            pessoa.Atualizar(input.Nome);

            _dbContext.Update(pessoa); // Opcional, pois o EF já está rastreando o objeto
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var pessoa = await _dbContext.Pessoa.SingleOrDefaultAsync(pessoa => pessoa.Id == id);
            if (pessoa is null)
                return NotFound();

            _dbContext.Contato.RemoveRange(pessoa.Contatos);
            _dbContext.Pessoa.Remove(pessoa);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/contatos")]
        public async Task<IActionResult> PostContato([FromBody] Contato input)
        {
            var pessoa = await _dbContext.Pessoa.SingleOrDefaultAsync(pessoa => pessoa.Id == input.PessoaId);
            if (pessoa is null)
                return BadRequest();

            await _dbContext.Contato.AddAsync(input);
            await _dbContext.SaveChangesAsync();

            return Created(string.Empty, new { id = input.Id });
        }

        [HttpPut("{id}/contatos/{contatoId}")]
        public async Task<IActionResult> PutContato(Guid contatoId, [FromBody] Contato input)
        {
            var pessoa = await _dbContext.Pessoa.SingleOrDefaultAsync(pessoa => pessoa.Id == input.PessoaId);
            if (pessoa is null)
                return BadRequest();

            var contato = await _dbContext.Contato.SingleOrDefaultAsync(contato => contato.Id == contatoId);
            if (contato is null)
                return NotFound();

            contato.Atualizar(input.Nome, input.Tipo, input.Valor);

            _dbContext.Update(contato); // Opcional, pois o EF já está rastreando o objeto
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}/contatos/{contatoId}")]
        public async Task<IActionResult> DeleteContato(Guid contatoId)
        {
            var contato = await _dbContext.Contato.SingleOrDefaultAsync(contato => contato.Id == contatoId);
            if (contato is null)
                return NotFound();

            _dbContext.Contato.Remove(contato);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
