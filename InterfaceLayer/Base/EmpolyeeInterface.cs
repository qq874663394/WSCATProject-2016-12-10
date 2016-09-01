using LogicLayer.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Base
{
    public class EmpolyeeInterface
    {
        EmpolyeeLogic el = new EmpolyeeLogic();
        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns>所有数据以DataTable的形式返回</returns>
        public DataTable SelSupplierTable(bool isflag)
        {
            return el.SelEmpolyeeTable(isflag);
        }
    }
}
