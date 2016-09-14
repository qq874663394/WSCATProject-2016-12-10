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
        public DataTable GetClientByBool(bool isflag)
        {
            return cb.GetClientByBool(isflag);
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="fieldName">需要查询的列：0：name,1：cityName</param>
        /// <param name="fieldValue">需要查询的列的值</param>
        /// <returns></returns>
        public DataTable GetList(int fieldName, string fieldValue)
        {
            return cb.GetList(fieldName,fieldValue);
        }
    }
}
