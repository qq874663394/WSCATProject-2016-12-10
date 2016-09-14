using LogicLayer.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Base
{
    public class SupplierInterface
    {
        SupplierLogic sl = new SupplierLogic();
        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns>所有数据以DataTable的形式返回</returns>
        public DataTable SelSupplierTable()
        {
            return sl.SelSupplierTable();
        }
        /// <summary>
        /// 自定义条件
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetList(int fieldName, string fieldValue)
        {
            return sl.GetList(fieldName, fieldValue);
        }
        /// <summary>
        /// 根据供应商code查询所有采购单
        /// </summary>
        /// <param name="code">供应商code</param>
        /// <returns></returns>
        public DataTable GetPurchaseList(string code)
        {
            return sl.GetPurchaseList(code);
        }
    }
}
