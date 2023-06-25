using System.ComponentModel.DataAnnotations;

namespace LoginApplication.ViewModels
{
    public class Register
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage ="Password and confirm-password did not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public string Branch { get; set; }
        public DateTime AccountCreationDate { get; set; }
        [Required]
        public string ManagerName { get; set; }
        public bool IsAdmin { get; set; }
        public bool PriceManager { get; set; }
        public bool HR { get; set; }
        public bool AssetManager { get; set; }
    }
}
