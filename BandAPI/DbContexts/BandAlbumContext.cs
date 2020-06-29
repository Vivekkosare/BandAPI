using BandAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.DbContexts
{
    public class BandAlbumContext : DbContext
    {
        public BandAlbumContext(DbContextOptions<BandAlbumContext> options) : base(options)
        {

        }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Band> Bands { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Band>().HasData(new Band { Id = Guid.Parse("88b3c63a-5b77-43cc-b0c0-d1a92fb701a9"), Name = "Metallica", Founded = new DateTime(1980, 01, 11), MainGenre = "Heavy Metal" },
                new Band { Id = Guid.Parse("68340f77-6e52-4df6-97b8-da14d02e7b5a"), Name = "Metallica", Founded = new DateTime(1980, 01, 11), MainGenre = "Heavy Metal" },
                new Band { Id = Guid.Parse("02798642-87b3-481b-a63f-183221f4223b"), Name = "Abba", Founded = new DateTime(1980, 01, 11), MainGenre = "Rock" },
                new Band { Id = Guid.Parse("ca4a6a3b-13e0-411c-9f84-28e56b80b99e"), Name = "Hellua", Founded = new DateTime(1980, 01, 11), MainGenre = "Jazz" });

            modelBuilder.Entity<Album>().HasData(new Album { Id = Guid.Parse("b4d79528-4615-4214-a4b4-c36a232e115a"), Title = "Master of Puppets", Description = "One of the finest bands it is", BandId = Guid.Parse("88b3c63a-5b77-43cc-b0c0-d1a92fb701a9") },
                new Album { Id = Guid.Parse("ebeaed26-91b6-45e7-80bc-ad720d865f40"), Title = "Hero of the destiny", Description = "Destiny plays a role", BandId = Guid.Parse("68340f77-6e52-4df6-97b8-da14d02e7b5a") },
                new Album { Id = Guid.Parse("f9e894ec-ff7a-427e-a75e-a7c19e39027a"), Title = "King of Romance", Description = "Romantic songs this album has", BandId = Guid.Parse("02798642-87b3-481b-a63f-183221f4223b") },
                new Album { Id = Guid.Parse("d5669b26-3415-4682-9c3a-7796b6ce5ca6"), Title = "Cites of the harmony", Description = "Harmonic destruction never returns back", BandId = Guid.Parse("ca4a6a3b-13e0-411c-9f84-28e56b80b99e") });

            base.OnModelCreating(modelBuilder);
        }
    }
}
