using System;
using System.Security.Cryptography;
using System.Text;
using KikNPay.www.usaepay.com;

namespace KikNPay
{
	/// <summary>
	/// Usaepay helper event arguments.
	/// </summary>
	public class PaymentControllerEventArgs : EventArgs
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
	public delegate void PaymentControllerEvent(object sender,PaymentControllerEventArgs arg);	
}
