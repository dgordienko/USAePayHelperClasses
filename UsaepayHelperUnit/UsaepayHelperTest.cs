using NUnit.Framework;
using Rhino.Mocks;
using NLog;
using UsaepayHelper;
using System;
using UsaepayHelper.www.usaepay.com;

namespace UsaepayHelperUnit
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
		private IUsaepayStrategy<usaepayService, ICCData> algoritm;

		private static readonly string testMD5String = "This is test srting dataThis is another test string data";

		[SetUp]
		public void Init()
		{
			Logger.Trace("Init test");
			helperConfig = MockRepository.GenerateMock<IUsaepayHelperConfig>();
			algoritmData = MockRepository.GenerateMock<ICCData>();
			//algoritm = MockRepository.GenerateMock<IUsaepayStrategy<usaepayService, ICCData>>();
		}

		[Test(Description="Create helper class test")]
		public void UsaepayHelperTestCase()
		{
			Logger.Trace("Begin UsaepayHelperTestCase");
			Assert.Throws<ArgumentNullException>(() =>
			{
				Logger.Trace("Test null argument");
				var helper = new PaymentController(null);
				Assert.That(helper == null);
			});

			Assert.DoesNotThrow(() => {
				Logger.Trace("Test constructor");
				var helper = new PaymentController(helperConfig);
				helper.MethodComplete += (sender, arg) => {
					Logger.Trace("Emmit executed event");
					Assert.IsNull(arg.Exception);
					Assert.IsNull(arg.Result);
				};
				//helper.ExecuteStrategy(algoritm);

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
