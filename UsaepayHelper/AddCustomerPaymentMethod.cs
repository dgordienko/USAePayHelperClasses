using System;
using USAePayAPI.com.usaepay.www;
// ReSharper disable All

namespace KlikNPayUsaEPay
{
	/// <summary>
	/// do we have enough info to make call ?
	/// validate if cc data is correct
	/// return from USAePay if there is something wrong
	/// provide list of all error codes and descriptions
	/// </summary>
	public class AddCustomerPaymentMethod : IKlikNPaymantStrategy<usaepayService, IKlikNPayUsaePayConfig, IKlikNPayUsaEPayData>
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
				throw new AddCustomerPaymentMethodException("Add customer payment exception", new ArgumentNullException(nameof(context)));
			if (config == null)				
				throw new AddCustomerPaymentMethodException("Add customer payment exception", new ArgumentNullException(nameof(config)));
			if (data == null)
				throw new AddCustomerPaymentMethodException("Add customer payment exception",new ArgumentNullException(nameof(data)));
            var result = new PaymentArgument();
			try{
				var tocken = config.GetSecurityToken();
				var res = KlikNPayUsaEPayExtentionMethods.CreatePaymentMethod(context, tocken, data);
				result.Result = res;
			}
			catch (Exception ex){
				var exception = new AddCustomerPaymentMethodException("Error update payment methods", ex);
				result.Exception = exception;
			}
			return result;
		}
	}


}