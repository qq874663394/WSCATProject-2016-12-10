using BaseLayer;
using HelperUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Purchase
{
    public class PurchaseLogic
    {
        PurchaseBase pb = new PurchaseBase();
        /// <summary>
        /// 复合查询
        /// </summary>
        /// <param name="fieldName">0:模糊name,1:模糊supplierName,2:supplierCode</param>
        /// <param name="fieldValue">条件值</param>
        /// <returns></returns>
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
                operationTable = "T_Purchase",
                operationTime = DateTime.Now,
                objective = "查询采购表"
            };
            try
            {
                switch (fieldName)
                {
                    case 0:
                        strWhere += string.Format("name like '{0}'",fieldValue);
                        break;
                    case 1:
                        strWhere += string.Format("supplierName like '{0}'",fieldValue);
                        break;
                    case 2:
                        strWhere += string.Format("supplierCode='{0}'",fieldValue);
                        break;
                }
                logModel.operationContent = "查询T_Purchase表的所有数据,条件:" + strWhere;
                dt = pb.GetList(strWhere);
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