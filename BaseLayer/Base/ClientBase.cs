﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace BaseLayer.Base
{
    public class ClientBase
    {
        /// <summary>
        /// 查询所有客户信息
        /// </summary>
        /// <param name="isflag">true显示全部 false显示未禁用</param>
        /// <returns></returns>
        public DataTable GetClientByBool(bool isflag)
        {
            string sql = "";
            DataSet ds = null;
            try
            {
                sql = "select * from T_BaseClient";
                if (isflag == false)
                {
                    sql += " where enable=1 ";
                }
                ds = DbHelperSQL.Query(sql);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }
        /// <summary>
        /// 自定义条件取得列表
        /// </summary>
        /// <param name="strWhere">where后面的条件</param>
        /// <returns></returns>
        public DataTable GetList(string strWhere)
        {
            string sql = "";
            DataSet ds = null;
            try
            {
                sql = "select * from T_BaseClient";
                if (strWhere.Trim() != "")
                {
                    sql += " where " + strWhere;
                }
                ds = DbHelperSQL.Query(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }
    }

}