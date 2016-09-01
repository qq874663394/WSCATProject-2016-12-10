using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer
{
    public class WarehouseOutDetailBase
    {
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append("select *");
                strSql.Append(" FROM T_WarehouseOutDetail ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
            }
            catch
            {
                throw new Exception("-1");
            }
            DataSet ds = null;
            try
            {
                ds = DbHelperSQL.Query(strSql.ToString());
            }
            catch
            {
                throw new Exception("-2");
            }
            return ds;
        }
    }
}
