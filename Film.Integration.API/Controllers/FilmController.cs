using Film.Integration.API.Repositories;
using Film.Integration.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Film.Integration.API.Providers;
using Newtonsoft.Json;

namespace Film.Integration.API.Controllers
{
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly ILogger<FilmController> _logger;
        private readonly IFilmRepository _filmRepository;
        private readonly IBlobStorageProvider _blobStorageProvider;

        public FilmController(ILogger<FilmController> logger, IFilmRepository filmRepository, IBlobStorageProvider blobStorageProvider)
        {
            _logger = logger;
            _filmRepository = filmRepository;
            _blobStorageProvider = blobStorageProvider;
        }

        [Route("/film")]
        [HttpGet]
        public virtual async Task<IActionResult> GetFilms()
        {
            return new OkObjectResult(await _filmRepository.GetFilms());
        }

        [Route("/film")]
        [HttpPost]
        public virtual async Task<IActionResult> PostFilm([FromBody] Movie movie)
        {
            var insertedMovie = await _filmRepository.AddFilm(movie);
            return Created("", insertedMovie);
        }

        [Route("/filmFromAzureStorage")]
        [HttpGet]
        public virtual async Task<IActionResult> GetFilmsFromAzureStorage()
        {
            using var data = await _blobStorageProvider.GetBlobContent("filmcontainer", "films.json");
            using StreamReader streamReader = new StreamReader(data);
            var movies = JsonConvert.DeserializeObject<List<Movie>>(streamReader.ReadToEnd());
            return new OkObjectResult(movies);
        }
    }
}