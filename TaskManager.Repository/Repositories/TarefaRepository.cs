using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Repository.Context;

namespace TaskManager.Repository.Repositories
{
    public class TarefaRepository : Repository<Tarefa>, ITarefaRepository
    {
        public TarefaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Tarefa>> GetTarefasPendentesAsync()
        {
            return await _dbSet.Where(t => !t.IsCompleted).ToListAsync();
        }
    }
}