using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Finance
{
    class FinanceSummaryLibraryBase
    {
        /// <summary>
        /// 获取最新的code
        /// </summary>
        /// <returns></returns>
        public string AddParentNode(string name)
        {
            string code = 
            string sql = string.Format("insert into [T_FinanceSummaryLibrary] (code,name) values('{0}','{1}')",,name);
            return DbHelperSQL.GetSingle(sql).ToString();
        }
    }
}
