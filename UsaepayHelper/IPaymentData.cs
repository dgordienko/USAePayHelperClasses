namespace KlikNPayUsaEPay
{
    /// <summary>
    /// Payment data main intreface
    /// </summary>
    public interface IPaymentData{
        /// <summary>
        ///AddNewCreditCardForm.html
        /// </summary>
        /// <value>The new info.</value>
         ICreditCardPaymentInfo CreditCardPaymentInfo { get; set; }
        /// <summary>
        /// MakeOneTimePayment.html
        /// </summary>
        IScedulePaymentInfo ScedulePaymentInfo { get; set; }

        /// <summary>
        /// makeBatchPayment
        /// </summary>
        IMakeBatchPaymentInfo MakeBatchPaymentInfo { get; set; }
    }
}
