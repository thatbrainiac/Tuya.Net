namespace Tuya.Net.Data
{
    /// <summary>
    /// Tuya identifiable object interface.
    /// </summary>
    public interface IIdentifiable
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public string? Id { get; set; }
    }
}
