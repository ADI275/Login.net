using LoginApplication.Model;
using LoginApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
        public async Task OnGet()
        {
            List<User> UserList = await authDbContext.Users.ToListAsync();
            DateTime currentDate = DateTime.Today;
            foreach (var User in UserList)
            {
                if (User.NameChangeDate != null && User.NameChangeDate.HasValue && currentDate.Date >= User.NameChangeDate.Value.Date)
                {
                    User.Branch = User.ChangedBranch;
                    User.NameChangeDate = null;
                }
            }
            if(Users != UserList)
            {
                authDbContext.Users.UpdateRange(UserList);
                await authDbContext.SaveChangesAsync();
            }
            Users = await authDbContext.Users.ToListAsync();
        }
    }
}
