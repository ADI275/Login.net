using LoginApplication.Model;
using LoginApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoginApplication.Pages
{
    [Authorize]
    public class AddUserModel : PageModel
    {
        private readonly UserManager<User> userManager;
        public readonly BranchDbContext branchDbContext;
        private readonly RoleManager<IdentityRole> roleManager;

        [BindProperty]
        public Register Model { get; set; }
        public AddUserModel(UserManager<User> userManager, BranchDbContext branchDbContext, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.branchDbContext = branchDbContext;
            this.roleManager = roleManager;
        }
        public List<SelectListItem> Options { get; set; }
        public List<Branch> Branches { get; set; }
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
                    Branch = Model.Branch,
                    IsAdmin = Model.IsAdmin,
                    HR = Model.HR,
                    PriceManager = Model.PriceManager,
                    AssetManager = Model.AssetManager,
                };
                var result = await userManager.CreateAsync(user, Model.Password);
                if (result.Succeeded)
                {
                    var currUser = await userManager.FindByEmailAsync(Model.Email);
                    if (Model.IsAdmin)
                    {
                        var roleExists = await roleManager.RoleExistsAsync("Admin");
                        if (!roleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole("Admin"));
                        }
                        await userManager.AddToRoleAsync(currUser, "Admin");
                    }
                    if (Model.HR)
                    {
                        var roleExists = await roleManager.RoleExistsAsync("HR");
                        if (!roleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole("HR"));
                        }
                        await userManager.AddToRoleAsync(currUser, "HR");
                    }
                    if (Model.PriceManager)
                    {
                        var roleExists = await roleManager.RoleExistsAsync("PriceManager");
                        if (!roleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole("PriceManager"));
                        }
                        await userManager.AddToRoleAsync(currUser, "PriceManager");
                    }
                    if (Model.HR)
                    {
                        var roleExists = await roleManager.RoleExistsAsync("AssetManager");
                        if (!roleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole("AssetManager"));
                        }
                        await userManager.AddToRoleAsync(currUser, "AssetManager");
                    }
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
