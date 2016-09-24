using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Warehouse
{
    public class WarehouseMainBase
    {
        /// <summary>
        /// 减少库存
        /// </summary>
        /// <param name="number"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public int updateReduce(int number, string code)
        {
            string sql = "";
            int result = 0;
            try
            {
                sql = string.Format("update T_WarehouseMain set enaNumber=enaNumber-{0} where code='{1}'", number, code);
                result = DbHelperSQL.ExecuteSql(sql);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>
        /// 增加库存
        /// </summary>
        /// <param name="number"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public int updateAug(int number,string code)
        {
            string sql = "";
            int result = 0;
            try
            {
                sql = string.Format("update T_WarehouseMain set enaNumber=enaNumber+{0} where code='{1}'", number, code);
                result = DbHelperSQL.ExecuteSql(sql);
            }
            catch(Exception ex)
            {
                throw ex;
            }                
            return result;
        }
        /// <summary>
        /// 林修改方法 查询库存商品
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetMaterialDetail(string fieldValue)
        {
            string sql = "";
            DataSet ds = null;
            try
            {
                sql = @"select * from T_BaseMaterial bm,T_WarehouseMain tw,T_WarehouseDetail td 
                    where tw.materialCode=bm.code and tw.code=td.warehouseOrdercode";
                if (fieldValue.Trim() != "")
                {
                    sql += " and tw.storageCode='"+ fieldValue + "' ";
                }
                ds = DbHelperSQL.Query(sql);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }
    }
}
