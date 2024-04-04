using DVDShops.Models;

namespace DVDShops.Services.Artists
{
    public interface IArtistService
    {
        List<Artist> GetAllArtists();
        List<Artist> SearchArtistsByName(string artistName);
        Artist GetById(int id);
        Artist GetByName(string artistName);
        bool Create(Artist artist);
        bool Update(Artist artist);
        bool Delete(int artistId);
    }
}
