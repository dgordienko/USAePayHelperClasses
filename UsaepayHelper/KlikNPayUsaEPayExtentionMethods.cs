using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using USAePayAPI.com.usaepay.www;

namespace KinNPayUsaEPay
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
		public static ueSecurityToken GetSecurityToken(this IKlikNPayUsaePayConfig config) {

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
			CustomerObject customer = null;
			PaymentMethod payment = null;
			data.With(x => x.NewInfo.Do(info => {
				if (info.CustomerId.HasValue)
				{		
					var id = data.NewInfo.CustomerId;
					customer = service.getCustomer(token, id.Value.ToString());
					if (customer == null)
						throw new AddCustomerPaymentMethodException("Customer not exist", 
						                                            new NullReferenceException($"{nameof(customer)} can not be null"));					
					payment = new PaymentMethod();
					payment.MethodName = info.Description;
					payment.AvsStreet = info.BillingAddressLine1;
					payment.AvsZip = info.ZipCode;
					payment.CardNumber = info.CreditCardNumber;
					payment.CardExpiration = info.ExpirationDate;
					payment.CardCode = info.CVC;
					payment.MethodType = "CreditCard";
					result = service.updateCustomerPaymentMethod(token, payment, true);
				}
				else {
					//Customer not exist in data base 
					//TODO create new customer/ need customer data model for client!
					throw new AddCustomerPaymentMethodException("Customer not existst, can not create new customer",
					                                   new NotImplementedException());
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
				var CheckNumbers = new ArrayList();
				int CardLength = cardNumber.Length;
				for (int i = CardLength - 2; i >= 0; i = i - 2)
					CheckNumbers.Add(int.Parse(cardNumber[i].ToString()) * 2);
				int CheckSum = 0;
				for (int iCount = 0; iCount <= CheckNumbers.Count - 1; iCount++)
				{
					int _count = 0;
					if ((int)CheckNumbers[iCount] > 9)
					{
						int _numLength = ((int)CheckNumbers[iCount]).ToString().Length;
						for (int x = 0; x < _numLength; x++)
						{
							_count = _count + int.Parse(
								  ((int)CheckNumbers[iCount]).ToString()[x].ToString());
						}
					}
					else
					{
						_count = (int)CheckNumbers[iCount];
					}
					CheckSum = CheckSum + _count;
				}
				int OriginalSum = 0;
				for (int y = CardLength - 1; y >= 0; y = y - 2)
				{
					OriginalSum = OriginalSum + int.Parse(cardNumber[y].ToString());
				}
				return (((OriginalSum + CheckSum) % 10) == 0);
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
