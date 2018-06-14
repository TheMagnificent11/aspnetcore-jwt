using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Jwt.Sample.Models.Client
{
    /// <summary>
    /// Update User Request
    /// </summary>
    public class UpdateUserRequest
    {
        /// <summary>
        /// Gets or sets the user's given name
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string GivenName { get; set; }

        /// <summary>
        /// Gets or sets the user's surname
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the email address
        /// </summary>
        [MaxLength(255)]
        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
    }
}
