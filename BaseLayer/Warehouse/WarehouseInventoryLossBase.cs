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
    public class WarehouseInventoryLossBase
    {
        public DataTable Search(string strWhere)
        {
            string sql = "";
            DataTable dt = null;
            try
            {
                sql = "select * from T_WarehouseInventoryloss";
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

        public int Add(WarehouseInventoryLoss wil)
        {
            string sql = "";
            int result = 0;
            try
            {
                sql = @"INSERT INTO T_WarehouseInventoryLoss
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
                new SqlParameter("@code",wil.code),
                new SqlParameter("@type",wil.type),
                new SqlParameter("@date",wil.date),
                new SqlParameter("@checkState",wil.checkState),
                new SqlParameter("@operation",wil.operation),
                new SqlParameter("@makeMan",wil.makeMan),
                new SqlParameter("@examine",wil.makeMan),
                new SqlParameter("@isClear",wil.isClear),
                new SqlParameter("@remark",wil.remark),
                new SqlParameter("@reserved1",wil.reserved1),
                new SqlParameter("@reserved2",wil.reserved2),
                new SqlParameter("@updatetime",wil.updatetime)
                };
                result = DbHelperSQL.ExecuteSql(sql, sps);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public int Modify(WarehouseInventoryLoss wil)
        {
            string sql = "";
            int result = 0;
            try
            {
                sql = @"UPDATE T_WarehouseInventoryLoss
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
                    new SqlParameter("@code",wil.code),
                    new SqlParameter("@type",wil.type),
                    new SqlParameter("@date",wil.date),
                    new SqlParameter("@checkState",wil.checkState),
                    new SqlParameter("@operation",wil.operation),
                    new SqlParameter("@makeMan",wil.makeMan),
                    new SqlParameter("@examine",wil.makeMan),
                    new SqlParameter("@isClear",wil.isClear),
                    new SqlParameter("@remark",wil.remark),
                    new SqlParameter("@reserved1",wil.reserved1),
                    new SqlParameter("@reserved2",wil.reserved2),
                    new SqlParameter("@updatetime",wil.updatetime)
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
