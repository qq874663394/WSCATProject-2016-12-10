using BaseLayer.Base;
using HelperUtility.Encrypt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Base
{
    public class OrderTypeLogic
    {
        OrderTypeBase otb = new OrderTypeBase();
        CodingHelper ch = new CodingHelper();
        public DataTable GetList(string strWhere)
        {
            return ch.DataTableReCoding(otb.GetList(strWhere));
        }
    }
}
