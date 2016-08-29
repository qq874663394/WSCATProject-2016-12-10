using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Model;

namespace BaseLayer
{
    /// <summary>
    /// 数据访问类:T_WarehouseIn
    /// </summary>
    public partial class WarehouseInBase
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(WarehouseIn model)
        {
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append("insert into T_WarehouseIn(");
                strSql.Append("code,goodsCode,defaultType,type,stock,operation,examine,remark,reserved1,reserved2,isClear,updateDate,state,date,purchaseCode,checkState)");
                strSql.Append(" values (");
                strSql.Append("@code,@goodsCode,@defaultType,@type,@stock,@operation,@examine,@remark,@reserved1,@reserved2,@isClear,@updateDate,@state,@date,@purchaseCode,@checkState)");
                strSql.Append(";select @@IDENTITY");
            }
            catch
            {
                return -1;
            }
            SqlParameter[] parameters = new SqlParameter[14];
            try
            {
                parameters[0] = new SqlParameter("@code", SqlDbType.NVarChar, 45);
                parameters[1] = new SqlParameter("@goodsCode", SqlDbType.NVarChar, 80);
                parameters[2] = new SqlParameter("@defaultType", SqlDbType.NVarChar, 30);
                parameters[3] = new SqlParameter("@type", SqlDbType.NVarChar, 30);
                parameters[4] = new SqlParameter("@stock", SqlDbType.NVarChar, 40);
                parameters[5] = new SqlParameter("@operation", SqlDbType.NVarChar, 40);
                parameters[6] = new SqlParameter("@examine", SqlDbType.NVarChar, 40);
                parameters[7] = new SqlParameter("@remark", SqlDbType.NVarChar, 400);
                parameters[8] = new SqlParameter("@reserved1", SqlDbType.NVarChar, 50);
                parameters[9] = new SqlParameter("@reserved2", SqlDbType.NVarChar, 50);
                parameters[10] = new SqlParameter("@isClear", SqlDbType.Int, 4);
                parameters[11] = new SqlParameter("@updateDate", SqlDbType.DateTime);
                parameters[12] = new SqlParameter("@state", SqlDbType.Int, 4);
                parameters[13] = new SqlParameter("@date", SqlDbType.DateTime);
                parameters[14] = new SqlParameter("@purchaseCode", SqlDbType.NVarChar, 45);
                parameters[15] = new SqlParameter("@checkState", SqlDbType.Int, 4);
            }
            catch
            {
                return -2;
            }
            try
            {
                parameters[0].Value = model.code;
                parameters[1].Value = model.GoodsCode;
                parameters[2].Value = model.DefaultType;
                parameters[3].Value = model.type;
                parameters[4].Value = model.stock;
                parameters[5].Value = model.operation;
                parameters[6].Value = model.examine;
                parameters[7].Value = model.remark;
                parameters[8].Value = model.reserved1;
                parameters[9].Value = model.reserved2;
                parameters[10].Value = model.isClear;
                parameters[11].Value = model.updateDate;
                parameters[12].Value = model.state;
                parameters[13].Value = model.date;
                parameters[14].Value = model.purchaseCode;
                parameters[15].Value = model.checkState;
            }
            catch
            {
                return -3;
            }
            int result = DbHelperSQL.GetSingle(strSql.ToString(), parameters);

            return result;
        }

        public int deleteByCode(string code)
        {
            string deleteStr = "delete T_WarehouseIn where code = '" + code + "'";
            int result = DbHelperSQL.ExecuteSql(deleteStr);
            return result;
        }
	}
}

