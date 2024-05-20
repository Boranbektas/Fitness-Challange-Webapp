using System;
using System.Collections.Generic;

namespace Fitness.Models;

public partial class UserDetail
{
    public int UserDetailId { get; set; }

    public string? UserDetailBio { get; set; }

    public byte[]? UserDetailPhoto { get; set; }

    public string? UserDetailUserId { get; set; }
}
