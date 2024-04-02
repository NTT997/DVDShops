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

        public bool Delete(Artist artist)
        {
            try
            {
                dbContext.Artists.Remove(artist);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Artist> GetArtistsByName(string artistName)
        {
            artistName = artistName.ToLower();
            return dbContext.Artists.Where(a => a.ArtistName.ToLower() == artistName).ToList();
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

        public dynamic GetAllDynamic()
        {
            var result = dbContext.Artists.Join(dbContext.Genres,
                                                a => a.GenreId,
                                                g => g.GenreId,
                                                (a, g) => new
                                                {
                                                    ArtistId = a.ArtistId,
                                                    ArtistName = a.ArtistName,
                                                    Bio = a.Bio,
                                                    Genres = g.GenreName
                                                })
                                                .GroupBy(ag => ag.ArtistName)
                                                .Select(group => new
                                                {
                                                    ArtistName = group.Key,
                                                    Bio = group.Select(ag => ag.Bio).FirstOrDefault(),
                                                    Genres = group.Select(ag => ag.Genres).ToList(),
                                                })
                                                .ToList();
            return result;
        }

        public List<Artist> GetAllArtists()
        {
            return dbContext.Artists.ToList();
        }
    }
}
