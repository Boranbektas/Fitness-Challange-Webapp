using System;
using System.Collections.Generic;

namespace Fitness.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? CommentChallengeId { get; set; }

    public string? CommentUserId { get; set; }

    public string? CommentText { get; set; }

    public string? CommentUserName { get; set; }
}
