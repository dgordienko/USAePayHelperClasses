using System;

namespace KlikNPayUsaEPay
{
	/// <summary>
	/// Payment argument.
	/// </summary>
    public interface IPaymentArgument
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>The result.</value>
        object Result { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        Exception Exception { get; set; }
    }
}