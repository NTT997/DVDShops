using DVDShops.Models;

namespace DVDShops.Services.Albums
{
    public interface IAlbumService
    {
        List<Album> GetAll();
        List<Album> SearchByName(string albumName);
        Album GetByName(string albumName);
        Album GetById(int id);
        bool Create(Album album);
        bool Delete(int id);
        bool Update(Album album);
    }
}
