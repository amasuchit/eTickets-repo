using eTickets.Data.Base;
using eTickets.Data.Enum;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Movie : IEntityBase
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        [Display(Name = "Movie Image")]
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }

        //Relationships

        public List<Actor_Movie> Actors_Movies { get; set; }

        
        public int ProducerId{ get; set; }
        [ForeignKey("ProducerId")]
        [ValidateNever]
        public Producer Producer { get; set; }

         
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]
        [ValidateNever]
        public Cinema Cinema { get; set; }


        

    }
}
