using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public string FeedbackContent { get; set; } = null!;

    public string? FeedbackReply { get; set; }

    public DateOnly FeedbackDate { get; set; }

    public int UsersId { get; set; }

    public int ProductId { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual User Users { get; set; } = null!;
}
