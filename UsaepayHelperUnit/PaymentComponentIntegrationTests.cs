using System.IO;
using NUnit.Framework;
using KlikNPayUsaEPay;
using Newtonsoft.Json;
using NLog;

namespace KlikNPayPaymentUnit
{
    [TestFixture (Description="Component intagrated test case")]
	public class  PaymentComponentIntegrationTests
	{
	    private const string ConfigPath =
            @"C:\Users\DGord\Documents\github\USAePayHelperClasses\UsaepayHelperUnit\Data\IPaymentConfig.json"; 
        private const string AddNewCreditCardPath =
             @"C:\Users\DGord\Documents\github\USAePayHelperClasses\UsaepayHelperUnit\Data\AddNewCreditCard.json";
        private const string ScheduleOneTimePaymentPath =
            @"C:\Users\DGord\Documents\github\USAePayHelperClasses\UsaepayHelperUnit\Data\ScheduleOneTimePayment.json";

        /// <summary>
        /// The following form data will be saved to klik database, at the same time these data will be submitted to
        /// USAePay gateway to do credit card validation. 
        /// Please refer to the html file named “AddNewCreditCardForm.html” for all form controls.
        /// </summary>
        [Test(Description="Validate credit card")]
		public void AddCustomerPaymentDataIntegrationTestCase()
		{
            Assert.DoesNotThrow(() =>
            {
                //get config for usaepay
                var configString = File.ReadAllText(ConfigPath);
                //get data 
                var webformJsonString = File.ReadAllText(AddNewCreditCardPath);
                //Desserialize config interface IPaymentConfig
                var config = JsonConvert.DeserializeObject<IPaymentConfig>(configString, new PaymentConfigConverter());
                //Desserialize config interface ICreditCardPaymentInfo
                var data = JsonConvert.DeserializeObject<ICreditCardPaymentInfo>(webformJsonString,
                    new AddCustomenrPaymentDataConverter());
                //Create component instace
                using (var paymentComponent = new PaymentComponent(config))
                {
                    var logger = LogManager.GetCurrentClassLogger();

                    //Create payment data
                    paymentComponent.Data = new PaymentData
                    {
                        CreditCardPaymentInfo = data
                    };
                    //subscribe for end of executed event
                    paymentComponent.MethodComplete += (sender, argument) =>
                    {
                        var paymentArgument = argument;
                        logger.Trace(JsonConvert.SerializeObject(argument));
                        //error
                        paymentArgument.With(x => x.Exception.Do(e => {
                            //TODO Exception logging                      
                            logger.Trace(JsonConvert.SerializeObject(e));
                        }));
                        //ok
                        paymentArgument.With(x => x.Result.Do(res => {
                            //TODO Result logging                      
                            logger.Trace(JsonConvert.SerializeObject(res));
                        }));
                    };
                    //run component
                    paymentComponent.ExecuteStrategy(new AddCustomerPaymentMethod());
                }
            });
		}

        /// <summary>
        /// Click “continue” button, it will go to the following payment confirmation page. 
        /// After click the “submit” button, the form data will be saved to klik database,
        /// at the same time these data will be sent to USAePay gateway to make a credit card payment.
        /// Please refer to the html page named “MakeOneTimePayment.html” for all form controls. 
        /// </summary>
		[TestCase(Description= "Schedule One TimePayment")]
		public void ScheduleOneTimePaymentIntegrationTestCase() {

            Assert.DoesNotThrow(() =>
            {
                //get config for usaepay
                var configString = File.ReadAllText(ConfigPath);
                //get data 
                var webformJsonString = File.ReadAllText(ScheduleOneTimePaymentPath);
                //Desserialize config interface IPaymentConfig
                var config = JsonConvert.DeserializeObject<IPaymentConfig>(configString, new PaymentConfigConverter());
                //Desserialize config interface IScedulePaymentInfo
                var data = JsonConvert.DeserializeObject<IScedulePaymentInfo>(webformJsonString,
                    new ScedulePaymentInfoConverter());
                //Create component instace
                using (var paymentComponent = new PaymentComponent(config))
                {
                    var logger = LogManager.GetCurrentClassLogger();
                    //Create payment data
                    paymentComponent.Data = new PaymentData
                    {
                        ScedulePaymentInfo = data
                    };
                    //subscribe for end of executed event
                    paymentComponent.MethodComplete += (sender, argument) =>
                    {
                        var paymentArgument = argument;
                        logger.Trace(JsonConvert.SerializeObject(argument));
                        //error
                        paymentArgument.With(x => x.Exception.Do(e => {
                            //TODO Exception logging                      
                            logger.Trace(JsonConvert.SerializeObject(e));
                        }));
                        //ok
                        paymentArgument.With(x => x.Result.Do(res => {
                            //TODO Result logging                      
                            logger.Trace(JsonConvert.SerializeObject(res));
                        }));
                    };
                    //run component
                    paymentComponent.ExecuteStrategy(new ScheduleOneTimePayment());
                }
            });
        }

		[TestCase]
		public void BatchPaymentIntegrationTestCase()
		{


		}
	}
}

