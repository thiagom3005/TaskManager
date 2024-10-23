namespace TaskManager.Domain.Interfaces
{
    /// <summary>
    /// Interface genérica para operações CRUD.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Obtém todas as entidades.
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Obtém uma entidade pelo ID.
        /// </summary>
        /// <param name="id">ID da entidade</param>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Adiciona uma nova entidade.
        /// </summary>
        /// <param name="entity">Entidade a ser adicionada</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Atualiza uma entidade existente.
        /// </summary>
        /// <param name="entity">Entidade a ser atualizada</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Exclui uma entidade.
        /// </summary>
        /// <param name="entity">Entidade a ser excluída</param>
        Task DeleteAsync(T entity);
    }
}
