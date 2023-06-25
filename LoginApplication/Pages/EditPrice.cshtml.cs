using LoginApplication.Model;
using LoginApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginApplication.Pages
{
    [Authorize(Roles = "Admin, PriceManager")]
    public class EditPriceModel : PageModel
    {
        private readonly StockDbContext stockDbContext;

        public EditPriceModel(StockDbContext stockDbContext)
        {
            this.stockDbContext = stockDbContext;
        }
        [BindProperty]
        public int Price { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public void OnGet(int id)
        {
            Id = id;
            var stock = stockDbContext.Stocks.FirstOrDefault(s => s.Id == Id);
            Name = stock.Name;
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            Id = id;
            var stock = await stockDbContext.Stocks.FindAsync(Id);
            if (stock != null)
            {
                stock.Price = Price;
                await stockDbContext.SaveChangesAsync();
                return RedirectToPage("Stocks");
            }
            return Page();
        }

    }
}
