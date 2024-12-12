using GameStore.Domain.Entities;
using GameStore.Domain.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure
{
    public class GameStoreDbContext : DbContext
    {
        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasKey(x => x.Id);
            modelBuilder.Entity<Role>().HasIndex(x => x.RoleName).IsUnique();

            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasIndex(x => new { x.UserName, x.Email }).IsUnique();
            modelBuilder.Entity<User>().Property(x => x.UserName).HasMaxLength(20);
            modelBuilder.Entity<User>().Property(x => x.Email).HasMaxLength(254);
            modelBuilder.Entity<User>().Property(u => u.Balance).HasColumnType("decimal(18, 2)").HasDefaultValue(0);
            modelBuilder.Entity<User>().HasOne(x => x.Role).WithMany(x => x.Users).HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<Cart>().HasKey(x => x.Id);
            modelBuilder.Entity<Cart>().HasOne(x => x.Game).WithMany(x => x.Carts).HasForeignKey(x => x.GameId);
            modelBuilder.Entity<Cart>().HasOne(x => x.User).WithMany(x => x.Carts).HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Category>().HasKey(x => x.Id);
            modelBuilder.Entity<Category>().HasIndex(x => x.CategoryName).IsUnique();

            modelBuilder.Entity<Game>().HasKey(x => x.Id);
            modelBuilder.Entity<Game>().Property(x => x.Name).HasMaxLength(100);
            modelBuilder.Entity<Game>().Property(x => x.Description).HasMaxLength(500);
            modelBuilder.Entity<Game>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Game>().Property(g => g.Price).HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<Game>().HasOne(x => x.Publisher).WithMany(x => x.Publishers).HasForeignKey(x => x.PublisherId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Game>().HasOne(x => x.Developer).WithMany(x => x.Developers).HasForeignKey(x => x.DeveloperId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GameCategory>().HasKey(x => x.Id);
            modelBuilder.Entity<GameCategory>().HasOne(x => x.Game).WithMany(x => x.Categories).HasForeignKey(x => x.GameId);
            modelBuilder.Entity<GameCategory>().HasOne(x => x.Category).WithMany(x => x.Games).HasForeignKey(x => x.CategoryId);

            modelBuilder.Entity<Library>().HasKey(x => x.Id);
            modelBuilder.Entity<Library>().HasOne(x => x.User).WithMany(x => x.Libraries).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<Library>().HasOne(x => x.Game).WithMany(x => x.Libraries).HasForeignKey(x => x.GameId);

            modelBuilder.Entity<SystemRequirement>().HasKey(x => x.Id);
            modelBuilder.Entity<SystemRequirement>().HasOne(x => x.Game).WithMany(x => x.SystemRequirements).HasForeignKey(x => x.GameId);
            modelBuilder.Entity<SystemRequirement>().Property(x => x.RequirementType).HasMaxLength(15);
            modelBuilder.Entity<SystemRequirement>().Property(x => x.Os).HasMaxLength(10);
            modelBuilder.Entity<SystemRequirement>().Property(x => x.MemoryRam).HasMaxLength(10);
            modelBuilder.Entity<SystemRequirement>().Property(x => x.VideoMemoryVram).HasMaxLength(10);
            modelBuilder.Entity<SystemRequirement>().Property(x => x.Storage).HasMaxLength(10);
            modelBuilder.Entity<SystemRequirement>().Property(x => x.Network).HasMaxLength(100);
            modelBuilder.Entity<SystemRequirement>().Property(x => x.AdditionalNotes).HasMaxLength(300);

            modelBuilder.Entity<Transaction>().HasKey(x => x.Id);
            modelBuilder.Entity<Transaction>().HasOne(x => x.User).WithMany(x => x.Transactions).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<Transaction>().Property(x => x.Description).HasMaxLength(100);
            modelBuilder.Entity<Transaction>().Property(t => t.TransactionsMade).HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Wishlist>().HasKey(x => x.Id);
            modelBuilder.Entity<Wishlist>().HasOne(x => x.User).WithMany(x => x.Wishlists).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<Wishlist>().HasOne(x => x.Game).WithMany(x => x.Wishlists).HasForeignKey(x => x.GamedId);



        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameCategory> GameCategories { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<SystemRequirement> SystemRequirements { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }

    }
}
