﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Purchase
{
    public class PurchaseDetail
    {
        public DataTable GetList(string purchaseCode, string zhujima)
        {
            string sql = "";
            DataTable dt = null;
            try
            {
                sql = "select * from T_PurchaseDetail";
                if (!string.IsNullOrWhiteSpace(purchaseCode) || string.IsNullOrWhiteSpace(zhujima))
                {
                    sql += " where ";
                    if (!string.IsNullOrWhiteSpace(purchaseCode))
                    {
                        sql += string.Format("PurchaseCode='{0}'", purchaseCode);
                    }
                    if (!string.IsNullOrWhiteSpace(purchaseCode) && string.IsNullOrWhiteSpace(zhujima))
                    {
                        sql += " and ";
                    }
                    if (!string.IsNullOrWhiteSpace(zhujima))
                    {
                        sql += string.Format("zhujima like '%{0}%'", zhujima);
                    }
                }
                dt = DbHelperSQL.Query(sql).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public decimal GetCheckNumber(string purchaseCode,string code)
        {
            string sql = "";
            decimal result = 0;
            try
            {
                sql = string.Format("select number from T_PurchaseDetail where purchaseCode='{0}' and code='{1}'", purchaseCode,code);
                result = Convert.ToDecimal(DbHelperSQL.GetSingle(sql));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}