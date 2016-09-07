using BaseLayer;
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
    public class WarehouseInventoryLogic
    {
        WarehouseInventoryBase bs = new WarehouseInventoryBase();

        /// <summary>
        /// 获取仓库列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetList()
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
                objective = "查询仓库列表",
                operationContent = "查询T_BaseStorage表的数据"
            };
            try
            {
                dt= bs.GetList();
                logModel.result = 1;//rz
            }
            catch(Exception ex)
            {
                logModel.result = 0;//rz
                throw ex;

            }
            lb.Add(logModel);//rz
            return dt;
        }

        /// <summary>
        /// 商品盘点表 read
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable GetTbList(int num, string code)
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
                operationContent = "查询T_WarehouseMain、T_BaseMaterial、T_WarehouseInventoryDetail表的数据,条件为code"+code
            };
            try
            {
                dt = bs.GetTbList(num, code);
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
