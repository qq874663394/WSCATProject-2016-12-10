using LogicLayer.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Base
{
    public class OrderTypeInterface
    {
        OrderTypeLogic otl = new OrderTypeLogic();
        public DataTable GetList(string strWhere)
        {
            return otl.GetList(strWhere);
        }
    }
}
