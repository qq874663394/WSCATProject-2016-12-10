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
            try
            {
                string sql = "select * from T_PurchaseMain po,T_PurchaseDetail pd where po.code=pd.PurchaseCode";
                SqlDataAdapter dapter = new SqlDataAdapter(sql, DbHelperSQL.connectionString);
                DataSet ds = new DataSet();
                dapter.Fill(ds, "T_PurchaseOrder");
                return ds.Tables[0];
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
