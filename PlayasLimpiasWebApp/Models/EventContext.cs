using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PlayasLimpiasWebApp.Models
{
    //Class representing models in Database
    public class EventContext : IdentityDbContext<User>
    {
        public EventContext(DbContextOptions<EventContext> options) : base(options)
        {
        }

        //Create Tables

        //Tables (Users table is automatically generated when using Identity)
        public DbSet<Event> Events { get; set; }
        public DbSet<User_Event> UserEvents { get; set; }
        public DbSet<Activity> Activity { get; set; }

        //Data seeding
        //Only runs once
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seeding: sample events
            modelBuilder.Entity<Event>().HasData(

                new Event
                {
                    Id = 1,
                    Name = "After major holiday disaster",
                    Date = System.DateTime.Now,
                    NumVolunteersReq = 60,
                    NumVolunteers = 0,
                    Location = "Playa El Escambrón, San Juan",
                    Image = "b1.jpeg",
                    Description = "Need some assistance cleaning all this trash"
        },

                new Event
                {
                    Id = 2,
                    Name = "Shoreline in Need!",
                    Date = System.DateTime.Now.AddDays(14),
                    NumVolunteersReq = 20,
                    NumVolunteers  = 0,
                    Location = "Playa Crash Boat, Aguadilla",
                    Image = "aboutImg (6).jpg",
                    Description = "Help me rescue this coastline"
                },

                new Event
                {
                    Id = 3,
                    Name = "Playa Santa Event",
                    Date = System.DateTime.Now.AddDays(4),
                    NumVolunteersReq = 40,
                    NumVolunteers = 0,
                    Location = "Playa Santa, Guánica",
                    Image = "aboutImg (4).jpg",
                    Description = "Marine endangered species in the area"
                },

                new Event
                {
                    Id = 4,
                    Name = "Come Join Us!",
                    Date = System.DateTime.Now.AddDays(34),
                    NumVolunteersReq = 30,
                    NumVolunteers = 0,
                    Location = "Playa Isla Verde, Carolina",
                    Image = "aboutImg (5).jpg",
                    Description = "Food and drinks provided for volunteers!"
                },

                new Event
                {
                    Id = 5,
                    Name = "Green Sea Turtle Eggs In danger",
                    Date = System.DateTime.Now.AddDays(1),
                    NumVolunteersReq = 40,
                    NumVolunteers = 0,
                    Location = "Poza de las Mujeres, Manatí",
                    Image = "aboutImg (3).jpg",
                    Description = "Lets clean this up and protect those eggs. Materials for a custome fence welcomed!"
                },

                new Event
                {
                    Id = 6,
                    Name = "Awful view, let's clean this up",
                    Date = System.DateTime.Now.AddDays(1),
                    NumVolunteersReq = 10,
                    NumVolunteers = 0,
                    Location = "Playa Flamenco, Culebra",
                    Image = "aboutImg (7).jpg",
                    Description = "Music, BBQ, Volleyball tournament after: free entry"
                }

                );

            //Seeding: Add posible roles for users (Admin rights or User rights)
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
                    NormalizedName = "USER",
                }

                );

            //Instruction for mapping the many-to-many relationship
            modelBuilder.Entity<User_Event>().HasOne(x => x.Event)
                .WithMany(x => x.UserEvents)
                .HasForeignKey(x => x.EventId);
            modelBuilder.Entity<User_Event>().HasOne(x => x.User)
                .WithMany(x => x.UserEvents)
                .HasForeignKey(x => x.UserId);


        }
    }
}
