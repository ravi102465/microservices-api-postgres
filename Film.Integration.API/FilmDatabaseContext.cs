using Film.Integration.API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Film.Integration.API
{
    public class FilmDatabaseContext : DbContext
    {
        public FilmDatabaseContext(DbContextOptions<FilmDatabaseContext> options)
            : base(options)
        {

        }
        public DbSet<Movie> movies { get; set; }
    }
}
