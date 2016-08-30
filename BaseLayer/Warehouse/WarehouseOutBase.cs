using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BaseLayer
{
    public class WarehouseOutBase
    {
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append("select id,code,type,stock,operation,examine,isClear,updateDate,");
                strSql.Append("state,salesCode,date,checkState,remark,reserved1,reserved2,delivery,clientCode,");
                strSql.Append("expressOdd,expressMan,expressPhone,defaultType ");
                strSql.Append(" FROM T_WarehouseOut ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
            }
            catch
            {
                throw new Exception("-1");
            }
            try
            {
                return DbHelperSQL.Query(strSql.ToString());
            }
            catch
            {
                throw new Exception("-4");
            }
            
        }

        public DataSet GetListByMainCode(string code)
        {

        }

        public int Add()
        {
            string strSql = "";
            try
            {
                strSql = string.Format("insert into T_WarehouseOut(code," +
                    "type,stock,operation,examine,isClear,updateDate," +
                    "warehouseOutState,salesCode,date,checkState,remark," +
                    "reserved1,reserved2,delivery,clientCode,expressOdd," +
                    "expressMan,expressPhone,defaultType) values (" +
                    "'{0}','{1}','{2}','{3}','{4}',{5},'{6}',{7},'{8}','{9}',{10}," +
                    "'{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}') ",
                    );
            }
            catch
            {

            }
        }

        public int update()
        {

        }

        public int delete()
        {

        }
    }
}
