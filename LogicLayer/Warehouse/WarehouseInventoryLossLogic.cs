using BaseLayer.Warehouse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Warehouse
{
    public class WarehouseInventoryLossLogic
    {
        WarehouseInventoryLossBase wilb = new WarehouseInventoryLossBase();
        public DataTable Search(int fieldName, string fieldValue)
        {
            string strWhere = "";
            DataTable dt = null;
            try
            {
                switch (fieldName)
                {
                    case 0:
                        strWhere += string.Format("code='{0}'", fieldValue);
                        break;
                    case 1:
                        strWhere += string.Format("checkState='{0}'", fieldValue);
                        break;
                    case 2:
                        strWhere += string.Format("isClear={0}", fieldValue);
                        break;
                }
                strWhere += " order by id";
                wilb.Search(strWhere);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}
