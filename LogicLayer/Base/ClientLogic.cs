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
        public DataTable GetClientByBool(bool isflag)
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
                dt = cb.GetClientByBool(isflag);
                model.result = 1;
            }
            catch (Exception ex)
            {
                model.result = 0;
                throw ex;
            }
            finally
            {
                lb.Add(model);
            }
            return dt;
        }
        public DataTable GetList(int fieldName,string fieldValue)
        {
            string strWhere = "";
            DataTable dt = null;
            LogBase lb = new LogBase();
            log model = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_Client",
                operationTime = DateTime.Now,
                objective = "查询客户信息"
            };
            try
            {
                switch (fieldName)
                {
                    case 0:
                        strWhere += string.Format("name like '%{0}%'",fieldValue);
                        break;
                    case 1:
                        strWhere += string.Format("cityName like '%{0}%'",fieldValue);
                        break;
                    default:
                        throw new Exception("-7");
                }
                model.operationContent = "查询T_Client表的所有数据,条件:" + strWhere;
                dt = cb.GetList(strWhere);
                model.result = 1;
            }
            catch (Exception ex)
            {
                model.result = 0;
                throw ex;
            }
            finally
            {
                lb.Add(model);
            }
            return dt;
        }
    }
}
