using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Producer
{
    public int ProdId { get; set; }

    public string ProdName { get; set; } = null!;

    public string ProdIntro { get; set; } = null!;

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
