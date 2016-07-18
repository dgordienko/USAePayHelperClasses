using System;
using System.Security.Cryptography;
using System.Text;
using UsaepayHelper.www.usaepay.com;

namespace UsaepayHelper
{
	/// <summary>
	/// Usaepay helper event arguments.
	/// </summary>
	public class UsaepayHelperEventArgs : EventArgs
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
		public object Exception { get; set; }
	}

	/// <summary>
	/// Usaepay helper event.
	/// </summary>
	public delegate void UsaepayHelperEvent(object sender,UsaepayHelperEventArgs arg);
	
}
