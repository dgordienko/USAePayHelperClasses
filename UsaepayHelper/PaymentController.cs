using System;
using KikNPay.www.usaepay.com;

namespace KikNPay
{
	/// <summary>
	/// Usaepay helper class.
	/// </summary>
	public sealed class PaymentController
	{
		/// <summary>
		/// The client Usaepay
		/// </summary>
		private readonly usaepayService client;

		/// <summary>
		/// The data.
		/// </summary>
		private ICCData data;

		private readonly IUsaepayHelperConfig _config;

		/// <summary>
		/// Gets or sets the data.
		/// </summary>
		/// <value>The data.</value> 
		public ICCData Data { 
			get 
			{ 
				return data;
			} 
			set 
			{ 
				data = value;
			} 
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UsaepayHelper.UsaepayHelperClass"/> class.
		/// </summary>
		public PaymentController(IUsaepayHelperConfig config) {
			if (config == null)
				throw new ArgumentNullException(nameof(config));
			client = new usaepayService();
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
		private void OnMethodComplete(PaymentControllerEventArgs e)
		{
			MethodComplete?.Invoke(this, e);
		}

		/// <summary>
		/// Dos the action.
		/// </summary>
		/// <returns>The action.</returns>
		/// <param name="algoritm">Algoritm.</param>
		public void  ExecuteStrategy(IUsaepayStrategy<usaepayService,IUsaepayHelperConfig,ICCData> algoritm) {
			if (algoritm == null)
				throw new ArgumentNullException(nameof(algoritm));			
			var argument = new PaymentControllerEventArgs();
			try
			{
				argument.Result = algoritm.Method(client,_config,data);
			}
			catch (Exception ex){
				argument.Exception = ex;
			}
			OnMethodComplete(argument);
		}
	}
}

