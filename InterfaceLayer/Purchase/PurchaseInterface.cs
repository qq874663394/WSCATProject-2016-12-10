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
        public DataTable GetList(string strWhere)
        {
            DataTable dt = null;
            try
            {
                dt = pl.GetList(strWhere);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}
