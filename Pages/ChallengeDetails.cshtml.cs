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
        public Comment newComment { get; set; }
        public string ChallangePoster{ get; set; }
        [TempData]
        public string StatusMessage {get;set;}
        [BindProperty]
        public string InputCommentText {get;set;}

        public ChallengeDetailsModel(FitnessDatabaseContext context , ApplicationDbContext appContext)
        {   
            _context = context;
            _appContext = appContext;
        } 
        public IActionResult OnGet(int id)
        {   
            challenge = _context.Challenges.FirstOrDefault(r=> r.ChallangeId == id);
            var user = _appContext.Users.FirstOrDefault(r => r.Id == challenge.ChallangeUserId);
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
            ChallangePoster = user.UserName;
            return Page();
        }
        public void OnPostFavorite(int id){
            challenge = _context.Challenges.FirstOrDefault(r=> r.ChallangeId == id);
            var user = _appContext.Users.FirstOrDefault(r => r.Id == challenge.ChallangeUserId);

            var result = _context.Favorites.FirstOrDefault(r=> r.FavoriteUserId == user.Id && r.FavoriteChallengeId == challenge.ChallangeId);

            if(result == null){
                newFavorite = new Favorite();
                 newFavorite.FavoriteChallengeId = challenge.ChallangeId;
                newFavorite.FavoriteUserId = user.Id;

                _context.Favorites.Add(newFavorite);
                _context.SaveChanges();
                StatusMessage = "Favorited";
            }
            else{
                _context.Favorites.Remove(result);
                _context.SaveChanges();
                StatusMessage = "UnFavorited";
            }
            ChallangePoster = user.UserName;

        }
        public void OnPostComment(int id)
        {
            challenge = _context.Challenges.FirstOrDefault(r=> r.ChallangeId == id);
            var user = _appContext.Users.FirstOrDefault(r => r.Id == challenge.ChallangeUserId);
            if(user == null){
                StatusMessage="You cant write comments";
            }
            else{
                newComment = new Comment(){
                    CommentChallengeId = challenge.ChallangeId,
                    CommentText = InputCommentText,
                    CommentUserId = user.Id
                };
                _context.Add(newComment);
                _context.SaveChanges();
            }

        }
    
    }
}
