using System;
using System.Collections.Generic;

namespace Fitness.Models;

public partial class UserRate
{
    public int UserRateId { get; set; }

    public string? UserRateUserId { get; set; }

    public int? UserRateChallengeId { get; set; }

    public short? UserRateRate { get; set; }
}
