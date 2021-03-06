using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using KlikNPayUsaEPay.com.usaepay;
using Newtonsoft.Json.Linq;


namespace KlikNPayUsaEPay
{
	/// <summary>
	/// Usaepay helper extention methods.
	/// </summary>
	[SuppressMessage("ReSharper", "UseNameofExpression")]
	public static class PaymentExtentionMethods 
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
 			var search = new SearchParam[1];
		    search[0] = new SearchParam
		    {
		        Field = "Invoice",
		        Type = "Contains",
		        Value = invoice
		    };
		    var result = context.searchBatches(token, search, true, "0", "10", "closed");
			var q = result.Batches.Any();
			return q;
		}


	    /// <summary>
        ///   Create csv file from json string 
        /// </summary>
        /// <param name="json">json string object </param>
        /// <returns></returns>
        public static string ToObjectCsv(this string json)
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
        public static com.usaepay.ueSecurityToken  GetSecurityToken(this IPaymentConfig config) {

			if (config == null)
				throw new ArgumentNullException("config");
            var result = new com.usaepay.ueSecurityToken {SourceKey = config.SourceKey};
            var pin = config.Pin;
            var hash = new com.usaepay.ueHash
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
        /// Validates the credit card number.
        /// </summary>
        /// <returns>bool</returns>
        /// <param name="cardNumber">Card number.</param>
        public static bool ValidateCardNumber(this string cardNumber)
		{
            //TODO replace int.Parce to int.TryParce
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
