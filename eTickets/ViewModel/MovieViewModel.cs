using eTickets.Data.Enum;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Components.Forms;

namespace eTickets.ViewModel
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Movie Image")]
        public string ImageURL { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }






        // IDs for selected items (bound from form)
        [Display(Name = "Select Actor(s)")]
        public List<int> ActorId { get; set; } = new();

        [Display(Name = "Select Cinema")]
        public int CinemaId { get; set; }

        [Display(Name = "Select Producer")]
        public int ProducerId { get; set; }

        // These populate dropdowns/multiselect lists in the form
        public IEnumerable<SelectListItem> Cinemas { get; set; }
        public IEnumerable<SelectListItem> Producers { get; set; }
        public IEnumerable<SelectListItem> Actors { get; set; }
    }
}
