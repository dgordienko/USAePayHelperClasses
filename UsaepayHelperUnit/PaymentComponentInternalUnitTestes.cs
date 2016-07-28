using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using KlikNPayUsaEPay;
using Newtonsoft.Json;
using NLog;
using NUnit.Framework;
using Rhino.Mocks;
using USAePayAPI.com.usaepay.www;
using USAePayAPI;

namespace KlikNPayPaymentUnit
{
	/// <summary>
	/// Internal unit testes
	/// </summary>
	[TestFixture(Description = "Unit testes")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class PaymentComponentInternalUnitTestes
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// The helper config.
        /// </summary>
        private IPaymentConfig helperConfig;
        /// <summary>
        /// The algoritm data.
        /// </summary>
        private IPaymentData algoritmData;
        /// <summary>
        /// The algoritm.
        /// </summary>
        private IPaymentStrategy<USAePay, IPaymentConfig, IPaymentData> algoritm;

        private const string testMD5String = "This is test srting dataThis is another test string data";

        
		/// <summary>
		/// Init this instance.
		/// </summary>
		[SetUp]
        public void Init()
        {
            Logger.Trace("Init test");
			helperConfig = MockRepository.GenerateStub<IPaymentConfig>();
			algoritmData = MockRepository.GenerateStub<IPaymentData>();
            algoritm = MockRepository.GenerateMock<IPaymentStrategy<USAePay, IPaymentConfig, IPaymentData>>();
        }

		/// <summary>
		/// Gets the security token test.
		/// </summary>
		/// <returns>The security token test.</returns>
		[Test(Description = "Get security token test")]
		public void GetSecurityTokenTest() {
			
			//Set configutation fields values
			helperConfig.SourceKey = "_72BI97rs34iw29035L89r98Xe143lyz";
			helperConfig.Pin = "2207";

			Assert.DoesNotThrow(() => {
				var token = helperConfig.GetSecurityToken();
				Assert.IsNotNull(token);
                Logger.Trace(JsonConvert.SerializeObject(token));
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
				var cardNumber = 4000100111112223.ToString();
				Assert.IsTrue(cardNumber.ValidateCardNumber());
				cardNumber = string.Concat(2, cardNumber);
				Assert.IsFalse(cardNumber.ValidateCardNumber());
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
                var helper = new PaymentComponent(null);
                Assert.That(helper == null);
            });
            Assert.DoesNotThrow(() =>
            {
                Logger.Trace("Test constructor");
                var helper = new PaymentComponent(helperConfig) { Data = algoritmData };
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

         /// <summary>
         ///  from any json to csv data
         /// </summary>
         [Test(Description = "Test butch file data")]
	    public void CreateCsvTest()
         {
            Assert.DoesNotThrow(() =>
            {
                var json = JsonConvert.SerializeObject(helperConfig);
                var s = json.ToObjectCSV();
                Assert.IsNotEmpty(s);


            });
         }
    }
}
