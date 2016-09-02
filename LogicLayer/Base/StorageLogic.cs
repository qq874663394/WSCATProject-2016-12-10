using BaseLayer;
using BaseLayer.Base;
using HelperUtility;
using HelperUtility.Encrypt;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Base
{
    public class StorageLogic
    {
        StorageBase srb = new StorageBase();
        public DataTable SelStorage()
        {
            DataTable dt = null;
            try
            {
                dt= srb.SelStorage();
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_StorageRack",
                    operationTime = DateTime.Now,
                    objective = "查询仓库信息",
                    result = 1,
                    operationContent = "查询T_Storage表的数据成功"
                };
                lb.Add(log);
            }
            catch(Exception ex)
            {
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_StorageRack",
                    operationTime = DateTime.Now,
                    objective = "查询仓库信息",
                    result = -1,
                    operationContent = "查询T_Storage表的数据失败"
                };
                lb.Add(log);
                throw ex;
            }
            return dt;
        }
    }
}
