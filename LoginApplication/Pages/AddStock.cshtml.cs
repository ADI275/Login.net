using FluentValidation.Results;
using LoginApplication.Model;
using LoginApplication.Validators;
using LoginApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoginApplication.Pages
{
    [Authorize(Roles = "Admin")]
    public class AddStockModel : PageModel
    {
        private readonly StockDbContext stockDbContext;
        private readonly BranchDbContext branchDbContext;

        public AddStockModel(StockDbContext stockDbContext, BranchDbContext branchDbContext)
        {
            this.stockDbContext = stockDbContext;
            this.branchDbContext = branchDbContext;
        }
        [BindProperty]
        public Stock Model { get; set; }
        public List<Branch> Branches { get; set; }
        /*[BindProperty]
        public string SelectedOption { get; set; }*/
        public List<SelectListItem> Options { get; set; }
        public void OnGet()
        {
            Branches = branchDbContext.Branches.ToList();
            Options = new List<SelectListItem>();
            foreach (var branch in Branches)
            {
                Options.Add(new SelectListItem { Value = branch.Name, Text = branch.Name });
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                var stock = new Stock()
                {
                    Name = Model.Name,
                    Price = Model.Price,
                    SellingPrice = Model.SellingPrice,
                    Qty = Model.Qty,
                    Branch = Model.Branch
                };
                var validator = new StockValidator(stockDbContext);
                ValidationResult results = validator.Validate(stock);
                if(results.IsValid)
                {
                    await stockDbContext.AddAsync(stock);
                    await stockDbContext.SaveChangesAsync();
                    return RedirectToPage("Stocks");
                }
                ModelState.AddModelError("", "Add a unique model name");
                return Page();
            }
            ModelState.AddModelError("", "stock wasn't added");
            return Page();
        }
    }
}
