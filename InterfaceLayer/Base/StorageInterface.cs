using LogicLayer.Base;
using System.Data;

namespace InterfaceLayer.Base
{
    public class StorageInterface
    {
        StorageLogic sl = new StorageLogic();
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public DataTable SelStorage()
        {
            return sl.SelStorage();
        }
        /// <summary>
        /// 自定义条件取得列表
        /// </summary>
        /// <param name="strWhere">where后面的条件</param>
        /// <returns></returns>
        public DataTable GetList(string strWhere)
        {
            return sl.GetList(strWhere);
        }
    }
}
