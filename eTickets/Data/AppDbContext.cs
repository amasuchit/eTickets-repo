using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        // Define DbSets for your entities here
        // public DbSet<YourEntity> YourEntities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Actor_Movie>().HasKey(am => new {
                am.ActorId,
                am.MovieId 
            });

            modelBuilder.Entity<Actor_Movie>()
                .HasOne(m=>m.Movie)
                .WithMany(am => am.Actors_Movies)
                .HasForeignKey(m => m.MovieId);

            modelBuilder.Entity<Actor_Movie>()
                .HasOne(a=>a.Actor)
                .WithMany(am => am.Actors_Movies)
                .HasForeignKey(a => a.ActorId);

            base.OnModelCreating(modelBuilder);
            // Configure your entity mappings here
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }

    }
   
}
