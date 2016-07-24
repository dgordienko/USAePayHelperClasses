// ReSharper disable All
namespace KlikNPayUsaEPay
{


	/// <summary>
	/// Add New Credit Card
	/// </summary>
	public interface IUsaEPayPaimentInfo
	{
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		string Description { get; set; }
		/// <summary>
		/// Gets or sets the credit card number.
		/// </summary>
		/// <value>The credit card number.</value>
		string CreditCardNumber { get; set; }
		/// <summary>
		/// Gets or sets the expiration date.
		/// </summary>
		/// <value>The expiration date.</value>
		string ExpirationDate { get; set; }
		/// <summary>
		/// Gets or sets the name on credit card.
		/// </summary>
		/// <value>The name on credit card.</value>
		string NameOnCreditCard { get; set; }
		/// <summary>
		/// Gets or sets the billing address.
		/// </summary>
		/// <value>The billing address.</value>
		string BillingAddressLine1 {get;set;}
		/// <summary>
		/// Gets or sets the billing address line2.
		/// </summary>
		/// <value>The billing address line2.</value>
		string BillingAddressLine2 { get; set; }
		/// <summary>
		/// Gets or sets the city.
		/// </summary>
		/// <value>The city.</value>
		string City { get; set; }
		/// <summary>
		/// Gets or sets the state.
		/// </summary>
		/// <value>The state.</value>
		string State { get; set; }
		/// <summary>
		/// Gets or sets the zip code.
		/// </summary>
		/// <value>The zip code.</value>
		string ZipCode { get; set; }
		/// <summary>
		/// Gets or sets the country.
		/// </summary>
		/// <value>The country.</value>
		string Country { get; set; }
		/// <summary>
		///	CVC
		/// </summary>
		/// <value>The code.</value>
		string CVC { get; set; }
		/// <summary>
		/// Gets or sets the customer identifier.
		/// </summary>
		/// <value>The customer identifier.</value>
		int? CustomerId { get; set; }
	}


	/// <summary>
	/// Usaepay helper data.
	/// </summary>
	public interface IKlikNPayUsaEPayData{	

		/// <summary>
		/// Gets or sets the new info.
		/// </summary>
		/// <value>The new info.</value>
		IUsaEPayPaimentInfo NewInfo { get; set; }
	}



	
}
