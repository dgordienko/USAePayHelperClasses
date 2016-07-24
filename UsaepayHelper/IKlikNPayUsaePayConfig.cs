namespace KlikNPayUsaEPay
{
	/// <summary>
	/// Usaepay helper config.
	/// </summary>
	public interface IKlikNPayUsaePayConfig
	{
		/// <summary>
		/// Gets or sets the source key.
		/// </summary>
		/// <value>The source key.</value>
		string SourceKey { get; set; }

		/// <summary>
		/// Gets or sets the pin.
		/// </summary>
		/// <value>The pin.</value>
		string Pin { get; set; }

		/// <summary>
		/// Gets or sets the use proxi.
		/// </summary>
		/// <value>The use proxi.</value>
		bool UseProxi { get; set; }
        
		/// <summary>
        /// 
        /// </summary>
        bool IsSendBox { get; set; }
	}
}
