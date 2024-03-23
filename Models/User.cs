using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class User
{
    public int UsersId { get; set; }

    public string UsersName { get; set; } = null!;

    public string UsersEmail { get; set; } = null!;

    public string UsersPassword { get; set; } = null!;

    public int UsersPhone { get; set; }

    public bool UsersActivate { get; set; }

    public bool IsAdmin { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
