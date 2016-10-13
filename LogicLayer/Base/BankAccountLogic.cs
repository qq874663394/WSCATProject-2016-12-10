using BaseLayer.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Base
{
    public class BankAccountLogic
    {
        BankAccountBase _dal = new BankAccountBase();
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="fieldName">条件字段名,0:code 1:模糊查询持卡人</param>
        /// <param name="fieldValue">条件值</param>
        /// <param name="isClear">是否检索所有删除状态,true:检索已删除和未删除的数据，false:只检索未删除的数据</param>
        /// <param name="isEnable">是否检索所有禁用状态,true:检索已禁用和未禁用的数据，false:只检索未禁用的数据</param>
        /// <returns></returns>
        public DataTable GetList(int fieldName, string fieldValue, bool isClear, bool isEnable)
        {
            DataTable dt = null;
            string strWhere = "";
            try
            {
                switch (fieldName)
                {
                    case 0:
                        strWhere += string.Format("and code='{0}'", fieldName);
                        break;
                    case 1:
                        strWhere += string.Format("and cardHolder like '%{0}%'",fieldName);
                        break;
                }
                if (isClear == false)
                {
                    strWhere += string.Format("and isClear=1");
                }
                if (isEnable == false)
                {
                    strWhere += string.Format("and isEnable=1");
                }
                dt = _dal.GetList(strWhere).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}
