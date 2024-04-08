using DVDShops.Models;

namespace DVDShops.Services.AlbumsGenres
{
    public class AlbumGenreService : IAlbumGenreService
    {
        private DvdshopContext dbContext;
        public AlbumGenreService(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool Create(AlbumsGenre albumGenre)
        {
            try
            {
                dbContext.AlbumsGenres.Add(albumGenre);
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
                dbContext.AlbumsGenres.Remove(GetById(id));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<AlbumsGenre> GetAll()
        {
            return dbContext.AlbumsGenres.ToList();
        }

        public AlbumsGenre GetById(int id)
        {
            return dbContext.AlbumsGenres.Find(id);
        }

        public bool Update(AlbumsGenre albumGenre)
        {
            try
            {
                dbContext.Entry(albumGenre).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
