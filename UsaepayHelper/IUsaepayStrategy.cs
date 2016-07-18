namespace UsaepayHelper
{
	/// <summary>
	/// Usaepay strategy.
	/// </summary>
	public interface IUsaepayStrategy<T,D> {		
		object Method(T client,D data);
	}	
}
