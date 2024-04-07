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

    public virtual User Users { get; set; } = null!;
}
