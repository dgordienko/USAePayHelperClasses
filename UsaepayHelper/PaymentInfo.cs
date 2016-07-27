// ReSharper disable All
using System;
using Newtonsoft.Json.Converters;

namespace KlikNPayUsaEPay
{
	/// <summary>
	/// Payment info.
	/// </summary>
	internal class PaymentInfo:IPaymentInfo {
		public string PaymentAmount { get; set; }
		public string ForAcount { get; set; }
		public string AccountToPayFrom { get; set; }
		public string PaymentDeliveryDate { get; set; }
	}

}
