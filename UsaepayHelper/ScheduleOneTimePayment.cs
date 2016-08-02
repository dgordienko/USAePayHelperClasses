using System;
using System.Diagnostics.CodeAnalysis;
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
    public class ScheduleOneTimePayment : IPaymentStrategy<USAePay, IPaymentConfig, IPaymentData>
	{
        /// <summary>
		/// Strategy the specified context, config and data.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="config">Config.</param>
		/// <param name="data">Data.</param>
		public object Strategy(USAePay context, IPaymentConfig config, IPaymentData data)
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
                    context.SourceKey = config.SourceKey;
                    context.Pin = config.Pin;
                    context.UseSandbox = config.IsSendBox;
                    context.Amount = info.Amount;
                    context.Description = info.Description;
                    context.CardHolder = info.NameOnCreditCard;
                    context.CardNumber = info.CreditCardNumber;
                    context.CardExp = info.ExpirationDate;
                    if (context.Sale())
                    {
                        result.Result = context.ResultRefNum;
                    }
                }
				catch (Exception ex){
					result.Exception = new ScheduleOneTimePaymentException(context.ErrorMesg, ex);
				}
			}));
			return result;
		}


	}
	
}