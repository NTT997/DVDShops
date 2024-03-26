using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class User
{
    public int UsersId { get; set; }

    public string UsersEmail { get; set; } = null!;

    public string UsersPassword { get; set; } = null!;

    public bool UsersActivate { get; set; }

    public bool IsAdmin { get; set; }

    public string? UsersProfileName { get; set; }

    public DateOnly? UsersDateOfBirth { get; set; }

    public string? UsersAddress { get; set; }

    public int? UsersPhone { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
