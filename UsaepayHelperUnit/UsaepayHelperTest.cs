using System;
using System.Diagnostics.CodeAnalysis;
using KlikNPayUsaEPay;
using NLog;
using NUnit.Framework;
using Rhino.Mocks;
using USAePayAPI.com.usaepay.www;

namespace KinNPayUsaEPayUnit
{
	/// <summary>
	/// Usaepay helper test.
	/// </summary>
	[TestFixture(Description = "UsaepayHelper Unit testes")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class UsaepayHelperTest
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// The helper config.
        /// </summary>
        private IKlikNPayUsaePayConfig helperConfig;
        /// <summary>
        /// The algoritm data.
        /// </summary>
        private IKlikNPayUsaEPayData algoritmData;
        /// <summary>
        /// The algoritm.
        /// </summary>
        private IKlikNPaymantStrategy<usaepayService, IKlikNPayUsaePayConfig, IKlikNPayUsaEPayData> algoritm;

        private const string testMD5String = "This is test srting dataThis is another test string data";

        
		/// <summary>
		/// Init this instance.
		/// </summary>
		[SetUp]
        public void Init()
        {
            Logger.Trace("Init test");
			helperConfig = MockRepository.GenerateStub<IKlikNPayUsaePayConfig>();
			algoritmData = MockRepository.GenerateStub<IKlikNPayUsaEPayData>();
            algoritm = MockRepository.GenerateMock<IKlikNPaymantStrategy<usaepayService, IKlikNPayUsaePayConfig, IKlikNPayUsaEPayData>>();
        }


		/// <summary>
		/// Gets the security token test.
		/// </summary>
		/// <returns>The security token test.</returns>
		[Test(Description = "Get security token test")]
		public void GetSecurityTokenTest() {
			//Set configutation fields values
			helperConfig.SourceKey = "k9cQPvfYyHaknG11Aa90Ny0YOhv56H4R";
			helperConfig.Pin = "2207";

			Assert.DoesNotThrow(() => {
				var token = helperConfig.GetSecurityToken();
				Assert.IsNotNull(token);
				Assert.IsInstanceOf<ueSecurityToken>(token);
			});
		}



		/// <summary>
		/// Validates the CCN umber test.
		/// </summary>
		/// <returns>The CCN umber test.</returns>
		[Test(Description = "Validate credit card number test")]
		public void ValidateCCNumberTest() {
			Assert.DoesNotThrow(() => {
				var cardNumber = 5168742352169654.ToString();
				Assert.IsTrue(cardNumber.ValidateCardNumber());
				cardNumber = string.Concat(2, cardNumber);
				Assert.IsFalse(cardNumber.ValidateCardNumber());
			});
		}

        
		/// <summary>
		/// Updates the payment info test case.
		/// </summary>
		/// <returns>The payment info test case.</returns>
		[Test(Description = "Update payment info")]
		public void UpdatePaymentInfoTestCase()
        {
            Assert.DoesNotThrow(() =>
            {
                Logger.Trace("Begin Test CC Validation");
				var paymentClient = new KlikNPayUsaEPayAdapter(helperConfig);
                paymentClient.MethodComplete += (sender, arg) =>
                {
                    Assert.IsNull(arg.Exception);
                    Assert.IsNotNull(arg.Result);                    
                    arg.With(x => x.Result.Do(res =>
                    {
                        Assert.IsInstanceOf<PaymentArgument>(res);
                        Assert.IsNull(((PaymentArgument)res).Exception);
                        Assert.IsNotNull(((PaymentArgument)res).Result);
						var result = (bool)((PaymentArgument)arg.Result).Result;
						Assert.IsTrue(result);
                    }));
                    Logger.Trace("Sucsess Test CC Validation");
                };
                paymentClient.Data = algoritmData;
                paymentClient.ExecuteStrategy(new AddCustomerPaymentMethod());
            });
        }

		/// <summary>
		/// Makes the batch payment.
		/// </summary>
		/// <returns>The batch payment.</returns>
		[Test(Description="Make Batch Payment")]
		public void MakeBatchPayment()
		{
			Assert.DoesNotThrow(() => {
				var paymentClient = new KlikNPayUsaEPayAdapter(helperConfig);
				Logger.Trace("Begin Test MakeBatchPayment");
				paymentClient.MethodComplete += (sender, arg) =>
				{
					Assert.IsNull(arg.Exception);
					Assert.IsNotNull(arg.Result);
					Assert.IsInstanceOf<PaymentArgument>(arg.Result);
					Assert.IsNull(((PaymentArgument)arg.Result).Exception);
					Assert.IsNotNull(((PaymentArgument)arg.Result).Result);
					Logger.Trace("Sucsess Test MakeBatchPayment");
				};
				paymentClient.Data = algoritmData;
				paymentClient.ExecuteStrategy(new MakeBatchPayment());				
			});
		}

		/// <summary>
		/// Makes the payment.
		/// </summary>
		/// <returns>The payment.</returns>
        [Test(Description = "Create Paymant Test")]
        public void MakePayment()
        {
            Assert.DoesNotThrow(() =>
            {
				var paymentClient = new KlikNPayUsaEPayAdapter(helperConfig);
                Logger.Trace("Begin Test Paymant ");
                paymentClient.MethodComplete += (sender, arg) =>
                {
                    Assert.IsNull(arg.Exception);
                    Assert.IsNotNull(arg.Result);
                    Assert.IsInstanceOf<PaymentArgument>(arg.Result);
                    Assert.IsNull(((PaymentArgument)arg.Result).Exception);
                    Assert.IsNotNull(((PaymentArgument)arg.Result).Result);
                    Logger.Trace("Sucsess Test Paymant");
                };
                paymentClient.Data = algoritmData;
                paymentClient.ExecuteStrategy(new ScheduleOneTimePayment());
            });
        }

		/// <summary>
		/// Bases the payment test case.
		/// </summary>
		/// <returns>The payment test case.</returns>
        [Test(Description = "Base test case")]
		public void BasePaymentTestCase()
        {
            Logger.Trace("Begin UsaepayHelperTestCase");
            Assert.Throws<ArgumentNullException>(() =>
            {
                Logger.Trace("Test null argument");
                var helper = new KlikNPayUsaEPayAdapter(null);
                Assert.That(helper == null);
            });
            Assert.DoesNotThrow(() =>
            {
                Logger.Trace("Test constructor");
                var helper = new KlikNPayUsaEPayAdapter(helperConfig) { Data = algoritmData };
                helper.MethodComplete += (sender, arg) =>
                {
                    Logger.Trace("Emmit executed event");
                    Assert.IsNull(arg.Exception);
                };
                helper.ExecuteStrategy(algoritm);
            });
            Logger.Trace("End UsaepayHelperTestCase");
        }

		/// <summary>
		/// Creates the MD 5 test case.
		/// </summary>
		/// <returns>The MD 5 test case.</returns>
        [Test(Description = "Get MD5 string testcase")]
        public void CreateMD5TestCase()
        {
            Logger.Trace("Test MD5");
            const string s = "This is test srting data";
            const string s0 = "This is another test string data";
            var ss = string.Concat(s, s0);
            var md5 = ss.GenerateHash();
            Assert.That(!string.IsNullOrWhiteSpace(md5));
            Assert.That(testMD5String.GenerateHash() == md5);
        }

    }
}
