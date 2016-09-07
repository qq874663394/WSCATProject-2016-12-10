using BaseLayer;
using HelperUtility;
using HelperUtility.Encrypt;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Warehouse
{
    public class WarehouseOutDetailLogic
    {
        WarehouseOutDetailBase wodb = new WarehouseOutDetailBase();
        public DataSet GetList(string strWhere)
        {
            DataSet ds = null;

            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseOutDetail",
                operationTime = DateTime.Now,
                objective = "查询出库明细表",
                operationContent = "T_WarehouseOutDetail，strWhere值为" + strWhere
            };

            try
            {
                ds = wodb.GetList(strWhere);
                logModel.result = 1;
            }
            catch (Exception ex)
            {
                logModel.result = 0;
                throw ex;
            }

            lb.Add(logModel);//rz
            return ds;
           
        }
    }
}
