﻿using System;
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
        public int UpdateList(Hashtable hashTable, string sql, List<SqlParameter[]> list)
        {
            int result = 0;
            try
            {
                result=DbHelperSQL.ExecuteSqlTran(hashTable, sql, list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
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
        /// 事务新增
        /// </summary>
        /// <param name="hashTable">主表的sql和parameter</param>
        /// <param name="sql">子表sql</param>
        /// <param name="list">子表的parameter</param>
        public int Add(Hashtable hashTable, string sql, List<SqlParameter[]> list)
        {
            int result = 0;
            try
            {
                result = DbHelperSQL.ExecuteSqlTran(hashTable, sql, list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>
        /// 上下一单
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="state">状态:0:下一单,1:上一单</param>
        /// <returns></returns>
        public WarehouseIn GetPreAndNext(int id, int state)
        {
            string sql = "";
            try
            {
                if (state == 0)
                {
                    sql = string.Format("select top 1 * from T_log where id>{0}", id);
                }
                else
                {
                    sql = string.Format("select top 1 * from T_log where id<{0} order by id desc", id);
                }

                SqlDataReader read = DbHelperSQL.ExecuteReader(sql);
                if (read.Read())
                {
                    WarehouseIn wi = new WarehouseIn()
                    {
                        id = Convert.ToInt32(read["id"]),
                        code = read["code"].ToString(),
                        checkState = Convert.ToInt32(read["checkState"]),
                        date = Convert.ToDateTime(read["checkState"]),
                        DefaultType = read["checkState"].ToString(),
                        examine = read["examine"].ToString(),
                        GoodsCode = read["GoodsCode"].ToString(),
                        isClear = Convert.ToInt32(read["isClear"]),
                        makeMan = read["makeMan"].ToString(),
                        operation = read["operation"].ToString(),
                        purchaseCode = read["purchaseCode"].ToString(),
                        remark = read["remark"].ToString(),
                        reserved1 = read["reserved1"].ToString(),
                        reserved2 = read["reserved2"].ToString(),
                        state = Convert.ToInt32(read["state"]),
                        stock = read["stock"].ToString(),
                        supplierCode = read["supplierCode"].ToString(),
                        supplierName = read["supplierName"].ToString(),
                        supplierPhone = read["supplierPhone"].ToString(),
                        type = read["type"].ToString(),
                        updateDate = Convert.ToDateTime(read["updateDate"])
                    };
                    return wi;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        /// <summary>
        /// 判断该客户编号判断是否存在
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool Exists(string code)
        {
            string sql = "";
            try
            {
                sql = string.Format("select count(1) from T_WarehouseIn where code='{0}'",code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return DbHelperSQL.Exists(sql);
        }
    }
}