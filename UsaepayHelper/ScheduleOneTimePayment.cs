using System;
using System.Diagnostics.CodeAnalysis;
using KlikNPayUsaEPay.com.usaepay;

namespace KlikNPayUsaEPay
{
    /// <summary>
    /// Send payment info to USAePay
    /// return success code
    /// provide list of all codes and descriptions
    /// special handling: if we don't gate "ok" from the gateway, then automatically send a status request for 
    /// that merchant and order number - to confirm that the gateway does not have that transaction. 
    /// This is to avoid the special case when the gateway sends back an 'ok' but we never get it.
    /// https://wiki.usaepay.com/developer/soap-1.4/methods/runtransaction
    /// </summary>
    [SuppressMessage("ReSharper", "UseNameofExpression")]
    public class ScheduleOneTimePayment : IPaymentStrategy<com.usaepay.usaepayService, IPaymentConfig, IPaymentData>
	{
        /// <summary>
		/// Strategy the specified context, config and data.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="config">Config.</param>
		/// <param name="data">Data.</param>
		public object Strategy(usaepayService context, IPaymentConfig config, IPaymentData data)
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
			data.With(x => x.ScedulePaymentInfo.Do(info => { 
				try
				{
				    var token = config.GetSecurityToken();
                    var transactionRequest = new TransactionRequestObject
                    {
                        Command = "cc:sale",
                        Details = new TransactionDetail
                        {
                            Amount = info.Amount,
                            AmountSpecified = true,
                            Description = info.Description,
                            Comments = info.Description
                        },
                        CreditCardData = new CreditCardData
                        {
                            CardNumber = info.CreditCardNumber,
                            CardExpiration = info.ExpirationDate,
                            CAVV = info.CVC                            
                        }
                    };
                    var transactionResponse = context.runTransaction(token, transactionRequest);
                    if (transactionResponse.ResultCode == "A"){
                        result.Result = transactionResponse.RefNum;
                    }
                    else{
                        throw new AddCustomerPaymentMethodException(transactionResponse.Error,
                             new Exception(transactionResponse.Error));
                    }
                }
				catch (Exception ex){                                        
					result.Exception = new ScheduleOneTimePaymentException(ex.Message, ex);
                }
			}));
			return result;
		}


	}
	
}