using System;
using System.Diagnostics.CodeAnalysis;
using USAePayAPI;
using USAePayAPI.com.usaepay.www;

namespace KlikNPayUsaEPay
{
	/// <summary>
	/// Payment controller
	/// </summary>
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	[SuppressMessage("ReSharper", "UseNameofExpression")]
	public sealed class PaymentComponent:IDisposable
	{
		/// <summary>
		/// The client Usaepay
		/// </summary>
		private readonly USAePay client;

	    private readonly IPaymentConfig _config;

		/// <summary>
		/// Gets or sets the data.
		/// </summary>
		/// <value>The data.</value> 
		public IPaymentData Data { get; set; }

	    /// <summary>
		/// Initializes a new instance of the <see cref="T:UsaepayHelper.UsaepayHelperClass"/> class.
		/// </summary>
		public PaymentComponent(IPaymentConfig config) {
			if (config == null)
				throw new ArgumentNullException("config");
			client = new USAePay();
            
			_config = config;
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
		private void OnMethodComplete(PaymentArgument e)
		{
			MethodComplete?.Invoke(this, e);
		}

		/// <summary>
		/// Dos the action.
		/// </summary>
		/// <returns>The action.</returns>
		/// <param name="algoritm">Algoritm.</param>
		public void  ExecuteStrategy(IPaymentStrategy<USAePay, IPaymentConfig,IPaymentData> algoritm) {
			if (algoritm == null)
				throw new ArgumentNullException("algoritm");			
			var argument = new PaymentArgument();
			try
			{
				argument.Result = algoritm.Method(client,_config,Data);
			}
			catch (Exception ex){
				argument.Exception = ex;
			}
			OnMethodComplete(argument);
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
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

