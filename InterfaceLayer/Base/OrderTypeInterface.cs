using LogicLayer.Base;
using System.Data;

namespace InterfaceLayer.Base
{
    public class OrderTypeInterface
    {
        OrderTypeLogic otl = new OrderTypeLogic();
        /// <summary>
        /// 自定义条件取得列表
        /// </summary>
        /// <param name="strWhere">where后面的条件</param>
        /// <returns></returns>
        public DataTable GetList(string strWhere)
        {
            return otl.GetList(strWhere);
        }
    }
}
