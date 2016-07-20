namespace KikNPay
{
	/// <summary>
	/// Usaepay helper config.
	/// </summary>
	public interface IUsaepayHelperConfig
	{
		/// <summary>
		/// Gets or sets the source key.
		/// </summary>
		/// <value>The source key.</value>
		string SourceKey { get; set; }

		/// <summary>
		/// Gets or sets the use proxi.
		/// </summary>
		/// <value>The use proxi.</value>
		bool UseProxi { get; set; }
	}
}
