using FluentValidation;
using LoginApplication.Model;
using LoginApplication.ViewModels;

namespace LoginApplication.Validators
{
    public class StockValidator : AbstractValidator<Stock>
    {
        private readonly StockDbContext stockDbContext;

        public StockValidator(StockDbContext stockDbContext)
        {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Must(BeUniqueName).WithMessage("Name must be unique.");
            this.stockDbContext = stockDbContext;
        }

        private bool BeUniqueName(string name)
        {
            return !(stockDbContext.Stocks.Any(m => m.Name == name));
        }
    }
}
