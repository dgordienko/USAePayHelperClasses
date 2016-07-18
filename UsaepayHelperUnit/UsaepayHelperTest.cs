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
		private IUsaepayHelperData algoritmData;
		/// <summary>
		/// The algoritm.
		/// </summary>
		private IUsaepayStrategy<usaepayService, IUsaepayHelperData> algoritm;

		[SetUp]
		public void Init() 
		{
			Logger.Trace("Init test");
			helperConfig = MockRepository.GenerateMock<IUsaepayHelperConfig>();
			algoritmData = MockRepository.GenerateMock<IUsaepayHelperData>();
			algoritm = MockRepository.GenerateMock<IUsaepayStrategy<usaepayService, IUsaepayHelperData>>();
		}

		[Test(Description="Create helper class test")]
		public void UsaepayHelperTestCase()
		{
			Logger.Trace("Begin UsaepayHelperTestCase");
			Assert.Throws<ArgumentNullException>(() =>
			{
				Logger.Trace("Test null argument");
				var helper = new UsaepayHelperClass(null);
				Assert.That(helper == null);
			});

			Assert.DoesNotThrow(() => {
				Logger.Trace("Test constructor");
				var helper = new UsaepayHelperClass(helperConfig);
				helper.MethodComplete += (sender, arg) => {
					Logger.Trace("Emmit executed event");
					Assert.IsNull(arg.Exception);
					Assert.IsNull(arg.Result);
				};
				helper.ExecuteStrategy(algoritm);

			});
			Logger.Trace("End UsaepayHelperTestCase");
		}
	}
}

