using LogicLayer.Base;
using System.Data;

namespace InterfaceLayer.Base
{
    public class EmpolyeeInterface
    {
        EmpolyeeLogic el = new EmpolyeeLogic();
        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <param name="isflag">是否显示禁用：true显示所有禁用状态的信息，false仅显示未禁用状态的信息</param>
        /// <returns></returns>
        public DataTable SelSupplierTable(bool isflag)
        {
            return el.GetEmpByBool(isflag);
        }
        /// <summary>
        /// 自定义条件取得列表
        /// </summary>
        /// <param name="strWhere">where后面的条件</param>
        /// <returns></returns>
        public DataTable GetList(string strWhere)
        {
            return el.GetList(strWhere);
        }
    }
}
