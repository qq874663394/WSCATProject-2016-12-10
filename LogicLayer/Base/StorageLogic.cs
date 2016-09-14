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
            }
            catch (Exception ex)
            {
                log.result = 0;
                throw ex;
            }
            finally
            {
                lb.Add(log);
            }
            return dt;
        }
        public DataTable GetList(int fieldName, string fieldValue)
        {
            string strWhere = "";
            DataTable dt = null;
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_StorageRack",
                operationTime = DateTime.Now,
                objective = "查询仓库信息"
            };
            try
            {
                //if (string.IsNullOrWhiteSpace(fieldValue))
                //{
                //    throw new Exception("-2");
                //}
                switch (fieldName)
                {
                    case 0:
                        strWhere += string.Format("name like '%{0}%'", fieldValue);
                        break;
                    case 1:
                        strWhere += string.Format("address like '%{0}%'", fieldValue);
                        break;
                    case 2:
                        strWhere += string.Format("name = '{0}'", fieldValue);
                        break;
                    case 3:
                        strWhere += string.Format("code='{0}'", fieldValue);
                        break;
                }
                logModel.operationContent = "查询T_Storage表的所有数据,条件:" + strWhere;
                dt = srb.GetList(strWhere);
                logModel.result = 1;
            }
            catch (Exception ex)
            {
                logModel.result = 0;
                throw ex;
            }
            finally
            {
                lb.Add(logModel);
            }
            return dt;
        }
    }
}
