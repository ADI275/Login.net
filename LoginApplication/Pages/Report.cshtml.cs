using LoginApplication.Model;
using LoginApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LoginApplication.Pages
{
    public class ReportModel : PageModel
    {
        private readonly RecordDbContext recordDbContext;

        public ReportModel(RecordDbContext recordDbContext)
        {
            this.recordDbContext = recordDbContext;
        }
        public List<Record> Records { get; set; }
        public List<Report> Reports { get; set; }
        public async Task OnGet()
        {
            Records = await recordDbContext.Records.ToListAsync();
            Reports = Records.GroupBy(r => r.BranchName).Select(group => new Report
            {
                BranchName = group.Key,
                TotalProfit = group.Sum(r => r.Profit)
            }).ToList();

        }
    }
    public class Report
    {
        public string BranchName { get; set;}
        public int TotalProfit { get; set;}
    }
}
