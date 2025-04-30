using eTickets.Data.Base;
using eTickets.Models;
using eTickets.ViewModel;

namespace eTickets.Data.Services
{
    public interface IMovieService: IEntityBaseRepository<Movie>
    {

        Task<IEnumerable<Movie>>  GetAllMoviesAsync ();

        Task AddMoviewithActor (MovieViewModel movieViewModel);

        

    }
}
