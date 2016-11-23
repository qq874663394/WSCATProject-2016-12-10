using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BaseLayer.Base
{
    public class FinanceVoucherEntryBase
    {
        /// <summary>
        /// 获取上一单数据
        /// </summary>
        /// <param name="entryCode"></param>
        /// <returns></returns>
        public DataTable GetLastDetail(string entryCode)
        {
            string sql = "";
            DataTable dt = null;
            sql = string.Format(@"select fvem.code,
fvem.date,
fvem.examine,
fvem.posting,
fvem.makeMan,
fvem.examineState,
fved.code,
fved.summary,
fved.subject,
fved.debitAmount,
fved.creditAmount
 from T_FinanceVoucherEntryMain fvem,T_FinanceVoucherEntryDetail fved
where fvem.code=fved.mainCode and 
fvem.code= (select top 1 code from T_FinanceVoucherEntryMain where id <
(select id from T_FinanceVoucherEntryMain where code = '{0}') order by id desc)", entryCode);
            dt = DbHelperSQL.Query(sql).Tables[0];
            return dt;
        }

        /// <summary>
        /// 默认的上一单数据
        /// </summary>
        /// <param name="entryCode"></param>
        /// <returns></returns>
        public DataTable GetFLastDetail(string entryCode)
        {
            string sql = "";
            DataTable dt = null;
            sql = string.Format(@"select fvem.code,
fvem.date,
fvem.examine,
fvem.posting,
fvem.makeMan,
fvem.examineState,
fved.code,
fved.summary,
fved.subject,
fved.debitAmount,
fved.creditAmount
 from T_FinanceVoucherEntryMain fvem,T_FinanceVoucherEntryDetail fved
where fvem.code=fved.mainCode and 
fvem.code= (select top 1 code from T_FinanceVoucherEntryMain where id =
(select id from T_FinanceVoucherEntryMain where code = '{0}') order by id desc)", entryCode);
            dt = DbHelperSQL.Query(sql).Tables[0];
            return dt;
        }

        /// <summary>
        /// 获取下一单数据
        /// </summary>
        /// <param name="entryCode"></param>
        /// <returns></returns>
        public DataTable GetNextDetail(string entryCode)
        {
            string sql = "";
            DataTable dt = null;
            sql = string.Format(@"select fvem.code,
fvem.date,
fvem.examine,
fvem.posting,
fvem.makeMan,
fvem.examineState,
fved.code,
fved.summary,
fved.subject,
fved.debitAmount,
fved.creditAmount
 from T_FinanceVoucherEntryMain fvem,T_FinanceVoucherEntryDetail fved
where fvem.code=fved.mainCode and 
fvem.code= (select top 1 code from T_FinanceVoucherEntryMain where id >
(select id from T_FinanceVoucherEntryMain where code = '{0}'))", entryCode);
            dt = DbHelperSQL.Query(sql).Tables[0];
            return dt;
        }

        /// <summary>
        /// 获取最新的code
        /// </summary>
        /// <returns></returns>
        public string GetNewCode()
        {
            string sql = "select top 1 code from  T_FinanceVoucherEntryMain order by id desc";
            return DbHelperSQL.GetSingle(sql).ToString();
        }
    }
}
