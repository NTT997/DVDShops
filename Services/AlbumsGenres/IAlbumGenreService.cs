using DVDShops.Models;

namespace DVDShops.Services.AlbumsGenres
{
    public interface IAlbumGenreService
    {
        List<AlbumsGenre> GetAll();
        AlbumsGenre GetById(int id);
        bool Create(AlbumsGenre albumGenre);
        bool Update(AlbumsGenre albumGenre);
        bool Delete(int id);
    }
}
