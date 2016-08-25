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
        WarehouseLogic warehouseLogic = new WarehouseLogic();

        /// <summary>
        /// 新增一条入库单
        /// </summary>
        /// <param name="wi">入库单实体</param>
        /// <returns></returns>
        public int add(WarehouseIn wi,List<WarehouseInDetail> widList)
        {
            return warehouseLogic.InsertWarehouseInTable(wi, widList);
        }
    }
}
