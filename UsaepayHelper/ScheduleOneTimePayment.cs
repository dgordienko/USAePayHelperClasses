using System;
using Newtonsoft.Json;
using USAePayAPI.com.usaepay.www;
using System.Text;

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
    public class ScheduleOneTimePayment : IKlikNPaymentStrategy<usaepayService, IKlikNPayUsaEPayConfig, IKlikNPayUsaEPayData>
	{
        private class BatchPaymentInfo : IPaymentInfo
        {

            public string Command { get; set; }

            #region Implementation of IPaymentInfo

            public string PaymentAmount { get; set; }
            public string ForAcount { get; set; }
            public string AccountToPayFrom { get; set; }

            [JsonIgnore]
            public string PaymentDeliveryDate { get; set; }

            #endregion
        }

		/// <summary>
		/// Method the specified context, config and data.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="config">Config.</param>
		/// <param name="data">Data.</param>
		public object Method(usaepayService context, IKlikNPayUsaEPayConfig config, IKlikNPayUsaEPayData data)
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
					var json = JsonConvert.SerializeObject(pInfo);
				    var csv = json.ToObjectCSV();
                    var token = config.GetSecurityToken();
					var fields = BatchFields.GetButchFields();
                    var res = context.createBatchUpload(token, Guid.NewGuid().ToString(), true, "csv", "base64", 
					                                    fields,Convert.ToBase64String(Encoding.Default.GetBytes(csv)), false);
				    result.Result = res;
					//special handling: if we don't gate "ok" from the gateway, then automatically send a status request 
					//for that merchant and order number - to confirm that the gateway does not have that transaction. 
					//This is to avoid the special case when the gateway sends back an 'ok' but we never get it.
					//TODO Must be unit test!
					if (res == null) {
						if (!KlikNPayUsaEPayExtentionMethods.SearchPaymentItem(pInfo.invoice,context,token)) {
							 res = context.createBatchUpload(token, Guid.NewGuid().ToString(), true, "csv", "base64",
							                                 fields, Convert.ToBase64String(Encoding.Default.GetBytes(csv)), false);							
						}
					}
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