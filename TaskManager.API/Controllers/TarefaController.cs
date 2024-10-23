using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.DTOs;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService TarefaService)
        {
            _tarefaService = TarefaService;
        }

        /// <summary>
        /// Obtém todas as tarefas.
        /// </summary>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var tarefas = await _tarefaService.GetAllTarefasAsync();
            var tarefaDtos = tarefas.Select(t => new TarefaDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt,
                CompletedAt = t.CompletedAt
            });
            return Ok(tarefaDtos);
        }

        /// <summary>
        /// Obtém uma tarefa pelo ID.
        /// </summary>
        /// <param name="id">ID da tarefa</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tarefa = await _tarefaService.GetTarefaByIdAsync(id);
            if (tarefa == null) return NotFound();

            var tarefaDto = new TarefaDto
            {
                Id = tarefa.Id,
                Title = tarefa.Title,
                Description = tarefa.Description,
                IsCompleted = tarefa.IsCompleted,
                CreatedAt = tarefa.CreatedAt,
                CompletedAt = tarefa.CompletedAt
            };
            return Ok(tarefaDto);
        }

        /// <summary>
        /// Obtém todas as tarefas pendentes.
        /// </summary>
        [HttpGet]
        [Route("GetTarefasPendentes")]
        public async Task<IActionResult> GetTarefasPendentes()
        {
            var tarefas = await _tarefaService.GetTarefasPendentesAsync();
            var tarefaDtos = tarefas.Select(t => new TarefaDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt,
                CompletedAt = t.CompletedAt
            }).ToList();
            return Ok(tarefaDtos);
        }

        /// <summary>
        /// Cria uma nova tarefa.
        /// </summary>
        /// <param name="tarefaDto">Dados da tarefa</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TarefaDto tarefaDto)
        {
            var tarefa = new Tarefa
            {
                Title = tarefaDto.Title,
                Description = tarefaDto.Description,
                IsCompleted = tarefaDto.IsCompleted,
                CreatedAt = tarefaDto.CreatedAt,
                CompletedAt = tarefaDto.CompletedAt
            };

            await _tarefaService.AddTarefaAsync(tarefa);
            tarefaDto.Id = tarefa.Id; // Update the DTO with the generated ID
            return CreatedAtAction(nameof(GetById), new { id = tarefaDto.Id }, tarefaDto);
        }

        /// <summary>
        /// Atualiza uma tarefa existente.
        /// </summary>
        /// <param name="id">ID da tarefa</param>
        /// <param name="tarefaDto">Dados atualizados da tarefa</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TarefaDto tarefaDto)
        {
            var existingTarefa = await _tarefaService.GetTarefaByIdAsync(id);
            if (existingTarefa == null) return BadRequest();

            existingTarefa.Title = tarefaDto.Title;
            existingTarefa.Description = tarefaDto.Description;
            existingTarefa.IsCompleted = tarefaDto.IsCompleted;
            existingTarefa.CreatedAt = tarefaDto.CreatedAt;
            existingTarefa.CompletedAt = tarefaDto.CompletedAt;

            await _tarefaService.UpdateTarefaAsync(existingTarefa);
            return NoContent();
        }

        /// <summary>
        /// Exclui uma tarefa pelo ID.
        /// </summary>
        /// <param name="id">ID da tarefa</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tarefa = await _tarefaService.GetTarefaByIdAsync(id);
            if (tarefa == null) return NotFound();

            await _tarefaService.DeleteTarefaAsync(id);
            return NoContent();
        }
    }
}
