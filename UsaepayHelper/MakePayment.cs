using System;
using KikNPay.www.usaepay.com;

namespace KikNPay
{

	/// <summary>
	/// Send payment info to USAePay
	/// return success code
	/// provide list of all codes and descriptions
	/// special handling: if we don't gate "ok" from the gateway, then automatically send a status request for that merchant and order number - to confirm that the gateway does not have that transaction. This is to avoid the special case when the gateway sends back an 'ok' but we never get it.
	/// </summary>
	public class MakePayment : IUsaepayStrategy<usaepayService, IUsaepayHelperConfig, ICCData>
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
			if (data == null)
				throw new ArgumentNullException(nameof(data));			
			throw new NotImplementedException();
		}
	}
	
}