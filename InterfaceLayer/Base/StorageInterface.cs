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
    }
}
