﻿using Microsoft.EntityFrameworkCore;

namespace PlayasLimpiasWebApp.Models
{
    //Class representing model in Database
    public class EventContext : DbContext
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


        }
    }
}
