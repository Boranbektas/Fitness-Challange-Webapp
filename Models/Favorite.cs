using System;
using System.Collections.Generic;

namespace Fitness.Models;

public partial class Favorite
{
    public int FavoriteId { get; set; }

    public int? FavoriteChallengeId { get; set; }

    public string? FavoriteUserId { get; set; }
}
