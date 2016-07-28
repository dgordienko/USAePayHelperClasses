namespace KlikNPayUsaEPay
{
    /// <summary>
    /// Add New Credit Card
    /// </summary>
    public interface IAddNewCreditCardInfo
    {
        string Description { get; set; }
        string CreditCardNumber { get; set; }
        string CVC { get; set; }
        string NameOnCreditCard { get; set; }
        string ExpirationDate { get; set; }
        string BillingAddress { get; set; }
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string City { get; set; }
        string StateProvince { get; set; }
        string ZipCode { get; set; }
        string Country { get; set; }
    }
}
