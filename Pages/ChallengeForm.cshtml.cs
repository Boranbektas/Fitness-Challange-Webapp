using System.Security.Claims;
using Fitness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;

namespace MyApp.Namespace
{
    [Authorize]
    public class ChallengeFormModel : PageModel
    {   
        private readonly FitnessDatabaseContext context;
        private readonly UserManager<IdentityUser> _userManager;
        public ChallengeFormModel(FitnessDatabaseContext _context,UserManager<IdentityUser> _userManager){
            this.context = _context;
            this._userManager = _userManager;
        }
        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public Challenge NewChallenge { get; set; }=default!;
        public List<SelectListItem> CategoryOptions {get;set;}
        public void OnGet()
        {   StatusMessage="";
               //For creating a drop down list of categories
            CategoryOptions=Enum.GetValues(typeof(Categories)).Cast<Categories>().
                        Select(c=> new SelectListItem(){
                            Value=c.ToString(),
                            Text =c.ToString(),
                        }).ToList();

        }
        public IActionResult OnPost(){
            var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            NewChallenge.ChallangeUserId=userId;
            NewChallenge.ChallangeIsDeleted=false;
            if(!ModelState.IsValid ||NewChallenge==null ){
                StatusMessage="Something went wrong";
                return Page();
            }
            context.Add(NewChallenge);
            context.SaveChanges();
            StatusMessage="Challenge created Susccsesfully";
            return Page();
        }
    }
}
