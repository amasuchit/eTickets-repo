using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class ActorService : IActorService
    {
        private readonly AppDbContext context;

        public ActorService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Actor actor)
        {
            await context.Actors.AddAsync(actor);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Actor>> GetAll()
        {
           var actors = await context.Actors.ToListAsync();
            return actors;
        }

        public async Task <Actor> GetbyIdAsync(int id)
        {
           var actor= await context.Actors.FirstOrDefaultAsync(a => a.Id == id);
            return actor;
            
        }

        public async Task<Actor> UpdateAsync(int id, Actor newActor)
        {
            throw new NotImplementedException();
        }
    }
}
