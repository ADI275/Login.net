using LoginApplication.Model;
using LoginApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginApplication.Pages
{
    public class StocksModel : PageModel
    {
        private readonly StockDbContext stockDbContext;
        public List<Stock> Stocks { get; set; }

        public StocksModel(StockDbContext stockDbContext)
        {
            this.stockDbContext = stockDbContext;
        }
        public void OnGet()
        {
            Stocks = stockDbContext.Stocks.ToList();
        }
    }
}
