using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Model;
using System.Collections.Generic;
using System.Collections;

namespace BaseLayer
{
    /// <summary>
    /// 数据访问类:T_WarehouseIn
    /// </summary>
    public partial class WarehouseInBase
    {
        /// <summary>
        /// 根据审核状态查询
        /// </summary>
        /// <returns></returns>
        public DataSet GetListToIn(int state)
        {
            string sql = "";
            DataSet ds = null;
            try
            {
                sql = string.Format("select code,defaultType,purchaseCode as mainCode,date,state,operation,examine from T_WarehouseIn where checkState={0}", state);
                ds = DbHelperSQL.Query(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        /// <summary>
        /// 事务修改
        /// </summary>
        /// <param name="hashTable">主表的sql和parameter</param>
        /// <param name="sql">子表sql</param>
        /// <param name="list">子表的parameter</param>
        public void UpdateList(Hashtable hashTable, string sql, List<SqlParameter[]> list)
        {
            try
            {
                DbHelperSQL.ExecuteSqlTran(hashTable, sql, list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append("select id,code,goodsCode,defaultType,type,stock,operation,examine,state,date,purchaseCode,checkState,isClear,updateDate,reserved1,reserved2,remark ");
                strSql.Append(" FROM T_WarehouseIn ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
            }
            catch
            {
                throw new Exception("-1");
            }
            DataSet ds = null;
            try
            {
                ds = DbHelperSQL.Query(strSql.ToString());
            }
            catch
            {
                throw new Exception("-2");
            }
            return ds;
        }

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
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int deleteByCode(string code)
        {
            string deleteStr = "";
            int result = 0;
            try
            {
                deleteStr = "delete T_WarehouseIn where code = '" + code + "'";
            }
            catch
            {
                throw new Exception("-1");
            }
            try
            {
                result = DbHelperSQL.ExecuteSql(deleteStr);
            }
            catch
            {
                throw new Exception("-2");
            }
            return result;
        }
        /// <summary>
        /// 根据code更新某一条数据
        /// </summary>
        /// <param name="wi"></param>
        /// <returns></returns>
        public int update(WarehouseIn wi)
        {
            string strSql = "";
            try
            {
                strSql = string.Format("update T_WarehouseIn set type='{0}',goodsCode='{1}'," +
                    "defaultType='{2}',stock='{3}',operation='{4}',examine='{5}',state={6},date='{7}'," +
                    "purchaseCode='{8}',checkState={9},isClear={10},updateDate='{11}',reserved1='{12}'," +
                    "reserved2='{13}',remark='{14}' where code='{15}'",
                    wi.type,
                    wi.GoodsCode,
                    wi.DefaultType,
                    wi.stock,
                    wi.operation,
                    wi.examine,
                    wi.state,
                    wi.date,
                    wi.purchaseCode,
                    wi.checkState,
                    wi.isClear,
                    wi.updateDate,
                    wi.reserved1,
                    wi.reserved2,
                    wi.remark,
                    wi.code);
            }
            catch
            {
                return -1;
            }
            try
            {
                int result = DbHelperSQL.ExecuteSql(strSql);
                return result;
            }
            catch
            {
                return -2;
            }
        }
        /// <summary>
        /// 修改审核状态
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int updateByCode(string code)
        {
            string sql = "";
            int result = 0;
            try
            {
                sql = string.Format("update from T_WarehouseIn set checkState=1 where code='{0}'", code);
                result = DbHelperSQL.ExecuteSql(sql);
            }
            catch
            {
                return -1;
            }
            return result;
        }
        public DataTable GetPre(int id)
        {
            string sql = "";
            DataTable dt = null;
            sql = string.Format("select top 1 * from T_log where id>{0}",id);
            dt = DbHelperSQL.Query(sql).Tables[0];
            return dt;
        }
    }
}