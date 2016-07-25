// ReSharper disable All
namespace KlikNPayUsaEPay
{

	/// <summary>
	/// Usaepay helper data.
	/// </summary>
	public interface IKlikNPayUsaEPayData{	

		/// <summary>
		/// Gets or sets the new info.
		/// </summary>
		/// <value>The new info.</value>
		IUsaEPayPaimentInfo NewInfo { get; set; }
		/// <summary>
		/// Gets or sets the paymant info.
		/// </summary>
		/// <value>The paymant info.</value>
		IPaymentInfo PaymantInfo { get; set; }
		/// <summary>
		/// Gets or sets the batch payment info.
		/// </summary>
		/// <value>The batch payment info.</value>
		IBatchPaymentInfo BatchPaymentInfo { get; set; }
	}
}
