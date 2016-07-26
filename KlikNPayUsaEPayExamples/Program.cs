using System;
using System.Collections.Generic;
using KlikNPayUsaEPay;
using NLog;

namespace KlikNPayUsaEPayExamples
{
    internal static class Program
    {
        /// <summary>
        /// Impement  IKlikNPayUsaEPayConfig (IntellySence)
        /// </summary>
        private class PaymentConfig:IKlikNPayUsaEPayConfig
        {
            #region Implementation of IKlikNPayUsaEPayConfig

            /// <summary>
            /// Gets or sets the source key.
            /// </summary>
            /// <value>The source key.</value>
            public string SourceKey { get; set; }

            /// <summary>
            /// Gets or sets the pin.
            /// </summary>
            /// <value>The pin.</value>
            public string Pin { get; set; }

            /// <summary>
            /// Gets or sets the use proxi.
            /// </summary>
            /// <value>The use proxi.</value>
            public bool UseProxi { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public bool IsSendBox { get; set; }

            #endregion
        }

        /// <summary>
        /// Implement  IKlikNPayUsaEPayData (IntellySence)
        /// </summary>
        private class PaymentData : IKlikNPayUsaEPayData
        {
			/// <summary>
			/// Gets or sets the batch upload record.(MakeOneTimePayment.html)
			/// </summary>
			/// <value>The batch upload record.</value>
			public IUsaEPayFields BatchUploadRecord
			{

				get;set;
			}
			/// <summary>
			/// Gets or sets the batch upload records.(make Batch Upload)
			/// </summary>
			/// <value>The batch upload records.</value>
			public IEnumerable<IUsaEPayFields> BatchUploadRecords
			{
				get;set;
			}
			#region Implementation of IKlikNPayUsaEPayData

			/// <summary>
			/// Gets or sets the new info.
			/// </summary>
			/// <value>The new info.</value>
			public IUsaEPayPaimentInfo NewInfo { get; set; }

            /// <summary>
            /// Gets or sets the paymant info.
            /// </summary>
            /// <value>The paymant info.</value>
            public IPaymentInfo PaymantInfo { get; set; }

			IUsaEPayFields IKlikNPayUsaEPayData.PaymantInfo
			{
				get;set;
			}


			#endregion
		}

        /// <summary>
        /// Implement IUsaEPayPaimentInfo (IntellySence)
        /// </summary>
        private class PaymentInfo:IUsaEPayPaimentInfo
        {
            #region Implementation of IUsaEPayPaimentInfo

            /// <summary>
            /// Gets or sets the description.
            /// </summary>
            /// <value>The description.</value>
            public string Description { get; set; }

            /// <summary>
            /// Gets or sets the credit card number.
            /// </summary>
            /// <value>The credit card number.</value>
            public string CreditCardNumber { get; set; }

            /// <summary>
            /// Gets or sets the expiration date.
            /// </summary>
            /// <value>The expiration date.</value>
            public string ExpirationDate { get; set; }

            /// <summary>
            /// Gets or sets the name on credit card.
            /// </summary>
            /// <value>The name on credit card.</value>
            public string NameOnCreditCard { get; set; }

            /// <summary>
            /// Gets or sets the billing address.
            /// </summary>
            /// <value>The billing address.</value>
            public string BillingAddressLine1 { get; set; }

            /// <summary>
            /// Gets or sets the billing address line2.
            /// </summary>
            /// <value>The billing address line2.</value>
            public string BillingAddressLine2 { get; set; }

            /// <summary>
            /// Gets or sets the city.
            /// </summary>
            /// <value>The city.</value>
            public string City { get; set; }

            /// <summary>
            /// Gets or sets the state.
            /// </summary>
            /// <value>The state.</value>
            public string State { get; set; }

            /// <summary>
            /// Gets or sets the zip code.
            /// </summary>
            /// <value>The zip code.</value>
            public string ZipCode { get; set; }

            /// <summary>
            /// Gets or sets the country.
            /// </summary>
            /// <value>The country.</value>
            public string Country { get; set; }

            /// <summary>
            ///	CVC
            /// </summary>
            /// <value>The code.</value>
            public string CVC { get; set; }

            /// <summary>
            /// Gets or sets the customer identifier.
            /// </summary>
            /// <value>The customer identifier.</value>
            public int? CustomerId { get; set; }

            #endregion
        }

        /// <summary>
        /// Nlog simple logger
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static void Main()
        {
            //create instance IKlikNPayUsaEPayConfig
            var config = new PaymentConfig
            {
                SourceKey = "SOURCE KEY",
                Pin = "PIN"
            };
            //create instance  IKlikNPayUsaEPayData
            var data = new PaymentData
            {
                //create instance IKlikNPayUsaEPayData
                NewInfo = new PaymentInfo
                {
                    CustomerId = 0,
                    CreditCardNumber = "CREDITCARDNUMBER"
                }
            };
            //add payment method
            using (var client = new KlikNPayUsaEPayAdapter(config))
            {
                client.Data = data;
                //subscript event 
                client.MethodComplete += (sender, argument) =>
                {
                    //error
                    argument.With(x => x.Exception.Do(e => { Logger.Trace(e.Message);}));
                    //result  is  IPaymentArgument
                    argument.With(x => x.Result.Do(result =>
                    {
                        var paymentArgument = (IPaymentArgument) result;
                        //UsaEPey return exception
                        paymentArgument.With(a => a.Exception.Do(pe => Logger.Trace(pe.Message)));
                        //UsaEPay reurn ok 
                        paymentArgument.With(a => a.Result.Do(res => Logger.Trace(res)));
                    }));

                };
                //call add new customer payment method
                client.ExecuteStrategy(new AddCustomerPaymentMethod());
            }
        }
    }
}
