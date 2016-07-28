// ReSharper disable All

namespace KlikNPayUsaEPay
{
    /// <summary>
    /// Usa EP ay paiment info.
    /// </summary>
    internal class AddNewCreditCardInfo : ICreditCardPaymentInfo
	{
	    #region Implementation of IAddNewCreditCardInfo

	    public string Description { get; set; }
	    public string CreditCardNumber { get; set; }
	    public string CVC { get; set; }
	    public string NameOnCreditCard { get; set; }
	    public string ExpirationDate { get; set; }
	    public string BillingAddress { get; set; }
	    public string AddressLine1 { get; set; }
	    public string AddressLine2 { get; set; }
	    public string City { get; set; }
	    public string StateProvince { get; set; }
	    public string ZipCode { get; set; }
	    public string Country { get; set; }

	    #endregion
	}	
}
