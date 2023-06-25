using System.ComponentModel.DataAnnotations;

namespace LoginApplication.ViewModels
{
    public class Stock
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Branch { get; set; }
        public int Qty { get; set; }
        public int SellingPrice { get; set; }
    }
}
