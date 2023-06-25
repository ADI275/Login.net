using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LoginApplication.ViewModels
{
    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Branch { get; set; }
        public DateTime AccountCreationDate { get; set; }
        [Required]
        public string ManagerName { get; set; }
        public bool IsAdmin { get; set; }
        public bool PriceManager { get; set; }
        public bool HR { get; set; }
        public bool AssetManager { get; set; }
        public DateTime? NameChangeDate { get; set; }
        public string? ChangedBranch { get; set; }
    }
}
