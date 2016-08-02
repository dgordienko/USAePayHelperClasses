using System;
using Newtonsoft.Json.Converters;

namespace KlikNPayUsaEPay
{
    public class MakeBatchPaymentInfoConverter : CustomCreationConverter<IMakeBatchPaymentInfo>
    {
        public override IMakeBatchPaymentInfo Create(Type objectType)
        {
            return new MakeBatchPaymentInfo();
        }
    }
}