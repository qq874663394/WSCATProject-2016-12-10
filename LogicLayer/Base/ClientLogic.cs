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
    public class ClientLogic
    {
        ClientBase cb = new ClientBase();
        /// <summary>
        /// 查询所有客户信息
        /// </summary>
        /// <param name="isflag">true显示全部 false显示未禁用</param>
        /// <returns></returns>
        public DataTable SelClient(bool isflag)
        {
            DataTable dt = null;
            LogBase lb = new LogBase();
            log model = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_Client",
                operationTime = DateTime.Now,
                operationContent = "查询T_Client表的所有数据"
            };
            try
            {
                dt = cb.SelClient(isflag);
                model.objective = "查询客户信息成功";
                model.result = 1;
                lb.Add(model);
            }
            catch(Exception ex)
            {
                model.objective = "查询客户信息失败";
                model.result = 0;
                lb.Add(model);
                throw new Exception(ex.Message);
            }
            return dt;
        }
    }
}
