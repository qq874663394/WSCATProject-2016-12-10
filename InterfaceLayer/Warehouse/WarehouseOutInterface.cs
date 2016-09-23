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
        /// 根据审核状态查询
        /// </summary>
        /// <returns></returns>
        public DataSet GetListToOut(int state)
        {
            return wol.GetListToOut(state);
        }
        /// <summary>
        /// 根据where条件获取出库单列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetList(int fieldName, string fieldValue)
        {
            return wol.GetList(fieldName, fieldValue);
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
        /// <summary>
        /// 更新主表和从表
        /// </summary>
        /// <param name="warehouseOut">主表：只有一行数据</param>
        /// <param name="listModel">从表：多行，用List类型保存多条model的数据</param>
        public void update(WarehouseOut warehouseOut, List<WarehouseOutDetail> listModel)
        {
            wol.update(warehouseOut, listModel);
        }
        public int delete(string code)
        {
            return wol.delete(code);
        }
    }
}
