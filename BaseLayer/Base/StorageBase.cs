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
            string sql = string.Format("SELECT * FROM T_BaseStorage order by id");
            SqlDataAdapter dapter = new SqlDataAdapter(sql, DbHelperSQL.connectionString);
            DataSet ds = new DataSet();
            dapter.Fill(ds, "T_Storage");
            return ds.Tables[0];
        }
    }
}