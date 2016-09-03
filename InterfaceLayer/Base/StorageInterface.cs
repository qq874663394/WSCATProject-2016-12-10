using LogicLayer.Base;
using System.Data;

namespace InterfaceLayer.Base
{
    public class StorageInterface
    {
        StorageLogic sl = new StorageLogic();
        public DataTable SelStorage()
        {
            return sl.SelStorage();
        }
        public DataTable GetList(string strWhere)
        {
            return sl.GetList(strWhere);
        }
    }
}
