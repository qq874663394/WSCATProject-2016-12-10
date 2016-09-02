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
            try
            {
                dt = sb.SelSupplierTable();
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_BaseSupplier",
                    operationTime = DateTime.Now,
                    objective = "查询供应商信息",
                    result = 1,
                    operationContent = "查询T_BaseSupplier表的数据成功"
                };
                lb.Add(log);
            }
            catch (Exception ex)
            {
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_BaseSupplier",
                    operationTime = DateTime.Now,
                    objective = "查询供应商信息",
                    result = 1,
                    operationContent = "查询T_BaseSupplier表的数据失败"
                };
                lb.Add(log);
                throw ex;
            }
            return dt;
        }
    }
}
