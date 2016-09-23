using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Sales
{
    public class SalesDetailBase
    {
        public DataTable GetList(string strWhere)
        {
            string sql = "";
            DataTable dt = null;
            try
            {
                sql = "select * from T_SalesDetail";
                if (!string.IsNullOrWhiteSpace(strWhere))
                {
                    sql += " where " + strWhere;
                }
                dt = DbHelperSQL.Query(sql).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable GetDetailByMainCode(string SalesCode, string strWhere)
        {
            string sql = "";
            DataTable dt = null;
            try
            {
                sql = string.Format(@"select sd.id,sd.code,bm.materialDaima,sd.materialCode,sd.unit,sd.needNumber,whd.storageRackLocation,money,discountAfterPrice,sd.remark,bm.zhujima,materialName,sd.materiaModel,   
bm.barCode from T_SalesMain sm, T_SalesDetail sd,T_WarehouseMain whm, T_BaseMaterial bm,T_WarehouseDetail whd
where sm.code = sd.salesMainCode and sd.materialCode = whm.materialCode and sd.materialCode = bm.code and 
whm.code = whd.mainCode and sd.salesMainCode = '{0}'", SalesCode);
                if (!string.IsNullOrWhiteSpace(strWhere))
                {
                    sql += " and " + strWhere;
                }
                dt = DbHelperSQL.Query(sql).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}