using System;
using Newtonsoft.Json.Converters;

namespace KlikNPayUsaEPay
{
    ///
    public sealed class ScedulePaymentInfoConverter : CustomCreationConverter<IScedulePaymentInfo>
    {
        public override IScedulePaymentInfo Create(Type objectType)
        {
            return new ScedulePaymentInfo();
        }
    }
}