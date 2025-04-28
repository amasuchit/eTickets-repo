using System.ComponentModel.DataAnnotations;

namespace eTickets.ViewModel
{
    public class ActorViewModel
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is required")]
        public string ProfilePictureURL { get; set; }

        [Display(Name = "Name")]

        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Bio is required")]
        public string Bio { get; set; }

    }
}
