using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Warehouse
{
    public class WarehouseInventoryBase
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            DataSet ds = null;
            string sql = "";
            try
            {
                sql = "select * from T_WarehouseInventory";
                if (!string.IsNullOrWhiteSpace(strWhere))
                {
                    strWhere += " where " + sql;
                }
                sql += " order by id";
                ds = DbHelperSQL.Query(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
