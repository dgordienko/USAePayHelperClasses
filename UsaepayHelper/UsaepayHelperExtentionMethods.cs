using System;
using System.Security.Cryptography;
using System.Text;

namespace UsaepayHelper
{

	/// <summary>
	/// Usaepay helper extention methods.
	/// </summary>
	public static class UsaepayHelperExtentionMethods 
	{
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
			byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
			var sBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}
			return sBuilder.ToString();
		}
	}
}
