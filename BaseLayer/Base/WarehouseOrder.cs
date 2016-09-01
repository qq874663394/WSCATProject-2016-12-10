using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Base
{
    public class WarehouseOrder
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
            }
            catch
            {
                throw new Exception("-1");
            }
            try
            {
                result = DbHelperSQL.ExecuteSql(sql);
            }
            catch
            {
                throw new Exception("-2");
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
            }
            catch
            {
                throw new Exception("-1");
            }
            try
            {
                result = DbHelperSQL.ExecuteSql(sql);
            }
            catch
            {
                throw new Exception("-2");
            }
            return result;
        }
    }
}
