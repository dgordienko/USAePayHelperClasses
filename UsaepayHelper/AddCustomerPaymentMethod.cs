using System;
using USAePayAPI;
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
	public class AddCustomerPaymentMethod : IKlikNPaymentStrategy<usaepayService, IPaymentConfig, IPaymentData>
	{
		/// <summary>
		/// Method the specified context, config and data. 
		/// return usaepay payment method id  string or exception
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="config">Config.</param>
		/// <param name="data">Data.</param>
		public object Method(usaepayService context, IPaymentConfig config, IPaymentData data)
		{			
			if (context == null)
				throw new AddCustomerPaymentMethodException("Add customer payment exception", new ArgumentNullException("context"));
			if (config == null)				
				throw new AddCustomerPaymentMethodException("Add customer payment exception", new ArgumentNullException("config"));
			if (data == null)
				throw new AddCustomerPaymentMethodException("Add customer payment exception",new ArgumentNullException("data"));
            var result = new PaymentArgument();
            var tocken = config.GetSecurityToken();
            var pay = new USAePay{
                SourceKey = config.SourceKey,
                Pin = config.Pin,
                UseSandbox = config.IsSendBox
            };
            try
            { 
			    data.With(x => x.AddNewCreditCardInfo.Do(addCard =>
			    {
			        pay.Amount = 1;
			        pay.Description = addCard.Description;
			        pay.CardHolder = addCard.NameOnCreditCard;
                    pay.CardNumber = addCard.CreditCardNumber;
			        pay.CardExp = addCard.ExpirationDate;
                    result.Result =   pay.Sale();
                    if ((bool)(result.Result)){
                        pay.Void(pay.ResultRefNum);
                    }
                }));                               
			}
			catch (Exception ex)
			{
			    ex.With(x => x.InnerException.Do(ie =>
			    {
                    var except = new AddCustomerPaymentMethodException(pay.ErrorMesg, ex);
                    result.Exception = except;
			    }));
			}
			return result;
		}
	}


}