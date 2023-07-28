using System.ComponentModel.DataAnnotations;

namespace Restaurant.AdminPanel.Models.Identity
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        public string Firstname { get; set; }
        public string? Lastname { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
