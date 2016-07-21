namespace KinNPayUsaEPay
{
	/// <summary>
	/// Billing Address Information
	/// </summary>
	public interface IBillingAddressInformation { 
		/// <summary>
		/// Gets or sets the name of the billing first.
		/// </summary>
		/// <value>The name of the billing first.</value>
		string BillingFirstName { get; set; }
		/// <summary>
		/// Gets or sets the name of the billing last.
		/// </summary>
		/// <value>The name of the billing last.</value>
		string BillingLastName { get; set; }
		/// <summary>
		/// Gets or sets the billing company.
		/// </summary>
		/// <value>The billing company.</value>
		string BillingCompany { get; set; }
		/// <summary>
		/// Gets or sets the billing street.
		/// </summary>
		/// <value>The billing street.</value>
		string BillingStreet { get; set; }
		/// <summary>
		/// Gets or sets the billing city.
		/// </summary>
		/// <value>The billing city.</value>
		string BillingCity { get; set; }
		/// <summary>
		/// Gets or sets the state of the billing.
		/// </summary>
		/// <value>The state of the billing.</value>
		string BillingState { get; set; }
		/// <summary>
		/// Gets or sets the billing zip.
		/// </summary>
		/// <value>The billing zip.</value>
		string BillingZip { get; set; }
		/// <summary>
		/// Gets or sets the billing country.
		/// </summary>
		/// <value>The billing country.</value>
		string BillingCountry { get; set; }
		/// <summary>
		/// Gets or sets the billing phone.
		/// </summary>
		/// <value>The billing phone.</value>
		string BillingPhone { get; set; }
		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>The email.</value>
		string Email { get; set; }
		/// <summary>
		/// Gets or sets the fax.
		/// </summary>
		/// <value>The fax.</value>
		string Fax { get; set; }
		/// <summary>
		/// Gets or sets the website.
		/// </summary>
		/// <value>The website.</value>
		string Website { get; set; }
	}
	
}
