﻿using LogicLayer.Purchase;
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
        /// <summary>
        /// 主表
        /// </summary>
        /// <returns></returns>
        public DataTable GetMainTable(string supplierCode)
        {
            return _dal.GetMainTable(supplierCode);
        }
        /// <summary>
        /// 从表
        /// </summary>
        /// <returns></returns>
        public DataTable GetMinorTable()
        {
            return _dal.GetMinorTable();
        }
        public DataTable GetJoinSearch(string mainCode, string detailCode)
        {
            return _dal.GetJoinSearch(mainCode, detailCode);
        }
        public bool Exists(string code)
        {
            return _dal.Exists(code);
        }
    }
}
