using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BaseLayer
{
    public class PurchaseBase
    {
        /// <summary>
        /// 获取入库仓库的商品列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetList(string strWhere)
        {
            string sql = "";
            DataTable dt = null;
            try
            {
                sql = "select * from T_PurchaseMain";
                if (!string.IsNullOrWhiteSpace(strWhere))
                {
                    sql += string.Format(" where {0}", strWhere);
                }
                dt = DbHelperSQL.Query(sql).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}
