using eTickets.Data.Enum;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.ViewModel
{
    public class MovieViewModel
    {
        internal List<Actor_Movie> Actors_Movies;

        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public double? Price { get; set; }

        [Display(Name = "Movie Image")]
        [Required(ErrorMessage = "Image URL is required")]
        public string? ImageURL { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }= DateTime.Now;

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Movie category is required")]
        public MovieCategory MovieCategory { get; set; }
       





        // IDs for selected items (bound from form)
        [Display(Name = "Select Actor(s)")]
        public List<int> ActorId { get; set; } = new();

        [Display(Name = "Select Cinema")]
        public int CinemaId { get; set; }

        [Display(Name = "Select Producer")]
        public int ProducerId { get; set; }

        // These populate dropdowns/multiselect lists in the form
        [ValidateNever]
        public IEnumerable<SelectListItem> Cinemas { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Producers { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Actors { get; set; }
    }
}
