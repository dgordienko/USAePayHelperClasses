using System;
using KikNPay.www.usaepay.com;

namespace KikNPay
{
	/// <summary>
	/// Usaepay strategy.
	/// </summary>
	public interface IUsaepayStrategy<C,T,D> {
		object Method(C context,T config ,D data);
	}



}