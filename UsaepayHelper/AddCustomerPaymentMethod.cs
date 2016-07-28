using System;
using USAePayAPI;

namespace KlikNPayUsaEPay
{
    /// <summary>
    /// do we have enough info to make call ?
    /// validate if cc data is correct
    /// return from USAePay if there is something wrong
    /// provide list of all error codes and descriptions
    /// </summary>
    public class AddCustomerPaymentMethod : IPaymentStrategy<USAePay, IPaymentConfig, IPaymentData>
	{
		/// <summary>
		/// Method the specified context, config and data. 
		/// return usaepay payment method id  string or exception
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="config">Config.</param>
		/// <param name="data">Data.</param>
		public object Method(USAePay context, IPaymentConfig config, IPaymentData data)
		{			
			if (context == null)
				throw new AddCustomerPaymentMethodException("Add customer payment exception",
                    new ArgumentNullException("context"));
			if (config == null)				
				throw new AddCustomerPaymentMethodException("Add customer payment exception", 
                    new ArgumentNullException("config"));
			if (data == null)
				throw new AddCustomerPaymentMethodException("Add customer payment exception",
                    new ArgumentNullException("data"));
            var result = new PaymentArgument();
            var tocken = config.GetSecurityToken();

            context.SourceKey = config.SourceKey;
            context.Pin = config.Pin;
            context.UseSandbox = config.IsSendBox;
                                
            try
            { 
			    data.With(x => x.CreditCardPaymentInfo.Do(addCard =>
			    {
			        context.Amount = 1;
                    context.Description = addCard.Description;
			        context.CardHolder = addCard.NameOnCreditCard;
                    context.CardNumber = addCard.CreditCardNumber;
			        context.CardExp = addCard.ExpirationDate;
                    var res = context.Sale();
                    result.Result = res;
                    if (res)
                    {
                        context.Void(context.ResultRefNum);
                    }
                }));                               
			}
			catch (Exception ex)
			{
			    ex.With(x => x.InnerException.Do(ie =>
			    {
                    var except = new AddCustomerPaymentMethodException(context.ErrorMesg, ex);
                    result.Exception = except;
			    }));
			}
			return result;
		}
	}


}