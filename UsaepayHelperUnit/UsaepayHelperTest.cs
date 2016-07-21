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
        private IUsaepayHelperConfig helperConfig;
        /// <summary>
        /// The algoritm data.
        /// </summary>
        private ICCData algoritmData;
        /// <summary>
        /// The algoritm.
        /// </summary>
        private IPaymantGatevateActionStrategy<usaepayService, IUsaepayHelperConfig, ICCData> algoritm;

        private const string testMD5String = "This is test srting dataThis is another test string data";

        /// <summary>
        /// The config.
        /// </summary>
        private IUsaepayHelperConfig _config;

        [SetUp]
        public void Init()
        {
            Logger.Trace("Init test");
            helperConfig = MockRepository.GenerateMock<IUsaepayHelperConfig>();
            algoritmData = MockRepository.GenerateMock<ICCData>();
            _config = MockRepository.GenerateMock<IUsaepayHelperConfig>();
            algoritm = MockRepository.GenerateMock<IPaymantGatevateActionStrategy<usaepayService, IUsaepayHelperConfig, ICCData>>();
        }

        [Test(Description = "Test CC Validation")]
        public void ValidateCCData()
        {
            Assert.DoesNotThrow(() =>
            {
                Logger.Trace("Begin Test CC Validation");
                var paymentClient = new KikNPayUsaEPayGate(_config);
                paymentClient.MethodComplete += (sender, arg) =>
                {
                    Assert.IsNull(arg.Exception);
                    Assert.IsNotNull(arg.Result);
                    
                    arg.With(x => x.Result.Do(res =>
                    {
                        Assert.IsInstanceOf<PaymentArgument>(res);
                        Assert.IsNull(((PaymentArgument)res).Exception);
                        Assert.IsNotNull(((PaymentArgument)res).Result);
                    }));

                    Logger.Trace("Sucsess Test CC Validation");
                };
                paymentClient.Data = algoritmData;
                paymentClient.ExecuteStrategy(new ValidateCCData());
            });
        }

		[Test(Description="")]
		public void MakeBatchPayment()
		{
			Assert.DoesNotThrow(() => {
				var paymentClient = new KikNPayUsaEPayGate(_config);
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
                var paymentClient = new KikNPayUsaEPayGate(_config);
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
                paymentClient.ExecuteStrategy(new MakePayment());
            });
        }


        [Test(Description = "Base test case")]
		public void BasePaymentTestCase()
        {
            Logger.Trace("Begin UsaepayHelperTestCase");
            Assert.Throws<ArgumentNullException>(() =>
            {
                Logger.Trace("Test null argument");
                var helper = new KikNPayUsaEPayGate(null);
                Assert.That(helper == null);
            });
            Assert.DoesNotThrow(() =>
            {
                Logger.Trace("Test constructor");
                var helper = new KikNPayUsaEPayGate(helperConfig) { Data = algoritmData };
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
