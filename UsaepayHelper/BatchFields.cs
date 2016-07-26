namespace KlikNPayUsaEPay
{
	/// <summary>
	/// Batch fields.
	/// </summary>
	public static class BatchFields {
		public static string[] GetButchFields() {
			string[] batchFields = new string[42];
			batchFields[0] = "command";
			batchFields[1] = "source";
			batchFields[2] = "invoice";
			batchFields[3] = "cardholder";
			batchFields[4] = "ccnum";
			batchFields[5] = "ccexp";
			batchFields[6] = "avsstreet";
			batchFields[7] = "avszip";
			batchFields[8] = "cvc";
			batchFields[9] = "amount";
			batchFields[10] = "tax";
			batchFields[11] = "description";
			batchFields[12] = "ponum";
			batchFields[13] = "orderid";
			batchFields[14] = "custid";
			batchFields[15] = "billing_company";
			batchFields[16] = "billing_fname";
			batchFields[17] = "billing_lname";
			batchFields[18] = "billing_street";
			batchFields[19] = "billing_street2";
			batchFields[20] = "billing_city";
			batchFields[21] = "billing_state";
			batchFields[22] = "billing_country";
			batchFields[23] = "billing_zip";
			batchFields[24] = "billing_phone";
			batchFields[25] = "shipping_company";
			batchFields[26] = "shipping_fname";
			batchFields[27] = "shipping_lname";
			batchFields[28] = "shipping_street";
			batchFields[29] = "shipping_street2";
			batchFields[30] = "shipping_city";
			batchFields[31] = "shipping_state";
			batchFields[32] = "shipping_zip";
			batchFields[33] = "shipping_country";
			batchFields[34] = "shipping_phone";
			batchFields[35] = "email";
			batchFields[36] = "checknum";
			batchFields[37] = "vcrouting";
			batchFields[38] = "vcaccount";
			batchFields[39] = "vcssn";
			batchFields[40] = "vcdl";
			batchFields[41] = "vcdlstate";
			return batchFields;
		}
	}
	
}