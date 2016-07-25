using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using USAePayAPI.com.usaepay.www;

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
