using System;
using UsaepayHelper.www.usaepay.com;

namespace UsaepayHelper
{
	/// <summary>
	/// Usaepay helper class.
	/// </summary>
	public sealed class UsaepayHelperClass
	{
		/// <summary>
		/// The client Usaepay
		/// </summary>
		private readonly usaepayService client;

		/// <summary>
		/// The data.
		/// </summary>
		private IUsaepayHelperData data;

		/// <summary>
		/// Gets or sets the data.
		/// </summary>
		/// <value>The data.</value> 
		public IUsaepayHelperData Data { 
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
		public UsaepayHelperClass(IUsaepayHelperConfig config) {
			if (config == null)
				throw new ArgumentNullException(nameof(config));
			client = new usaepayService();
		}

		/// <summary>
		/// Occurs when method complete.
		/// </summary>
		public  event UsaepayHelperEvent MethodComplete;

		/// <summary>
		/// Ons the method complete.
		/// </summary>
		/// <returns>The method complete.</returns>
		/// <param name="e">E.</param>
		private void OnMethodComplete(UsaepayHelperEventArgs e)
		{
			MethodComplete?.Invoke(this, e);
		}

		/// <summary>
		/// Dos the action.
		/// </summary>
		/// <returns>The action.</returns>
		/// <param name="algoritm">Algoritm.</param>
		public void  ExecuteStrategy(IUsaepayStrategy<usaepayService,IUsaepayHelperData> algoritm) {
			if (algoritm == null)
				throw new ArgumentNullException(nameof(algoritm));			
			var argument = new UsaepayHelperEventArgs();
			try
			{
				argument.Result = algoritm.Method(client,data);
			}
			catch (Exception ex){
				argument.Exception = ex;
			}
			OnMethodComplete(argument);
		}
	}
}

