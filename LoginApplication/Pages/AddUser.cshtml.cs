using LoginApplication.Model;
using LoginApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginApplication.Pages
{
    [Authorize]
    public class AddUserModel : PageModel
    {
        private readonly UserManager<User> userManager;

        [BindProperty]
        public Register Model { get; set; }
        public AddUserModel(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    UserName = Model.Email,
                    Email = Model.Email,
                    Name = Model.Name,
                    Address = Model.Address,
                    ManagerName = Model.ManagerName,
                    AccountCreationDate = DateTime.UtcNow.Date,
                    Permits = Model.Permits
                };
                var result = await userManager.CreateAsync(user, Model.Password);
                if (result.Succeeded)
                {
                    return RedirectToPage("Users");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }
    }
}
