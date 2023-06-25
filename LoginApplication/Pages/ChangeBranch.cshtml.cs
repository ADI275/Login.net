using LoginApplication.Model;
using LoginApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoginApplication.Pages
{
    [Authorize(Roles = "Admin, HR")]
    public class ChangeBranchModel : PageModel
    {
        public readonly BranchDbContext branchDbContext;
        public readonly AuthDbContext authDbContext;

        public ChangeBranchModel(BranchDbContext branchDbContext, AuthDbContext authDbContext)
        {
            this.branchDbContext = branchDbContext;
            this.authDbContext = authDbContext;
        }
        [BindProperty]
        public string SelectedBranchOption { get; set; }
        [BindProperty]
        public string SelectedUserOption { get; set; }
        [BindProperty]
        public DateTime DateInput { get; set; }
        public List<User> Users { get; set; }
        public List<SelectListItem> UserOptions { get; set; }
        public List<Branch> Branches { get; set; }
        public List<SelectListItem> BranchOptions { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = authDbContext.Users.FirstOrDefault(user => user.Name == SelectedUserOption);
            user.ChangedBranch = SelectedBranchOption;
            user.NameChangeDate = DateInput;
            await authDbContext.SaveChangesAsync();
            return RedirectToPage("Users");
        }
    }
}
