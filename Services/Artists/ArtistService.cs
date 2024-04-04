using DVDShops.Models;
using System.Diagnostics;

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
                dbContext.Artists.Remove(GetById(artistId));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Artist artist)
        {
            try
            {
                dbContext.Entry(artist).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public Artist GetByName(string artistName)
        {
            artistName = artistName.ToLower();
            return dbContext.Artists.FirstOrDefault(a => a.ArtistName.ToLower() == artistName);
        }

        public List<Artist> SearchArtistsByName(string artistName)
        {
            return dbContext.Artists.Where(a => a.ArtistName.ToLower().Contains(artistName.ToLower())).ToList();
        }

        public List<Artist> GetAllArtists()
        {
            return dbContext.Artists.ToList();
        }

    }
}
