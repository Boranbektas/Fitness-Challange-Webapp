using System.Windows.Markup;
using Fitness.Data;
using Fitness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace MyApp.Namespace
{
    [Authorize]
    public class FavChallangesModel : PageModel
    {
        private readonly FitnessDatabaseContext _context;
        private readonly ApplicationDbContext _appContext;
        private readonly UserManager<IdentityUser> _userManager;
        public List<Challenge> challenges {get;set;}
        public FavChallangesModel(FitnessDatabaseContext context, ApplicationDbContext appContext, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _appContext = appContext;
            _userManager = userManager;
        } 
         public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var query = from favs in _context.Favorites
                                join challenge in _context.Challenges on favs.FavoriteChallengeId equals challenge.ChallangeId  
                                where favs.FavoriteUserId == user.Id
                                select challenge;
            challenges = query.ToList();
            return Page();
        }
    }
}
