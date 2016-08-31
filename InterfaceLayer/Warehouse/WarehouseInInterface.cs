using LogicLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Warehouse
{
    public class WarehouseInInterface
    {
        WarehouseInLogic wil = new WarehouseInLogic();
        /// <summary>
        /// 根据where条件获取数据列表
        /// </summary>
        /// <param name="strWhere">where条件</param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            return wil.GetList(strWhere);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <returns>返回新增结果,1为成功</returns>
        public int InsertWarehouseInTable(WarehouseIn wi,
            List<WarehouseInDetail> widList
            )
        {
            return wil.InsertWarehouseInTable(wi, widList);
        }
        /// <summary>
        /// 根据code删除一条数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int deleteByCode(string code)
        {
            return wil.deleteByCode(code);
        }
        public int update(WarehouseIn wi)
        {
            return wil.update(wi);
        }
    }
}
