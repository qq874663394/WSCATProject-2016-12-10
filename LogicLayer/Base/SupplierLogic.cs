using BaseLayer;
using BaseLayer.Base;
using HelperUtility;
using HelperUtility.Encrypt;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Base
{
    public class SupplierLogic
    {
        SupplierBase sb = new SupplierBase();
        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns>所有数据以DataTable的形式返回</returns>
        public DataTable SelSupplierTable()
        {
            DataTable dt = null;
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_BaseSupplier",
                operationTime = DateTime.Now,
                objective = "查询供应商信息",
                operationContent = "查询T_BaseSupplier表的数据成功"
            };
            try
            {
                dt = sb.SelSupplierTable();
                logModel.result = 1;
            }
            catch (Exception ex)
            {
                logModel.result = 0;
                throw ex;
            }
            lb.Add(logModel);
            return dt;
        }
        public DataTable GetList(string strWhere)
        {
            DataTable dt = null;
            LogBase lb = new LogBase();
            log model = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_BaseSupplier",
                operationTime = DateTime.Now,
                objective = "查询员工信息",
                operationContent = "查询T_BaseSupplier表的所有数据,条件:" + strWhere
            };
            try
            {
                dt = sb.GetList(strWhere);
                model.result = 1;
            }
            catch (Exception ex)
            {
                model.result = 0;
                throw ex;
            }
            lb.Add(model);
            return dt;
        }
    }
}
