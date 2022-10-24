using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants.ApplicationUser;

namespace Library.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(UserUserNameMaxLength, MinimumLength = UserUserNameMinLength)]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(UserEmailMaxLength, MinimumLength = UserEmailMinLength)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(UserPasswordMaxLength, MinimumLength = UserPasswordMinLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
