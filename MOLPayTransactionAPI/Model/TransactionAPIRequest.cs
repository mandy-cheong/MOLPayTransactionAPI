using goodmaji;
using MOLPayTransactionAPI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class TransactionAPIRequest
{
    public Guid SysId { get; set; }

    public string merchantID { get; set; }
    public string skey { get; set; }
    public string rdate { get; set; }
    public string rduration { get; set; }
    public string status { get { return "00|11|22"; } }
    public string version { get { return "2"; } }
    public string additional_fields { get { return "BillingEmail,TransactionRate,BillingInfo,TransactionCost,Channel,BillingMobileNumber,TransactionFee,GST,NetAmount,IPAddress,BankName,BIN,ExpiryDate,StatusDescription,SettlementDate,PaidDate,TerminalID,PayTransactionID,BuyerName"; } }
    public string response_type { get { return "json"; } }

    public string ResponseData { get; set; }
}

