namespace LoginApplication.ViewModels
{
    public class Record
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string StockName { get; set; }
        public string BranchName { get; set; }
        public int Qty { get; set; }
        public DateTime? BoughtOn { get; set; }
        public DateTime? SoldOn { get; set;}
        public int Profit { get; set; }
    }
}
