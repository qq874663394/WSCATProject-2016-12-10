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
        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns>所有数据以DataTable的形式返回</returns>
        public DataTable SelEmpolyeeTable(bool isflag)
        {
            DataTable dt = null;
            LogBase lb = new LogBase();
            log model = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_BaseEmpolyee",
                operationTime = DateTime.Now,
                objective = "查询员工信息"
            };
            try
            {
                dt = eb.SelEmpolyee(isflag);
                model.result = 1;
                model.operationContent = "查询T_BaseEmpolyee表的数据成功";
                lb.Add(model);
            }
            catch
            {
                model.result = 0;
                model.operationContent = "查询T_BaseEmpolyee表的数据失败";
                lb.Add(model);
            }
            return dt;
        }
    }
}