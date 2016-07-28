using System;
using Newtonsoft.Json.Converters;

namespace KlikNPayUsaEPay
{
    /// <summary>
    /// Add New Credit Card
    /// </summary>
    public interface ICreditCardPaymentInfo
    {
        string Description { get; set; }
        string CreditCardNumber { get; set; }
        string CVC { get; set; }
        string NameOnCreditCard { get; set; }
        string ExpirationDate { get; set; }
        string BillingAddress { get; set; }
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string City { get; set; }
        string StateProvince { get; set; }
        string ZipCode { get; set; }
        string Country { get; set; }
    }


    /// <summary>
    /// BatchPayment data
    /// </summary>
    public interface IMakeBatchPaymentInfo
    {
        /// <summary>
        /// path to resource file
        /// </summary>
        string PathToFile { get; set; }
        /// <summary>
        /// url to soap usarpay
        /// </summary>
        string SoapServerUrl { get; set; }
    }
    internal class MakeBatchPaymentInfo: IMakeBatchPaymentInfo
    {
        /// <summary>
        /// path to resource file
        /// </summary>
        public string PathToFile { get; set; }
        /// <summary>
        /// url to soap usarpay
        /// </summary>
        public string SoapServerUrl { get; set; }
    }

    public class MakeBatchPaymentInfoConverter : CustomCreationConverter<IMakeBatchPaymentInfo>
    {
        public override IMakeBatchPaymentInfo Create(Type objectType)
        {
            return new MakeBatchPaymentInfo();
        }
    }

    public interface IScedulePaymentInfo
    {
        string Description { get; set; }
        string CreditCardNumber { get; set; }
        string CVC { get; set; }
        string NameOnCreditCard { get; set; }
        string ExpirationDate { get; set; }
        string BillingAddress { get; set; }

        decimal Amount { get; set; }
    }


    internal class ScedulePaymentInfo : IScedulePaymentInfo
    {
        public string Description { get; set; }
        public string CreditCardNumber { get; set; }
        public string CVC { get; set; }
        public string NameOnCreditCard { get; set; }
        public string ExpirationDate { get; set; }
        public string BillingAddress { get; set; }

        public decimal Amount{ get; set; }
    }
    ///
    public sealed class ScedulePaymentInfoConverter : CustomCreationConverter<IScedulePaymentInfo>
    {
        public override IScedulePaymentInfo Create(Type objectType)
        {
            return new ScedulePaymentInfo();
        }
    }
}
