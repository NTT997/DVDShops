using DVDShops.Models;

namespace DVDShops.Services.AlbumsSongs
{
    public class AlbumsSongsService : IAlbumsSongsService
    {
        private DvdshopContext dbContext;
        public AlbumsSongsService(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool Create(AlbumsSong albumSong)
        {
            try
            {
                dbContext.AlbumsSongs.Add(albumSong);
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
                dbContext.AlbumsSongs.Remove(GetById(id));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<AlbumsSong> GetAll()
        {
            return dbContext.AlbumsSongs.ToList();
        }

        public List<AlbumsSong> GetByAlbumId(int albumId)
        {
            return dbContext.AlbumsSongs.Where(als => als.AlbumId == albumId).ToList(); 
        }

        public AlbumsSong GetById(int id)
        {
            return dbContext.AlbumsSongs.Find(id);
        }

        public List<AlbumsSong> GetBySongId(int songId)
        {
            return dbContext.AlbumsSongs.Where(als => als.SongId == songId).ToList();
        }

        public bool Update(AlbumsSong albumSong)
        {
            try
            {
                dbContext.Entry(albumSong).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
