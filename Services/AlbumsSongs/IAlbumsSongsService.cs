using DVDShops.Models;

namespace DVDShops.Services.AlbumsSongs
{
    public interface IAlbumsSongsService
    {
        List<AlbumsSong> GetAll();
        List<AlbumsSong> GetBySongId(int songId);
        List<AlbumsSong> GetByAlbumId(int albumId);
        AlbumsSong GetById(int id);
        bool Create(AlbumsSong albumGenre);
        bool Update(AlbumsSong albumGenre);
        bool Delete(int id);
    }
}
