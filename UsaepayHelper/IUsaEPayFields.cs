namespace KlikNPayUsaEPay
{
	/// <summary>
	/// http://wiki.usaepay.com/developer/soap-1.4/methods/createbatchupload
	/// </summary>
	public interface IUsaEPayFields { 
		string command { get; set;}
		string source { get; set;}
		string invoice { get; set;}
		string cardholder { get; set;}
		string ccnum { get; set;}
		string ccexp { get; set;}
		string avsstreet { get; set;}
		string avszip { get; set;}
		string cvc { get; set;}
		string amount { get; set;}
		string tax { get; set;}
		string description { get; set;}
		string ponum { get; set;}
		string orderid { get; set;}
		string custid { get; set;}
		string billing_company { get; set;}
		string billing_fname { get; set;}
		string billing_lname { get; set;}
		string billing_street { get; set;}
		string billing_street2 { get; set;}
		string billing_city { get; set;}
		string billing_state { get; set;}
		string billing_country { get; set;}
		string billing_zip { get; set;}
		string billing_phone { get; set;}
		string shipping_company { get; set;}
		string shipping_fname { get; set;}
		string shipping_lname { get; set;}
		string shipping_street { get; set;}
		string shipping_street2 { get; set;}
		string shipping_city { get; set;}
		string shipping_state { get; set;}
		string shipping_zip { get; set;}
		string shipping_country { get; set;}
		string shipping_phone { get; set;}
		string email { get; set;}
		string checknum { get; set;}
		string vcrouting { get; set;}
		string vcaccount { get; set;}
		string vcssn { get; set;}
		string vcdl { get; set;}
		string vcdlstate { get; set;}
	}
    
}
