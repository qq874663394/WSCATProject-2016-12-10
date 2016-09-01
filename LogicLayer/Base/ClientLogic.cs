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
    public class ClientLogic
    {
        ClientBase cb = new ClientBase();
        CodingHelper ch = new CodingHelper();
        /// <summary>
        /// 查询所有客户信息
        /// </summary>
        /// <param name="isflag">true显示全部 false显示未禁用</param>
        /// <returns></returns>
        public DataTable SelClient(bool isflag)
        {
            return ch.DataTableReCoding(cb.SelClient(isflag));
        }
    }
}
