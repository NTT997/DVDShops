using DVDShops.Models;

namespace DVDShops.Services.Artists
{
    public class ArtistService : IArtistService
    {

        private DvdshopContext dbContext;
        public ArtistService(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool Create(Artist artist)
        {
            try
            {
                dbContext.Artists.Add(artist);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Artist GetById(int artistId)
        {
            return dbContext.Artists.Find(artistId);
        }

        public bool Delete(int artistId)
        {
            try
            {
                dbContext.Artists.Add(GetById(artistId));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Artist> GetAll()
        {
            return dbContext.Artists.ToList();
        }

        public List<Artist> GetArtistsByName(string artistName)
        {
            return dbContext.Artists.Where(a => a.ArtistName.ToLower().Contains(artistName.ToLower())).ToList();
        }

        public List<Artist> GetByGenres(int genreId)
        {
            return dbContext.Artists.Where(a => a.GenreId == genreId).ToList();
        }

        public bool Update(Artist artist)
        {
            try
            {
                dbContext.Entry(artist).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Artist GetByName(string artistName)
        {
            return dbContext.Artists.SingleOrDefault(artist => artist.ArtistName.ToLower() == artistName.ToLower());
        }
    }
}
