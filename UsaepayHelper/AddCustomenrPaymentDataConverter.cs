// ReSharper disable All
using System;
using Newtonsoft.Json.Converters;

namespace KlikNPayUsaEPay
{
	/// <summary>
	/// Usa EP ay paiment info converter.
	/// </summary>
	public class AddCustomenrPaymentDataConverter : CustomCreationConverter<IAddNewCreditCardInfo>
	{
		public override IAddNewCreditCardInfo Create(Type objectType)
		{
			return new AddNewCreditCardInfo();
		}
	}
}
