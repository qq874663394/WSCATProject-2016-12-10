using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Base
{
    public class ClientBase
    {
        /// <summary>
        /// 查询所有客户信息
        /// </summary>
        /// <param name="isflag">true显示全部 false显示未禁用</param>
        /// <returns></returns>
        public DataTable SelClient(bool isflag)
        {
            string sql = "";
            DataSet ds = null;
            try
            {
                sql = "select * from T_BaseClient";
                if (isflag == false)
                {
                    sql += " where enable=1";
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
                adapter.Fill(ds, "T_BaseClient");
            }
            catch
            {
                throw new Exception("-2");
            }
            return ds.Tables[0];
        }
    }
}
