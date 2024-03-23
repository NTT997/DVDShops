using DVDShops.Models;

namespace DVDShops.Services.Songs
{
    public interface ISongService
    {
        List<Song> GetAll();
        Song GetById(int songId);
        List<Song> GetByName(string songName);
        List<Song> GetByGenre(int genreId);
        List<Song> GetByProducer(int producerId);
        List<Song> GetByArtist(int artistId);
        bool Create(Song song);
        bool Update(Song song);
        bool Delete(int songId);
    }
}
