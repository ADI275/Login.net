using LoginApplication.Model;
using LoginApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LoginApplication.Pages
{
    public class RecordsModel : PageModel
    {
        private readonly RecordDbContext recordDbContext;

        public RecordsModel(RecordDbContext recordDbContext)
        {
            this.recordDbContext = recordDbContext;
        }
        public List<Record> Records { get; set; }
        public async Task OnGet()
        {
            Records = await recordDbContext.Records.ToListAsync();
        }
    }
}
