// ReSharper disable All
using System;
using Newtonsoft.Json.Converters;

namespace KlikNPayUsaEPay
{
	/// <summary>
	/// usaepay paiment info converter.
	/// </summary>
	public class AddCustomenrPaymentDataConverter : CustomCreationConverter<ICreditCardPaymentInfo>
	{
		public override ICreditCardPaymentInfo Create(Type objectType)
		{
			return new CreditCardPaymentInfo();
		}
	}
}
