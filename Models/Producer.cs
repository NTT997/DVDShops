using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Producer
{
    public int ProducerId { get; set; }

    public string ProducerName { get; set; } = null!;

    public string ProducerIntroduction { get; set; } = null!;

    public bool DeleteStatus { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
