using NUnit.Framework;
using Rhino.Mocks;
using NLog;
using KikNPay;
using System;
using KikNPay.www.usaepay.com;

namespace KikNPayUsaEPayTestCases
{
	[TestFixture(Description="UsaepayHelper Unit testes")]
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
		private IUsaepayStrategy<usaepayService,IUsaepayHelperConfig, ICCData> algoritm;

		private static readonly string testMD5String = "This is test srting dataThis is another test string data";
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
			algoritm = MockRepository.GenerateMock<IUsaepayStrategy<usaepayService, IUsaepayHelperConfig, ICCData>>();
		}

		[Test(Description="Test CC Validation")]
		public void ValidateCCData() {
			Assert.DoesNotThrow(() => {
				Logger.Trace("Begin Test CC Validation");
				var paymentClient = new KikNPayUsaEPay(_config);
				paymentClient.MethodComplete += (sender, arg) => {
					Assert.IsNull(arg.Exception);
					Assert.IsNotNull(arg.Result);
					Assert.IsInstanceOf<PaymentControllerEventArgs>(arg.Result);
					Assert.IsNull(((PaymentControllerEventArgs)arg.Result).Exception);
					Assert.IsNotNull(((PaymentControllerEventArgs)arg.Result).Result);
					Logger.Trace("Sucsess Test CC Validation");
				};
				paymentClient.Data = algoritmData;
				paymentClient.ExecuteStrategy(new ValidateCCData());
			});
		}

		[Test(Description="Create Paymant Test")]
		public void MakePayment() {
			Assert.DoesNotThrow(() =>
			{
				var paymentClient = new KikNPayUsaEPay(_config);
				Logger.Trace("Begin Test Paymant Test");
				paymentClient.MethodComplete += (sender, arg) =>
				{
					Assert.IsNull(arg.Exception);
					Assert.IsNotNull(arg.Result);
					Assert.IsInstanceOf<PaymentControllerEventArgs>(arg.Result);
					Assert.IsNull(((PaymentControllerEventArgs)arg.Result).Exception);
					Assert.IsNotNull(((PaymentControllerEventArgs)arg.Result).Result);
					Logger.Trace("Sucsess Test Paymant Test");
				};
				paymentClient.Data = algoritmData;
				paymentClient.ExecuteStrategy(new MakePayment());
			});
		}	


		[Test(Description="Create helper class test")]
		public void UsaepayHelperTestCase()
		{
			Logger.Trace("Begin UsaepayHelperTestCase");
			Assert.Throws<ArgumentNullException>(() =>
			{
				Logger.Trace("Test null argument");
				var helper = new KikNPayUsaEPay(null);
				Assert.That(helper == null);
			});

			Assert.DoesNotThrow(() => {
				Logger.Trace("Test constructor");
				var helper = new KikNPayUsaEPay(helperConfig);
				helper.Data = algoritmData;
				helper.MethodComplete += (sender, arg) => {
					Logger.Trace("Emmit executed event");
					Assert.IsNull(arg.Exception);
					Assert.IsNull(arg.Result);
					//Assert.IsInstanceOf<PaymentControllerEventArgs>(arg.Result);
					//Assert.IsNull(((PaymentControllerEventArgs)arg.Result).Exception);
					//Assert.IsNotNull(((PaymentControllerEventArgs)arg.Result).Result);
				};
				helper.ExecuteStrategy(algoritm);
			});
			Logger.Trace("End UsaepayHelperTestCase");
		}


		[Test(Description="Get MD5 string testcase")]
		public void CreateMD5TestCase()
		{
			Logger.Trace("Test MD5");
			var s = "This is test srting data";
			var s0 = "This is another test string data";
			var ss = string.Concat(s, s0);
			var md5 = ss.GenerateHash();
			Assert.That(!string.IsNullOrWhiteSpace(md5));
			Assert.That(testMD5String.GenerateHash() == md5);
		}
	}
}
