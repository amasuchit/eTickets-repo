
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eTickets.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext context;

        public EntityBaseRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            EntityEntry entityEntry = context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()=> await context.Set<T>().ToListAsync();
            
        

        public async Task<T> GetbyIdAsync(int id)=> await context.Set<T>().FirstOrDefaultAsync(a => a.Id == id);
            
        

        public async Task UpdateAsync(T entity)
        {
            EntityEntry entityEntry = context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
