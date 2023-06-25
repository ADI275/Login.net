using LoginApplication.Model;
using LoginApplication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginApplication.Pages
{
    public class BuyModel : PageModel
    {
        private readonly StockDbContext stockDbContext;
        private readonly RecordDbContext recordDbContext;
        private readonly UserManager<User> userManager;

        public BuyModel(StockDbContext stockDbContext, RecordDbContext recordDbContext, UserManager<User> userManager)
        {
            this.stockDbContext = stockDbContext;
            this.recordDbContext = recordDbContext;
            this.userManager = userManager;
        }

        [BindProperty]
        public int Qty { get; set; }
        public Stock Stock { get; set; }
        public User User { get; set; }
        public async Task SetState(int id)
        {
            Stock = await stockDbContext.Stocks.FindAsync(id);
            User = await userManager.GetUserAsync(HttpContext.User);
        }
        public async Task OnGet(int id)
        {
            await SetState(id);
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if(ModelState.IsValid)
            {
                await SetState(id);
                Stock.Qty = Stock.Qty - Qty;
                await stockDbContext.SaveChangesAsync();
                var record = new Record()
                {
                    UserName = User.Name,
                    StockName = Stock.Name,
                    BranchName = User.Branch,
                    Qty = Qty,
                    BoughtOn = DateTime.Today,
                    SoldOn = null,
                };
                await recordDbContext.AddAsync(record);
                await recordDbContext.SaveChangesAsync();
                return RedirectToPage("UserStocks");
            }
            return Page();
        }
    }
}
