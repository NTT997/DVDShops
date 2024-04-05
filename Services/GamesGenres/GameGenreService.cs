using DVDShops.Models;

namespace DVDShops.Services.GamesGenres
{
    public class GameGenreService : IGameGenreService
    {
        private DvdshopContext dbContext;
        public GameGenreService(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool Create(GamesGenre gameGenre)
        {
            try
            {
                dbContext.GamesGenres.Add(gameGenre);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                dbContext.GamesGenres.Remove(GetById(id));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<GamesGenre> GetAll()
        {
            return dbContext.GamesGenres.ToList();
        }

        public List<GamesGenre> GetByGameId(int gameId)
        {
            return dbContext.GamesGenres.Where(gg => gg.GameId == gameId).ToList();
        }

        public List<GamesGenre> GetByGenreID(int genreId)
        {
            return dbContext.GamesGenres.Where(gg => gg.GenreId == genreId).ToList();
        }

        public GamesGenre GetById(int id)
        {
            return dbContext.GamesGenres.Find(id);
        }

        public bool Update(GamesGenre gameGenre)
        {
            try
            {
                dbContext.Entry(gameGenre).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
