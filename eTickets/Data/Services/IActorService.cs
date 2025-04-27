using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IActorService
    {

        Task<IEnumerable<Actor>> GetAll();
        Actor GetbyId(int id);
        void Add(Actor actor);
        Actor Update(int id, Actor newActor);
        void Delete(int id);

    }
}
