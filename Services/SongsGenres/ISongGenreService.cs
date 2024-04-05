using DVDShops.Models;

namespace DVDShops.Services.SongsGenres
{
    public interface ISongGenreService
    {
        List<SongsGenre> GetAll();
        List<SongsGenre> GetBySongId(int songId);
        List<SongsGenre> GetByGenreId(int genreId);
        SongsGenre GetById(int id);
        bool Create(SongsGenre albumGenre);
        bool Update(SongsGenre albumGenre);
        bool Delete(int id);
    }
}
