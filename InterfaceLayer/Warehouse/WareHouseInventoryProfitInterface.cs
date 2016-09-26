using LogicLayer.Warehouse;
using Model.Warehouse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Warehouse
{
    public class WareHouseInventoryProfitInterface
    {
        WareHouseInventoryProfitLogic wipl = new WareHouseInventoryProfitLogic();
        /// <summary>
        /// 复合查询
        /// </summary>
        /// <param name="fieldName">0:code,1:checkState,2:isClear</param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public DataTable Search(int fieldName, string fieldValue)
        {
            return wipl.Search(fieldName, fieldValue);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public int Add(WarehouseInventoryProfit Model)
        {
            return wipl.Add(Model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public int Modify(WarehouseInventoryProfit Model)
        {
            return wipl.Modify(Model);
        }
    }
}
