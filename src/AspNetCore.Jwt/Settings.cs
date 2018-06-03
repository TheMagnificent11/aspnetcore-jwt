namespace AspNetCore.Jwt
{
    /// <summary>
    /// JWT Settings
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Gets or sets the signing key
        /// </summary>
        public string SigningKey { get; set; }

        /// <summary>
        /// Gets or sets the issues
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the audience
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets the minutes to token expiry
        /// </summary>
        public int ExpiryMins { get; set; }
    }
}
