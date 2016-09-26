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
    public class WarehouseInventoryLossInterface
    {
        WarehouseInventoryLossLogic will = new WarehouseInventoryLossLogic();
        /// <summary>
        /// 复合查询
        /// </summary>
        /// <param name="fieldName">0:code,1:checkState,2:isClear</param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public DataTable Search(int fieldName, string fieldValue)
        {
            return will.Search(fieldName, fieldValue);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="wil"></param>
        /// <returns></returns>
        public object Add(WarehouseInventoryLoss warehouseInventoryLoss, List<WarehouseInventoryLossDetail> warehouseInventoryLossDetail)
        {
            return will.Add(warehouseInventoryLoss,warehouseInventoryLossDetail);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="wil"></param>
        /// <returns></returns>
        public int Modify(WarehouseInventoryLoss wil)
        {
            return will.Modify(wil);
        }
    }
}
