using System;
using KikNPay.www.usaepay.com;

namespace KikNPay
{
	/// <summary>
	/// do we have enough info to make call ?
	/// validate if cc data is correct
	/// return from USAePay if there is something wrong
	/// provide list of all error codes and descriptions
	/// </summary>
	public class ValidateCCData : IUsaepayStrategy<usaepayService, IUsaepayHelperConfig, ICCData>
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
				throw new ValidateCCDataException("Validate Credit Card Data", new ArgumentNullException(nameof(context)));
			if (config == null)				
				throw new ValidateCCDataException("Validate Credit Card Data", new ArgumentNullException(nameof(config)));
			if (data == null)
				throw new ValidateCCDataException("Validate Credit Card Data",new ArgumentNullException(nameof(data)));
			var result = new PaymentControllerEventArgs();
			try{				
				throw new ValidateCCDataException("Validate Credit Card Data", new NotImplementedException());
			}
			catch (Exception ex){
				result.Exception = ex;
			}
			return result;
		}
	}


}