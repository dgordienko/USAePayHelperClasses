using System;
using KikNPay.www.usaepay.com;

namespace KikNPay
{
	/// <summary>
	/// Send batch file to USAePay for processing
	/// return success code
	/// provide list of all codes and descriptions
	/// </summary>
	public class MakeBatchPayment : IUsaepayStrategy<usaepayService, IUsaepayHelperConfig, ICCData>
	{
		/// <summary>
		/// Method the specified context, config and data.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="config">Config.</param>
		/// <param name="data">Data.</param>
		public object Method(usaepayService context, IUsaepayHelperConfig config, ICCData data)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));
			if (config == null)
				throw new ArgumentNullException(nameof(config));
			if(data == null)
				throw new ArgumentNullException(nameof(data));
			throw new NotImplementedException();
		}
	}
}