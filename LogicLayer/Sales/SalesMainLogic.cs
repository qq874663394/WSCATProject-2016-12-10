using BaseLayer;
using BaseLayer.Sales;
using HelperUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Sales
{
    public class SalesMainLogic
    {
        private SalesMainBase smb = new SalesMainBase();
        public DataTable GetList(int fieldName, string fieldValue)
        {
            string strWhere = "";
            DataTable dt = null;
            LogBase lb = new LogBase();
            Log model = new Log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_SalesMain",
                operationTime = DateTime.Now,
                objective = "查询采购单信息"
            };
            try
            {
                switch (fieldName)
                {
                    case 0:
                        strWhere += string.Format("clientCode='{0}'", fieldValue);
                        break;
                }
                model.operationContent = "查询T_SalesMain表的数据,条件：where="+strWhere;
                dt = smb.GetList(strWhere);
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
        public DataTable GetTableByClientCode(string clientCode)
        {
            DataTable dt = null;
            LogBase lb = new LogBase();
            Log model = new Log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_SalesMain",
                operationTime = DateTime.Now,
                objective = "查询采购单信息"
            };
            try
            {
                model.operationContent = "查询T_SalesMain表的数据,条件：clientCode=" + clientCode;
                dt = smb.GetTableByClientCode(clientCode);
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
