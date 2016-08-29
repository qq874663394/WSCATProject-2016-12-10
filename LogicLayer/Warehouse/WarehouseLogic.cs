using System;
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

namespace LogicLayer
{
    public class WarehouseLogic
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <returns>返回新增结果,1为成功</returns>
        public int InsertWarehouseInTable(WarehouseIn wi, 
            List<WarehouseInDetail> widList
            )
        {
            LogBase lb = new LogBase();
            log log = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                //OperationTable = "操作表名",
                operationTime = DateTime.Now,
                //OperationContent = "",
                objective = "增加入库信息"
            };
            WarehouseInBase warehouseInBase = new WarehouseInBase();
            WarehouseInDetailBase warehouseInDetailBase = new WarehouseInDetailBase();
            if (wi != null)
            {
                int addWarehouseInResult = warehouseInBase.Add(wi);
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
                        addWarehouseInDetailResult = warehouseInDetailBase.Add(widList[i]);
                        if (addWarehouseInDetailResult < 1)
                        {
                            addcount = i;
                            addErr = true;
                            break;
                        }
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
                            //调用删除方法删掉加入的详情单
                            log.objective = "回滚入库商品详情";
                            log.operationTable = "T_WarehouseInDetail";
                            log.result = addWarehouseInResult;
                            log.operationContent = widList[i].code;
                            lb.Add(log);
                        }
                        //调用删除方法删掉入库单
                    }
                    //调用流程表
                    if (!addErr)
                    {
                        WarehouseInProcessBase warehouseInProcessBase = new WarehouseInProcessBase();
                        WarehouseInProcess Warehousep = new WarehouseInProcess();//操作流程
                        Warehousep.code = XYEEncoding.strCodeHex(BuildCode.ModuleCode("WP"));
                        DateTime dt = DateTime.Now;
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
                        WarehouseUpdataManager updateManager = new WarehouseUpdataManager();
                        string updateManagerCode = BuildCode.ModuleCode("WU");
                        updateManager.addWarehouseIn(updateManagerCode, wi.code,
                            "T_WarehouseIn", addWarehouseInResult, "", "", DateTime.Now);

                        log.operationContent = updateManagerCode;
                        log.objective = "新增入库表的更新管理";
                        log.operationTable = "T_Warehouse";
                        log.result = addWarehouseInResult;
                        lb.Add(log);
                    }
                    if(!addErr)
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
    }
}
