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
        /// 获取数据列表
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">0:code,1:type,2:checkState,3:clientCode,4:mainCode</param>
        /// <returns></returns>
        public DataTable GetList(int fieldName, string fieldValue)
        {
            return wol.GetList(fieldName, fieldValue);
        }
        /// <summary>
        /// 事务新增
        /// </summary>
        /// <param name="warehouseOut">主表：只有一行数据</param>
        /// <param name="warehouseOutDetail">从表：多行，用List类型保存多条model的数据</param>
        public object Add(WarehouseOut warehouseOut, List<WarehouseOutDetail> warehouseOutDetail)
        {
            return wol.Add(warehouseOut, warehouseOutDetail);
        }
        /// <summary>
        /// 更新主表和从表
        /// </summary>
        /// <param name="warehouseOut">主表：只有一行数据</param>
        /// <param name="listModel">从表：多行，用List类型保存多条model的数据</param>
        public int update(WarehouseOut warehouseOut, List<WarehouseOutDetail> listModel)
        {
            return wol.update(warehouseOut, listModel);
        }
    }
}
