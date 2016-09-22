using Model;
using System;
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