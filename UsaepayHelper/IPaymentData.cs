// ReSharper disable All
using System;
using System.Collections.Generic;
using NLog.Targets.Wrappers;

namespace KlikNPayUsaEPay
{
//    _72BI97rs34iw29035L89r98Xe143lyz  2207


    /// <summary>
    /// Usaepay helper data.
    /// </summary>
    public interface IPaymentData{	

		/// <summary>
		/// Gets or sets the new info.
		/// </summary>
		/// <value>The new info.</value>
		IAddNewCreditCardInfo AddNewCreditCardInfo { get; set; }
		/// <summary>
		/// Gets or sets the paymant info.
		/// </summary>
		/// <value>The paymant info.</value>
		IUsaEPayFields PaymantInfo { get; set; }

		/// <summary>
		/// Gets or sets the batch upload record.
		/// </summary>
		/// <value>The batch upload record.</value>
		IUsaEPayFields BatchUploadRecord { get; set; }
		/// <summary>
		/// Gets or sets the batch upload records.
		/// </summary>
		/// <value>The batch upload records.</value>
		IEnumerable<IUsaEPayFields> BatchUploadRecords { get; set; }
	}
}
