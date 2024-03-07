using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_API.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string ? FatherName { get; set; }
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
        public ICollection<Wallet> wallets { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
