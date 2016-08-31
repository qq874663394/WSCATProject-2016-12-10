using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Base
{
    public class SupplierBase
    {
        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns>所有数据以DataTable的形式返回</returns>
        public DataTable SelSupplierTable()
        {
            string sql = "";
            DataSet ds = null;
            try
            {
                sql = @"select 
                        Su_Code as 编码,
                        Su_Name as 单位名称,
                        Su_Address as 通讯地址,
                        Su_EmpName as 联系人,
                        Su_EmpPhone as 联系手机,
                        Su_fax as 传真,
                        Su_Email as 邮箱,
                        Su_Credit as 信用等级,
                        Su_Money as 账款额度,
                        Su_Surplus as 剩余额度,
                        Su_Reckoning as 月结日,
                        Su_Area as 城市,
                        Su_Remark as 备注,
                        Su_Enable
                        from T_Supplier where Su_Clear=1 and Su_Enable=1";
            }
            catch
            {
                throw new Exception("-1");
            }
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, DbHelperSQL.connectionString);
                ds = new DataSet();
                adapter.Fill(ds, "T_Supplier");
            }
            catch
            {
                throw new Exception("-2");
            }
            return ds.Tables[0];
        }
    }
}
