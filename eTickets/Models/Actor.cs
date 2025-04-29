using eTickets.Data.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Actor :IEntityBase
    {
        [Key]
        
        public int Id { get; set; }

        
        public string ProfilePictureURL { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }

        [ValidateNever]
        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
