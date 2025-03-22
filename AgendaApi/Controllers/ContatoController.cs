using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using global::Agenda.Application.DTOS.InputModels;
using global::Agenda.Application.Services;


namespace AgendaApi.Controllers
{

  

        [ApiController]
        [Route("api/[controller]")]
        public class ContatoController : ControllerBase
        {
            private readonly IContatoService _contatoService;

            public ContatoController(IContatoService contatoService)
            {
                _contatoService = contatoService;
            }

           
            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                try
                {
                    var contatos = await _contatoService.GetAllAsync();
                    if (!contatos.Any())
                        return NotFound(new { Message = "Nenhum contato encontrado." });

                    return Ok(contatos);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "Erro ao buscar contatos.", Error = ex.Message });
                }
            }

           
            [HttpGet("{id:int}")]
            public async Task<IActionResult> GetById(int id)
            {
                try
                {
                    var contato = await _contatoService.GetByIdAsync(id);
                    if (contato == null)
                        return NotFound(new { Message = $"Contato com ID {id} não encontrado." });

                    return Ok(contato);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "Erro ao buscar o contato.", Error = ex.Message });
                }
            }

            // POST: api/contato
            [HttpPost]
            public async Task<IActionResult> Create([FromBody] CreateContatoInputModel model)
            {
                try
                {
                    if (model == null)
                        return BadRequest(new { Message = "Dados inválidos." });

                    var novoContato = await _contatoService.AddAsync(model);
                    return CreatedAtAction(nameof(GetById), new { id = novoContato.Id }, novoContato);
                }
                catch (ValidationException ex)
                {
                    return BadRequest(new { Message = "Erro de validação.", Errors = ex.Errors.Select(e => e.ErrorMessage) });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "Erro ao criar o contato.", Error = ex.Message });
                }
            }

            // PUT: api/contato/{id}
            [HttpPut("{id:int}")]
            public async Task<IActionResult> Update(int id, [FromBody] UpdateContatoInput model)
            {
                try
                {
                    if (model == null)
                        return BadRequest(new { Message = "Dados inválidos." });

                    var contatoAtualizado = await _contatoService.UpdateAsync(id, model);
                    if (contatoAtualizado == null)
                        return NotFound(new { Message = $"Contato com ID {id} não encontrado." });

                    return Ok(contatoAtualizado);
                }
                catch (ValidationException ex)
                {
                    return BadRequest(new { Message = "Erro de validação.", Errors = ex.Errors.Select(e => e.ErrorMessage) });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "Erro ao atualizar o contato.", Error = ex.Message });
                }
            }

            // DELETE: api/contato/{id}
            [HttpDelete("{id:int}")]
            public async Task<IActionResult> Delete(int id)
            {
                try
                {
                    var deleted = await _contatoService.DeleteAsync(id);
                    if (!deleted)
                        return NotFound(new { Message = $"Contato com ID {id} não encontrado." });

                    return NoContent();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "Erro ao deletar o contato.", Error = ex.Message });
                }
            }
        }
    }

