using BaseLayer;
using BaseLayer.Base;
using HelperUtility;
using Model;
using System;
using System.Data;
using UpdateManagerLayer;

namespace LogicLayer.Base
{
    public class ClientLogic
    {
        BaseUpdataManager bum = new BaseUpdataManager();
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
                objective = "查询客户信息",
                operationContent = "查询T_Client表的所有数据,显示全部:" + isflag.ToString()
            };
            try
            {
                dt = cb.SelClient(isflag);
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
        public DataTable GetList(string strWhere)
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
                objective = "查询客户信息",
                operationContent = "查询T_Client表的所有数据,条件:" + strWhere
            };
            try
            {
                dt = cb.GetList(strWhere);
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
