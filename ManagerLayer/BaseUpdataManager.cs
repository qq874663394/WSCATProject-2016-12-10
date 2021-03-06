﻿using BaseLayer;
using HelperUtility;
using HelperUtility.Encrypt;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateManagerLayer
{
    public class BaseUpdataManager
    {
        /// <summary>
        /// 新增管理记录
        /// </summary>
        /// <param name="updateCode">操作的code</param>
        /// <param name="updateTableName">操作的表名</param>
        /// <param name="updateId">受影响的行数</param>
        /// <param name="reserve">备用</param>
        /// <param name="dateTime">操作时间</param>
        /// <returns></returns>
        public int add(string updateCode, string updateTableName, int updateId, string reserve, DateTime dateTime)
        {
            string sqlstr = "";
            int result = 0;
            try
            {
                sqlstr = string.Format(@"insert into T_Base(updateCode,updateTableName,updateId,reserve,dateTime) 
                        values('{0}','{1}',{2},'{3}','{4}')", 
                        XYEEncoding.strCodeHex(updateCode), XYEEncoding.strCodeHex(updateTableName), updateId, XYEEncoding.strCodeHex(reserve), DateTime.Now);
                result = DbHelperSQL.ExecuteSql(sqlstr);
            }
            catch
            {
                throw new Exception("-1");
            }
            return result;
        }
    }
}