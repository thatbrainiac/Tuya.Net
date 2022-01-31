namespace Tuya.Net.Exceptions
{
    /// <summary>
    /// Tuya response exception (coming from Tuya API)
    /// </summary>
    internal class TuyaResponseException : Exception
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public override string Message { get; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="TuyaResponseException"/> class.
        /// </summary>
        /// <param name="code">Error code.</param>
        /// <param name="message">Error message.</param>
        public TuyaResponseException(string code, string message) : base(message)
        {
            Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TuyaResponseException"/> class.
        /// </summary>
        /// <param name="message">Error message.</param>
        public TuyaResponseException(string message) : base(message)
        {
        }
    }
}
