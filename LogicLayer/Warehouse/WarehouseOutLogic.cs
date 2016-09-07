using BaseLayer;
using HelperUtility;
using HelperUtility.Encrypt;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Warehouse
{
    public class WarehouseOutLogic
    {
        WarehouseOutBase wob = new WarehouseOutBase();
        /// <summary>
        /// 根据审核状态查询
        /// </summary>
        /// <returns></returns>
        public DataSet GetListToOut(int state)
        {
            DataSet ds = null;

            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseOut",
                operationTime = DateTime.Now,
                objective = "查询出库表",
                operationContent = "根据审核状态查询T_WarehouseOut，条件state值为" + state
            };

            try
            {
                ds = wob.GetListToOut(state);
                logModel.result = 1;
            }
            catch (Exception ex)
            {
                logModel.result = 0;
                throw ex;
            }

            lb.Add(logModel);//rz
            return ds;
        }
        public DataSet GetList(string strWhere)
        {
            DataSet ds = null;

            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseOut/T_SalesMain",
                operationTime = DateTime.Now,
                objective = "根据where条件获取出库单列表",
                operationContent = "根据where条件查询表T_WarehouseOut、T_SalesMain出库单列表，条件strWhere值为" + strWhere
            };

            try
            {
                ds = wob.GetList(strWhere);
                logModel.result = 1;
            }
            catch (Exception ex)
            {
                logModel.result = 0;
                throw ex;
            }

            lb.Add(logModel);//rz
            return ds;
        }

        public int Add(WarehouseOut wo)
        {
            int result = wob.Add(wo);

            //添加日志
            LogBase lb = new LogBase();
            log log = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseOut",
                operationTime = DateTime.Now,
                objective = "添加出库单信息",
                result = result,
            };
            if (result > 0)
            {
                log.operationContent = "添加T_WarehouseOut表数据成功";
                lb.Add(log);
            }
            else
            {
                log.operationContent = "添加T_WarehouseOut表数据失败";
                lb.Add(log);
            }
            return result;
           
        }

        public int update(WarehouseOut wo)
        {
            int result = wob.update(wo);

            //添加日志
            LogBase lb = new LogBase();
            log log = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseOut",
                operationTime = DateTime.Now,
                objective = "根据条件更新出库表信息",
                result = result,
            };
            if (result > 0)
            {
                log.operationContent = "更新T_WarehouseOut表的数据";
                lb.Add(log);
            }
            else
            {
                log.operationContent = "更新T_WarehouseOut表数据失败";
                lb.Add(log);
            }
            return result;
            
        }

        public int update(string field, int state, string code)
        {
            int result = wob.update(field, state, code);

            //添加日志
            LogBase lb = new LogBase();
            log log = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseOut",
                operationTime = DateTime.Now,
                objective = "根据条件更新出库表信息",
                result = result,
            };
            if (result > 0)
            {
                log.operationContent = "更新T_WarehouseOut表的数据成功,code为:" + code;
                lb.Add(log);
            }
            else
            {
                log.operationContent = "更新T_WarehouseOut表数据失败,code为:" + code;
                lb.Add(log);
            }
            return result;
           
        }

        public void update(WarehouseOut warehouseOut, List<WarehouseOutDetail> listModel)
        {
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseOutDetail/T_WarehouseOut",
                operationTime = DateTime.Now,
                objective = "修改入库表和入库明细表",
                operationContent = "修改T_WarehouseOutDetail和T_WarehouseOut表的数据"
            };

            WarehouseInBase warehouseInBase = new WarehouseInBase();
            try
            {
                wob.update(warehouseOut, listModel);
                logModel.result = 1;
            }
            catch (Exception ex)
            {
                logModel.result = 0;//rz
                throw ex;
            }
            lb.Add(logModel);//rz
            
        }

        public int delete(string code)
        {
            int result = wob.delete(code);

            //添加日志
            LogBase lb = new LogBase();
            log log = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseOut",
                operationTime = DateTime.Now,
                objective = "根据条件code删除出库表信息",
                result = result,
            };
            if (result > 0)
            {
                log.operationContent = "删除T_WarehouseOut表的数据,code为:" + code;
                lb.Add(log);
            }
            else
            {
                log.operationContent = "删除T_WarehouseOut表数据失败,code为:" + code;
                lb.Add(log);
            }
            return result;
           
        }
    }
}
