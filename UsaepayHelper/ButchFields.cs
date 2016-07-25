using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using USAePayAPI.com.usaepay.www;

namespace KlikNPayUsaEPay
{

	public static class ButchFields {
		public static string[] GetButchFields() {
			string[] butchFields = new string[42];
			butchFields[0] = "command";
			butchFields[1] = "source";
			butchFields[2] = "invoice";
			butchFields[3] = "cardholder";
			butchFields[4] = "ccnum";
			butchFields[5] = "ccexp";
			butchFields[6] = "avsstreet";
			butchFields[7] = "avszip";
			butchFields[8] = "cvc";
			butchFields[9] = "amount";
			butchFields[10] = "tax";
			butchFields[11] = "description";
			butchFields[12] = "ponum";
			butchFields[13] = "orderid";
			butchFields[14] = "custid";
			butchFields[15] = "billing_company";
			butchFields[16] = "billing_fname";
			butchFields[17] = "billing_lname";
			butchFields[18] = "billing_street";
			butchFields[19] = "billing_street2";
			butchFields[20] = "billing_city";
			butchFields[21] = "billing_state";
			butchFields[22] = "billing_country";
			butchFields[23] = "billing_zip";
			butchFields[24] = "billing_phone";
			butchFields[25] = "shipping_company";
			butchFields[26] = "shipping_fname";
			butchFields[27] = "shipping_lname";
			butchFields[28] = "shipping_street";
			butchFields[29] = "shipping_street2";
			butchFields[30] = "shipping_city";
			butchFields[31] = "shipping_state";
			butchFields[32] = "shipping_zip";
			butchFields[33] = "shipping_country";
			butchFields[34] = "shipping_phone";
			butchFields[35] = "email";
			butchFields[36] = "checknum";
			butchFields[37] = "vcrouting";
			butchFields[38] = "vcaccount";
			butchFields[39] = "vcssn";
			butchFields[40] = "vcdl";
			butchFields[41] = "vcdlstate";
			return butchFields;
		}
	}
	
}