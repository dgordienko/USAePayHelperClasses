using System.Diagnostics.CodeAnalysis;

namespace KlikNPayUsaEPay
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IScedulePaymentInfo
    {
        string Description { get; set; }
        string CreditCardNumber { get; set; }
        string CVC { get; set; }
        string NameOnCreditCard { get; set; }
        string ExpirationDate { get; set; }
        string BillingAddress { get; set; }

        decimal Amount { get; set; }
    }
}