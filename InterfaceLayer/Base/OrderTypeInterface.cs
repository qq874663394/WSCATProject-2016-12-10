using LogicLayer.Base;
using System.Data;

namespace InterfaceLayer.Base
{
    public class OrderTypeInterface
    {
        OrderTypeLogic otl = new OrderTypeLogic();
        /// <summary>
        /// 复合查询
        /// </summary>
        /// <param name="fieldName">0:模糊name,1:name,2:code</param>
        /// <param name="fieldValue">条件值</param>
        /// <returns></returns>
        public DataTable GetList(int fieldName, string fieldValue)
        {
            return otl.GetList(fieldName, fieldValue);
        }
    }
}
