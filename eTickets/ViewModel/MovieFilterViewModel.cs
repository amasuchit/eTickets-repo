using eTickets.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eTickets.ViewModel
{
    public class MovieFilterViewModel
    {
        public int? SelectedCinemaId { get; set; }
        public int? SelectedMovieId { get; set; }

        public IEnumerable<SelectListItem> Cinemas { get; set; }
        public IEnumerable<SelectListItem> Movies { get; set; }
        public List<Movie> FilteredMovies { get; set; }
    }
}
