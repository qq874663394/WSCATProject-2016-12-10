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
                    sql += " where enable=1 and";
                }
            }
            catch
            {
                throw new Exception("-1");
            }
            try
            {
                ds = DbHelperSQL.Query(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ds.Tables[0];
        }
    }
}
