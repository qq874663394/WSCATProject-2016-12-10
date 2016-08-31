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
        public DataTable selectInMaterial()
        {
            string sql = "";
            DataSet ds = null;
            try
            {
                sql = "select * from T_PurchaseMain po,T_PurchaseDetail pd where po.code=pd.PurchaseCode";
            }
            catch
            {
                throw new Exception("-1");
            }
            try
            {
                SqlDataAdapter dapter = new SqlDataAdapter(sql, DbHelperSQL.connectionString);
                ds = new DataSet();
                dapter.Fill(ds, "T_PurchaseOrder");
            }
            catch
            {
                throw new Exception("-2");
            }
            return ds.Tables[0];
        }
    }
}
