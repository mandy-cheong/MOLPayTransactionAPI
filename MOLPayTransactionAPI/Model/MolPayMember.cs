using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOLPayTransactionAPI.Model
{
    public class MolPayMember
    {
        public string MemberId { get; set; }
        public string MemberName { get; set; }

        public string MerchantId { get; set; }
        public string  VerifyKey { get; set; }
        public string  SecretKey { get; set; }
    }
}
