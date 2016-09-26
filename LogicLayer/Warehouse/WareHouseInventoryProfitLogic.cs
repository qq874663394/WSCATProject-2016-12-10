using BaseLayer;
using BaseLayer.Warehouse;
using HelperUtility;
using Model;
using Model.Warehouse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public object Add(WarehouseInventoryProfit warehouseInventoryProfit, List<WarehouseInventoryProfitDetail> warehouseInventoryProfitDetail)
        {
            List<SqlParameter[]> list = new List<SqlParameter[]>();
            Hashtable hashTable = new Hashtable();
            object result = null;
            string sqlDetail = "";
            string sqlMain = "";
            try
            {
                sqlMain = @"INSERT INTO T_WareHouseInventoryProfit
               (code
               , type
               , date
               , checkState
               , operation
               , makeMan
               , examine
               , isClear
               , remark
               , reserved1
               , reserved2
               , updatetime)
                VALUES
               (@code
               , @type
               , @date
               , @checkState
               , @operation
               , @makeMan
               , @examine
               , @isClear
               , @remark
               , @reserved1
               , @reserved2
               , @updatetime);select scope_identity()";
                SqlParameter[] spsMain =
                {
                new SqlParameter("@code",warehouseInventoryProfit.code),
                new SqlParameter("@type",warehouseInventoryProfit.type),
                new SqlParameter("@date",warehouseInventoryProfit.date),
                new SqlParameter("@checkState",warehouseInventoryProfit.checkState),
                new SqlParameter("@operation",warehouseInventoryProfit.operation),
                new SqlParameter("@makeMan",warehouseInventoryProfit.makeMan),
                new SqlParameter("@examine",warehouseInventoryProfit.makeMan),
                new SqlParameter("@isClear",warehouseInventoryProfit.isClear),
                new SqlParameter("@remark",warehouseInventoryProfit.remark),
                new SqlParameter("@reserved1",warehouseInventoryProfit.reserved1),
                new SqlParameter("@reserved2",warehouseInventoryProfit.reserved2),
                new SqlParameter("@updatetime",warehouseInventoryProfit.updatetime)
                };
                hashTable.Add(sqlMain, spsMain);
                sqlDetail = @"INSERT INTO T_WarehouseInventoryProfitDetail
           (code
           ,mainCode
           ,barCode
           ,materialDaima
           ,materialCode
           ,materialName
           ,materialModel
           ,materiaUnit
           ,warehouseCode
           ,warehouseName
           ,price
           ,number
           ,inventoryNumber
           ,profitNumber
           ,profitMoney
           ,productionDate
           ,qualityDate
           ,effectiveDate
           ,isClear
           ,updateDate
           ,reserved1
           ,reserved2)
     VALUES
           (@code
           ,@mainCode
           ,@barCode
           ,@materialDaima
           ,@materialCode
           ,@materialName
           ,@materialModel
           ,@materiaUnit
           ,@warehouseCode
           ,@warehouseName
           ,@price
           ,@number
           ,@inventoryNumber
           ,@profitNumber
           ,@profitMoney
           ,@productionDate
           ,@qualityDate
           ,@effectiveDate
           ,@isClear
           ,@updateDate
           ,@reserved1
           ,@reserved2";

                foreach (var item in warehouseInventoryProfitDetail)
                {
                    SqlParameter[] spsDetail =
                    {
                        new SqlParameter("@code",item.code),
                        new SqlParameter("@mainCode",item.mainCode),
                        new SqlParameter("@barCode",item.barCode),
                        new SqlParameter("@materialDaima",item.materialDaima),
                        new SqlParameter("@materialCode",item.materialCode),
                        new SqlParameter("@materialName",item.materialName),
                        new SqlParameter("@materialModel",item.materialModel),
                        new SqlParameter("@materiaUnit",item.materiaUnit),
                        new SqlParameter("@warehouseCode",item.warehouseCode),
                        new SqlParameter("@warehouseName",item.warehouseName),
                        new SqlParameter("@price",item.price),
                        new SqlParameter("@number",item.number),
                        new SqlParameter("@inventoryNumber",item.inventoryNumber),
                        new SqlParameter("@lossNumber",item.profitNumber),
                        new SqlParameter("@lossMoney",item.profitMoney),
                        new SqlParameter("@productionDate",item.productionDate),
                        new SqlParameter("@qualityDate",item.qualityDate),
                        new SqlParameter("@effectiveDate",item.effectiveDate),
                        new SqlParameter("@isClear",item.isClear),
                        new SqlParameter("@updateDate",item.updateDate),
                        new SqlParameter("@reserved1",item.reserved1),
                        new SqlParameter("@reserved2",item.reserved2)
                    };
                    list.Add(spsDetail);
                }
                result = BaseLayer.DbHelperSQL.ExecuteSqlTranScalar(hashTable, sqlDetail, list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public int Modify(WarehouseInventoryProfit warehouseInventoryProfit, List<WarehouseInventoryProfitDetail> warehouseInventoryProfitDetail)
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
