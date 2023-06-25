using System.ComponentModel.DataAnnotations;

namespace LoginApplication.ViewModels
{
    public class Branch
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
