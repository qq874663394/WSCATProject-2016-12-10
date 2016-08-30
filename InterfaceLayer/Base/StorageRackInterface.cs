using LogicLayer.Base;
using Model;
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
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public DataTable SelStorageRack()
        {
            return srl.SelStorageRack();
        }
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int DelStorageRackByCode(string code)
        {
            return srl.DelStorageRackByCode(code);
        }
        /// <summary>
        /// 修改名字
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int UpdateStorageRackByCode(string code)
        {
            return srl.UpdateStorageRackByCode(code);
        }
        /// <summary>
        /// 新增货架信息
        /// </summary>
        /// <param name="srb"></param>
        /// <returns></returns>
        public int InsStorageRack(BaseStorageRack srb)
        {
            return srl.InsStorageRack(srb);
        }
    }
}