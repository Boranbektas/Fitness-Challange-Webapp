using Fitness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class ReadCommentModel : PageModel
    {
        private readonly FitnessDatabaseContext _context;
        public List<Comment> Comments { get; set; }
        public Challenge challenge{ get; set; }
        public ReadCommentModel(FitnessDatabaseContext context){
            _context = context;
        }
        public void OnGet(int id)
        {
            challenge =_context.Challenges.FirstOrDefault(x => x.ChallangeId == id);
            Comments = _context.Comments.Where(c => c.CommentChallengeId == id).ToList();
        }
    }
}
