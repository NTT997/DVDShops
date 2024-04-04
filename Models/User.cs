using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class User
{
    public int UsersId { get; set; }

    public string UsersEmail { get; set; } = null!;

    public string UsersPassword { get; set; } = null!;

    public string UsersProfileName { get; set; } = null!;

    public DateOnly UsersDateOfBirth { get; set; }

    public string UsersAddress { get; set; } = null!;

    public long UsersPhone { get; set; }

    public bool UsersActivated { get; set; }

    public bool IsAdmin { get; set; }

    public bool IsCustomer { get; set; }

    public bool IsCancel { get; set; }

    public bool DeleteStatus { get; set; }

    public virtual ICollection<AdminsPermission> AdminsPermissions { get; set; } = new List<AdminsPermission>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
