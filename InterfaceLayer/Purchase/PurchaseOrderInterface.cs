using LogicLayer.Purchase;
using Model.Purchase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Purchase
{
    public class PurchaseOrderInterface
    {
        PurchaseOrderLogic _dal = new PurchaseOrderLogic();
        /// <summary>
        /// 保存审核公用
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelDetail"></param>
        /// <returns></returns>
        public object AddOrUpdateToMainOrDetail(PurchaseOrder model, List<PurchaseOrderDetail> modelDetail)
        {
            return _dal.AddOrUpdateToMainOrDetail(model, modelDetail);
        }
        public DataTable GetMainTable()
        {
            return _dal.GetMainTable();
        }
        public DataTable GetMinorTable()
        {
            return _dal.GetMinorTable();
        }
    }
}
