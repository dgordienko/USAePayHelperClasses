namespace UsaepayHelper
{
	/// <summary>
	/// Usaepay helper data.
	/// </summary>
	public interface ICCData{	
		/// <summary>
		/// Gets or sets the detalis.
		/// </summary>
		/// <value>The detalis.</value>
		ICCDetalis Detalis { get; set; }

		/// <summary>
		/// Gets or sets the autotization.
		/// </summary>
		/// <value>The autotization.</value>
		ICardholderAuthorization Autotization { get; set; }

		/// <summary>
		/// Gets or sets the billing adress.
		/// </summary>
		/// <value>The billing adress.</value>
		IBillingAddressInformation BillingAdress { get; set; }
	}
	
}
