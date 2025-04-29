using eTickets.Data.Base;
using eTickets.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class ActorService : EntityBaseRepository<Actor>,  IActorService
    {
        private readonly AppDbContext context;

        public ActorService(AppDbContext context): base(context)
        {
           
        }
       
    }
}
