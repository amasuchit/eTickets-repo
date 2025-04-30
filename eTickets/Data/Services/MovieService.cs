using eTickets.Data.Base;
using eTickets.Models;
using eTickets.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class MovieService : EntityBaseRepository<Movie>, IMovieService
    {
        private readonly AppDbContext context;
        public MovieService(AppDbContext context) : base(context)
        {
            this.context = context;
        }



        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(mc => mc.MovieCategory)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);
            return movieDetails;
        }



        public async Task AddMoviewithActor(MovieViewModel viewModel)
        {
            var movie = new Movie
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
                ImageURL = viewModel.ImageURL,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate,
                MovieCategory = viewModel.MovieCategory,
                CinemaId = viewModel.CinemaId,
                ProducerId = viewModel.ProducerId
            };

            context.Movies.Add(movie);
            await context.SaveChangesAsync();

            foreach (var actorId in viewModel.ActorId)
            {
                var actorMovie = new Actor_Movie
                {
                    MovieId = movie.Id,
                    ActorId = actorId
                };
                context.Actors_Movies.Add(actorMovie);
            }

            await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            var allMovies = await context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(mc => mc.MovieCategory)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .ToListAsync();
           
            return allMovies;

        }
    }
   
}
