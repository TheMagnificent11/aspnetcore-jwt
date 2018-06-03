using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Jwt.Sample.Models.Client
{
    /// <summary>
    /// Login Request
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Gets or sets the email address
        /// </summary>
        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
