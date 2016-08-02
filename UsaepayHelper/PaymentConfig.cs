namespace KlikNPayUsaEPay
{
	/// <summary>
	///  config.
	/// </summary>
	internal class PaymentConfig : IPaymentConfig
	{
		public bool IsSendBox
		{
			get; set;
		}

		public string Pin
		{
			get; set;
		}

        public string SoapServerUrl
        {
            get;set;
        }

        public string SourceKey
		{
			get; set;
		}
	}
	
}
