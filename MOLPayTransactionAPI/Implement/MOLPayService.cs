using goodmaji;
using MOLPayTransactionAPI.Helper;
using MOLPayTransactionAPI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOLPayTransactionAPI.Implement
{
    public class MOLPayService  
    {
        private string _url = "https://api.merchant.razer.com/RMS/API/PSQ/psq-daily.php?";
        private Guid _id = Guid.NewGuid();

        public void GetTransactions(DateTime date)
        {
            var molpayMembers = GetAPIRequests(date);
            foreach (var member in molpayMembers)
            {
                var transactionRequest = new TransactionAPIRequest();
                try
                {
                    transactionRequest.merchantID = member.MerchantId;
                    transactionRequest.rdate = date.ToString("yyyy-MM-dd");
                    transactionRequest.skey = Encryptor.ToMD5(transactionRequest.rdate + transactionRequest.merchantID + member.VerifyKey).ToLower();
                    transactionRequest.ResponseData = APIHelper.Get(_url + ParamHelper.ObjToURL(transactionRequest));
                    var response = JsonConvert.DeserializeObject<List<TransactionAPIResponse>>(transactionRequest.ResponseData);
                    AddMolPayTransaction(response, member);
                }
                catch (Exception ex)
                {
                    Logger.AddLog(transactionRequest.ResponseData, ex.Message);
                }
            }
        }

        
        private void AddMolPayTransaction(List<TransactionAPIResponse> transactionAPIResponses, MolPayMember member)
        {
            var cmdList = new List<SqlCommand>();
            foreach (var response in transactionAPIResponses)
            {
                response.SysId = Guid.NewGuid();
                if (response.SettlementDate.Contains("0000-00-00"))
                    response.SettlementDate = null;
                if (response.PaidDate.Contains("0000-00-00"))
                    response.PaidDate = null;
                response.MemberName = member.MemberName;
                response.MemberId = member.MemberId;
                cmdList.Add(SqlExtension.GetInsertSqlCmd("MolPayTransaction", response));
            }

            SqlDbmanager.ExecuteNonQryMutiSqlCmd(cmdList);
        }

        private TransactionAPIResponse SetMemberInfo(TransactionAPIResponse transactionAPIResponse)
        {
            string strSql = @"SELECT  MI02 AS MemberId, MI07 As MemberName
                            FROM    GoodMaji.dbo.CollectionOfMoney 
                            INNER JOIN GoodMaji.dbo.MemberAccount ON MA02=COM04
                            INNER JOIN GoodMaji.dbo.MemberInfo ON MI02=MA05
                            WHERE COM02=@OrderID ";
            var cmd = new SqlCommand { CommandText = strSql };
            cmd.Parameters.Add(SafeSQL.CreateInputParam("@OrderID", System.Data.SqlDbType.VarChar, transactionAPIResponse.OrderID));
            var dt = SqlDbmanager.queryBySql(cmd);

            if (dt == null || dt.Rows == null || dt.Rows.Count == 0)
                return transactionAPIResponse;

            var dr = dt.Rows[0];
            transactionAPIResponse.MemberId = dr["MemberId"].ToString();
            transactionAPIResponse.MemberName = dr["MemberName"].ToString();

            return transactionAPIResponse;
        }

        private List<MolPayMember> GetAPIRequests(DateTime date)
        {
            string sql = @"SELECT  MI02 AS MemberId, MI07 As MemberName, MI77 AS MerchantId, MI78 As VerifyKey, MI79 As SecretKey
                            FROM    GoodMaji.dbo.MemberInfo ON MI02=MA05
                            WHERE MI80=1 ";
            var cmd = new SqlCommand { CommandText = sql };
            var dt =SqlDbmanager.queryBySql(cmd);
            var transactionApiRequests = new List<MolPayMember>();
            if (dt == null || dt.Rows == null || dt.Rows.Count == 0)
                return transactionApiRequests;

            foreach (DataRow dr in dt.Rows)
            {
                var transactionRequest = new MolPayMember();
                transactionRequest.MerchantId = dr["MerchantId"].ToString();
                transactionRequest.MemberId = dr["MemberId"].ToString();
                transactionRequest.MemberName = dr["MemberName"].ToString();
                transactionRequest.SecretKey = dr["SecretKey"].ToString();
                transactionRequest.VerifyKey = dr["VerifyKey"].ToString();
                transactionApiRequests.Add(transactionRequest);
            }

            return transactionApiRequests;

        }
    }
}
