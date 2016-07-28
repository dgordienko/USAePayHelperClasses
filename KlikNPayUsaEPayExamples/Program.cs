using System;
using System.Collections.Generic;
using KlikNPayUsaEPay;
using NLog;

namespace KlikNPayUsaEPayExamples
{
    internal static class Program
    {
        /// <summary>
        /// Impement  IPaymentConfig (IntellySence)
        /// </summary>
        private class PaymentConfig:IPaymentConfig
        {
            #region Implementation of IPaymentConfig

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
        /// Implement  IPaymentData (IntellySence)
        /// </summary>
        private class PaymentData : IPaymentData
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
			#region Implementation of IPaymentData

			/// <summary>
			/// Gets or sets the new info.
			/// </summary>
			/// <value>The new info.</value>
			public IAddNewCreditCardInfo AddNewCreditCardInfo { get; set; }

            /// <summary>
            /// Gets or sets the paymant info.
            /// </summary>
            /// <value>The paymant info.</value>
            public IPaymentInfo PaymantInfo { get; set; }

			IUsaEPayFields IPaymentData.PaymantInfo
			{
				get;set;
			}


			#endregion
		}

        /// <summary>
        /// Implement IAddNewCreditCardInfo (IntellySence)
        /// </summary>
        private class PaymentInfo:IAddNewCreditCardInfo
        {
            #region Implementation of IAddNewCreditCardInfo

            public string Description { get; set; }
            public string CreditCardNumber { get; set; }
            public string CVC { get; set; }
            public string NameOnCreditCard { get; set; }
            public string ExpirationDate { get; set; }
            public string BillingAddress { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string City { get; set; }
            public string StateProvince { get; set; }
            public string ZipCode { get; set; }
            public string Country { get; set; }

            #endregion
        }

        /// <summary>
        /// Nlog simple logger
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static void Main()
        {
            //create instance IPaymentConfig
            var config = new PaymentConfig
            {
                SourceKey = "SOURCE KEY",
                Pin = "PIN"
            };
            //create instance  IPaymentData
            var data = new PaymentData
            {
                //create instance IPaymentData
                AddNewCreditCardInfo = new PaymentInfo
                {

                    CreditCardNumber = "CREDITCARDNUMBER"
                }
            };
            //add payment method
            using (var client = new PaymentComponent(config))
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
