using BaseLayer;
using HelperUtility.Encrypt;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Warehouse
{
    public class WarehouseOutLogic
    {
        WarehouseOutBase wob = new WarehouseOutBase();
        /// <summary>
        /// 根据where条件获取出库单列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            return wob.GetList(strWhere);
        }
        /// <summary>
        /// 添加出库单
        /// </summary>
        /// <param name="wo"></param>
        /// <returns></returns>
        public int Add(WarehouseOut wo)
        {
            return wob.Add(wo);
        }

        public int update(WarehouseOut wo)
        {
            return wob.update(wo);
        }
        public int delete(string code)
        {
            return wob.delete(code);
        }
    }
}
