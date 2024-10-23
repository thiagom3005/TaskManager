using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Interfaces
{
    /// <summary>
    /// Interface para o serviço de Tarefas.
    /// </summary>
    public interface ITarefaService
    {
        /// <summary>
        /// Obtém todas as tarefas.
        /// </summary>
        Task<IEnumerable<Tarefa>> GetAllTarefasAsync();

        /// <summary>
        /// Obtém uma tarefa pelo ID.
        /// </summary>
        /// <param name="id">ID da tarefa</param>
        Task<Tarefa> GetTarefaByIdAsync(int id);

        /// <summary>
        /// Obtém todas as tarefas pendentes.
        /// </summary>
        Task<IEnumerable<Tarefa>> GetTarefasPendentesAsync();

        /// <summary>
        /// Adiciona uma nova tarefa.
        /// </summary>
        /// <param name="tarefa">Tarefa a ser adicionada</param>
        Task AddTarefaAsync(Tarefa tarefa);

        /// <summary>
        /// Atualiza uma tarefa existente.
        /// </summary>
        /// <param name="tarefa">Tarefa a ser atualizada</param>
        Task UpdateTarefaAsync(Tarefa tarefa);

        /// <summary>
        /// Exclui uma tarefa pelo ID.
        /// </summary>
        /// <param name="id">ID da tarefa</param>
        Task DeleteTarefaAsync(int id);
    }
}
