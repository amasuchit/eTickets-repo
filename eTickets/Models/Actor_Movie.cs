using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace eTickets.Models
{
    public class Actor_Movie
    {
        public int ActorId { get; set; }
        [ValidateNever]
        public Actor Actor { get; set; }

        public int MovieId { get; set; }
        [ValidateNever]
        public Movie Movie { get; set; }
    }
}
