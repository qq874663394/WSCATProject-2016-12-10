using BaseLayer;
using BaseLayer.Base;
using BaseLayer.Warehouse;
using HelperUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Warehouse
{
    public class WarehouseMainLogic
    {
        WarehouseMainBase wo = new WarehouseMainBase();
        /// <summary>
        /// 减少库存
        /// </summary>
        /// <param name="number"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public int updateReduce(int number, string code)
        {
            int result = wo.updateReduce(number, code);
            if (result > 0)
            {
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_WarehouseMain",
                    operationTime = DateTime.Now,
                    objective = "减少库存",
                    result = result,
                    operationContent = "减少T_WarehouseMain表的数据,number为" + number + "，code为:" + code
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
                    operationTable = "T_WarehouseMain",
                    operationTime = DateTime.Now,
                    objective = "减少库存信息失败",
                    result = result,
                    operationContent = "减少T_WarehouseMain数据失败,number为" + number + "，code为:" + code
                };
                lb.Add(log);

                return result;
            }
         
        }
        /// <summary>
        /// 增加库存
        /// </summary>
        /// <param name="number"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public int updateAug(int number, string code)
        {
            int result = wo.updateAug(number, code);
            if (result > 0)
            {
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_WarehouseMain",
                    operationTime = DateTime.Now,
                    objective = "增加库存",
                    result = result,
                    operationContent = "增加T_WarehouseMain表的数据,number为"+number+"，code为:" + code
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
                    operationTable = "T_WarehouseMain",
                    operationTime = DateTime.Now,
                    objective = "增加库存信息失败",
                    result = result,
                    operationContent = "增加T_WarehouseMain数据失败,number为" + number + "，code为:" + code
                };
                lb.Add(log);

                return result;
            }
            
        }
        /// <summary>
        /// 自定义条件获取列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetList(string strWhere)
        {
            DataTable dt = null;

            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_BaseStorage",
                operationTime = DateTime.Now,
                objective = "商品盘点报告表",
                operationContent = "查询T_BaseMaterial、T_WarehouseMain、T_WarehouseDetail表的数据,条件strWhere为"+ strWhere
            };
            try
            {
                dt = wo.GetList(strWhere);
                logModel.result = 1;//rz
            }
            catch (Exception ex)
            {
                logModel.result = 0;//rz
                throw ex;

            }
            lb.Add(logModel);//rz
            return dt;
        }
    }
}
