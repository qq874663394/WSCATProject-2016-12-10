using LogicLayer.Warehouse;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Warehouse
{
    public class WarehouseOutInterface
    {
        WarehouseOutLogic wol = new WarehouseOutLogic();
        /// <summary>
        /// 根据where条件获取出库单列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            return wol.GetList(strWhere);
        }
        /// <summary>
        /// 添加出库单
        /// </summary>
        /// <param name="wo"></param>
        /// <returns></returns>
        public int Add(WarehouseOut wo)
        {
            return wol.Add(wo);
        }

        public int update(WarehouseOut wo)
        {
            return wol.update(wo);
        }
        public int delete(string code)
        {
            return wol.delete(code);
        }
    }
}
