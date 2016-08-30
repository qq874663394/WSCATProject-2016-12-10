using BaseLayer.Base;
using HelperUtility.Encrypt;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Base
{
    public class StorageRackLogic
    {
        CodingHelper ch = new CodingHelper();
        StorageRackBase sr = new StorageRackBase();

        /// <summary>
        /// 根据父级ID查询货架信息
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public DataTable SelStorageRackByCode(string parentId)
        {
            return ch.DataTableReCoding(sr.SelStorageRackByCode(parentId));
        }
        public DataTable SelStorageRack()
        {
            return sr.SelStorageRack();
        }
        public int DelStorageRackByCode(string code)
        {
            return sr.DelStorageRackByCode(code);
        }
        public int UpdateStorageRackByCode(string code)
        {
            return sr.UpdateStorageRackByCode(code);
        }
        public int InsStorageRack(BaseStorageRack srb)
        {
            return sr.InsStorageRack(srb);
        }
    }
}