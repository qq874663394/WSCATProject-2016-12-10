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
                        code as 编码,
                        name as 单位名称,
                        address as 通讯地址,
                        linkMan as 联系人,
                        mobilePhone as 联系手机,
                        fax as 传真,
                        email as 邮箱,
                        creditRank as 信用等级,
                        availableBalance as 账款额度,
                        balance as 剩余额度,
                        statementDate as 月结日,
                        cityName as 城市,
                        remark as 备注,
                        isEnable
                        from T_BaseSupplier where isClear=1 and isEnable=1";
            }
            catch
            {
                throw new Exception("-1");
            }
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, DbHelperSQL.connectionString);
                ds = new DataSet();
                adapter.Fill(ds, "T_BaseSupplier");
            }
            catch
            {
                throw new Exception("-2");
            }
            return ds.Tables[0];
        }
    }
}
