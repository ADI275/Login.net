using LoginApplication.Model;
using LoginApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginApplication.Pages
{
    [Authorize]
    public class UsersModel : PageModel
    {
        private AuthDbContext authDbContext;
        public List<User> Users { get; set; }
        public UserManager<User> userManager { get; }

        public UsersModel(AuthDbContext authDbContext, UserManager<User> userManager)
        {
            this.authDbContext = authDbContext;
            this.userManager = userManager;
        }
        public Task<User> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);
        public void OnGet()
        {
            Users = authDbContext.Users.ToList();
        }
    }
}
