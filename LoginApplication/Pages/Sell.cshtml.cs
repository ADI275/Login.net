using LoginApplication.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginApplication.Pages
{
    public class SellModel : PageModel
    {
        private readonly RecordDbContext recordDbContext;
        private readonly StockDbContext stockDbContext;

        public SellModel(RecordDbContext recordDbContext, StockDbContext stockDbContext)
        {
            this.recordDbContext = recordDbContext;
            this.stockDbContext = stockDbContext;
        }

        public void OnGet(int id)
        {
        }
        public async Task<IActionResult> OnPostSellAsync(int id)
        {
            var stock = await stockDbContext.Stocks.FindAsync(id);
            var record = recordDbContext.Records.OrderBy(r => r.Id).LastOrDefault(r => r.StockName == stock.Name);
            record.SoldOn = DateTime.Today;
            record.Profit = record.Qty * (stock.SellingPrice - stock.Price);
            await recordDbContext.SaveChangesAsync();
            return RedirectToPage("UserStocks");
        }
        public IActionResult OnPostDontSellAsync()
        {
            return RedirectToPage("UserStocks");
        }
    }
}
