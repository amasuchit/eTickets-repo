using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IActorService
    {

        Task<IEnumerable<Actor>> GetAll();
        Task<Actor> GetbyIdAsync(int id);
        Task AddAsync(Actor actor);
        Task<Actor> UpdateAsync(int id, Actor newActor);
        Task DeleteAsync(int id);
    }
}
