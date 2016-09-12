using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Purchase
{
    public class PurchaseDetailInterface
    {
        PurchaseDetailInterface pdi = new PurchaseDetailInterface();
        public DataTable GetList(string purchaseCode, string zhujima)
        {
            return pdi.GetList(purchaseCode, zhujima);
        }
        public decimal GetCheckNumber(string purchaseCode, string code)
        {
            return pdi.GetCheckNumber(purchaseCode, code);
        }
    }
}
