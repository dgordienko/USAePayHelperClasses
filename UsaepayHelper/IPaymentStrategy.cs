namespace KlikNPayUsaEPay
{
    /// <summary>
    /// Generic for create any payment operation
    /// </summary>
    /// <typeparam name="TC">Context</typeparam>
    /// <typeparam name="T">Config</typeparam>
    /// <typeparam name="TD">Data</typeparam>
    public interface IPaymentStrategy<in TC, in T, in TD> {
		object Strategy(TC context,T config ,TD data);
	}
}