using System;

namespace KlikNPayUsaEPay
{
    /// <summary>
	/// Usaepay helper event arguments.
	/// </summary>
	internal class PaymentArgument : EventArgs, IPaymentArgument
    {
		/// <summary>
		/// Gets or sets the result.
		/// </summary>
		/// <value>The result.</value>
		public object Result { get; set; }
		/// <summary>
		/// Gets or sets the exception.
		/// </summary>
		/// <value>The exception.</value>
		public Exception Exception { get; set; }
	}

	/// <summary>
	/// Usaepay helper event.
	/// </summary>
	public delegate void PaymentControllerEvent(object sender, IPaymentArgument arg);	
}
