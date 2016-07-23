using System;
using System.Diagnostics.CodeAnalysis;

namespace KinNPayUsaEPay
{
	/// <summary>
	/// Validate CCD ata exception.
	/// </summary>
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class AddCustomerPaymentMethodException : Exception
	{
	    /// <summary>
		/// Initializes a new instance of the <see cref="T:MyException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. </param>
		public AddCustomerPaymentMethodException(string message, Exception inner) : base(message, inner)
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:MyException"/> class
		/// </summary>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <param name="info">The object that holds the serialized object data.</param>
		protected AddCustomerPaymentMethodException(System.Runtime.Serialization.SerializationInfo info, 
		                                            System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
			
		}
	}		
		
}