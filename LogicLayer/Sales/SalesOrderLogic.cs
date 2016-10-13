using BaseLayer;
using BaseLayer.Sales;
using HelperUtility;
using Model;
using Model.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateManagerLayer;

namespace LogicLayer.Sales
{
    public class SalesOrderLogic
    {
        SalesOrderBase _dal = new SalesOrderBase();
        SalesUpdataManager _upManager = new SalesUpdataManager();
        LogBase _logDal = new LogBase();
        /// <summary>
        /// 事务操作
        /// </summary>
        /// <returns>返回结果,不等于null成功</returns>
        public object AddOrUpdate(SalesOrder model, List<SalesOrderDetail> modelDetail)
        {
            object result = null;
            Log logModel = new Log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_SalesOrder/T_SalesOrderDetail",
                operationTime = DateTime.Now
            };

            WarehouseInBase warehouseInBase = new WarehouseInBase();
            try
            {
                if (model == null || modelDetail == null)
                {
                    throw new Exception("-2");
                }
                if (_dal.Exists(model.code) == false)
                {
                    result = _dal.AddSalesOrToDetail(model, modelDetail);
                    logModel.objective = "新增销售订单,新增销售订单详情";
                    logModel.operationContent = "新增T_SalesOrder和T_SalesOrderDetail表的数据";
                }
                else
                {
                    result = _dal.UpdateSalesOrToDetail(model, modelDetail);
                    logModel.objective = "修改销售订单,修改销售订单详情";
                    logModel.operationContent = "修改T_SalesOrder和T_SalesOrderDetail表的数据";
                }
                if (result == null)
                {
                    throw new Exception("-3");
                }
                logModel.result = 1;
                _upManager.add(model.code, logModel.operationTable, modelDetail.Count + 1, "", logModel.operationTime);
            }
            catch (Exception ex)
            {
                logModel.result = 0;
                throw ex;
            }
            finally
            {
                _logDal.Add(logModel);
            }
            return result;
        }
        public DataTable GetSalesJoinSearch(string clientcode)
        {
            return _dal.GetSalesJoinSearch(clientcode);
        }
        public DataTable GetSalesDetailJoinSearch()
        {
            return _dal.GetSalesDetailJoinSearch();
        }
        public DataTable GetSelectedDetail(string salesCode, string salesDetailCode)
        {
            return _dal.GetSelectedDetail(salesCode, salesDetailCode);
        }
    }
}
