using System.ComponentModel.DataAnnotations;

namespace UserManager.Domain.Models.Dtos
{
    public class UpsertUserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(Password))]
        public string PasswordConfirmation { get; set; } = string.Empty;
    }
}
