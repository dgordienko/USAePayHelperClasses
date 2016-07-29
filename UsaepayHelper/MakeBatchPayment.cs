using System;
using System.Linq;
using System.Text;
using USAePayAPI;
using System.IO;

namespace KlikNPayUsaEPay
{
    /// <summary>
    /// Send batch file to USAePay for processing
    /// return success code
    /// provide list of all codes and descriptions
    /// </summary>
    public class MakeBatchPayment : IPaymentStrategy<USAePay, IPaymentConfig, IPaymentData>
	{
		/// <summary>
		/// Method the specified context, config and data.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="config">Config.</param>
		/// <param name="data">Data.</param>
		public object Method(USAePay context, IPaymentConfig config, IPaymentData data)
		{
			if (context == null)
				throw new MakeBatchPaymentException("context is null",new ArgumentNullException("context"));
			if (config == null)
			throw new MakeBatchPaymentException("MakeBatchPayment config is null",
				                                new ArgumentNullException("config"));
			if(data == null)
				throw new MakeBatchPaymentException("MakeBatchPayment data is null",
				                                    new ArgumentNullException("data"));
			var result = new PaymentArgument();
            string statusString;
            var client = new com.usaepay.usaepayService();
            client.Url = config.SoapServerUrl;
            try
			{
                data.With(x => x.MakeBatchPaymentInfo.Do(info =>
                {
                    var path = info.PathToFile;
                    var csvLine = File.ReadAllLines(path);
                    if (csvLine.Any())
                    {                        
                        var fields =  csvLine[0].Split(',') ;                        
                        var name = Guid.NewGuid().ToString();
                        var token = config.GetSecurityToken();
                        var content = File.ReadAllText(path);
                        var status = client.createBatchUpload(token,name,true, "csv", "base64", fields,
                            Convert.ToBase64String(Encoding.Default.GetBytes(content)),true);
                        statusString = (string.Concat(name," #", status.UploadRefNum, " trans:", status.Remaining));
                        result.Result = statusString;
                    }
                    else
                    {
                        throw new MakeBatchPaymentException("this is not csv file data");
                    }
                }));
			}
			catch (Exception ex)
			{
                result.Result = null;
				result.Exception = new MakeBatchPaymentException(ex.Message,ex);
			}
			return result;
		}
	}
}