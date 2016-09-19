﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;
using BaseLayer;
using HelperUtility.Encrypt;
using HelperUtility;
using UpdateManagerLayer;
using System.Collections;
using System.Data.SqlClient;

namespace LogicLayer
{
    public class WarehouseInLogic
    {
        WarehouseUpdataManager wum = new WarehouseUpdataManager();
        /// <summary>
        /// 根据审核状态查询
        /// </summary>
        /// <returns></returns>
        public DataSet GetListToIn(int state)
        {
            WarehouseInBase warehouseInBase = new WarehouseInBase();
            DataSet ds = null;

            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseIn",
                operationTime = DateTime.Now,
                objective = "根据审核状态查询",
                operationContent = "根据审核状态查询入库表,条件为：state=" + state
            };
            try
            {
                ds = warehouseInBase.GetListToIn(state);
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
            return ds;
        }


        /// <summary>
        /// 事务修改
        /// </summary>
        /// <param name="hashTable">主表的sql和parameter</param>
        /// <param name="sql">子表sql</param>
        /// <param name="list">子表的parameter</param>
        public int UpdateList(Hashtable hashTable, string sql, List<SqlParameter[]> list)
        {
            int result = 0;
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseIn",
                operationTime = DateTime.Now,
                objective = "修改入库信息,修改入库商品详情",
                operationContent = "修改T_WarehouseIn和T_WarehouseInDetail表的数据"
            };

            WarehouseInBase warehouseInBase = new WarehouseInBase();
            try
            {
                result=warehouseInBase.UpdateList(hashTable, sql, list);
                logModel.result = 1;
                wum.add("", logModel.operationTable, list.Count, "", logModel.operationTime);
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
            return result;
        }

        /// <summary>
        /// 根据where条件获取数据列表
        /// </summary>
        /// <param name="strWhere">where条件</param>
        /// <returns></returns>
        public DataSet GetList(int fieldName, string fieldValue)
        {
            string strWhere = "";
            WarehouseInBase warehouseInBase = new WarehouseInBase();
            DataSet ds = null;
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseIn",
                operationTime = DateTime.Now,
                objective = "查询入库信息",
                operationContent = "查询T_WarehouseIn表的数据,条件为:" + strWhere
            };
            try
            {
                switch (fieldName)
                {
                    case 1:
                        strWhere += string.Format(" code='{0}'");
                        break;
                    case 2:
                        strWhere += string.Format(" type='{0}'");
                        break;
                }
                ds = warehouseInBase.GetList(strWhere);
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
            return ds;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <returns>返回新增结果,1为成功</returns>
        public int Add(Hashtable hashTable, string sql, List<SqlParameter[]> list)
        {
            int result = 0;
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseIn",
                operationTime = DateTime.Now,
                objective = "新增入库信息,新增入库商品详情",
                operationContent = "新增T_WarehouseIn和T_WarehouseInDetail表的数据"
            };

            WarehouseInBase warehouseInBase = new WarehouseInBase();
            try
            {
                result = warehouseInBase.Add(hashTable, sql, list);
                logModel.result = 1;
                wum.add("", logModel.operationTable, list.Count, "", logModel.operationTime);
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
            return result;
        }

        /// <summary>
        /// 根据code删除一条数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int deleteByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return -7;
            }
            WarehouseInBase warehouseInBase = new WarehouseInBase();
            int result = warehouseInBase.deleteByCode(code);
            if (result > 0)
            {
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_WarehouseIn",
                    operationTime = DateTime.Now,
                    objective = "删除入库信息",
                    result = result,
                    operationContent = "删除T_WarehouseIn表的数据,code为:" + code
                };
                lb.Add(log);

                return result;
            }
            else
            {
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_WarehouseIn",
                    operationTime = DateTime.Now,
                    objective = "删除入库信息失败",
                    result = result,
                    operationContent = "删除T_WarehouseIn数据失败,code为:" + code
                };
                lb.Add(log);

                return result;
            }
        }

        public int update(WarehouseIn wi)
        {
            if (wi == null)
            {
                return -7;
            }
            WarehouseInBase warehouseInBase = new WarehouseInBase();
            int result = warehouseInBase.update(wi);

            //添加日志
            LogBase lb = new LogBase();
            log log = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseIn",
                operationTime = DateTime.Now,
                objective = "更新入库表信息",
                result = result,
            };
            if (result > 0)
            {
                log.operationContent = "更新T_WarehouseIn表的数据,code为:" + wi.code;
                lb.Add(log);
            }
            else
            {
                log.operationContent = "更新T_WarehouseIn表数据失败,code为:" + wi.code;
                lb.Add(log);
            }
            return result;
        }

        /// <summary>
        /// 修改审核状态
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int updateByCode(string code)
        {
            WarehouseInBase warehouseInBase = new WarehouseInBase();

            int upcode = 0;
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseIn",
                operationTime = DateTime.Now,
                objective = "修改入库状态",
                operationContent = "修改T_WarehouseIn表的数据,条件code为:" + code
            };
            try
            {
                upcode = warehouseInBase.updateByCode(code);
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
            return upcode;
        }

        /// <summary>
        /// 上下一单
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="state">状态:0:下一单,1:上一单</param>
        /// <returns></returns>
        public WarehouseIn GetPreAndNext(int id, int state)
        {
            WarehouseInBase warehouseInBase = new WarehouseInBase();

            return warehouseInBase.GetPreAndNext(id, state);
        }
        /// <summary>
        /// 判断该客户编号判断是否存在
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool Exists(string code)
        {
            bool result = false;
            WarehouseInBase warehouseInBase = new WarehouseInBase();
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseIn",
                operationTime = DateTime.Now,
                objective = "判断入库单是否存在",
                operationContent = "查询T_WarehouseIn表的数据是否存在,条件code为:" + code
            };
            try
            {
                if (string.IsNullOrWhiteSpace(code))
                {
                    throw new Exception("-2");
                }
                result = warehouseInBase.Exists(code);
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
