using eTickets.Data.Base;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public class CinemaService : EntityBaseRepository<Cinema>, ICinemaService
    {
        private readonly AppDbContext context;
        public CinemaService(AppDbContext context) : base(context)
        {
            this.context = context;
        }
    }
    {
    }
}
