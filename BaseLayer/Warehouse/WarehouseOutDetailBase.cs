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
            DataSet ds = null;
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append("select *");
                strSql.Append(" FROM T_WarehouseOutDetail ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                ds = DbHelperSQL.Query(strSql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
