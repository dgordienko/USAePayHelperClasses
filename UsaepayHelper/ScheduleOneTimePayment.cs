using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using USAePayAPI.com.usaepay.www;
using System.Text;
using USAePayAPI;

namespace KlikNPayUsaEPay
{
    /// <summary>
    /// Send payment info to USAePay
    /// return success code
    /// provide list of all codes and descriptions
    /// special handling: if we don't gate "ok" from the gateway, then automatically send a status request for 
    /// that merchant and order number - to confirm that the gateway does not have that transaction. 
    /// This is to avoid the special case when the gateway sends back an 'ok' but we never get it.
    /// </summary>
    [SuppressMessage("ReSharper", "UseNameofExpression")]
    public class ScheduleOneTimePayment : IKlikNPaymentStrategy<usaepayService, IPaymentConfig, IPaymentData>
	{
        /// <summary>
		/// Method the specified context, config and data.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="config">Config.</param>
		/// <param name="data">Data.</param>
		public object Method(usaepayService context, IPaymentConfig config, IPaymentData data)
		{
			if (context == null)
				throw new ScheduleOneTimePaymentException("MakePayment Argument Null Exception", 
				                               new ArgumentNullException("context"));
			if (config == null)
				throw new ScheduleOneTimePaymentException("MakePayment Argument Null Exception", 
				                               new ArgumentNullException("config"));
			if (data == null)
				throw new ScheduleOneTimePaymentException("MakePayment Argument Null Exception",
				                               new ArgumentNullException("data"));
			//return success code
			var result = new PaymentArgument();	
			//Send payment info to USAePay
			data.With(x => x.PaymantInfo.Do(pInfo => { 
				try
				{
                    var pay = new USAePay
                    {
                        SourceKey = config.SourceKey,
                        Pin = config.Pin,
                        UseSandbox = config.IsSendBox
                    };

                }
				catch (Exception ex)
				{
					result.Exception = new ScheduleOneTimePaymentException(ex.Message, ex);
				}
			}));
			return result;
		}


	}
	
}