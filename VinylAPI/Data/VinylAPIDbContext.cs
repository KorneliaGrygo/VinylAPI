using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylAPI.Entities;

namespace VinylAPI.Data
{
    public class VinylAPIDbContext : DbContext
    {
        public VinylAPIDbContext(DbContextOptions opt):base(opt)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<MusicAlbum> MusicAlbums { get; set; }
        public DbSet<Song> Songs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.Name)
                .IsRequired();
        }
    }
    
}
