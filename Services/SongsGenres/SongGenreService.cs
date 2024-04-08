using DVDShops.Models;

namespace DVDShops.Services.SongsGenres
{
    public class SongGenreService : ISongGenreService
    {
        private DvdshopContext dbContext;
        public SongGenreService(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool Create(SongsGenre songGenre)
        {
            try
            {
                dbContext.SongsGenres.Add(songGenre);
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
                dbContext.SongsGenres.Remove(GetById(id));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<SongsGenre> GetAll()
        {
            return dbContext.SongsGenres.ToList();
        }

        public List<SongsGenre> GetByGenreId(int genreId)
        {
            return dbContext.SongsGenres.Where(sg => sg.GenreId == genreId).ToList();
        }

        public SongsGenre GetById(int id)
        {
            return dbContext.SongsGenres.Find(id);
        }

        public List<SongsGenre> GetBySongId(int songId)
        {
            return dbContext.SongsGenres.Where(sg => sg.SongId == songId).ToList();
        }

        public bool Update(SongsGenre songGenre)
        {
            try
            {
                dbContext.Entry(songGenre).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
