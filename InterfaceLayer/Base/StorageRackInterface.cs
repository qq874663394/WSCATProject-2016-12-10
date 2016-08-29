using LogicLayer.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Base
{
    public class StorageRackInterface
    {
        StorageRackLogic srl = new StorageRackLogic();

        /// <summary>
        /// 根据父级ID查询货架信息
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public DataTable SelStorageRackByCode(string parentId)
        {
            return srl.SelStorageRackByCode(parentId);
        }
    }
}