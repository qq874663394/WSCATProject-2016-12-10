using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BaseLayer;
using HelperUtility.Encrypt;
using System.Data;
using HelperUtility;
using UpdateManagerLayer;

namespace LogicLayer
{
    public class WarehouseInDetailLogic
    {
        private WarehouseInDetailBase wdb = new WarehouseInDetailBase();
        /// <summary>
        /// 新增一条数据 
        /// </summary>
        public int InsertWarehouseInDetailTable(WarehouseInDetail model)
        {
            int result = 0;
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseInDetail",
                operationTime = DateTime.Now,
                objective = "新增入库商品详情",
                operationContent = "新增T_WarehouseInDetail表的数据"
            };
            if (model != null)
            {
                result = wdb.Add(model);
                if (result > 0)
                {
                    logModel.result = 1;
                }
                else
                {
                    logModel.result = 0;
                }
            }
            else
            {
                logModel.result = 0;
                result = -7;
            }
            lb.Add(logModel);
            return result;
        }

        /// <summary>
        /// 根据code删除表里数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int deleteInDetailTable(string code)
        {
            int result = 0;
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseInDetail",
                operationTime = DateTime.Now,
                objective = "删除入库商品详情",
                operationContent = "删除T_WarehouseInDetail表的数据,条件为:code=" + code
            };
            if (!string.IsNullOrWhiteSpace(code))
            {
                WarehouseInDetailBase warehouseInDetailBase = new WarehouseInDetailBase();
                result = warehouseInDetailBase.deleteByCode(code);
                if (result > 0)
                {
                    logModel.result = 1;
                }
                else
                {
                    logModel.result = 0;
                }
            }
            else
            {
                logModel.result = 0;
                result = -7;
            }
            lb.Add(logModel);
            return result;
        }
        /// <summary>
        /// 根据where条件获取数据列表
        /// </summary>
        /// <param name="strWhere">where条件</param>
        /// <returns></returns>
        public DataSet getList(string strWhere)
        {
            WarehouseInDetailBase warehouseInDetailBase = new WarehouseInDetailBase();
            DataSet ds = null;
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseInDetail",
                operationTime = DateTime.Now,
                objective = "查询入库商品详情",
                operationContent = "查询T_WarehouseInDetail表的数据,条件为:" + strWhere
            };
            try
            {
                ds = warehouseInDetailBase.getList(strWhere);
                logModel.result = 1;
            }
            catch (Exception ex)
            {
                logModel.result = 0;
                throw ex;
            }
            lb.Add(logModel);
            return ds;
        }
        /// <summary>
        /// 根据主单code获取数据列表
        /// </summary>
        /// <param name="mainCode">主单code</param>
        /// <returns></returns>
        public DataSet getListByMainCode(string mainCode)
        {
            WarehouseInDetailBase warehouseInDetailBase = new WarehouseInDetailBase();
            DataSet ds = null;
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseInDetail",
                operationTime = DateTime.Now,
                objective = "根据主单code查询入库商品详情",
                operationContent = "根据主单code查询T_WarehouseInDetail表的数据,条件为:mainCode=" + mainCode
            };
            if (!string.IsNullOrWhiteSpace(mainCode))
            {
                try
                {
                    ds = warehouseInDetailBase.getListByMainCode(mainCode);
                    logModel.result = 1;
                }
                catch (Exception ex)
                {
                    logModel.result = 0;
                    throw ex;
                }
            }
            else
            {
                logModel.result = 0;
                throw new Exception("-7");
            }
            lb.Add(logModel);
            return ds;
        }
        /// <summary>
        /// 根据code来更新入库状态
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int updateByCode(string code)
        {
            int result = 0;
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseInDetail",
                operationTime = DateTime.Now,
                objective = "修改入库商品详情",
                operationContent = "修改T_WarehouseInDetail表的数据,条件为:code=" + code
            };
            
            if (!string.IsNullOrWhiteSpace(code))
            {
                try
                {
                    result = wdb.updateByCode(code);
                    logModel.result = 1;
                }
                catch (Exception ex)
                {
                    logModel.result = 0;
                    throw ex;
                }
            }
            else
            {
                logModel.result = 0;
                throw new Exception("-7");
            }
            lb.Add(logModel);
            return result;
        }
    }
}