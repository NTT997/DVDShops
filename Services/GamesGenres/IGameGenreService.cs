using DVDShops.Models;

namespace DVDShops.Services.GamesGenres
{
    public interface IGameGenreService
    {
        List<GamesGenre> GetAll();
        List<GamesGenre> GetByGameId(int gameId);
        List<GamesGenre> GetByGenreID(int genreId);
        GamesGenre GetById(int id);
        bool Create(GamesGenre gameGenre);
        bool Update(GamesGenre gameGenre);
        bool Delete(int id);
    }
}
