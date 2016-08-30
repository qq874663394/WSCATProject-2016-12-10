using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer;
using System.Data;
using Model;

namespace InterfaceLayer.Warehouse
{
    public class WarehouseInterface
    {
        WarehouseInLogic warehouseInLogic = new WarehouseInLogic();

        /// <summary>
        /// 新增一条入库单
        /// </summary>
        /// <param name="wi">入库单实体</param>
        /// <returns></returns>
        public int add(WarehouseIn wi,List<WarehouseInDetail> widList)
        {
            return warehouseInLogic.InsertWarehouseInTable(wi, widList);
        }
    }
}
