|Class or method Name|Result|Exception|Unit Test|
|-----------|------|---------|---------|
|AddCustomerPaymentMethod|IPaymentArgument result - string|AddCustomerPaymentMethodException|UpdatePaymentInfoTestCase|
|ScheduleOneTimePayment|IPaymentArgument result -ButchUploadStatus|ScheduleOneTimePaymentException|MakePayment|
|MakeBatchPayment|IPaymentArgument result IList(ButchUploadStatus)>|MakeBanchPaymentException|MakeBatchPayment|
|KlikNPayUsaEPayExtentionMethods.GenerateHash|string|Exception|CreateMD5TestCase|
|KlikNPayUsaEPayExtentionMethods.ValidateCardNumber|bool|Exception|ValidateCCNumberTest|
|KlikNPayUsaEPayExtentionMethods.GetSecurityToken|ueSecurityToken|Exception|GetSecurityTokenTest|

use component  (KlikNPayUsaEPayExamples)


    usecase 1

    using (var client = new KlikNPayUsaEPayAdapter(config))
      {
        client.Data = data;
        //subscript event
        client.MethodComplete += (sender, argument) =>
        {
        //error
        argument.With(x => x.Exception.Do(e => { Logger.Trace(e.Message);}));
        //result  is  IPaymentArgument
      argument.With(x => x.Result.Do(result =>
      {
        var paymentArgument = (IPaymentArgument) result;
        //UsaEPey return exception
        paymentArgument.With(a => a.Exception.Do(pe => Logger.Trace(pe.Message)));
        //UsaEPay reurn ok
        paymentArgument.With(a => a.Result.Do(res => Logger.Trace($"{res}")));
        }));
        };
          //call add new customer payment method
          client.ExecuteStrategy(new AddCustomerPaymentMethod());
        }

        ....use case 2

        using (var client = new KlikNPayUsaEPayAdapter(config))
          {
            client.Data = data;
            //subscript event
            client.MethodComplete += (sender, argument) =>
            {
            //error
            argument.With(x => x.Exception.Do(e => { Logger.Trace(e.Message);}));
            //result  is  IPaymentArgument
          argument.With(x => x.Result.Do(result =>
          {
            var paymentArgument = (IPaymentArgument) result;
            //UsaEPey return exception
            paymentArgument.With(a => a.Exception.Do(pe => Logger.Trace(pe.Message)));
            //UsaEPay reurn ok
            paymentArgument.With(a => a.Result.Do(res => Logger.Trace($"{res}")));
            }));
            };
              //call add new customer payment method
              client.ExecuteStrategy(new ScheduleOneTimePayment());
            }

sources

|name|url|
|---|---|
|Strategy pattern|https://en.wikipedia.org/wiki/Strategy_pattern|
|usaepay api|http://wiki.usaepay.com/developer/soap-1.4/howto/csharp|

external dependencies

|name|url|nuget|
|----|---|-----|
|Json.NET|http://www.newtonsoft.com/json|Install-Package Newtonsoft.Json|
|NUnit|http://www.nunit.org|Install-Package NUnit|
|AutoFixture|https://github.com/AutoFixture/AutoFixture|Install-Package AutoFixture|
|NLog|http://nlog-project.org|Install-Package NLog|
