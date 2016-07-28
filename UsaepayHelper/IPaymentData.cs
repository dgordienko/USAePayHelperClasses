// ReSharper disable All
using System;
using System.Collections.Generic;
using NLog.Targets.Wrappers;

namespace KlikNPayUsaEPay
{
    //    _72BI97rs34iw29035L89r98Xe143lyz  2207

    /// <summary>
    /// 
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
        public IMakeBatchPayment MakeBatchPaymentInfo { get; set; }
        #endregion
    }

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
        IMakeBatchPayment MakeBatchPaymentInfo { get; set; }
    }
}
