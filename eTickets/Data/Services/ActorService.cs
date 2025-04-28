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
        public void Add(Actor actor)
        {
            context.Actors.Add(actor);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Actor>> GetAll()
        {
           var actors = await context.Actors.ToListAsync();
            return actors;
        }

        public Actor GetbyId(int id)
        {
            var actor= context.Actors.FirstOrDefault(a => a.Id == id);
            return actor;
        }

        public Actor Update(int id, Actor newActor)
        {
            throw new NotImplementedException();
        }
    }
}
