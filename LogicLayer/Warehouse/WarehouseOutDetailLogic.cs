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
        public DataSet GetList(int fieldName,string fieldValue)
        {
            DataSet ds = null;
            string strWhere = "";
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseOutDetail",
                operationTime = DateTime.Now,
                objective = "查询出库明细表"
            };
            try
            {
                switch (fieldName)
                {
                    case 0:
                        strWhere += string.Format("mainCode='{0}'",fieldValue);
                        break;
                }
                ds = wodb.GetList(strWhere);
                logModel.result = 1;
            }
            catch (Exception ex)
            {
                logModel.result = 0;
                throw ex;
            }
            finally
            {
                logModel.operationContent = "T_WarehouseOutDetail，strWhere值为" + strWhere;
                lb.Add(logModel);
            }
            return ds;
        }
    }
}
