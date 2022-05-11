using Microsoft.EntityFrameworkCore;
using project1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project1.Data
{
    public class DbContex : DbContext
    {
        public DbContex(DbContextOptions<DbContex> options) : base(options)
        {


        }
        public DbSet<Auther> authers { get; set; }

        public DbSet<Books> books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auther>()
                .HasMany(t => t.books)
                .WithOne(t => t.Auther)
                .HasForeignKey(t => t.Id);
            modelBuilder.Entity<Books>()
                .HasOne(t => t.Auther)
                .WithMany(t => t.books)
                .HasForeignKey(t => t.autherId);
        }


    }
}
