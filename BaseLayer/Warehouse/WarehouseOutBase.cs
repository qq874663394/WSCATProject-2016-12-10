using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;

namespace BaseLayer
{
    public class WarehouseOutBase
    {
        /// <summary>
        /// 根据where条件获取出库单列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append("select id,code,type,stock,operation,examine,isClear,updateDate,");
                strSql.Append("state,salesCode,date,checkState,remark,reserved1,reserved2,delivey,clientCode,");
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

        /// <summary>
        /// 添加出库单
        /// </summary>
        /// <param name="wo"></param>
        /// <returns></returns>
        public int Add(WarehouseOut wo)
        {
            string strSql = "";
            try
            {
                strSql = string.Format("insert into T_WarehouseOut(code," +
                    "type,stock,operation,examine,isClear,updateDate," +
                    "state,salesCode,date,checkState,remark," +
                    "reserved1,reserved2,delivery,clientCode,expressOdd," +
                    "expressMan,expressPhone,defaultType) values (" +
                    "'{0}','{1}','{2}','{3}','{4}',{5},'{6}',{7},'{8}','{9}',{10}," +
                    "'{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}') ",
                    wo.code,
                    wo.type,
                    wo.stock,
                    wo.operation,
                    wo.examine,
                    wo.isClear,
                    wo.updateDate,
                    wo.state,
                    wo.salesCode,
                    wo.date,
                    wo.checkState,
                    wo.remark,
                    wo.reserved1,
                    wo.reserved2,
                    wo.Delivery,
                    wo.ClientCode,
                    wo.ExpressOdd,
                    wo.ExpressMan,
                    wo.ExpressPhone,
                    wo.DefaultType);
            }
            catch
            {
                return -1;
            }
            try
            {
                int result = DbHelperSQL.ExecuteSql(strSql);
                return result;
            }
            catch
            {
                return -2;
            }
        }

        public int update(WarehouseOut wo)
        {
            string strSql = "";
            try
            {
                strSql = string.Format("update T_WarehouseOut set type='{0}',"+
                    "stock='{1}',operation='{2}',examine='{3}'," +
                    "isClear={4},updateDate='{5}',state={6},salesCode='{7}',date='{8}'," +
                    "checkState={9},remark='{10}',reserved1='{11}',reserved2='{12}'," +
                    "delivery='{13}',clientCode='{14}',expressOdd='{15}'," +
                    "expressMan='{16}',expressPhone='{17}',defaultType='{18}' where code='{19}'",
                    wo.type,
                    wo.stock,
                    wo.operation,
                    wo.examine,
                    wo.isClear,
                    wo.updateDate,
                    wo.state,
                    wo.salesCode,
                    wo.date,
                    wo.checkState,
                    wo.remark,
                    wo.reserved1,
                    wo.reserved2,
                    wo.Delivery,
                    wo.ClientCode,
                    wo.ExpressOdd,
                    wo.ExpressMan,
                    wo.ExpressPhone,
                    wo.DefaultType,
                    wo.code);
            }
            catch
            {
                return -1;
            }
            int result = 0;
            try
            {
                result = DbHelperSQL.ExecuteSql(strSql);
            }
            catch
            {
                return -2;
            }
            return result;
        }
        
        public int delete(string code)
        {
            string strSql = "";
            try
            {
                strSql = string.Format("delete from T_WarehouseOut where code={0}",code);
            }
            catch
            {
                return -1;
            }
            int result = 0;
            try
            {
                result = DbHelperSQL.ExecuteSql(strSql);
            }
            catch
            {
                return -2;
            }

            return result;
        }
    }
}
