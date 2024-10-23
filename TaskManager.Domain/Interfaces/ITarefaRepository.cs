using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

public interface ITarefaRepository : IRepository<Tarefa>
{
    Task<IEnumerable<Tarefa>> GetTarefasPendentesAsync();
}