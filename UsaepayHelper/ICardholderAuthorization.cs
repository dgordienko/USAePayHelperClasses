namespace KinNPayUsaEPay
{

	/// <summary>
	/// Cardholder authorization.
	/// </summary>
	public interface ICardholderAuthorization {

		/// <summary>
		/// Enables USAePay Cardholder Authentication.The merchant must have a Cardinal 
		/// Commerce account enabled in the gateway. If they don't have an account, then the this field will 
		/// be ignored and the transaction will be processed normally (without attempting authentication). 
		/// Set cardauth=1 to enable cardholder authentication, set cardauth=0 to disable authentication. 
		/// (defaults to disabled) If using a thirdparty service (such as the Cardinal Commerce Thin Client) 
		/// to perform authentication set this value to 0 and populate the CAVV and ECI fields)
		/// </summary>
		/// <value>The card auth.</value>
		string CardAuth { get; set; }

		/// <summary>
		///The authentication response received from independent authentication site.
		/// </summary>
		/// <value>The pares.</value>
		string Pares { get; set; }

		/// <summary>
		///  The cavv provided by an external third party verification platform. Omit if using USAePay to perform verification.
		/// </summary>
		/// <value>The cavv.</value>
		string CAVV { get; set; }

		/// <summary>
		///ECI value provided by external third party.Omit if you are not using third party verification.
		/// </summary>
		/// <value>The eci.</value>
		string ECI { get; set; }
	}
	
}
