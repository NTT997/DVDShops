using DVDShops.Models;

namespace DVDShops.Services.Games
{
    public class GameService : IGameService
    {
        private DvdshopContext dbContext;
        public GameService(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Create(Game game)
        {
            try
            {
                dbContext.Games.Add(game);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int gameId)
        {
            try
            {
                dbContext.Games.Remove(GetById(gameId));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Game> GetAll()
        {
            return dbContext.Games.ToList();
        }

        public Game GetById(int gameId)
        {
            return dbContext.Games.Find(gameId);
        }

        public Game GetByTitle(string gameTitle)
        {
            gameTitle = gameTitle.Trim();
            return dbContext.Games.Where(g => g.GameTitle.ToLower() == gameTitle).OrderByDescending(g => g.GameId).FirstOrDefault();
        }

        public List<Game> SearchByTitle(string gameTitle)
        {
            gameTitle = gameTitle.Trim();
            return dbContext.Games.Where(g => g.GameTitle.ToLower().Contains(gameTitle)).ToList();
        }

        public bool Update(Game game)
        {
            try
            {
                dbContext.Entry(game).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
