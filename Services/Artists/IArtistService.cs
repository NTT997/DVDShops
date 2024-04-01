using DVDShops.Models;

namespace DVDShops.Services.Artists
{
    public interface IArtistService
    { 
        dynamic GetAllDynamic();
        List<Artist> GetAllArtists();
        List<Artist> SearchArtistsByName(string artistName);
        List<Artist> GetArtistsByName(string artistName);
        List<Artist> GetByGenres(int genreId);
        Artist GetById(int id);
        Artist GetByName(string artistName);
        bool Create(Artist artist);
        bool Update(Artist artist);
        bool Delete(Artist artist);
    }
}
