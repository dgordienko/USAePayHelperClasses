using System;
using System.Diagnostics.CodeAnalysis;
using KinNPayUsaEPay;
using NLog;
using NUnit.Framework;
using Rhino.Mocks;
using USAePayAPI.com.usaepay.www;

namespace KinNPayUsaEPayUnit
{
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

        [SetUp]
        public void Init()
        {
            Logger.Trace("Init test");
			helperConfig = MockRepository.GenerateStub<IKlikNPayUsaePayConfig>();
			algoritmData = MockRepository.GenerateStub<IKlikNPayUsaEPayData>();
            algoritm = MockRepository.GenerateMock<IKlikNPaymantStrategy<usaepayService, IKlikNPayUsaePayConfig, IKlikNPayUsaEPayData>>();
        }

		[Test(Description="Get security token test")]
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


		[Test(Description="Validate credit card number test")]
		public void ValidateCCNumberTest() {
			Assert.DoesNotThrow(() => {
				var cardNumber = 5168742352169654.ToString();
				Assert.IsTrue(cardNumber.ValidateCardNumber());
				cardNumber = string.Concat(2, cardNumber);
				Assert.IsFalse(cardNumber.ValidateCardNumber());
			});
		}

        [Test(Description = "Test CC Validation")]
		public void UpdatePaymentInfoTestCase()
        {
            Assert.DoesNotThrow(() =>
            {
                Logger.Trace("Begin Test CC Validation");
				var paymentClient = new KlikNPayUsaEPayGate(helperConfig);
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

		[Test(Description="MakeBatchPayment")]
		public void MakeBatchPayment()
		{
			Assert.DoesNotThrow(() => {
				var paymentClient = new KlikNPayUsaEPayGate(helperConfig);
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

        [Test(Description = "Create Paymant Test")]
        public void MakePayment()
        {
            Assert.DoesNotThrow(() =>
            {
				var paymentClient = new KlikNPayUsaEPayGate(helperConfig);
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


        [Test(Description = "Base test case")]
		public void BasePaymentTestCase()
        {
            Logger.Trace("Begin UsaepayHelperTestCase");
            Assert.Throws<ArgumentNullException>(() =>
            {
                Logger.Trace("Test null argument");
                var helper = new KlikNPayUsaEPayGate(null);
                Assert.That(helper == null);
            });
            Assert.DoesNotThrow(() =>
            {
                Logger.Trace("Test constructor");
                var helper = new KlikNPayUsaEPayGate(helperConfig) { Data = algoritmData };
                helper.MethodComplete += (sender, arg) =>
                {
                    Logger.Trace("Emmit executed event");
                    Assert.IsNull(arg.Exception);
                };
                helper.ExecuteStrategy(algoritm);
            });
            Logger.Trace("End UsaepayHelperTestCase");
        }


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
