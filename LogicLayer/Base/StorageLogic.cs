using BaseLayer;
using BaseLayer.Base;
using HelperUtility;
using Model;
using System;
using System.Data;

namespace LogicLayer.Base
{
    public class StorageLogic
    {
        StorageBase srb = new StorageBase();
        public DataTable SelStorage()
        {
            DataTable dt = null;
            LogBase lb = new LogBase();
            log log = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_StorageRack",
                operationTime = DateTime.Now,
                objective = "查询仓库信息",
                operationContent = "查询T_Storage表的所有数据"
            };
            try
            {
                dt = srb.SelStorage();
                log.result = 1;
                lb.Add(log);
            }
            catch (Exception ex)
            {
                log.result = 0;
                lb.Add(log);
                throw ex;
            }
            return dt;
        }
        public DataTable GetList(string strWhere)
        {
            DataTable dt = null;
            LogBase lb = new LogBase();
            log log = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_StorageRack",
                operationTime = DateTime.Now,
                objective = "查询仓库信息",
                operationContent = "查询T_Storage表的所有数据"
            };
            try
            {
                dt = srb.GetList();
                log.result = 1;
                lb.Add(log);
            }
            catch (Exception ex)
            {
                log.result = 0;
                lb.Add(log);
                throw ex;
            }
            return dt;
        }
    }
}
