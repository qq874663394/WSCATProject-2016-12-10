using System;
using System.Data;

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
                    sql += " where enable=1 ";
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
