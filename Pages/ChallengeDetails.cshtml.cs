using System.Security.Cryptography.X509Certificates;
using Fitness.Data;
using Fitness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MyApp.Namespace
{
    [Authorize]
    public class ChallengeDetailsModel : PageModel
    {
        private readonly FitnessDatabaseContext _context;
        private readonly ApplicationDbContext _appContext;
        public Challenge challenge{ get; set; }
        public Favorite newFavorite { get; set; }
        public string ChallangePoster{ get; set; }
        public string favorite { get; set; }
        public string delete { get; set; }
        [TempData]
         public string StatusMessage {get;set;}
      

        public ChallengeDetailsModel(FitnessDatabaseContext context , ApplicationDbContext appContext)
        {   
            _context = context;
            _appContext = appContext;
        } 
        public IActionResult OnGet(int id)
        {   
            challenge = _context.Challenges.FirstOrDefault(r=> r.ChallangeId == id);
            var user = _appContext.Users.FirstOrDefault(r => r.Id == challenge.ChallangeUserId);
            if(favorite == "favorite"){
                newFavorite.FavoriteChallengeId = challenge.ChallangeId;
                newFavorite.FavoriteUserId = user.Id;
                _context.Favorites.Add(newFavorite);
                _context.SaveChanges();
            }
            ChallangePoster = user.UserName;
            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            challenge = _context.Challenges.FirstOrDefault(r=> r.ChallangeId == id);
            var user = _appContext.Users.FirstOrDefault(r => r.Id == challenge.ChallangeUserId);
            if(challenge.ChallangeUserId == user.Id){
                challenge.ChallangeIsDeleted=true;
                _context.Challenges.Update(challenge);
                _context.SaveChanges();
                return RedirectToPage("ChallengeTable");
            }
            StatusMessage = "You cannot delete this challenge.";
            return Page();
        }
        public void OnPostFavorite(){

        }
    
    }
}
