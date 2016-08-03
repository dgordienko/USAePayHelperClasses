namespace KlikNPayUsaEPay
{
    internal class ScedulePaymentInfo : IScedulePaymentInfo
    {
        public string Description { get; set; }
        public string CreditCardNumber { get; set; }
        public string CVC { get; set; }
        public string NameOnCreditCard { get; set; }
        public string ExpirationDate { get; set; }
        public string BillingAddress { get; set; }
        public double Amount{ get; set; }
    }
}