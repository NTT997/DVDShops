using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DVDShops.Models
{
    public class ArtistMetaData
    {
        [Required(ErrorMessage = "Artist Name Is Must Have!")]
        public required string ArtistName { get; set; }
    }

    [ModelMetadataType(typeof(Artist))]
    public partial class Artist { }
}
