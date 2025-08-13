using Microsoft.EntityFrameworkCore;
using NationalParkApi.Models;

namespace NationalParkApi.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
        }

        public DbSet<NationalPark>  NationalParks { get; set; }
        public DbSet<Trail> Trailers { get; set; }
    }
}
