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
        /// 根据审核状态查询
        /// </summary>
        /// <returns></returns>
        public DataSet GetListToOut(int state)
        {
            return wob.GetListToOut(state);
        }
        public DataSet GetList(string strWhere)
        {
            return wob.GetList(strWhere);
        }
        public int Add(WarehouseOut wo)
        {
            return wob.Add(wo);
        }
        public int update(WarehouseOut wo)
        {
            return wob.update(wo);
        }
        public int update(string field, int state, string code)
        {
            return wob.update(field, state, code);
        }
        public void update(WarehouseOut warehouseOut, List<WarehouseOutDetail> listModel)
        {
            wob.update(warehouseOut, listModel);
        }
        public int delete(string code)
        {
            return wob.delete(code);
        }
    }
}
