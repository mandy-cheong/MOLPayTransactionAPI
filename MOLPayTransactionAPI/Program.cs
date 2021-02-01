using MOLPayTransactionAPI.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOLPayTransactionAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            //var sdate = new DateTime(2020, 12, 1);
            //var edate = new DateTime(2021, 1, 16);

            //while (sdate <= edate)
            //{
            var sdate = DateTime.Now.Date;
            Console.WriteLine($"running date :" + sdate.ToString());
            var molpayService = new MOLPayService();
            molpayService.GetTransactions(sdate);
            Console.WriteLine($"completed date :" + sdate.ToString());
            sdate = sdate.AddDays(1);
            //}
        }
    }
}
