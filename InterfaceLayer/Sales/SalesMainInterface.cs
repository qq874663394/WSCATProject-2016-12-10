using LogicLayer.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Sales
{
    public class SalesMainInterface
    {
        private SalesMainLogic sml = new SalesMainLogic();
        /// <summary>
        /// 根据条件获取销售单列表
        /// </summary>
        /// <param name="fieldName">字段名，0：clientCode</param>
        /// <param name="fieldValue">条件值</param>
        /// <returns></returns>
        public DataTable GetList(int fieldName, string fieldValue)
        {
            return sml.GetList(fieldName, fieldValue);
        }
        /// <summary>
        /// 根据客户编号返回销售单的Table(id,code)
        /// </summary>
        /// <param name="clientCode"></param>
        /// <returns></returns>
        public DataTable GetTableByClientCode(string clientCode)
        {
            return sml.GetTableByClientCode(clientCode);
        }
    }
}
