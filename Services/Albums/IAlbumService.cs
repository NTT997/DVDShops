using DVDShops.Models;

namespace DVDShops.Services.Albums
{
    public interface IAlbumService
    {
        List<Album> GetAll();
        Album GetById(int id);
        bool Create(Album album);
        bool Delete(int id);
        bool Update(Album album);
    }
}
