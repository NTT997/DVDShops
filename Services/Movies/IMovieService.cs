using DVDShops.Models;

namespace DVDShops.Services.Movies
{
    public interface IMovieService
    {
        Movie GetById(int movieId);
        List<Movie> GetAll();
        List<Movie> GetByTitle(string movieTitle);
        List<Movie> GetByGenre(int genreId);
        List<Movie> GetByProducer(int producerId);
        bool Create(Movie movie);
        bool Update(Movie movie);
        bool Delete(int movieId);
    }
}
