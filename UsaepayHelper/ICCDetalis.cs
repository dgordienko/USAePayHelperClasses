namespace KikNPay
{

	/// <summary>
	/// Credit Card Details
	/// </summary>
	public interface ICCDetalis
	{
		/// <summary>
		///  CC Credit Card Number with no spaces or dashes
		/// </summary>
		/// <value>The card number.</value>
		string CardNumber { get; set; }
		/// <summary>
		///  CC  Expiration Date in the form of MMYY with no spaces or punctuation
		/// </summary>
		/// <value>The card exp.</value>
		string CardExp { get; set; }
		/// <summary>
		///  String Street address for use in AVS check.
		/// </summary>
		/// <value>The avs street.</value>
		string AvsStreet { get; set; }
		/// <summary>
		/// Zipcode for AVS check.
		/// </summary>
		/// <value>The avs zip.</value>
		string AvsZip { get; set; }
		/// <summary>
		///  CVC/CVV2 3-4 digit security code from back of credit card (optional).
		/// </summary>
		string Cvv2 {get;set;}
		/// <summary>
		/// If set to true and the transaction has been approved, the system will issue a token for future use.
		/// </summary>
		/// <value>The save card.</value>
		bool SaveCard { get; set; }
	}
	
}
