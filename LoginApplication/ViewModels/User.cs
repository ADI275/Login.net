using Microsoft.AspNetCore.Identity;

namespace LoginApplication.ViewModels
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public string ManagerName { get; set; }
        public bool Permits { get; set; }
    }
}
