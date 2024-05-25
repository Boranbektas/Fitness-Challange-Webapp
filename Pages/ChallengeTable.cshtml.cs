using Fitness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace MyApp.Namespace
{
    public class ChallengeTableModel : PageModel
    {
        private readonly FitnessDatabaseContext _context;
        public ChallengeTableModel(FitnessDatabaseContext context){
            _context = context;
            CategoryOptions=Enum.GetValues(typeof(Categories)).Cast<Categories>().
                        Select(c=> new SelectListItem(){
                            Value=c.ToString(),
                            Text =c.ToString(),
                        }).ToList();
        }
        public string ChallengeNameSort { get; set; }
        public string ChallengeDateSort { get; set; }
        public string ChallengeDifficultySort {get;set;}
        public string ChallengeCategorySort { get; set; }
        public string CurrentSort {get;set;}
        public string CurrentFilter {get;set;}
        public IList<Challenge> challenges{ get; set; }
        public List<SelectListItem> CategoryOptions {get;set;}
        
        
        public async Task OnGetAsync(string sortOrder,string NameSearchString ,int DiffSearch,string CategorySearch,DateTime? StartDateSearch){
            ChallengeNameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ChallengeDateSort =sortOrder == "date" ? "date_desc":"date";
            ChallengeDifficultySort =sortOrder =="diff" ?  "diff_desc":"diff";
            ChallengeCategorySort = sortOrder == "category" ? "category_desc": "category";

            CurrentFilter = NameSearchString;
            

            IQueryable<Challenge> challengesIQ =from r in _context.Challenges
                                                where r.ChallangeIsDeleted == false
                                                select r;
            
            if(!String.IsNullOrEmpty(NameSearchString))
            {
                challengesIQ = challengesIQ.Where(r => r.ChallangeName.Contains(NameSearchString));
            }

            if(DiffSearch != 0)
            {
                challengesIQ = challengesIQ.Where(r=>r.ChallangeDifficulty == DiffSearch);
            }

            if(!String.IsNullOrEmpty(CategorySearch))
            {
                challengesIQ = challengesIQ.Where(r => r.ChallangeCategory.Contains(CategorySearch));
            }
            if(StartDateSearch != null){
                challengesIQ = challengesIQ.Where(r=> r.ChallangeStartDate > StartDateSearch);
            }

            switch(sortOrder)
            {
                case "name_desc":
                    challengesIQ = challengesIQ.OrderByDescending(r=> r.ChallangeName);
                    break;
                case "":
                    challengesIQ = challengesIQ.OrderBy(r=> r.ChallangeName);
                    break;
                case "date":
                    challengesIQ = challengesIQ.OrderBy(r=> r.ChallangeStartDate);
                    break;
                case "date_desc":
                    challengesIQ = challengesIQ.OrderByDescending(r=> r.ChallangeStartDate);
                    break;
                case "diff":
                    challengesIQ = challengesIQ.OrderBy(r=> r.ChallangeDifficulty);
                    break;
                case "diff_desc":
                    challengesIQ = challengesIQ.OrderByDescending(r=> r.ChallangeDifficulty);
                    break;
                case "category":
                    challengesIQ = challengesIQ.OrderBy(r =>r.ChallangeCategory);
                    break;
                case "category_desc":
                    challengesIQ = challengesIQ.OrderByDescending(r => r.ChallangeCategory);
                    break;
            }

            challenges = await challengesIQ.AsNoTracking().ToListAsync();
        }
    }
}
