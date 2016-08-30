using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateManagerLayer
{
    public class WarehouseInDetailUpdataManager
    {
        /// <summary>
        /// 新增入库单的更新信息
        /// </summary>
        /// <param name="updateCode">入库单code</param>
        /// <param name="updateTableName">表名</param>
        /// <param name="reserve1">预留1</param>
        /// <param name="reserve2">预留2</param>
        /// <param name="datetime">更新时间</param>
        /// <returns></returns>
        public int addWarehouseInDetail(string warehouse_Code, string updateCode, string updateTableName,
            int id, string reserve1, string reserve2, DateTime datetime)
        {
            string sqlstr = "";
            try
            {
                sqlstr = string.Format(
                    "insert into T_WarehouseInDetail(warehouseDetail_Code,warehouseDetail_Update_Code," +
                    "warehouseDetail_Update_TableName,warehouseDetail_Update_ID," +
                    "warehouseDetail_Reserve1,warehouseDetail_Reserve2,warehouseDetail_DateTime) values (" +
                    "'{0}','{1}','{2}',{3},'{4}','{5}','{6}')",
                    warehouse_Code, updateCode, updateTableName, id, reserve1, reserve2, datetime);
            }
            catch
            {
                return -1;
            }
            int result = DbHelperSQL.ExecuteSql(sqlstr);

            return result;
        }
    }
}
