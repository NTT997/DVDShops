using DVDShops.Models;

namespace DVDShops.Services.Games
{
    public interface IGameService
    {
        List<Game> GetAll();
        List<Game> SearchByTitle(string gameTitle);
        Game GetByTitle(string gameTitle);
        Game GetById(int gameId);
        bool Create(Game game);
        bool Update(Game game);
        bool Delete(int gameId);
    }
}
