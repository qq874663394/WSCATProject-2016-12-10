using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Base
{
    public class MaterialBase
    {
        public int SetMaterialNumber(string materialCode, string price)
        {
            string sql = "";
            int result = 0;
            try
            {
                sql = string.Format(@"update T_BaseMaterial set price={0} where code='{1}'", price, materialCode);
                result = DbHelperSQL.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
