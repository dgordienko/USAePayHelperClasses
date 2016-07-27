// ReSharper disable All
using System;
using Newtonsoft.Json.Converters;

namespace KlikNPayUsaEPay
{
	/// <summary>
	/// Payment info.
	/// </summary>
	public interface IPaymentInfo { 
        string PaymentAmount { get; set; }
        string ForAcount { get; set; }
        string AccountToPayFrom { get; set; }
        string PaymentDeliveryDate { get; set; }
    }


	internal class PaymentInfo:IPaymentInfo {
		public string PaymentAmount { get; set; }
		public string ForAcount { get; set; }
		public string AccountToPayFrom { get; set; }
		public string PaymentDeliveryDate { get; set; }
	}

}
