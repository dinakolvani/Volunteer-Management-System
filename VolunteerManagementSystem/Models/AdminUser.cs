using System.ComponentModel.DataAnnotations;

namespace VolunteerManagementSystem.Models
{
    public class AdminUser
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
