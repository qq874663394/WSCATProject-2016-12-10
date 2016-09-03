using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Base
{
    public class StorageBase
    {
        public DataTable SelStorage()
        {
            string sql = "";
            DataSet ds = null;
            try
            {
                sql = string.Format("SELECT * FROM T_BaseStorage order by id");
                ds = DbHelperSQL.Query(sql);
            }
            catch
            {
                throw new Exception("-1");
            }
            return ds.Tables[0];
        }
        public DataTable GetList(string strWhere)
        {
            string sql = "";
            DataSet ds = null;
            try
            {
                sql = "select * from T_WarehouseMain";
                if (strWhere.Trim() != "")
                {
                    sql += " where " + strWhere;
                }
                ds = DbHelperSQL.Query(sql);
            }
            catch
            {
                throw new Exception("-1");
            }
            return ds.Tables[0];
        }
    }
}