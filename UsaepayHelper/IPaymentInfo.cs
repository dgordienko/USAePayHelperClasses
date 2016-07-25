// ReSharper disable All
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
	
}
