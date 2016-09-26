using BaseLayer;
using BaseLayer.Warehouse;
using HelperUtility;
using Model;
using Model.Warehouse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateManagerLayer;

namespace LogicLayer.Warehouse
{
    public class WareHouseInventoryProfitLogic
    {
        WarehouseUpdataManager wum = new WarehouseUpdataManager();
        WareHouseInventoryProfitBase whipb = new WareHouseInventoryProfitBase();
        public DataTable Search(int fieldName, string fieldValue)
        {
            string strWhere = "";
            DataTable dt = null;
            LogBase lb = new LogBase();
            log model = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WareHouseInventoryProfit",
                operationTime = DateTime.Now,
                objective = "新增盘盈信息"
            };
            try
            {
                switch (fieldName)
                {
                    case 0:
                        strWhere += string.Format("code='{0}'", fieldValue);
                        break;
                    case 1:
                        strWhere += string.Format("checkState={0}", fieldValue);
                        break;
                    case 2:
                        strWhere += string.Format("isClear={0}", fieldValue);
                        break;
                }
                dt = whipb.Search(strWhere);
                model.operationContent = "新增T_WarehouseInventoryProfit表的数据,条件为：where=" + strWhere;
                wum.add("", model.operationTable, dt.Rows.Count, "", model.operationTime);
                model.result = 1;
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
            return dt;
        }
        public int Add(WarehouseInventoryProfit Model)
        {
            int result = 0;
            LogBase lb = new LogBase();
            log model = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseInventoryProfit",
                operationTime = DateTime.Now,
                objective = "新增盘盈信息",
                operationContent = "新增T_WarehouseInventoryProfit表的数据,条件为：code=" + Model.code
            };
            try
            {
                if (Model == null)
                {
                    throw new Exception("-2");
                }
                if (whipb.Exists(Model.code) == true)
                {
                    result = Modify(Model);
                }
                else
                {
                    result = whipb.Add(Model);
                }
                result = whipb.Add(Model);
                if (result <= 0)
                {
                    throw new Exception("-3");
                }
                model.result = 1;
                wum.add(Model.code, model.operationTable, result, "", model.operationTime);
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
            return result;
        }
        public int Modify(WarehouseInventoryProfit Model)
        {
            int result = 0;
            LogBase lb = new LogBase();
            log model = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseInventoryProfit",
                operationTime = DateTime.Now,
                objective = "修改盘盈信息",
                operationContent = "修改T_WarehouseInventoryProfit表的数据,条件为：code=" + Model.code
            };
            try
            {
                if (Model == null)
                {
                    throw new Exception("-2");
                }
                if (whipb.Exists(Model.code) == true)
                {
                    result = Modify(Model);
                }
                else
                {
                    result = whipb.Add(Model);
                }
                result = whipb.Add(Model);
                if (result <= 0)
                {
                    throw new Exception("-3");
                }
                model.result = 1;
                wum.add(Model.code, model.operationTable, result, "", model.operationTime);
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
            return result;
        }
    }
}
