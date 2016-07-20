using System;
using UsaepayHelper.www.usaepay.com;

namespace UsaepayHelper
{
	/// <summary>
	/// Usaepay strategy.
	/// </summary>
	public interface IUsaepayStrategy<C,T,D> {
		object Method(C context,T config ,D data);
	}

	/// <summary>
	/// do we have enough info to make call ?
	/// validate if cc data is correct
	/// return from USAePay if there is something wrong
	/// provide list of all error codes and descriptions
	/// </summary>
	public class ValidateCCData : IUsaepayStrategy<usaepayService, IUsaepayHelperConfig, ICCData>
	{
		/// <summary>
		/// Method the specified context, config and data.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="config">Config.</param>
		/// <param name="data">Data.</param>
		public object Method(usaepayService context, IUsaepayHelperConfig config, ICCData data)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));
			if (config == null)
				throw new ArgumentNullException(nameof(config));
			if (data == null)
				throw new ArgumentNullException(nameof(data));
			throw new NotImplementedException();
		}
	}

	/// <summary>
	/// Send payment info to USAePay
	/// return success code
	/// provide list of all codes and descriptions
	/// special handling: if we don't gate "ok" from the gateway, then automatically send a status request for that merchant and order number - to confirm that the gateway does not have that transaction. This is to avoid the special case when the gateway sends back an 'ok' but we never get it.
	/// </summary>
	public class MakePayment : IUsaepayStrategy<usaepayService, IUsaepayHelperConfig, ICCData>
	{
		/// <summary>
		/// Method the specified context, config and data.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="config">Config.</param>
		/// <param name="data">Data.</param>
		public object Method(usaepayService context, IUsaepayHelperConfig config, ICCData data)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));
			if (config == null)
				throw new ArgumentNullException(nameof(config));
			if (data == null)
				throw new ArgumentNullException(nameof(data));			
			throw new NotImplementedException();
		}
	}

	/// <summary>
	/// Send batch file to USAePay for processing
	/// return success code
	/// provide list of all codes and descriptions
	/// </summary>
	public class MakeBatchPayment : IUsaepayStrategy<usaepayService, IUsaepayHelperConfig, ICCData>
	{
		/// <summary>
		/// Method the specified context, config and data.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="config">Config.</param>
		/// <param name="data">Data.</param>
		public object Method(usaepayService context, IUsaepayHelperConfig config, ICCData data)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));
			if (config == null)
				throw new ArgumentNullException(nameof(config));
			if(data == null)
				throw new ArgumentNullException(nameof(data));
			throw new NotImplementedException();
		}
	}
}