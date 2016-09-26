using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Warehouse
{
    public class WarehouseInventoryDetailBase
    {
        public DataTable Search(string strWhere)
        {
            string sql = "";
            DataTable dt = null;
            try
            {
                sql = "select * from T_WarehouseInventoryDetail";
                if (!string.IsNullOrWhiteSpace(strWhere))
                {
                    sql += " where " + strWhere;
                }
                sql += " order by id";
                dt = DbHelperSQL.Query(sql).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public int Add(WarehouseInventoryDetail wid)
        {
            string sql = "";
            int result = 0;
            try
            {
                sql = @"INSERT INTO T_WarehouseInventoryDetail
           (code
           ,materialCode
           ,materialName
           ,materialModel
           ,materialUnit
           ,curNumber
           ,checkNumber
           ,lostNumber
           ,price
           ,lostMoney
           ,cause
           ,isClear
           ,updateDate
           ,reserved1
           ,reserved2
           ,remark)
     VALUES
           (@code
           ,@materialCode
           ,@materialName
           ,@materialModel
           ,@materialUnit
           ,@curNumber
           ,@checkNumber
           ,@lostNumber
           ,@price
           ,@lostMoney
           ,@cause
           ,@isClear
           ,@updateDate
           ,@reserved1
           ,@reserved2
           ,@remark)";
                SqlParameter[] sps =
                {
                new SqlParameter("@code",wid.code),
                new SqlParameter("@materialCode",wid.materialCode),
                new SqlParameter("@materialName",wid.materialName),
                new SqlParameter("@materialModel",wid.materialModel),
                new SqlParameter("@materialUnit",wid.materialUnit),
                new SqlParameter("@curNumber",wid.curNumber),
                new SqlParameter("@checkNumber",wid.checkNumber),
                new SqlParameter("@lostNumber",wid.lostNumber),
                new SqlParameter("@price",wid.price),
                new SqlParameter("@lostMoney",wid.lostMoney),
                new SqlParameter("@cause",wid.cause),
                new SqlParameter("@isClear",wid.isClear),
                new SqlParameter("@updateDate",wid.updateDate),
                new SqlParameter("@reserved1",wid.reserved1),
                new SqlParameter("@reserved2",wid.reserved2),
                new SqlParameter("@remark",wid.remark)
            };
                result = DbHelperSQL.ExecuteSql(sql, sps);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
