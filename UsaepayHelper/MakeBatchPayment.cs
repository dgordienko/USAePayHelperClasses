using System;
using USAePayAPI.com.usaepay.www;

namespace KinNPayUsaEPay
{

	
	/// <summary>
	/// Send batch file to USAePay for processing
	/// return success code
	/// provide list of all codes and descriptions
	/// </summary>
	public class MakeBatchPayment : IKlikNPaymantStrategy<usaepayService, IKlikNPayUsaePayConfig, IKlikNPayUsaEPayData>
	{
		/// <summary>
		/// Method the specified context, config and data.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="config">Config.</param>
		/// <param name="data">Data.</param>
		public object Method(usaepayService context, IKlikNPayUsaePayConfig config, IKlikNPayUsaEPayData data)
		{
			if (context == null)
				throw new MakeBanchPaymentException($"MakeBatchPayment {nameof(context)} is null", new ArgumentNullException(nameof(context)));
			if (config == null)
			throw new MakeBanchPaymentException($"MakeBatchPayment {nameof(config)} is null", new ArgumentNullException(nameof(config)));
			if(data == null)
				throw new MakeBanchPaymentException($"MakeBatchPayment {nameof(data)} is null", new ArgumentNullException(nameof(context)));
				throw new ArgumentNullException(nameof(data));
			throw new NotImplementedException();
		}
	}
}