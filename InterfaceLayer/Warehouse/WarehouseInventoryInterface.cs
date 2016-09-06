using LogicLayer.Warehouse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Warehouse
{
    public class WarehouseInventoryInterface
    {
        WarehouseInventoryLogic iface = new WarehouseInventoryLogic();

        /// <summary>
        /// 获取仓库列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetList()
        {
            return iface.GetList();
        }

        /// <summary>
        /// 商品盘点表 read
        /// </summary>
        /// <param name="num">1为全部数据，2为对仓库数据</param>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable GetTbList(int num,string code)
        {
            return iface.GetTbList(num,code);
        }
    }
}
