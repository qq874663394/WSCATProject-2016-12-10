using LogicLayer.Warehouse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Warehouse
{
    public class WarehouseOutDetailInterface
    {
        WarehouseOutDetailLogic wodl = new WarehouseOutDetailLogic();
        public DataSet GetList(string strWhere)
        {
            return wodl.GetList(strWhere);
        }
    }
}
