using eTickets.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task DeleteAsync(int id)
        {
            var data =  await GetbyIdAsync(id);
            context.Actors.Remove(data);
            await context.SaveChangesAsync();
        }

        public async Task<Actor> UpdateAsync(Actor newActor)
        {
           var actorfromdb= await GetbyIdAsync(newActor.Id);
            if (actorfromdb != null)
            {
                actorfromdb.FullName = newActor.FullName;
                actorfromdb.ProfilePictureURL = newActor.ProfilePictureURL;
                actorfromdb.Bio = newActor.Bio;
                await context.SaveChangesAsync();
            }
            return actorfromdb;
        }
    }
}
