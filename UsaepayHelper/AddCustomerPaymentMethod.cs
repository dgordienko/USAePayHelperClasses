using System;
using System.Diagnostics.CodeAnalysis;
using KlikNPayUsaEPay.com.usaepay;

namespace KlikNPayUsaEPay
{
    /// <summary>
    /// do we have enough info to make call ?
    /// validate if cc data is correct
    /// return from USAePay if there is something wrong
    /// provide list of all error codes and descriptions
    /// https://wiki.usaepay.com/developer/soap-1.4/methods/runauthonly
    /// </summary>
    [SuppressMessage("ReSharper", "UseNameofExpression")]
    public class AddCustomerPaymentMethod : IPaymentStrategy<usaepayService, IPaymentConfig, IPaymentData>
	{
		/// <summary>
		/// Strategy the specified context, config and data. 
		/// return usaepay payment method id  string or exception
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="config">Config.</param>
		/// <param name="data">Data.</param>
		public object Strategy(usaepayService context, IPaymentConfig config, IPaymentData data)
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
            var token = config.GetSecurityToken();
		    try
            { 
			    data.With(x => x.CreditCardPaymentInfo.Do(addCard =>
			    {
			        var transactionRequest = new TransactionRequestObject
			        {
			            Command = "cc:authonly",
			            Details = new TransactionDetail
			            {
			                Amount = 1.00,
			                AmountSpecified = true,
			                Invoice = addCard.CreditCardNumber,
			                Description = addCard.Description,
                            Comments = addCard.NameOnCreditCard                                                      
			            },
			            CreditCardData = new CreditCardData
			            {
			                CardNumber = addCard.CreditCardNumber,
			                CardExpiration = addCard.ExpirationDate,
                            CAVV = addCard.CVC,
                            AvsZip= addCard.ZipCode,
                            AvsStreet = addCard.AddressLine1                            
			            }
			        };			        
                    var transactionResponse = context.runTransaction(token, transactionRequest);
                    if(transactionResponse.ResultCode == "A")
                    {
                        result.Result = transactionResponse.RefNum;
                    }
                    else
                    {
                       throw new AddCustomerPaymentMethodException(transactionResponse.Error,
                            new Exception(transactionResponse.Error));
                    }
                }));                               
			}
			catch (Exception ex)
			{
			    ex.With(x => x.InnerException.Do(ie =>
			    {
			        var except = new AddCustomerPaymentMethodException(ex.Message, ex);
			        result.Exception = except;
			    }));
			}
			return result;
		}
	}


}