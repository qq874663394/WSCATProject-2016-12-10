﻿using BaseLayer;
using BaseLayer.Purchase;
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
    public class PurchaseDetailLogic
    {
        PurchaseDetailBase pdl = new PurchaseDetailBase();
        public DataTable GetList(string purchaseCode, string zhujima)
        {
            DataTable dt = null;
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_PurchaseDetail",
                operationTime = DateTime.Now,
                objective = "查询采购明细表",
                operationContent = string.Format("查询T_PurchaseDetail表的数据,条件为:purchaseCode={0},zhujima={1}",purchaseCode,zhujima)
            };
            try
            {
                dt = pdl.GetList(purchaseCode, zhujima);
                logModel.result = 1;
            }
            catch (Exception ex)
            {
                logModel.result = 0;
                throw ex;
            }
            lb.Add(logModel);
            return dt;
        }
        public decimal GetCheckNumber(string purchaseCode, string code)
        {
            decimal result = 0;
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_PurchaseDetail",
                operationTime = DateTime.Now,
                objective = "查询采购明细表",
                operationContent = string.Format("查询T_PurchaseDetail表的数据,条件为:purchaseCode={0},code={1}", purchaseCode, code)
            };
            try
            {
                result = pdl.GetCheckNumber(purchaseCode, code);
                logModel.result = 1;
            }
            catch (Exception ex)
            {
                logModel.result = 0;
                throw ex;
            }
            lb.Add(logModel);
            return result;
        }
    }
}
