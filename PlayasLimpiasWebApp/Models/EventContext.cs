using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PlayasLimpiasWebApp.Models
{
    //Class representing model in Database
    public class EventContext : IdentityDbContext<User>
    {
        public EventContext(DbContextOptions<EventContext> options) : base(options)
        {
        }

        //Create Tables

        //Table Events
        public DbSet<Event> Events { get; set; }

        //Data seeding
        //Only runs once
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed sample events
            modelBuilder.Entity<Event>().HasData(

                new Event
                {
                    Id = 1,
                    Name = "After major holiday disaster",
                    Date = System.DateTime.Now,
                    NumVolunteersReq = 60,
                    Location = "Playa Escambrón, San Juan",
                    Description = "Ayudame a salvar esta playa"
                },

                new Event
                {
                    Id = 2,
                    Name = "Shoreline in Need!",
                    Date = System.DateTime.Now.AddDays(14),
                    NumVolunteersReq = 20,
                    Location = "Playa Escondida, Luquillo",
                    Description = "Ayudame a salvar esta playa"
                }

                );

            //Add posible roles for users (Admin rights or User rights)
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() 
                { 
                    Id = "fab4fac1-c546-41de-aebc-a14da6895711", 
                    Name = "Admin", 
                    ConcurrencyStamp = "1", 
                    NormalizedName = "ADMIN",
                    
                },

                new IdentityRole()
                {
                    Id = "c7b013f0-5201-4317-abd8-c211f91b7330",
                    Name = "User",
                    ConcurrencyStamp = "2",
                    NormalizedName = "USER"
                }

                );


        }
    }
}
