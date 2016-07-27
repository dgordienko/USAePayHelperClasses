// ReSharper disable All
using System;
using Newtonsoft.Json.Converters;

namespace KlikNPayUsaEPay
{
	/// <summary>
	/// Usa EP ay paiment info converter.
	/// </summary>
	public class UsaEPayPaimentInfoConverter : CustomCreationConverter<IUsaEPayPaimentInfo>
	{
		public override IUsaEPayPaimentInfo Create(Type objectType)
		{
			return new UsaEPayPaimentInfo();
		}
	}
}
