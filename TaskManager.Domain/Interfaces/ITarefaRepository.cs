using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

/// <summary>
/// Interface específica para o repositório de Tarefas.
/// </summary>
public interface ITarefaRepository : IRepository<Tarefa>
{
    /// <summary>
    /// Obtém todas as tarefas pendentes.
    /// </summary>
    Task<IEnumerable<Tarefa>> GetTarefasPendentesAsync();
}