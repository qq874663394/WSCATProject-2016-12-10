using BaseLayer;
using BaseLayer.Base;
using HelperUtility;
using Model;
using System;
using System.Data;

namespace LogicLayer.Base
{
    public class ClientLogic
    {
        ClientBase cb = new ClientBase();
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
