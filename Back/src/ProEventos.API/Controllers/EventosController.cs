using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService eventoService;
        public EventosController(IEventoService eventoService)
        {
            this.eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await eventoService.GetAllEventosAsync(true);
                if (eventos == null) return NotFound("Nenhum evento encontrado.");

                return Ok(eventos);
            }            
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Error ao tentar recupear eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await eventoService.GetEventoByIdAsync(id, true);
                if (evento == null) return NotFound("Evento por Id não encontrado.");

                return Ok(evento);
            }            
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Error ao tentar recupear eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("tema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var evento = await eventoService.GetAllEventosByTemaAsync(tema, true);
                if (evento == null) return NotFound("Eventos por tema não encontrados.");

                return Ok(evento);
            }            
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Error ao tentar recupear eventos. Erro: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var evento = await eventoService.AddEvento(model);
                if (evento == null) return BadRequest("Erro ao tentar adicionar evento.");

                return Ok(evento);
            }            
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Error ao tentar adicionar evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await eventoService.UpdateEvento(id, model);
                if (evento == null) return BadRequest("Erro ao tentar atualizar evento.");

                return Ok(evento);
            }            
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Error ao tentar atualizar evento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await eventoService.DeleteEvento(id) ?
                    Ok("Deletado") : BadRequest("Evento não deletado");                                    
            }            
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Error ao tentar deletar evento. Erro: {ex.Message}");
            }
        }
    }
}
