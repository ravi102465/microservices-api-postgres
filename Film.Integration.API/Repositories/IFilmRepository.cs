using Film.Integration.API.Models.DTO;

namespace Film.Integration.API.Repositories
{
    public interface IFilmRepository
    {
        Task<IEnumerable<Movie>> GetFilms();
        Task<Movie> AddFilm(Movie movie);
        Task UpdateFilm(Movie movie);
        Task DeleteFilm(Movie movie);

    }
}
