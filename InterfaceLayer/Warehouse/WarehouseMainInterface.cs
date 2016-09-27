using LogicLayer.Warehouse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Warehouse
{
    public class WarehouseMainInterface
    {
        WarehouseMainLogic wo = new WarehouseMainLogic();
        /// <summary>
        /// 减少库存
        /// </summary>
        /// <param name="number"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public int updateReduce(int number, string code)
        {
            return wo.updateReduce(number, code);
        }
        /// <summary>
        /// 增加库存
        /// </summary>
        /// <param name="number"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public int updateAug(int number, string code)
        {
            return wo.updateAug(number, code);
        }
        /// <summary>
        /// 自定义条件获取列表
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public DataTable GetMaterialDetail(string fieldValue)
        {
            return wo.GetMaterialDetail(fieldValue);
        }
        /// <summary>
        /// 根据查出来的仓库code查库存表，再根据库存表的商品code查物料信息表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable GetMaterialByMain(string code)
        {
            return wo.GetMaterialByMain(code);
        }
        /// <summary>
        /// 根据仓库code查询库存里面的商品再根据商品code找出物料的信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable GetWMainAndMaterialByWMCode(string code)
        {
            return wo.GetWMainAndMaterialByWMCode(code);
        }
    }
}
