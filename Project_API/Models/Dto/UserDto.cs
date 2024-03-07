using System.ComponentModel.DataAnnotations;

namespace Project_API.Models.Dto
{
    public class UserDto
    {
        [Required]
        public string FirstName { get; set; }
        public string FaterName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public DateTime DataOfBirth { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
