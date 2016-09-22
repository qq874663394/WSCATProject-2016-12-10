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
                operationTable = "T_WarehouseOut",
                operationTime = DateTime.Now,
                objective = "根据where条件获取出库单列表",
                operationContent = "根据where条件查询表T_WarehouseOut出库单列表，条件strWhere值为" + strWhere
            };
            try
            {
                switch (fieldName)
                {
                    case 0:
                        strWhere += string.Format("code='{0}'", fieldValue);
                        break;
                    case 1:
                        strWhere += string.Format("type='{0}'", fieldValue);
                        break;
                    case 2:
                        strWhere += string.Format("checkState='{0}'", fieldValue);
                        break;
                    case 3:
                        strWhere += string.Format("clientCode = '{0}'", fieldValue);
                        break;
                }
                dt = wob.GetList(strWhere);
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

        public int Add(WarehouseOut wo)
        {
            return 0;
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
