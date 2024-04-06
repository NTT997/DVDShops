using DVDShops.Models;

namespace DVDShops.Services.Movies
{
    public interface IMovieService
    {
        List<Movie> GetAll();
        List<Movie> SearchByTitle(string movieTitle);
        Movie GetByTitle(string movieTitle);
        Movie GetById(int movieId);
        bool Create(Movie movie);
        bool Update(Movie movie);
        bool Delete(int movieId);
    }
}
