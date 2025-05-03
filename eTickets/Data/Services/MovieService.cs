using eTickets.Data.Base;
using eTickets.Models;
using eTickets.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);
            return movieDetails;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            var allMovies = await context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .ToListAsync();

            return allMovies;

        }

        public async Task AddMoviewithActor(MovieViewModel viewModel)
        {
            var movie = new Movie
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = (double)viewModel.Price,
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

        public async Task UpdateMovieAsync(MovieViewModel movieViewModel)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieViewModel.Id);

            if (movie != null)
            {
                movie.Id = movieViewModel.Id;
                movie.Name = movieViewModel.Name;
                movie.Description = movieViewModel.Description;
                movie.Price = movieViewModel.Price;
                movie.ImageURL = movieViewModel.ImageURL;
                movie.StartDate = movieViewModel.StartDate;
                movie.EndDate = movieViewModel.EndDate;
                movie.MovieCategory = movieViewModel.MovieCategory;
                movie.CinemaId = movieViewModel.CinemaId;
                movie.ProducerId = movieViewModel.ProducerId;
                await context.SaveChangesAsync();
            };
            var existingactors = await context.Actors_Movies.Where(am => am.MovieId == movie.Id).ToListAsync();
            context.Actors_Movies.RemoveRange(existingactors);
           await context.SaveChangesAsync();
          
            foreach (var actorId in movieViewModel.ActorId)
            {
                var newActorMovie = new Actor_Movie
                {
                    MovieId= movieViewModel.Id,
                    ActorId = actorId
                };
               await context.Actors_Movies.AddAsync(newActorMovie);
            }
            await context.SaveChangesAsync();

        }




        public async Task<MovieViewModel> DropDownForMovies()
        {
            var modelViewModel = new MovieViewModel
            {
                Cinemas = context.Cinemas.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Producers = context.Producers.Select(p => new SelectListItem
                {
                    Text = p.FullName,
                    Value = p.Id.ToString()
                }),
                Actors = context.Actors.Select(a => new SelectListItem
                {
                    Text = a.FullName,
                    Value = a.Id.ToString()
                })
            };

            return modelViewModel;
        }

       
    }
   
}
