using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Warehouse
{
    public class WarehouseAdjustPriceBase
    {
        public DataTable Search(string strWhere)
        {
            string sql = "";
            DataTable dt = null;
            sql = "";
            return dt;
        }
        public object Add(WarehouseAdjustPrice warehouseAdjustPrice, List<WarehouseInventoryLossDetail> warehouseInventoryLossDetail)

        {
            List<SqlParameter[]> list = new List<SqlParameter[]>();
            Hashtable hashTable = new Hashtable();
            object result = null;
            string sqlDetail = "";
            string sqlMain = "";
            try
            {
                sqlMain = @"INSERT INTO T_WarehouseInventoryLoss
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
                new SqlParameter("@code",warehouseInventoryLoss.code),
                new SqlParameter("@type",warehouseInventoryLoss.type),
                new SqlParameter("@date",warehouseInventoryLoss.date),
                new SqlParameter("@checkState",warehouseInventoryLoss.checkState),
                new SqlParameter("@operation",warehouseInventoryLoss.operation),
                new SqlParameter("@makeMan",warehouseInventoryLoss.makeMan),
                new SqlParameter("@examine",warehouseInventoryLoss.makeMan),
                new SqlParameter("@isClear",warehouseInventoryLoss.isClear),
                new SqlParameter("@remark",warehouseInventoryLoss.remark),
                new SqlParameter("@reserved1",warehouseInventoryLoss.reserved1),
                new SqlParameter("@reserved2",warehouseInventoryLoss.reserved2),
                new SqlParameter("@updatetime",warehouseInventoryLoss.updatetime)
                };
                hashTable.Add(sqlMain, spsMain);
                sqlDetail = @"INSERT INTO T_WarehouseInventoryLossDetail
           (code
           ,mainCode
           ,barCode
           ,materialDaima
           ,materialCode
           ,materialName
           ,materialModel
           ,materialUnit
           ,warehouseCode
           ,warehouseName
           ,price
           ,number
           ,inventoryNumber
           ,lossNumber
           ,lossMoney
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
           ,@materialUnit
           ,@warehouseCode
           ,@warehouseName
           ,@price
           ,@number
           ,@inventoryNumber
           ,@lossNumber
           ,@lossMoney
           ,@productionDate
           ,@qualityDate
           ,@effectiveDate
           ,@isClear
           ,@updateDate
           ,@reserved1
           ,@reserved2);select SCOPE_IDENTITY();";

                foreach (var item in warehouseInventoryLossDetail)
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
                        new SqlParameter("@materialUnit",item.materialUnit),
                        new SqlParameter("@warehouseCode",item.warehouseCode),
                        new SqlParameter("@warehouseName",item.warehouseName),
                        new SqlParameter("@price",item.price),
                        new SqlParameter("@number",item.number),
                        new SqlParameter("@inventoryNumber",item.inventoryNumber),
                        new SqlParameter("@lossNumber",item.lossNumber),
                        new SqlParameter("@lossMoney",item.lossMoney),
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
                result = DbHelperSQL.ExecuteSqlTranScalar(hashTable, sqlDetail, list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
