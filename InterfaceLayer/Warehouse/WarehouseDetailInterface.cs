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
    class WarehouseDetailInterface
    {
        WarehouseDetailLogic wdl = new WarehouseDetailLogic();
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertWarehouseInDetailTable(WarehouseInDetail model)
        {
            return wdl.InsertWarehouseInDetailTable(model);
        }
        /// <summary>
        /// 根据code删除数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int deleteInDetailTable(string code)
        {
            return wdl.deleteInDetailTable(code);
        }
        /// <summary>
        /// 根据where条件获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet getList(string strWhere)
        {
            return wdl.getList(strWhere);
        }
        /// <summary>
        /// 根据主表code获取列表
        /// </summary>
        /// <param name="mainCode"></param>
        /// <returns></returns>
        public DataSet getListByMainCode(string mainCode)
        {
            return wdl.getListByMainCode(mainCode);
        }
    }
}
