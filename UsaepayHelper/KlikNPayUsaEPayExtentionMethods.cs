using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using USAePayAPI.com.usaepay.www;

namespace KlikNPayUsaEPay
{

	/// <summary>
	/// Usaepay helper extention methods.
	/// </summary>
	public static class KlikNPayUsaEPayExtentionMethods 
	{
		/// <summary>
		/// Searchs the payment item.
		/// </summary>
		/// <returns>The payment item.</returns>
		public static bool SearchPaymentItem(string invoice,usaepayService context,ueSecurityToken token) {
			if (string.IsNullOrWhiteSpace(invoice))
				throw new ArgumentNullException("invoice");
			if (context == null)
				throw new ArgumentNullException("context");
 			SearchParam[] search = new SearchParam[1];
			search[0] = new SearchParam();
			search[0].Field = "Invoice";
			search[0].Type = "Contains";
			search[0].Value = invoice;
			var result = new BatchSearchResult();
			result = context.searchBatches(token, search, true, "0", "10", "closed");
			var q = result.Batches.Any();
			return q;
		}


		/// <summary>
		///   Create csv file content from json string 
		/// </summary>
		/// <param name="json">json string object </param>
		/// <returns></returns>
		public static string ToArrayCSV(this string json)
		{
			if (string.IsNullOrWhiteSpace(json))
				throw new ArgumentNullException("json");
			
			var arrayJ = JArray.Parse(json);
			string result = string.Empty;
			foreach (var item in arrayJ)
			{
				var element = (JObject)item;
				var values = element.DescendantsAndSelf()
					.OfType<JProperty>()
					.Where(p => p.Value is JValue)
					.GroupBy(p => p.Name)
					.ToList();
				var columns = values.Select(g => g.Key).ToArray();
				// Filter JObjects that have child objects that have values.
				var parentsWithChildren = values.SelectMany(g => g).SelectMany(v => v.AncestorsAndSelf()
					.OfType<JObject>().Skip(1)).ToHashSet();
				// Collect all data rows: for every object, go through the column titles and get the value of that property in the closest ancestor or self that has a value of that name.
				var rows = element
					.DescendantsAndSelf()
					.OfType<JObject>()
					.Where(o => o.PropertyValues().OfType<JValue>().Any())
					.Where(o => o == element || !parentsWithChildren.Contains(o)) // Show a row for the root object + objects that have no children.
					.Select(o => columns.Select(c => o.AncestorsAndSelf()
						.OfType<JObject>()
						.Select(parent => parent[c])
						.Where(v => v is JValue)
						.Select(v => (string)v)
						.FirstOrDefault())
						.Reverse() // Trim trailing nulls
						.SkipWhile(s => s == null)
						.Reverse());
				// Convert to CSV
				var csvRows = new[] { columns }.Concat(rows).Select(r => string.Join(",", r));
				result += string.Join("\n", csvRows);
			}
			return result;
		}

	     /// <summary>
        ///   Create csv file from json string 
        /// </summary>
        /// <param name="json">json string object </param>
        /// <returns></returns>
        public static string ToObjectCSV(this string json)
        {
			if (string.IsNullOrWhiteSpace(json))
				throw new ArgumentNullException("json");
			
            var obj = JObject.Parse(json);
            // Collect column titles: all property names whose values are of type JValue, distinct, in order of encountering them.
            var values = obj.DescendantsAndSelf()
                .OfType<JProperty>()
                .Where(p => p.Value is JValue)
                .GroupBy(p => p.Name)
                .ToList();
            var columns = values.Select(g => g.Key).ToArray();
            // Filter JObjects that have child objects that have values.
            var parentsWithChildren = values.SelectMany(g => g).SelectMany(v => v.AncestorsAndSelf()
                .OfType<JObject>().Skip(1)).ToHashSet();
            // Collect all data rows: for every object, go through the column titles and get the value of that property in the closest ancestor or self that has a value of that name.
            var rows = obj
                .DescendantsAndSelf()
                .OfType<JObject>()
                .Where(o => o.PropertyValues().OfType<JValue>().Any())
                .Where(o => o == obj || !parentsWithChildren.Contains(o)) // Show a row for the root object + objects that have no children.
                .Select(o => columns.Select(c => o.AncestorsAndSelf()
                    .OfType<JObject>()
                    .Select(parent => parent[c])
                    .Where(v => v is JValue)
                    .Select(v => (string)v)
                    .FirstOrDefault())
                    .Reverse() // Trim trailing nulls
                    .SkipWhile(s => s == null)
                    .Reverse());

            // Convert to CSV
            var csvRows = new[] { columns }.Concat(rows).Select(r => string.Join(",", r));
            var result = string.Join("\n", csvRows);
            return result;
        }


        /// <summary>
        ///  http://stackoverflow.com/questions/3471899/how-to-convert-linq-results-to-hashset-or-hashedset
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        private static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }

        /// <summary>
        /// Gets the security token.
        /// </summary>
        /// <returns>The security token.</returns>
        /// <param name="config">Config.</param>
        public static ueSecurityToken  GetSecurityToken(this IKlikNPayUsaEPayConfig config) {

			if (config == null)
				throw new ArgumentNullException("config");
            var result = new ueSecurityToken {SourceKey = config.SourceKey};
            var pin = config.Pin;
            var hash = new ueHash
            {
                Type = "md5",
                Seed = Guid.NewGuid().ToString()
            };
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
		/// <param name="tocken">Token.</param>
		/// <param name="data">Data.</param>
		public static string AddCutomersPaymentMethod(usaepayService service,ueSecurityToken tocken,IKlikNPayUsaEPayData data) {

			if (service == null)
				throw new ArgumentNullException("service");
			if (tocken == null)
				throw new ArgumentNullException("tocken");
			if (data == null)
				throw new ArgumentNullException("data");
			string result =null;
			CustomerObject customer;
			PaymentMethod payment;
			data.With(x => x.NewInfo.Do(info => {
				if (info.CustomerId.HasValue)
				{		
					var id = data.NewInfo.CustomerId;
					customer = service.getCustomer(tocken, id.Value.ToString());
					if (customer == null)
						throw new AddCustomerPaymentMethodException("Customer not exist", 
							new NullReferenceException("customer is null"));
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
				    result = service.addCustomerPaymentMethod(tocken, customer.CustomerID, payment, false, true);
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
					originalSum = originalSum + Int32.Parse(cardNumber[y].ToString());
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
			if (String.IsNullOrWhiteSpace(input))
				throw new ArgumentNullException("input");
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
