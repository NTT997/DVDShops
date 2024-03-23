using DVDShops.Models;

namespace DVDShops.Services.Artists
{
    public interface IArtistService
    { 
        List<Artist> GetAll();
        List<Artist> GetArtistsByName(string artistName);
        List<Artist> GetByGenres(int genreId);
        Artist GetById(int id);
        bool Create(Artist artist);
        bool Update(Artist artist);
        bool Delete(int artistId);
    }
}
