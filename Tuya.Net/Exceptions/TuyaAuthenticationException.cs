namespace Tuya.Net.Exceptions
{
    /// <summary>
    /// Tuya authentication exception class.
    /// </summary>
    internal class TuyaAuthenticationException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TuyaAuthenticationException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public TuyaAuthenticationException(string? message) : base(message)
        {
        }
    }
}
