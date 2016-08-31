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
    public class WarehouseInDetailInterface
    {
        WarehouseInDetailLogic widl = new WarehouseInDetailLogic();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(WarehouseInDetail model)
        {
            return widl.Add(model);
        }
        /// <summary>
        /// 根据code删除数据
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns></returns>
        public int deleteByCode(string code)
        {
            return widl.deleteByCode(code);
        }
        /// <summary>
        /// 根据code更新数据
        /// </summary>
        /// <param name="wid">入库详细列表</param>
        /// <returns></returns>
        public int updateByCode(WarehouseInDetail wid)
        {
            return widl.updateByCode(wid);
        }
        public DataSet getList(string strWhere)
        {
            return widl.getList(strWhere);
        }
        public DataSet getListByMainCode(string mainCode)
        {
            return widl.getListByMainCode(mainCode);
        }
    }
}
