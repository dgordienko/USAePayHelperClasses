using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using USAePayAPI.com.usaepay.www;

namespace KlikNPayUsaEPay
{
	/// <summary>
	/// Usaepay helper extention methods.
	/// </summary>
	public static class KlikNPayUsaEPayExtentionMethods 
	{
		/// <summary>
		/// Gets the security token.
		/// </summary>
		/// <returns>The security token.</returns>
		/// <param name="config">Config.</param>
		public static ueSecurityToken  GetSecurityToken(this IKlikNPayUsaEPayConfig config) {

			if (config == null)
				throw new ArgumentNullException(nameof(config));			
			var result = new ueSecurityToken();
			result.SourceKey = config.SourceKey;
			var pin = config.Pin;
			var hash = new ueHash();
			hash.Type = "md5";
			hash.Seed = Guid.NewGuid().ToString();
			var pvalue = string.Concat(result.SourceKey, hash.Seed, pin);
			hash.HashValue = pvalue.GenerateHash();
			result.PinHash = hash;
			return result;
		}

		/// <summary>
		/// Updates the credit card data.
		/// </summary>
		/// <returns>The credit card data.</returns>
		/// <param name="service">Service.</param>
		/// <param name="token">Token.</param>
		/// <param name="data">Data.</param>
		public static bool CreatePaymentMethod(usaepayService service,ueSecurityToken token,IKlikNPayUsaEPayData data) {

			if (service == null)
				throw new ArgumentNullException(nameof(service));
			if (token == null)
				throw new ArgumentNullException(nameof(token));
			if (data == null)
				throw new ArgumentNullException(nameof(data));
			var result = false;
			CustomerObject customer;
			PaymentMethod payment;
			data.With(x => x.NewInfo.Do(info => {
				if (info.CustomerId.HasValue)
				{		
					var id = data.NewInfo.CustomerId;
					customer = service.getCustomer(token, id.Value.ToString());
					if (customer == null)
						throw new AddCustomerPaymentMethodException("Customer not exist", 
							new NullReferenceException($"{nameof(customer)} can not be null"));
				    payment = new PaymentMethod
				    {
				        MethodName = info.Description,
				        AvsStreet = info.BillingAddressLine1,
				        AvsZip = info.ZipCode,
				        CardNumber = info.CreditCardNumber,
				        CardExpiration = info.ExpirationDate,
				        CardCode = info.CVC,
				        MethodType = "CreditCard"
				    };
				    result = service.updateCustomerPaymentMethod(token, payment, true);
				}
				else {
					//Customer not exist in data base 
					//TODO create new customer/ need customer data model for client!
					throw new AddCustomerPaymentMethodException("Customer not existst, can not create new customer",new NotImplementedException());
				}
			}));
			return result;
		}

		/// <summary>
		/// Validates the credit card number.
		/// </summary>
		/// <returns>The card number.</returns>
		/// <param name="cardNumber">Card number.</param>
		public static bool ValidateCardNumber(this string cardNumber)
		{
			try
			{
				var checkNumbers = new ArrayList();
				var cardLength = cardNumber.Length;
				for (var i = cardLength - 2; i >= 0; i = i - 2)
					checkNumbers.Add(int.Parse(cardNumber[i].ToString()) * 2);
				var checkSum = 0;
				for (var iCount = 0; iCount <= checkNumbers.Count - 1; iCount++)
				{
					var count = 0;
					if ((int)checkNumbers[iCount] > 9)
					{
						var numLength = ((int)checkNumbers[iCount]).ToString().Length;
						for (var x = 0; x < numLength; x++)
						{
							count = count + int.Parse(
								  ((int)checkNumbers[iCount]).ToString()[x].ToString());
						}
					}
					else
					{
						count = (int)checkNumbers[iCount];
					}
					checkSum = checkSum + count;
				}
				var originalSum = 0;
				for (var y = cardLength - 1; y >= 0; y = y - 2)
				{
					originalSum = originalSum + int.Parse(cardNumber[y].ToString());
				}
				return (originalSum + checkSum) % 10 == 0;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Generates the hash.
		/// http://wiki.usaepay.com/developer/soap-1.4/howto/csharp
		/// </summary>
		/// <returns>The hash.</returns>
		/// <param name="input">Input.</param>
		public static string GenerateHash(this string input)
		{
			if (string.IsNullOrWhiteSpace(input))
				throw new ArgumentNullException(nameof(input));
			var md5Hasher = MD5.Create();
			var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
			var sBuilder = new StringBuilder();
			foreach (var t in data)
			{
			    sBuilder.Append(t.ToString("x2"));
			}
		    return sBuilder.ToString();
		}
	}
}
