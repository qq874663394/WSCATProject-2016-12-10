using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Base
{
    public class OrderTypeBase
    {
        public DataTable GetList(string strWhere)
        {
            string sql = "";
            DataSet ds = null;
            try
            {
                sql = "select * from T_BaseOrderType";
                if (strWhere.Trim() != "")
                {
                    sql += " where " + strWhere;
                }
            }
            catch
            {
                throw new Exception("-1");
            }
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, DbHelperSQL.connectionString);
                ds = new DataSet();
                adapter.Fill(ds, "T_BaseOrderType");
            }
            catch
            {
                throw new Exception("-2");
            }
            return ds.Tables[0];
        }
    }
}
