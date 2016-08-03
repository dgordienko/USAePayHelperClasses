namespace KlikNPayUsaEPay
{
    /// <summary>
    ///   Implementation of IPaymentData
    /// </summary>
    public sealed class PaymentData : IPaymentData
    {
        #region Implementation of IPaymentData

        /// <summary>
        ///AddNewCreditCardForm.html
        /// </summary>
        /// <value>The new info.</value>
        public ICreditCardPaymentInfo CreditCardPaymentInfo { get; set; }
        /// <summary>
        /// MakeOneTimePayment.html
        /// </summary>
        public IScedulePaymentInfo ScedulePaymentInfo { get; set; }

        /// <summary>
        /// makeBatchPayment
        /// </summary>
        public IMakeBatchPaymentInfo MakeBatchPaymentInfo { get; set; }
        #endregion
    }
}