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

namespace LogicLayer
{
    public class WarehouseDetailLogic
    {
        private WarehouseInDetailBase wdb = new WarehouseInDetailBase();
        /// <summary>
        /// 新增一条数据 
        /// </summary>
        public int InsertWarehouseInDetailTable(WarehouseInDetail model)
        {
            int result = wdb.Add(model);
            if(result > 0)
            {
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_WarehouseInDetail",
                    operationTime = DateTime.Now,
                    objective = "新增入库商品详情",
                    result = 1,
                    operationContent = "查询T_WarehouseInDetail表的数据,code为:" + model.code
                };
                lb.Add(log);
            }
            else
            {
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_WarehouseInDetail",
                    operationTime = DateTime.Now,
                    objective = "新增入库商品详情失败",
                    result = 1,
                    operationContent = "新增T_WarehouseInDetail表的数据失败,code为:" + model.code
                };
                lb.Add(log);
            }
            return result;
        }

        /// <summary>
        /// 根据code删除表里数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int deleteInDetailTable(string code)
        {
            if(!string.IsNullOrWhiteSpace(code))
            {
                WarehouseInDetailBase warehouseInDetailBase = new WarehouseInDetailBase();
                int result = warehouseInDetailBase.deleteByCode(code);
                return result;
            }
            else
            {
                return -7;
            }
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
            log log = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseInDetail",
                operationTime = DateTime.Now,
                objective = "查询入库商品详情",
                result = 1,
                operationContent = "查询T_WarehouseInDetail表的数据,条件为:" + strWhere
            };
            try
            {
                ds = warehouseInDetailBase.getList(strWhere);
                log.operationContent = "查询T_WarehouseInDetail表的数据列表,条件为:" + strWhere;
                lb.Add(log);
            }
            catch(Exception ex)
            {
                log.operationContent = "T_WarehouseInDetail表的数据列表查询失败,条件为:" + strWhere;
                lb.Add(log);

                throw ex;
            }

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
            log log = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseInDetail",
                operationTime = DateTime.Now,
                objective = "查询入库商品详情",
                result = 1,
                operationContent = "查询T_WarehouseInDetail表的数据,条件为:" + mainCode
            };
            try
            {
                ds = warehouseInDetailBase.getListByMainCode(mainCode);

                log.operationContent = "根据主表code查询T_WarehouseInDetail表的数据列表,条件为:" + mainCode;
                lb.Add(log);
            }
            catch(Exception ex)
            {
                log.operationContent = "T_WarehouseInDetail表的数据列表查询失败,code为:" + mainCode;
                lb.Add(log);

                throw ex;
            }
            
            return ds;
        }
        /// <summary>
        /// 根据code来更新入库状态
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int updateByCode(string code)
        {
            return wdb.updateByCode(code);
        }
    }
}
