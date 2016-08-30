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
            try
            {
                sql = string.Format("SELECT * FROM T_BaseStorage order by id");
            }
            catch
            {
                throw new Exception("-1");
            }
            DataSet ds = null;
            try
            {
                SqlDataAdapter dapter = new SqlDataAdapter(sql, DbHelperSQL.connectionString);
                ds = new DataSet();
                dapter.Fill(ds, "T_Storage");
            }
            catch
            {
                throw new Exception("-2");
            }
            return ds.Tables[0];
        }
    }
}