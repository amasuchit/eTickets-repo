using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Logo")]
        public string CinemaLogo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Relationship
        public List<Movie> Movies { get; set; }

    }
}
