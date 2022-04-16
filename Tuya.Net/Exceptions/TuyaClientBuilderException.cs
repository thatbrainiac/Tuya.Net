using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuya.Net.Exceptions
{
    /// <summary>
    /// Tuya client builder exception.
    /// </summary>
    internal class TuyaClientBuilderException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TuyaClientBuilderException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public TuyaClientBuilderException(string? message) : base(message)
        {
        }
    }
}
