using Model;
using Model.Warehouse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Warehouse
{
    public class WarehouseAllotBase
    {
        public object Add(WarehouseAllot model, List<WarehouseAllotDetail> modelDetail)
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
                new SqlParameter("@code",model.code),
                new SqlParameter("@type",model.type),
                new SqlParameter("@date",model.date),
                new SqlParameter("@checkState",model.checkState),
                new SqlParameter("@operation",model.operation),
                new SqlParameter("@makeMan",model.makeMan),
                new SqlParameter("@examine",model.makeMan),
                new SqlParameter("@isClear",model.isClear),
                new SqlParameter("@remark",model.remark),
                new SqlParameter("@reserved1",model.reserved1),
                new SqlParameter("@reserved2",model.reserved2),
                new SqlParameter("@updatetime",model.updatetime)
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
