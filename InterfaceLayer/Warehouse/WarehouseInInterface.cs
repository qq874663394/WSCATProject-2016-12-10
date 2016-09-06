using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer;
using System.Data;
using Model;
using System.Collections;
using System.Data.SqlClient;

namespace InterfaceLayer.Warehouse
{
    public class WarehouseInInterface
    {
        WarehouseInLogic warehouseInLogic = new WarehouseInLogic();

        /// <summary>
        /// 根据审核状态查询
        /// </summary>
        /// <returns></returns>
        public DataSet GetListToIn(int state)
        {
            return warehouseInLogic.GetListToIn(state);
        }
        /// <summary>
        /// 事务修改
        /// </summary>
        /// <param name="hashTable">主表的sql和parameter</param>
        /// <param name="sql">子表sql</param>
        /// <param name="list">子表的parameter</param>
        public void UpdateList(Hashtable hashTable, string sql, List<SqlParameter[]> list)
        {
            warehouseInLogic.UpdateList(hashTable, sql, list);
        }
        /// <summary>
        /// 新增一条入库单
        /// </summary>
        /// <param name="wi">入库单实体</param>
        /// <returns></returns>
        public int addWarehouseIn(WarehouseIn wi, List<WarehouseInDetail> widList)
        {
            return warehouseInLogic.InsertWarehouseInTable(wi, widList);
        }

        /// <summary>
        /// 获取入库数据列表
        /// </summary>
        /// <param name="strWhere">where条件</param>
        /// <returns></returns>
        public DataSet getWarehouseInList(string strWhere)
        {
            return warehouseInLogic.GetList(strWhere);
        }

        /// <summary>
        /// 根据code删除一条数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int deleteWarehouseInByCode(string code)
        {
            return warehouseInLogic.deleteByCode(code);
        }
        public int updateByCode(string code)
        {
            return warehouseInLogic.updateByCode(code);
        }

    }
}
