using LogicLayer.Purchase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Purchase
{
    public class PurchaseInterface
    {
        PurchaseLogic pl = new PurchaseLogic();
        /// <summary>
        /// 复合查询
        /// </summary>
        /// <param name="fieldName">0:模糊name,1:模糊supplierName,2:supplierCode</param>
        /// <param name="fieldValue">条件值</param>
        /// <returns></returns>
        public DataTable GetList(int fieldName, string fieldValue)
        {
            DataTable dt = null;
            try
            {
                dt = pl.GetList(fieldName,fieldValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}
