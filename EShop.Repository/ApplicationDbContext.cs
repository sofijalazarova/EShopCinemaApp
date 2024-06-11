//using EShop.Domain.DomainModels;
//using EShop.Domain.Identity;
//using EShop.Domain.Relations;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

//namespace EShop.Repository
//{
//    public class ApplicationDbContext : IdentityDbContext<EShopApplicationUser>
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//        : base(options)
//        {
//        }

//        public virtual DbSet<Movie> Movies { get; set; }
//        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
//        public virtual DbSet<TicketInShoppingCart> TicketInShoppingCarts { get; set; }

//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            base.OnModelCreating(builder);

//            builder.Entity<Movie>()
//                .Property(z => z.Id)
//                .ValueGeneratedOnAdd();

//            builder.Entity<ShoppingCart>()
//                .Property(z => z.Id)
//                .ValueGeneratedOnAdd();

//            builder.Entity<TicketInShoppingCart>()
//                .Property(z => z.Id)
//                .ValueGeneratedOnAdd();

//            builder.Entity<TicketInShoppingCart>()
//                .HasOne(z => z.Movie)
//                .WithMany(z => z.TicketInShoppingCarts)
//                .HasForeignKey(z => z.MovieId);

//            builder.Entity<TicketInShoppingCart>()
//                .HasOne(z => z.UserCart)
//                .WithMany(z => z.TicketInShoppingCarts)
//                .HasForeignKey(z => z.ShoppingCartId);

//            builder.Entity<ShoppingCart>()
//                .HasOne<EShopApplicationUser>(z => z.Owner)
//                .WithOne(z => z.UserCart)
//                .HasForeignKey<ShoppingCart>(z => z.OwnerId);

//            builder.Entity<TicketInOrder>()
//                .Property(z => z.Id)
//                .ValueGeneratedOnAdd();

//            builder.Entity<TicketInOrder>()
//                .HasOne(z => z.Movie)
//                .WithMany(z => z.TicketInOrders)
//                .HasForeignKey(z => z.MovieId);

//            builder.Entity<TicketInOrder>()
//                .HasOne(z => z.Order)
//                .WithMany(z => z.TicketInOrders)
//                .HasForeignKey(z => z.OrderId);

//        }
//    }
//}

using EShop.Domain.DomainModels;
using EShop.Domain.Identity;
using EShop.Domain.Relations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Mail;

namespace EShop.Repository
{
    public class ApplicationDbContext : IdentityDbContext<EShopApplicationUser>
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<TicketInShoppingCart> TicketInShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Movie>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ShoppingCart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<TicketInShoppingCart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<TicketInShoppingCart>()
                .HasOne(z => z.CurrentMovie)
                .WithMany(z => z.TicketInShoppingCarts)
                .HasForeignKey(z => z.MovieId);

            builder.Entity<TicketInShoppingCart>()
                .HasOne(z => z.UserCart)
                .WithMany(z => z.TicketInShoppingCarts)
                .HasForeignKey(z => z.ShoppingCartId);

            builder.Entity<ShoppingCart>()
                .HasOne<EShopApplicationUser>(z => z.Owner)
                .WithOne(z => z.UserCart)
                .HasForeignKey<ShoppingCart>(z => z.OwnerId);

            builder.Entity<TicketInOrder>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<TicketInOrder>()
                .HasOne(z => z.Movie)
                .WithMany(z => z.TicketInOrders)
                .HasForeignKey(z => z.MovieId);

            builder.Entity<TicketInOrder>()
                .HasOne(z => z.Order)
                .WithMany(z => z.TicketInOrders)
                .HasForeignKey(z => z.OrderId);
        }
    }
}