using LoginApplication.Model;
using LoginApplication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginApplication.Pages
{
    public class UserStocksModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly StockDbContext stockDbContext;
        private readonly RecordDbContext recordDbContext;

        public UserStocksModel(UserManager<User> userManager, StockDbContext stockDbContext, RecordDbContext recordDbContext)
        {
            this.userManager = userManager;
            this.stockDbContext = stockDbContext;
            this.recordDbContext = recordDbContext;
        }
        [NonHandler]
        public bool Valid(string name)
        {
            var record = recordDbContext.Records.OrderBy(r => r.Id).LastOrDefault(r => r.StockName == name);
            if(record == null)
            {
                return false;
            }
            else if(record.SoldOn != null)
            {
                return false;
            }
            return true;
        }
        public async Task<IActionResult> HandleSell(string name, int price, int sellingPrice)
        {
            var record = recordDbContext.Records.OrderBy(r => r.Id).LastOrDefault(r => r.StockName == name);
            record.SoldOn = DateTime.Today;
            record.Profit = record.Qty * (sellingPrice - price);
            await recordDbContext.SaveChangesAsync();
            return RedirectToPage("UserStocks");
        }
        public User User { get; set; }
        public List<Stock> Stocks { get; set; }
        public Task<User> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);
        public async Task OnGet()
        {
            User = await GetCurrentUserAsync();
            var userName = User.Name;
            var branchName = User.Branch;
            Stocks = stockDbContext.Stocks.Where(s => s.Branch == branchName).ToList();
        }
    }
}
