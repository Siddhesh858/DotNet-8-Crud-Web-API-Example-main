using DotNetCrudWebApi.Movies;
using Microsoft.EntityFrameworkCore;

namespace DotNetCrudWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<MovieModel> Movies { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
