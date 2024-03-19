using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManager.Data.Entities
{
    [Table("user")]
    public class User : IdentityUser
    {
        public override string? UserName { get; set; } = string.Empty;
    }
}
