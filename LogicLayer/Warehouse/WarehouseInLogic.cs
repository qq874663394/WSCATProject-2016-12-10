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
                operationContent = "根据审核状态查询入库表,state值为"+state
            };

            try
            {
                ds= warehouseInBase.GetListToIn(state);
                logModel.result = 1;
            }
            catch(Exception ex)
            {
                logModel.result = 0;
                throw ex;
            }
            
            lb.Add(logModel);//rz
            return ds;
        }


        /// <summary>
        /// 事务修改
        /// </summary>
        /// <param name="hashTable">主表的sql和parameter</param>
        /// <param name="sql">子表sql</param>
        /// <param name="list">子表的parameter</param>
        public void UpdateList(Hashtable hashTable, string sql, List<SqlParameter[]> list)
        {
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
                warehouseInBase.UpdateList(hashTable, sql, list);
                logModel.result = 1;
            }
            catch(Exception ex)
            {
                logModel.result = 0;//rz
                throw ex;
            }
            lb.Add(logModel);//rz
        }

        /// <summary>
        /// 根据where条件获取数据列表
        /// </summary>
        /// <param name="strWhere">where条件</param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
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
                objective = "查询入库信息",
                operationContent = "查询T_WarehouseIn表的数据,条件为:" + strWhere
            };



            try
            {
                ds = warehouseInBase.GetList(strWhere);
                logModel.result = 1;//rz
            }
            catch (Exception ex)
            {
                logModel.result = 0;//rz
                throw ex;
            }
            lb.Add(logModel);//rz
            return ds;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <returns>返回新增结果,1为成功</returns>
        public int InsertWarehouseInTable(WarehouseIn wi,
            List<WarehouseInDetail> widList
            )
        {
            DateTime nowDateTime = DateTime.Now;
            LogBase lb = new LogBase();
            log log = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "操作表名",
                operationTime = nowDateTime,
                objective = "增加入库信息"
            };
            WarehouseInBase warehouseInBase = new WarehouseInBase();
            WarehouseInDetailBase warehouseInDetailBase = new WarehouseInDetailBase();
            WarehouseUpdataManager updateManager = new WarehouseUpdataManager();
            if (wi != null)
            {
                int addWarehouseInResult = warehouseInBase.update(wi);
                int addWarehouseInDetailResult = 0;

                //当正确新增时开始新增详情表
                if (addWarehouseInResult > 0)
                {
                    log.operationTable = "T_WarehouseIn";
                    log.result = addWarehouseInResult;
                    log.operationContent = wi.code;
                    lb.Add(log);

                    int addcount = 0;
                    //判断新增过程中有没有失败
                    bool addErr = false;
                    //循环加入多条数据到详情表
                    for (int i = 0; i < widList.Count; i++)
                    {
                        addWarehouseInDetailResult = warehouseInDetailBase.updateByCode(widList[i]);
                        if (addWarehouseInDetailResult < 1)
                        {
                            addcount = i;
                            addErr = true;
                            break;
                        }
                        //调用管理层 循环添加管理的数据
                        log.objective = "新增入库商品详情";
                        log.operationTable = "T_WarehouseInDetail";
                        log.result = addWarehouseInDetailResult;
                        log.operationContent = widList[i].code;
                        lb.Add(log);
                    }
                    //如果加入过程中出错,倒过来删除之前加入的
                    if (!addErr)
                    {
                        for (int i = addcount; i < 0; i--)
                        {
                            warehouseInDetailBase.deleteByCode(widList[i].code);

                            //调用删除删掉入库的更新管理数据
                            warehouseInDetailBase.deleteByCode(widList[i].code);

                            //调用删除方法删掉加入的详情单
                            log.objective = "回滚入库商品详情";
                            log.operationTable = "T_WarehouseInDetail";
                            log.result = addWarehouseInResult;
                            log.operationContent = widList[i].code;
                            lb.Add(log);
                        }
                        //调用删除方法删掉入库单
                        warehouseInBase.deleteByCode(wi.code);
                    }
                    //调用流程表
                    if (!addErr)
                    {
                        WarehouseInProcessBase warehouseInProcessBase = new WarehouseInProcessBase();
                        WarehouseInProcess Warehousep = new WarehouseInProcess();//操作流程
                        Warehousep.code = XYEEncoding.strCodeHex(BuildCode.ModuleCode("WP"));
                        DateTime dt = nowDateTime;
                        Warehousep.createDatetime = dt;
                        Warehousep.isClear = 1;
                        Warehousep.Operator = XYEEncoding.strCodeHex("保存入库单");
                        Warehousep.operatorMan = "";
                        Warehousep.remark = "";
                        Warehousep.updateDate = dt;
                        Warehousep.warehouseInDetailCode = wi.code;
                        warehouseInProcessBase.Add(Warehousep);

                        log.operationContent = Warehousep.code;
                        log.objective = "新增入库流程数据";
                        log.operationTable = "T_WarehouseInProcess";
                        log.result = addWarehouseInResult;
                        lb.Add(log);
                    }
                    //调用管理层
                    if (!addErr)
                    {
                        log.operationContent = "";
                        log.objective = "新增入库表的更新管理";
                        log.operationTable = "T_Warehouse";
                        log.result = addWarehouseInResult;
                        lb.Add(log);
                    }
                    if (!addErr)
                    {
                        return addWarehouseInDetailResult;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    log.objective = "新增入库商品详情错误";
                    log.operationTable = "T_WarehouseInDetail";
                    log.result = addWarehouseInResult;
                    lb.Add(log);
                    return addWarehouseInResult;
                }
            }
            else
            {
                return -7;
            }
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
                objective = "查询入库信息",
                operationContent = "查询T_WarehouseIn表的数据,条件code为:" + code
            };
 
            try
            {
                upcode= warehouseInBase.updateByCode(code);
                logModel.result = 1;//rz
            }
            catch(Exception ex)
            {
                logModel.result = 0;//rz
                throw ex;
            }
            lb.Add(logModel);//rz
            return upcode;
        }
    }
}
