using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Service.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _repository;

        public TarefaService(ITarefaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Tarefa>> GetAllTarefasAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<Tarefa>> GetTarefasPendentesAsync()
        {
            return await _repository.GetTarefasPendentesAsync();
        }

        public async Task<Tarefa> GetTarefaByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddTarefaAsync(Tarefa tarefa)
        {
            await _repository.AddAsync(tarefa);
        }

        public async Task UpdateTarefaAsync(Tarefa tarefa)
        {
            await _repository.UpdateAsync(tarefa);
        }

        public async Task DeleteTarefaAsync(int id)
        {
            var tarefa = await _repository.GetByIdAsync(id);
            if (tarefa != null)
            {
                await _repository.DeleteAsync(tarefa);
            }
        }
    }
}
