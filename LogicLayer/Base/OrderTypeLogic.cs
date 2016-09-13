using BaseLayer;
using BaseLayer.Base;
using HelperUtility;
using HelperUtility.Encrypt;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Base
{
    public class OrderTypeLogic
    {
        OrderTypeBase otb = new OrderTypeBase();
        public DataTable GetList(string strWhere)
        {
            DataTable dt = null;
            LogBase lb = new LogBase();
            log model = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_OrderType",
                operationTime = DateTime.Now,
                objective = "查询客户信息",
                operationContent = "查询T_OrderType表的所有数据,条件:" + strWhere
            };
            try
            {
                dt = otb.GetList(strWhere);
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
