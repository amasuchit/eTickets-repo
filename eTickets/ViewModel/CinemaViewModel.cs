using System.ComponentModel.DataAnnotations;

namespace eTickets.ViewModel
{
    public class CinemaViewModel
    {
        [Key]
        public int Id { get; set; }


        [Display(Name = "Logo")]
        [Required(ErrorMessage = "Logo is required")]
        public string CinemaLogo { get; set; }


        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
