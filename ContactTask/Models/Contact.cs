using System.ComponentModel.DataAnnotations;

namespace ContactTask.Models
{
    public class Contact
    {
        public int Id { get; set; } // Contact id.

        [Required(ErrorMessage = "Please enter first name!")]
        public string FirstName { get; set; } = string.Empty; // Contact first name.

        [Required(ErrorMessage = "please enter last name!")]
        public string LastName { get; set; } = string.Empty; // Contact last name.

        [Required(ErrorMessage = "please enter an email")]
        [EmailAddress(ErrorMessage = "Invalid email address!")]
        public string Email { get; set; } = string.Empty;// Contact email.

        [Required(ErrorMessage = "please enter phone number!")]
        [RegularExpression(@"^((\+962)|0)?7[789]\d{7}$", ErrorMessage = "Invalid Jordanian phone number!")]
        public string PhoneNumber { get; set; } = string.Empty; // Contact phone number (Jordan format).
    }
}
