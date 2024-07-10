using OutOfOfficeApp.Entities;
using System.ComponentModel.DataAnnotations;

namespace OutOfOfficeApp.Models
{
    public class RegisterUserDtoIn
    {
        [Required]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password{ get; set; }
        public string ConfirmPassword { get; set; }
        public int RoleId { get; set; }
    }
}
