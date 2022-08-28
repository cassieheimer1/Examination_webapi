namespace WebApi_Examinationen.Models.User
{
    public class User
    {
        public string firstName;
        public string lastName;
        public string password;
        public string email;
        public string streetAddress;
        public string postalCode;
        public string city;

        public User(string firstName, string lastName, string password, string email, string streetAdress, string postalCode, string city)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.password = password;
            this.email = email;
            this.streetAddress = streetAdress;
            this.postalCode = postalCode;
            this.city = city;
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string StreetAdress { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public int TelephoneNumber { get; set; }
    }
}
