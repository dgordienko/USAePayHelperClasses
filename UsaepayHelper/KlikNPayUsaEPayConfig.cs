using System;
using Newtonsoft.Json.Converters;

namespace KlikNPayUsaEPay
{
	/// <summary>
	///  config.
	/// </summary>
	internal class KlikNPayUsaEPayConfig : IKlikNPayUsaEPayConfig
	{
		public bool IsSendBox
		{
			get; set;
		}

		public string Pin
		{
			get; set;
		}

		public string SourceKey
		{
			get; set;
		}
	}
	
}
