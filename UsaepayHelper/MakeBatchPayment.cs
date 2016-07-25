using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using USAePayAPI.com.usaepay.www;

namespace KlikNPayUsaEPay
{

	

	/// <summary>
	/// Send batch file to USAePay for processing
	/// return success code
	/// provide list of all codes and descriptions
	/// </summary>
	public class MakeBatchPayment : IKlikNPaymentStrategy<usaepayService, IKlikNPayUsaEPayConfig, IKlikNPayUsaEPayData>
	{
		/// <summary>
		/// Method the specified context, config and data.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="config">Config.</param>
		/// <param name="data">Data.</param>
		public object Method(usaepayService context, IKlikNPayUsaEPayConfig config, IKlikNPayUsaEPayData data)
		{
			if (context == null)
				throw new MakeBanchPaymentException($"MakeBatchPayment {nameof(context)} is null",
				                                    new ArgumentNullException(nameof(context)));
			if (config == null)
			throw new MakeBanchPaymentException($"MakeBatchPayment {nameof(config)} is null",
				                                new ArgumentNullException(nameof(config)));
			if(data == null)
				throw new MakeBanchPaymentException($"MakeBatchPayment {nameof(data)} is null",
				                                    new ArgumentNullException(nameof(context)));
			var result = new PaymentArgument();
			try
			{
				data.With(x => x.BatchUploadRecords.Do(records =>
				{
					var r = records.ToList();
					var json = JsonConvert.SerializeObject(r);
					var csv = json.ToArrayCSV();
					var token = config.GetSecurityToken();
					var res = context.createBatchUpload(token, Guid.NewGuid().ToString(), true, "csv", "base64",
					                                    ButchFields.GetButchFields(), Convert.ToBase64String(Encoding.Default.GetBytes(csv)), false);
					result.Result = res;
				}));
			}
			catch (Exception ex)
			{
				result.Exception = ex;
			}

			return result;
		}

		private readonly string[] butchFields;

		public MakeBatchPayment() {
			butchFields = new string[42];

		}
	}
}