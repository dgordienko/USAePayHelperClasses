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
	public class AddCustomerPaymentMethod : IKlikNPaymentStrategy<usaepayService, IKlikNPayUsaEPayConfig, IKlikNPayUsaEPayData>
	{
		/// <summary>
		/// Method the specified context, config and data. 
		/// return usaepay payment method id  string or exception
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="config">Config.</param>
		/// <param name="data">Data.</param>
		public object Method(usaepayService context, IKlikNPayUsaEPayConfig config, IKlikNPayUsaEPayData data)
		{			
			if (context == null)
				throw new AddCustomerPaymentMethodException("Add customer payment exception", new ArgumentNullException("context"));
			if (config == null)				
				throw new AddCustomerPaymentMethodException("Add customer payment exception", new ArgumentNullException("config"));
			if (data == null)
				throw new AddCustomerPaymentMethodException("Add customer payment exception",new ArgumentNullException("data"));
            var result = new PaymentArgument();
			try{
				var tocken = config.GetSecurityToken();
				var res = KlikNPayUsaEPayExtentionMethods.AddCutomersPaymentMethod(context, tocken, data);
                if(string.IsNullOrWhiteSpace(res))
                    result.Result = res;
				else
				{
				    throw new AddCustomerPaymentMethodException("Exception update",
				        new Exception("payment method information is not updated"));
				}
			}
			catch (Exception ex){
				var exception = new AddCustomerPaymentMethodException("Error update payment methods", ex);
				result.Exception = exception;
			}
			return result;
		}
	}


}