using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Jwt.Sample.Models.Client
{
    /// <summary>
    /// Registration Request
    /// </summary>
    public class RegistrationRequest
    {
        /// <summary>
        /// Gets or sets the given name
        /// </summary>
        [Description("Given Name")]
        [MaxLength(100)]
        [Required(ErrorMessage = "Given Name is required")]
        public string GivenName { get; set; }

        /// <summary>
        /// Gets or sets the surname
        /// </summary>
        [MaxLength(100)]
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the email address
        /// </summary>
        [MaxLength(255)]
        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password confirmation
        /// </summary>
        [Description("Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
