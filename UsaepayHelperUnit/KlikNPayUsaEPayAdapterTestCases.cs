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
	/// Usaepay helper test.
	/// </summary>
	[TestFixture(Description = "UsaepayHelper Unit testes")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class KlikNPayUsaEPayAdapterTestCases
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
				var cardNumber = 5168742352169654.ToString();
				Assert.IsTrue(cardNumber.ValidateCardNumber());
				cardNumber = string.Concat(2, cardNumber);
				Assert.IsFalse(cardNumber.ValidateCardNumber());
			});
		}

		/// <summary>
		/// Serialize Deserialize IUsaEPayFieldsTest 
		/// </summary>
		/// <returns>The desserialise test.</returns>
		[Test(Description="")]
		public void SerializeDeserializeIUsaEPayFieldsTest() {
			//Fake data!!!
			var i = 100;
			var batchRecord = new BatchUploadRecord()
			{
				command = "sale",
				source = i.ToString(),
				invoice = i.ToString(),
				cardholder = i.ToString(),
				ccnum = i.ToString(),
				ccexp = i.ToString(),
				avsstreet = i.ToString(),
				avszip = i.ToString(),
				cvc = i.ToString(),
				amount = i.ToString(),
				tax = i.ToString(),
				description = i.ToString(),
				ponum = i.ToString(),
				orderid = i.ToString(),
				custid = i.ToString(),
				billing_company = i.ToString(),
				billing_fname = i.ToString(),
				billing_lname = i.ToString(),
				billing_street = i.ToString(),
				billing_street2 = i.ToString(),
				billing_city = i.ToString(),
				billing_state = i.ToString(),
				billing_country = i.ToString(),
				billing_zip = i.ToString(),
				billing_phone = i.ToString(),
				shipping_company = i.ToString(),
				shipping_fname = i.ToString(),
				shipping_lname = i.ToString(),
				shipping_street = i.ToString(),
				shipping_street2 = i.ToString(),
				shipping_city = i.ToString(),
				shipping_state = i.ToString(),
				shipping_zip = i.ToString(),
				shipping_country = i.ToString(),
				shipping_phone = i.ToString(),
				email = i.ToString(),
				checknum = i.ToString(),
				vcrouting = i.ToString(),
				vcaccount = i.ToString(),
				vcssn = i.ToString(),
				vcdl = i.ToString(),
				vcdlstate = i.ToString()
			};
			var js = JsonConvert.SerializeObject(batchRecord);
			Assert.IsNotEmpty(js);
			var csv = js.ToObjectCSV();
			Assert.IsNotEmpty(csv);
			var paymentObject = JsonConvert.DeserializeObject<IUsaEPayFields>(js,new UsaEPayFieldsConfigConverter());
			Assert.IsInstanceOf<IUsaEPayFields>(paymentObject);
			var paymentList = new List<IUsaEPayFields>();
			paymentList.Add(paymentObject);
			var jsList = JsonConvert.SerializeObject(paymentList);
			var dpaymentList = JsonConvert.DeserializeObject<List<IUsaEPayFields>>(jsList, new UsaEPayFieldsConfigConverter());
			Assert.IsInstanceOf<IEnumerable<IUsaEPayFields>>(dpaymentList);
			var listCVSData = jsList.ToArrayCSV();
			Assert.IsNotEmpty(listCVSData);
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

		/// <summary>
		/// Creates the upladed data.
		/// </summary>
		/// <returns>The upladed data.</returns>
		[Test(Description="http://wiki.usaepay.com/developer/soap-1.4/methods/createbatchupload")]
		public void CreateUpladedData() {
			Assert.DoesNotThrow(() => {
				var fileContent = new BatchUploadFile();
				var bS = new List<string>();
				for (int i = 0; i < 100; i++)
				{
					var batchRecord = new BatchUploadRecord()
					{
						command = "sale",
						source = i.ToString(),
						invoice = i.ToString(),
						cardholder = i.ToString(),
						ccnum = i.ToString(),
						ccexp = i.ToString(),
						avsstreet = i.ToString(),
						avszip = i.ToString(),
						cvc = i.ToString(),
						amount = i.ToString(),
						tax = i.ToString(),
						description = i.ToString(),
						ponum = i.ToString(),
						orderid = i.ToString(),
						custid = i.ToString(),
						billing_company = i.ToString(),
						billing_fname = i.ToString(),
						billing_lname = i.ToString(),
						billing_street = i.ToString(),
						billing_street2 = i.ToString(),
						billing_city = i.ToString(),
						billing_state = i.ToString(),
						billing_country = i.ToString(),
						billing_zip = i.ToString(),
						billing_phone = i.ToString(),
						shipping_company = i.ToString(),
						shipping_fname = i.ToString(),
						shipping_lname = i.ToString(),
						shipping_street = i.ToString(),
						shipping_street2 = i.ToString(),
						shipping_city = i.ToString(),
						shipping_state = i.ToString(),
						shipping_zip = i.ToString(),
						shipping_country = i.ToString(),
						shipping_phone = i.ToString(),
						email = i.ToString(),
						checknum = i.ToString(),
						vcrouting = i.ToString(),
						vcaccount = i.ToString(),
						vcssn = i.ToString(),
						vcdl = i.ToString(),
						vcdlstate = i.ToString()
					};
					var js = JsonConvert.SerializeObject(batchRecord);
					bS.Add(js);                  
					fileContent.Add(batchRecord);
				}
				var json = JsonConvert.SerializeObject(fileContent);
				var p = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				var path = Path.Combine(p,string.Format("{0}.json", DateTime.Now.ToOADate()));

				File.WriteAllText(path, json);
				var fsCSV = json.ToArrayCSV();
				Assert.IsNotEmpty(fsCSV);
			});
		}
    }
}
