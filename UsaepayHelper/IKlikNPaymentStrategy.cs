namespace KlikNPayUsaEPay
{
    /// <summary>
    /// Generic for create any payment operation
    /// </summary>
    /// <typeparam name="TC">Context</typeparam>
    /// <typeparam name="T">Config</typeparam>
    /// <typeparam name="TD">Data</typeparam>
    public interface IKlikNPaymentStrategy<in TC, in T, in TD> {
		object Method(TC context,T config ,TD data);
	}
}