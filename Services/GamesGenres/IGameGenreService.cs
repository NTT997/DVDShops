using DVDShops.Models;

namespace DVDShops.Services.GamesGenres
{
    public interface IGameGenreService
    {
        List<GamesGenre> GetAll();
        GamesGenre GetById(int id);
        bool Create(GamesGenre gameGenre);
        bool Update(GamesGenre gameGenre);
        bool Delete(int id);
    }
}
