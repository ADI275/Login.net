using LoginApplication.Model;
using LoginApplication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoginApplication.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public readonly BranchDbContext branchDbContext;

        [BindProperty]
        public Register Model { get; set; }
        public RegisterModel(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, BranchDbContext branchDbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.branchDbContext = branchDbContext;
        }
        public List<Branch> Branches { get; set; }
        public List<SelectListItem> Options { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                var user = new User()
                {
                    UserName = Model.Email,
                    Email = Model.Email,
                    Name = Model.Name,
                    Address = Model.Address,
                    Branch = Model.Branch,
                    ManagerName = Model.ManagerName,
                    AccountCreationDate = DateTime.UtcNow.Date,
                    IsAdmin = Model.IsAdmin,
                    HR = Model.HR,
                    PriceManager = Model.PriceManager,
                    AssetManager = Model.AssetManager,
                };
                var result = await userManager.CreateAsync(user, Model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    var currUser = await userManager.FindByEmailAsync(Model.Email);
                    if(Model.IsAdmin)
                    {
                        var roleExists = await roleManager.RoleExistsAsync("Admin");
                        if(!roleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole("Admin"));
                        }
                        await userManager.AddToRoleAsync(currUser, "Admin");
                    }
                    if(Model.HR)
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
                    return RedirectToPage("Index");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }
    }
}
