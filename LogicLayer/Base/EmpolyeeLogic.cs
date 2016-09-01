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
    public class EmpolyeeLogic
    {
        EmpolyeeBase eb = new EmpolyeeBase();
        CodingHelper ch = new CodingHelper();
        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns>所有数据以DataTable的形式返回</returns>
        public DataTable SelEmpolyeeTable(bool isflag)
        {
            DataTable dt = null;
            try
            {
                dt = ch.DataTableReCoding(eb.SelEmpolyee(isflag));
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_BaseEmpolyee",
                    operationTime = DateTime.Now,
                    objective = "查询员工信息",
                    result = 1,
                    operationContent = "查询T_BaseEmpolyee表的数据成功"
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
                    operationTable = "T_BaseEmpolyee",
                    operationTime = DateTime.Now,
                    objective = "查询员工信息",
                    result = 1,
                    operationContent = "查询T_BaseEmpolyee表的数据失败"
                };
                lb.Add(log);
                throw ex;
            }
            return dt;
        }
    }
}
