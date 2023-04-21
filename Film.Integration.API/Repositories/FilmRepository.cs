using Film.Integration.API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Film.Integration.API.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly FilmDatabaseContext _filmDBContext;
        public FilmRepository(FilmDatabaseContext filmDBContext)
        {
            _filmDBContext = filmDBContext;
        }
        public async Task<Movie> AddFilm(Movie movie)
        {
            _filmDBContext.Add(movie);
            await _filmDBContext.SaveChangesAsync();
            return movie;
        }

        public Task DeleteFilm(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFilm(Movie movie)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetFilms()
        {
            return await _filmDBContext.movies.ToListAsync();
        }
    }
}
