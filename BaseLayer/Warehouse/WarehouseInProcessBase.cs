using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BaseLayer
{
    public class WarehouseInProcessBase
    {
        public int Add(WarehouseInProcess model)
        {
            string sqlstr = "";
            try
            {
                sqlstr = string.Format(
                    "insert into T_WarehouseInProcess(isClear,code," +
                    "warehouseInDetailCode,createDatetime,operator,operatorMan," +
                    "remark,updateDate" +
                    ") values (" +
                    "{0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                    model.isClear, model.code, model.warehouseInDetailCode,
                    model.createDatetime, model.Operator, model.operatorMan,
                    model.remark, model.updateDate);
            }
            catch
            {
                return -1;
            }
            try
            {
                int result = DbHelperSQL.ExecuteSql(sqlstr);
                return result;
            }
            catch
            {
                return -6;
            }
        }
    }
}
