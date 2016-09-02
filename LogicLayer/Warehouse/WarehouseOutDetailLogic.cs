using BaseLayer;
using HelperUtility.Encrypt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Warehouse
{
    public class WarehouseOutDetailLogic
    {
        WarehouseOutDetailBase wodb = new WarehouseOutDetailBase();
        public DataSet GetList(string strWhere)
        {
            return wodb.GetList(strWhere);
        }
    }
}
