using System;

public class TransactionAPIResponse {
    public Guid SysId { get; set; }
    public DateTime BillingDate { get; set; }
    public string OrderID { get; set; }
    public int TranID { get; set; }
    public string Channel { get; set; }
    public decimal Amount { get; set; }
    public string StatCode { get; set; }
    public string StatName { get; set; }
    public string BillingName { get; set; }
    public string ServiceItem { get; set; }
    public string BillingEmail { get; set; }
    public decimal TransactionRate { get; set; }
    public decimal TransactionCost { get; set; }
    public string BillingMobileNumber { get; set; }
    public decimal? TransactionFee { get; set; }
    public decimal GST { get; set; }
    public decimal NetAmount { get; set; }
    public string IPAddress { get; set; }
    public string BankName { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string StatusDescription { get; set; }
    public string SettlementDate { get; set; }
    public string PaidDate { get; set; }
    public string TerminalID { get; set; }
    public string MemberId { get; set; }
    public string MemberName { get; set; }
}