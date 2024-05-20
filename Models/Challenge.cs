using System;
using System.Collections.Generic;

namespace Fitness.Models;

public partial class Challenge
{
    public int ChallangeId { get; set; }

    public string? ChallangeName { get; set; }

    public string? ChallangeDesc { get; set; }

    public DateTime? ChallangeStartDate { get; set; }

    public DateTime? ChallangeEndDate { get; set; }

    public bool? ChallangeIsDeleted { get; set; }

    public short? ChallangeDifficulty { get; set; }

    public string? ChallangeCategory { get; set; }

    public string? ChallangeUserId { get; set; }
}
