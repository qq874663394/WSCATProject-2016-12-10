using LogicLayer.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Sales
{
    public class SalesDetailInterface
    {
        SalesDetailLogic sdl = new SalesDetailLogic();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName">条件字段：0:salesMainCode</param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public DataTable GetList(int fieldName, string fieldValue)
        {
            return sdl.GetList(fieldName, fieldValue);
        }
    }
}
