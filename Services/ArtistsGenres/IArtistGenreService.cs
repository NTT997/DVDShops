using DVDShops.Models;

namespace DVDShops.Services.ArtistsGenres
{
    public interface IArtistGenreService
    {
        List<ArtistsGenre> GetAll();
        List<ArtistsGenre> GetByArtistId(int artistId);
        List<ArtistsGenre> GetByGenreId(int genreId);
        ArtistsGenre GetById(int id);
        bool Create(ArtistsGenre artistGenre);
        bool Update(ArtistsGenre artistGenre);
        bool Delete(int id);
    }
}
