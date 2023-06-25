using LoginApplication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<User> userManager;

        public IndexModel(ILogger<IndexModel> logger, UserManager<User> userManager)
        {
            _logger = logger;
            this.userManager = userManager;
        }
        public User User { get; set; }
        public async Task OnGet()
        {
            User = await userManager.GetUserAsync(HttpContext.User);
        }
    }
}