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
    public class WarehouseInLogic
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
            Log log = new Log()
            {
                Code = BuildCode.ModuleCode("log"),
                OperationCode = "操作人code",
                OperationName = "操作人名",
                //OperationTable = "操作表名",
                OperationTime = DateTime.Now,
                //OperationContent = "",
                Objective = "增加入库信息"
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
                    log.OperationTable = "T_WarehouseIn";
                    log.Result = addWarehouseInResult;
                    log.OperationContent = wi.code;
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
                        log.Objective = "新增入库商品详情";
                        log.OperationTable = "T_WarehouseInDetail";
                        log.Result = addWarehouseInDetailResult;
                        log.OperationContent = widList[i].code;
                        lb.Add(log);
                    }
                    //如果加入过程中出错,倒过来删除之前加入的
                    if (!addErr)
                    {
                        for (int i = addcount; i < 0; i--)
                        {
                            warehouseInDetailBase.deleteByCode(widList[i].code);

                            //调用删除方法删掉加入的详情单
                            log.Objective = "回滚入库商品详情";
                            log.OperationTable = "T_WarehouseInDetail";
                            log.Result = addWarehouseInResult;
                            log.OperationContent = widList[i].code;
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
                        DateTime dt = DateTime.Now;
                        Warehousep.createDatetime = dt;
                        Warehousep.isClear = 1;
                        Warehousep.Operator = XYEEncoding.strCodeHex("保存入库单");
                        Warehousep.operatorMan = "";
                        Warehousep.remark = "";
                        Warehousep.updateDate = dt;
                        Warehousep.warehouseInDetailCode = wi.code;
                        warehouseInProcessBase.Add(Warehousep);

                        log.OperationContent = Warehousep.code;
                        log.Objective = "新增入库流程数据";
                        log.OperationTable = "T_WarehouseInProcess";
                        log.Result = addWarehouseInResult;
                        lb.Add(log);
                    }
                    //调用管理层
                    if (!addErr)
                    {
                        WarehouseUpdataManager updateManager = new WarehouseUpdataManager();
                        string updateManagerCode = BuildCode.ModuleCode("WU");
                        updateManager.addWarehouseIn(updateManagerCode, wi.code,
                            "T_WarehouseIn", addWarehouseInResult, "", "", DateTime.Now);

                        log.OperationContent = updateManagerCode;
                        log.Objective = "新增入库表的更新管理";
                        log.OperationTable = "T_Warehouse";
                        log.Result = addWarehouseInResult;
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
                    log.Objective = "新增入库商品详情错误";
                    log.OperationTable = "T_WarehouseInDetail";
                    log.Result = addWarehouseInResult;
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
