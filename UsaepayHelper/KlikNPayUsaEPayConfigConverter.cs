using System;
using Newtonsoft.Json.Converters;

namespace KlikNPayUsaEPay
{
	/// <summary>
	/// json config converter.
	/// </summary>
	public class KlikNPayUsaEPayConfigConverter : CustomCreationConverter<IKlikNPayUsaEPayConfig>
	{
		public override IKlikNPayUsaEPayConfig Create(Type objectType)
		{
			return new KlikNPayUsaEPayConfig();
		}
	}
}
