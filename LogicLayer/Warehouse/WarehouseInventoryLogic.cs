using BaseLayer.Warehouse;
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
            return bs.GetList();
        }

        /// <summary>
        /// 商品盘点表 read
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable GetTbList(int num, string code)
        {
            return bs.GetTbList(num,code);
        }
    }
}
