﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace KlikNPayUsaEPay
{
	/// <summary>
	/// Payment controller
	/// </summary>
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	[SuppressMessage("ReSharper", "UseNameofExpression")]
	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
	public sealed class PaymentComponent:IDisposable
	{
		/// <summary>
		/// The client Usaepay
		/// </summary>
		private readonly com.usaepay.usaepayService client;

	    private readonly IPaymentConfig _config;

		/// <summary>
		/// Gets or sets the data.
		/// </summary>
		/// <value>The data.</value> 
		public IPaymentData Data { get; set; }

	    /// <summary>
		/// Initializes a new instance of class.
		/// </summary>
		public PaymentComponent(IPaymentConfig config) {
			if (config == null)
				throw new ArgumentNullException("config");
			_config = config;
	        client = new com.usaepay.usaepayService {Url = _config.SoapServerUrl};
		}

		/// <summary>
		/// Occurs when method complete.
		/// </summary>
		public  event PaymentControllerEvent MethodComplete;

		/// <summary>
		/// Ons the method complete.
		/// </summary>
		/// <returns>The method complete.</returns>
		/// <param name="e">E.</param>
		private void OnMethodComplete(IPaymentArgument e)
		{
			MethodComplete?.Invoke(this, e);
		}

		/// <summary>
		/// Dos the action.
		/// </summary>
		/// <returns>The action.</returns>
		/// <param name="algoritm">Algoritm.</param>
		public void  ExecuteStrategy(IPaymentStrategy<com.usaepay.usaepayService, IPaymentConfig,IPaymentData> algoritm) {
			if (algoritm == null)
				throw new ArgumentNullException("algoritm");			
			var argument = new PaymentArgument();
			try
			{
				argument.Result = algoritm.Strategy(client,_config,Data);
			}
			catch (Exception ex){
				argument.Exception = ex;
			}
			OnMethodComplete(argument);
		}

		#region IDisposable Support
		private bool disposedValue; // To detect redundant calls

	    private void Dispose(bool disposing)
		{
		    if (disposedValue) return;
		    if (disposing)
		    {
		        // TODO: dispose managed state (managed objects).
		    }

		    // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
		    // TODO: set large fields to null.

		    disposedValue = true;
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~PaymentController() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}

