namespace KlikNPayUsaEPay
{

    /// <summary>
    /// Base operation
    /// </summary>
    /// <typeparam name="TC">Context</typeparam>
    /// <typeparam name="T">Config</typeparam>
    /// <typeparam name="TD">Data</typeparam>
    public interface IKlikNPaymantStrategy<in TC, in T, in TD> {
		object Method(TC context,T config ,TD data);
	}
}