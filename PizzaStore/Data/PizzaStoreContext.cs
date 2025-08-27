using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PizzaStore.Data
{
    public class PizzaStoreContext : DbContext
    {
        public PizzaStoreContext(DbContextOptions<PizzaStoreContext> options) : base(options) { }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Topping> Toppings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure many-to-many relationship
            modelBuilder.Entity<Pizza>()
                .HasMany(p => p.Toppings)
                .WithMany(t => t.Pizzas);

            // Configure unique constraints
            modelBuilder.Entity<Pizza>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Topping>()
                .HasIndex(t => t.Name)
                .IsUnique();

            // Seed data
            modelBuilder.Entity<Topping>().HasData(
                new Topping { Id = 1, Name = "Pepperoni", Price = 2.50m },
                new Topping { Id = 2, Name = "Mushrooms", Price = 1.75m },
                new Topping { Id = 3, Name = "Cheese", Price = 2.00m },
                new Topping { Id = 4, Name = "Sausage", Price = 3.00m },
                new Topping { Id = 5, Name = "Bell Peppers", Price = 1.50m }
            );

            modelBuilder.Entity<Pizza>().HasData(
                new Pizza { Id = 1, Name = "Margherita", Description = "Classic pizza with cheese", BasePrice = 12.99m },
                new Pizza { Id = 2, Name = "Pepperoni Classic", Description = "Pizza with pepperoni", BasePrice = 15.99m }
            );
        }
    }
}