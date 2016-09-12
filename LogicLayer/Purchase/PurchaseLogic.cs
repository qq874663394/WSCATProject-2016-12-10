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
        /// 获取入库仓库的商品列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetList(string strWhere)
        {
            DataTable dt = null;
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_Purchase",
                operationTime = DateTime.Now,
                objective = "查询采购表",
                operationContent = string.Format("查询T_Purchase表的数据,条件为:", strWhere)
            };
            try
            {
                dt=pb.GetList(strWhere);
                logModel.result = 1;
            }
            catch (Exception ex)
            {
                logModel.result = 0;
                throw ex;
            }
            return dt;
        }
    }
}