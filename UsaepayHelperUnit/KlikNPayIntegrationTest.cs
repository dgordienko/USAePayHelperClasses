using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

using KlikNPayUsaEPay;
using Newtonsoft.Json;

namespace KlikNPayPaymentUnit
{
	[TestFixture (Description="Component intagrated test case")]
	public class KlikNPayIntegrationTest
	{

        private class PaymentData:IPaymentData
        {
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
            public IUsaEPayFields PaymantInfo { get; set; }

            /// <summary>
            /// Gets or sets the batch upload record.
            /// </summary>
            /// <value>The batch upload record.</value>
            public IUsaEPayFields BatchUploadRecord { get; set; }

            /// <summary>
            /// Gets or sets the batch upload records.
            /// </summary>
            /// <value>The batch upload records.</value>
            public IEnumerable<IUsaEPayFields> BatchUploadRecords { get; set; }

            #endregion
        }

	    private const string ConfigPath =
	        @"C:\Users\DGord\Documents\repo\git\USAePayHelperClasses\UsaepayHelperUnit\Data\IPaymentConfig.json"; 
        private const string AddNewCreditCardPath =
             @"C:\Users\DGord\Documents\repo\git\USAePayHelperClasses\UsaepayHelperUnit\Data\AddNewCreditCard.json";

        [Test(Description="Validate credit card")]
		public void AddCustomenrPaymentDataIntegratedTestCase()
		{
			//get config for usaepay
		    var configString = File.ReadAllText(ConfigPath);
            //get data 
            var dataString = File.ReadAllText(AddNewCreditCardPath);
            //Desserialize config interface IPaymentConfig
            var config = JsonConvert.DeserializeObject<IPaymentConfig>(configString, new PaymentConfigConverter());
            var data = JsonConvert.DeserializeObject<IAddNewCreditCardInfo>(dataString,
                new AddCustomenrPaymentDataConverter());
            //Create component instace

            using (var paymentComponent = new PaymentComponent(config))
		    {
                //Create payment data
		        paymentComponent.Data = new PaymentData
		        {
		            AddNewCreditCardInfo = data
		        };
		        //subscribe fo end of executed event
		        paymentComponent.MethodComplete += (sender, argument) =>
		        {
		            var paymentArgument = argument;
		            paymentArgument.With(x => x.Exception.Do(ex =>
		            {
                       //TODO exception logging
		            }));
                    paymentArgument.With(x => x.Result.Do(res => {
                       //TODO Result logging
		            }));
		        };
                //run methos
                paymentComponent.ExecuteStrategy(new AddCustomerPaymentMethod());
		    }
		}

		[TestCase(Description="")]
		public void KlikNPayItegrationTestCase2() { 
			
			
		}

		[TestCase]
		public void KlikNPayItegrationTestCase3()
		{


		}
	}
}

