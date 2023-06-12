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
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public string ManagerName { get; set; }
        public bool Permits { get; set; }
    }
}
