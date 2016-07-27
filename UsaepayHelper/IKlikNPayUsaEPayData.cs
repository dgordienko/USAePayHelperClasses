// ReSharper disable All
using System.Collections.Generic;

namespace KlikNPayUsaEPay
{
	/// <summary>
	/// Usaepay helper data.
	/// </summary>
	public interface IKlikNPayUsaEPayData{	

		/// <summary>
		/// Gets or sets the new info.
		/// </summary>
		/// <value>The new info.</value>
		IUsaEPayPaimentInfo NewInfo { get; set; }
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
