using System;
using Newtonsoft.Json.Converters;

namespace KlikNPayUsaEPay
{
	/// <summary>
	/// json config converter.
	/// </summary>
	public class PaymentConfigConverter : CustomCreationConverter<IPaymentConfig>
	{
		public override IPaymentConfig Create(Type objectType)
		{
			return new PaymentConfig();
		}
	}
}
