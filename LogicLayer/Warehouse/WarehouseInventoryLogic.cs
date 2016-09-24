using BaseLayer;
using BaseLayer.Warehouse;
using HelperUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Warehouse
{
    public class WarehouseInventoryLogic
    {
        WarehouseInventoryBase bs = new WarehouseInventoryBase();
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetList(int fieldName, string fieldValue)
        {
            DataTable dt = null;
            string strWhere = "";
            LogBase lb = new LogBase();
            log model = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseInventory",
                operationTime = DateTime.Now,
                objective = "查询盘点列表"
            };
            try
            {
                switch (fieldName)
                {
                    case 0:
                        strWhere += string.Format("code='{0}'", fieldValue);
                        break;
                    case 1:
                        strWhere += string.Format("stockName='{0}'", fieldValue);
                        break;
                    case 2:
                        strWhere += string.Format("checkMan='{0}'", fieldValue);
                        break;
                    case 3:
                        strWhere += string.Format("stockCode='{0}'", fieldValue);
                        break;
                }
                model.operationContent = "查询T_WarehouseInventory表数据,条件：where " + strWhere;
                dt = bs.GetList(strWhere).Tables[0];
                model.result = 1;
                return dt;
            }
            catch (Exception ex)
            {
                model.result = 0;
                throw ex;
            }
            finally
            {
                lb.Add(model);
            }
        }
    }
}
