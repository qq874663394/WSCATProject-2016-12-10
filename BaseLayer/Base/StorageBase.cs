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
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 自定义条件取得列表
        /// </summary>
        /// <param name="strWhere">where后面的条件</param>
        /// <returns></returns>
        public DataTable GetList(string strWhere)
        {
            string sql = "";
            DataSet ds = null;
            try
            {
                sql = "select * from T_BaseStorage";
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