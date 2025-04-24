using eTickets.Data.Enum;
using eTickets.Models;

namespace eTickets.Data
{
    public class AppDbInitializer
    {

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                //Cinemas
                if(!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema()
                        {
                            CinemaLogo = "https://example.com/logo1.jpg",
                            Name = "Cinema 1",
                            Description = "Description 1"
                        },
                        new Cinema()
                        {
                            CinemaLogo = "https://example.com/logo2.jpg",
                            Name = "Cinema 2",
                            Description = "Description 2"
                        }
                    });
                    context.SaveChanges();
                }


                //Actors
                if(!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            ProfilePictureURL = "https://example.com/actor1.jpg",
                            FullName = "Actor 1",
                            Bio = "Bio 1"
                        },
                        new Actor()
                        {
                            ProfilePictureURL = "https://example.com/actor2.jpg",
                            FullName = "Actor 2",
                            Bio = "Bio 2"
                        }
                    });
                    context.SaveChanges();
                }



                //Producers
                if(!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer()
                        {
                            ProfilePictureURL = "https://example.com/producer1.jpg",
                            FullName = "Producer 1",
                            Bio = "Bio 1"
                        },
                        new Producer()
                        {
                            ProfilePictureURL = "https://example.com/producer2.jpg",
                            FullName = "Producer 2",
                            Bio = "Bio 2"
                        }
                    });
                    context.SaveChanges();
                }


                //Movies
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            Name = "Movie 1",
                            Description = "Description 1",
                            Price = 10.99,
                            ImageURL = "https://example.com/movie1.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(10),
                            MovieCategory = MovieCategory.Action,
                            ProducerId = 1,
                            CinemaId = 1
                        },
                        new Movie()
                        {
                            Name = "Movie 2",
                            Description = "Description 2",
                            Price = 12.99,
                            ImageURL = "https://example.com/movie2.jpg",
                            StartDate = DateTime.Now.AddDays(-5),
                            EndDate = DateTime.Now.AddDays(15),
                            MovieCategory = MovieCategory.Comedy,
                            ProducerId = 2,
                            CinemaId = 2
                        },
                        new Movie()
                        {
                            Name = "Movie 3",
                            Description = "Description 3",
                            Price = 8.99,
                            ImageURL = "https://example.com/movie3.jpg",
                            StartDate = DateTime.Now.AddDays(-20),
                            EndDate = DateTime.Now.AddDays(5),
                            MovieCategory = MovieCategory.Drama,
                            ProducerId = 1,
                            CinemaId = 2
                        },
                        new Movie()
                        {
                            Name = "Movie 4",
                            Description = "Description 4",
                            Price = 15.99,
                            ImageURL = "https://example.com/movie4.jpg",
                            StartDate = DateTime.Now.AddDays(-30),
                            EndDate = DateTime.Now.AddDays(20),
                            MovieCategory = MovieCategory.Horror,
                            ProducerId = 2,
                            CinemaId = 1
                        }
                    });
                    context.SaveChanges();
                }

                //Actor & Movie
                if(!context.Actors_Movies.Any())
                {
                    context.Actors_Movies.AddRange(new List<Actor_Movie>()
                    {
                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 1
                        },
                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 2
                        },
                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 3
                        },
                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 4
                        }
                    });
                    context.SaveChanges();
                }


            }
        }

    }
}
