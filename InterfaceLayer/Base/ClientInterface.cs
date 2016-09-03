using LogicLayer.Base;
using System.Data;

namespace InterfaceLayer.Base
{
    public class ClientInterface
    {
        ClientLogic cb = new ClientLogic();
        /// <summary>
        /// 查询所有客户信息
        /// </summary>
        /// <param name="isflag">true显示全部 false显示未禁用</param>
        /// <returns></returns>
        public DataTable SelClient(bool isflag)
        {
            return cb.SelClient(isflag);
        }
    }
}
