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
        public void InsertWarehouseInDetailTable(WarehouseInDetail model)
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
        }

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
    }
}
