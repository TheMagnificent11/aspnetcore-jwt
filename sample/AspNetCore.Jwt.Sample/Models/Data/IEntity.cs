namespace AspNetCore.Jwt.Sample.Models.Data
{
    /// <summary>
    /// Entity Interface
    /// </summary>
    /// <typeparam name="TId">ID type</typeparam>
    public interface IEntity<TId>
    {
        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        TId Id { get; set; }
    }
}
