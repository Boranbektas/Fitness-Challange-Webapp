using Fitness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    [Authorize]
    public class ChallengeDetailsModel : PageModel
    {
        private readonly FitnessDatabaseContext _context;
        public Challenge challenge{ get; set; }
        public ChallengeDetailsModel(FitnessDatabaseContext context )
        {   
            _context = context;
        }
        public async Task OnGetAsync(int id)
        {
            challenge = _context.Challenges.FirstOrDefault(r=> r.ChallangeId == id);


        }
    }
}
