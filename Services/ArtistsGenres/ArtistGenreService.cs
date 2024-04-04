using DVDShops.Models;

namespace DVDShops.Services.ArtistsGenres
{
    public class ArtistGenreService : IArtistGenreService
    {
        private DvdshopContext dbContext;
        public ArtistGenreService(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool Create(ArtistsGenre artistGenre)
        {
            try
            {
                dbContext.ArtistsGenres.Add(artistGenre);
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
                dbContext.ArtistsGenres.Remove(GetById(id));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ArtistsGenre> GetAll()
        {
            return dbContext.ArtistsGenres.ToList();
        }

        public List<ArtistsGenre> GetByArtistId(int artistId)
        {
            return dbContext.ArtistsGenres.Where(ag => ag.ArtistId == artistId).ToList();
        }

        public List<ArtistsGenre> GetByGenreId(int genreId)
        {
            return dbContext.ArtistsGenres.Where(ag => ag.GenreId == genreId).ToList();
        }

        public ArtistsGenre GetById(int id)
        {
            return dbContext.ArtistsGenres.Find(id);
        }

        public bool Update(ArtistsGenre artistGenre)
        {
            try
            {
                dbContext.Entry(artistGenre).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
