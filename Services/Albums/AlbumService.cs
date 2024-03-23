using DVDShops.Models;

namespace DVDShops.Services.Albums
{
    public class AlbumService : IAlbumService
    {
        private DvdshopContext dbContext;
        public AlbumService(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Create(Album album)
        {
            try
            {
                dbContext.Albums.Add(album);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Album GetById(int albumId)
        {
            return dbContext.Albums.Find(albumId);
        }

        public bool Delete(int albumId)
        {
            try
            {
                dbContext.Albums.Remove(GetById(albumId));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Album> GetAll()
        {
            return dbContext.Albums.ToList();
        }

        public bool Update(Album album)
        {
            try
            {
                dbContext.Entry(album).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
