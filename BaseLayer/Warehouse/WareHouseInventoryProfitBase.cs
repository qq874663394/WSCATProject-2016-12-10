using Model.Warehouse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Warehouse
{
    public class WareHouseInventoryProfitBase
    {
        public DataTable Search(string strWhere)
        {
            string sql = "";
            DataTable dt = null;
            try
            {
                sql = "SELECT id FROM T_WareHouseInventoryProfit";
                if (!string.IsNullOrWhiteSpace(strWhere))
                {
                    sql += " where " + strWhere;
                }
                dt = DbHelperSQL.Query(sql).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public int Add(WarehouseInventoryProfit Model)
        {
            string sql = "";
            int result = 0;
            try
            {
               sql = @"INSERT INTO T_WarehouseInventoryProfit
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
               , @updatetime)";
                SqlParameter[] sps =
                {
                new SqlParameter("@code",Model.code),
                new SqlParameter("@type",Model.type),
                new SqlParameter("@date",Model.date),
                new SqlParameter("@checkState",Model.checkState),
                new SqlParameter("@operation",Model.operation),
                new SqlParameter("@makeMan",Model.makeMan),
                new SqlParameter("@examine",Model.makeMan),
                new SqlParameter("@isClear",Model.isClear),
                new SqlParameter("@remark",Model.remark),
                new SqlParameter("@reserved1",Model.reserved1),
                new SqlParameter("@reserved2",Model.reserved2),
                new SqlParameter("@updatetime",Model.updatetime)
                };
                result = DbHelperSQL.ExecuteSql(sql, sps);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public int Modify(WarehouseInventoryProfit Model)
        {
            string sql = "";
            int result = 0;
            try
            {
                sql = @"UPDATE T_WarehouseInventoryProfit
                SET type = @type
                , date = @date
                , checkState = @checkState
                , operation = @operation
                , makeMan = @makeMan
                , examine = @examine
                , isClear = @isClear
                , remark = @remark
                , reserved1 = @reserved1
                , reserved2 = @reserved2
                , updatetime = @updatetime
                WHERE  code = @code";
                SqlParameter[] sps =
                {
                    new SqlParameter("@code",Model.code),
                    new SqlParameter("@type",Model.type),
                    new SqlParameter("@date",Model.date),
                    new SqlParameter("@checkState",Model.checkState),
                    new SqlParameter("@operation",Model.operation),
                    new SqlParameter("@makeMan",Model.makeMan),
                    new SqlParameter("@examine",Model.makeMan),
                    new SqlParameter("@isClear",Model.isClear),
                    new SqlParameter("@remark",Model.remark),
                    new SqlParameter("@reserved1",Model.reserved1),
                    new SqlParameter("@reserved2",Model.reserved2),
                    new SqlParameter("@updatetime",Model.updatetime)
                };
                result = DbHelperSQL.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
