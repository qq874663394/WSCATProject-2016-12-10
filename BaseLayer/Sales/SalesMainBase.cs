using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Sales
{
    public class SalesMainBase
    {
        /// <summary>
        /// 复合查询
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetList(string strWhere)
        {
            DataTable dt = null;
            string sql = "select * from T_SalesMain";
            try
            {
                if (!string.IsNullOrWhiteSpace(strWhere))
                {
                    sql += " where " + strWhere;
                }
                dt = DbHelperSQL.Query(sql).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        /// <summary>
        /// 查询id和code列
        /// </summary>
        /// <param name="clientCode">客户code</param>
        /// <returns></returns>
        public DataTable GetTableByClientCode(string clientCode)
        {
            string sql = "";
            DataTable dt = null;
            try
            {
                sql = string.Format("select id,code from T_SalesMain where clientCode='{0}'", clientCode);
                dt = DbHelperSQL.Query(sql).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}