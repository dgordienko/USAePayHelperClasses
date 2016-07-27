// ReSharper disable All
using System;
using Newtonsoft.Json.Converters;

namespace KlikNPayUsaEPay
{
	/// <summary>
	/// Usa EP ay paiment info.
	/// </summary>
	internal class UsaEPayPaimentInfo : IUsaEPayPaimentInfo
	{
		public string BillingAddressLine1
		{
			get;set;
		}

		public string BillingAddressLine2
		{
			get;set;
		}

		public string City
		{
			get;set;
		}

		public string Country
		{
			get;set;
		}

		public string CreditCardNumber
		{
			get;set;
		}

		public int? CustomerId
		{
			get;set;
		}

		public string CVC
		{
			get;set;
		}

		public string Description
		{
			get;set;
		}

		public string ExpirationDate
		{
			get;set;
		}

		public string NameOnCreditCard
		{
			get;set;
		}

		public string State
		{
			get;set;
		}

		public string ZipCode
		{
			get;set;
		}
	}
	
}
