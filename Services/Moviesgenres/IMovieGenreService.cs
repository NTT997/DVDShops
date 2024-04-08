using DVDShops.Models;

namespace DVDShops.Services.Moviesgenres
{
    public interface IMovieGenreService
    {
        List<MoviesGenre> GetAll();
        List<MoviesGenre> GetByMovieId(int gameId);
        List<MoviesGenre> GetByGenreId(int genreId);
        MoviesGenre GetById(int id);
        bool Create(MoviesGenre movieGenre);
        bool Update(MoviesGenre movieGenre);
        bool Delete(int id);
    }
}
