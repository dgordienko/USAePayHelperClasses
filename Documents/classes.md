
* .NET version = .NET framework  4.0
* Development environment: Microsoft Visual studio  2010
* language: C#


###### Class diagram
![alt text](https://raw.githubusercontent.com/dgordienko/USAePayHelperClasses/master/Documents/ClassDiagram.png "Class diagram")


###### PaymentComponent
A major component that you will use in your code

|Name|type|argument|
|-|-|
|ExecuteStrategy|method|IPaymentStrategy(USAePay, IPaymentConfig,IPaymentData)|
|MethodComplete|event|PaymentControllerEvent|

Subscribe event MethodComplete and perform method ExecuteStrategy


              Will save customer info locally
              1. validateCCData()
              -- do we have enough info to make call ?
              -- validate if cc data is correct
              -- return from USAePay if there is something wrong
              -- provide list of all error codes and descriptions
              2. makePayment(CC data)
              -- send payment info to USAePay
              -- return success code
              -- provide list of all codes and descriptions
              -- special handling: if we don't gate "ok" from the gateway, then automatically send a status request for that merchant and order number - to confirm that the gateway does not have that transaction. This is to avoid the special case when the gateway sends back an 'ok' but we never get it.
              3. makeBatchPayment(batch file path to CC data)
              -- send batch file to USAePay for processing
              -- return success code
              -- provide list of all codes and descriptions


For ease of implementation of the selected strategy pattern and we will implement an interface

      public interface IPaymentStrategy<in TC, in T, in TD> {
          object Strategy(TC context,T config ,TD data);
      }

      conext - usaepay soap
      config strategy config
      data strategy data



###### IPaymentConfig
Interface of configuration payment component

    {
      "SourceKey": "_72BI97rs34iw29035L89r98Xe143lyz",
      "Pin": "2207",
      "IsSendBox": true,
      "SoapServerUrl": "https://sandbox.usaepay.com/soap/gate/1412E031"
    }

    SourceKey - usaepay source key
    Pin  - usaepay pin
    SoapServerUrl - usaepay soap gete url

###### validateCCData()

|Class|Config|Data|Unit test|Intagrated test|
|-|-|-|
|AddCustomerPaymentMethod|IPaymentConfig|ICreditCardPaymentInfo|BasePaymentTestCase()|AddCustomerPaymentDataIntegrationTestCase()|

    sample use payment component

    var jsonConfig = @"{"SourceKey": "_72BI97rs34iw29035L89r98Xe143lyz","Pin": "2207","IsSendBox": true,"SoapServerUrl":"https://sandbox.usaepay.com/soap/gate/1412E031"}";
    var config = JsonConvert.DeserializeObject<IPaymentConfig>(configString, new PaymentConfigConverter());
    var jsonData = @"{"Description": "Add New Credit Card testcase","CreditCardNumber": "4000600011112223","CVC": "999","NameOnCreditCard": "Dmitry Gordienko","ExpirationDate": "0919","BillingAddress": true,"AddressLine1": "Washington Square Park","AddressLine2": "Near Crown Center in downtown Kansas City","City": "Kansas","StateProvince": "MO","ZipCode": "64101","Country": "USA"}";
    var data = JsonConvert.DeserializeObject<ICreditCardPaymentInfo>(jsonData,new AddCustomenrPaymentDataConverter());

        //Create component instace
    using (var paymentComponent = new PaymentComponent(config))
    {
        //Create payment data
        paymentComponent.Data = new PaymentData
        {
            CreditCardPaymentInfo = data
        };
        //subscribe for end of executed event
        paymentComponent.MethodComplete += (sender, argument) =>
        {
            var paymentArgument = argument;
            logger.Trace(JsonConvert.SerializeObject(argument));
            //error
            paymentArgument.With(x => x.Result.Do(rm =>
            {
                var res = (IPaymentArgument)rm;
                res.With(r => r.Exception.Do(exp => {                    
                    throw exp;
                }));
                res.With(r => r.Result.Do(rslt => {

                }));
            }
           ));
        };
        //run component
        paymentComponent.ExecuteStrategy(new AddCustomerPaymentMethod());
    }
    });


###### makePayment(CC data)

|Class|Config|Data|Unit test|Intagrated test|
|-|-|-|
|ScheduleOneTimePayment|IPaymentConfig|IScedulePaymentInfo|BasePaymentTestCase()|ScheduleOneTimePaymentIntegrationTestCase()|


######  makeBatchPayment(batch file path to CC data)
|Class|Config|Data|Unit test|Intagrated test|
|-|-|-|
|ScheduleOneTimePayment|IPaymentConfig|IScedulePaymentInfo|BasePaymentTestCase()|BatchPaymentIntegrationTestCase()|

##### Links

|name|url|
|---|---|
|Strategy pattern|https://en.wikipedia.org/wiki/Strategy_pattern|
|usaepay api|https://wiki.usaepay.com/developer/soap|
|Maybe monad|http://devtalk.net/2010/09/12/chained-null-checks-and-the-maybe-monad/|

##### Dependencies

|name|url|nuget|
|----|---|-----|
|Json.NET|http://www.newtonsoft.com/json|Install-Package Newtonsoft.Json|
|NUnit|http://www.nunit.org|Install-Package NUnit|
|AutoFixture|https://github.com/AutoFixture/AutoFixture|Install-Package AutoFixture|
|NLog|http://nlog-project.org|Install-Package NLog|
