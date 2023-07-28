using System.ComponentModel.DataAnnotations;

namespace Restaurant.AdminPanel.Models.Identity
{
    public class LoginViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
