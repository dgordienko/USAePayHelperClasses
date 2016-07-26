using System;
using Newtonsoft.Json.Converters;

namespace KlikNPayUsaEPay
{

	/// <summary>
	/// UsaEPayFieldsConfigConverter converter.
	/// </summary>
	public sealed class UsaEPayFieldsConfigConverter : CustomCreationConverter<IUsaEPayFields>
	{
		/// <summary>
		/// Create the specified objectType.
		/// </summary>
		/// <param name="objectType">Object type.</param>
		public override IUsaEPayFields Create(Type objectType)
		{
			return new UsaEPayFields();
		}
	}
    
}
