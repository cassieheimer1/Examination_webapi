using System.ComponentModel.DataAnnotations;

namespace WebApi_Examinationen.Models.User
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string StreetAdress { get; set; } = null!;

        [Required]
        public string PostalCode { get; set; } = null!;

        [Required]
        public string City { get; set; } = null!;

        [Required]
        public int TelephoneNumber { get; set; }

    }
}
