using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;
using System.Collections;

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
                strSql.Append(@"select 
                tw.id as ID,
                tw.code as 出库单号,
                tw.type as 单据类型,
                tw.salesCode as 销售单号,
                tw.date as 开单时间,
                tw.state as 单据状态,
                tw.operation as 出库员,
                tw.examine as 审核人,
                tw.remark as 备注,
                ts.transportMathod as 运送方式,
                ts.logistics as 快递名称,
                ts.logisticsOddCode as 快递单号,
                ts.logisticsPhone as 快递电话
                ");
                strSql.Append(" from T_WarehouseOut tw ,T_SalesMain ts where tw.salesCode=ts.code  ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" and tw.type='" + strWhere + "'");
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
        /// <summary>
        /// 修改出库单
        /// </summary>
        /// <param name="wo"></param>
        /// <returns></returns>
        public int update(WarehouseOut wo)
        {
            string strSql = "";
            try
            {
                strSql = string.Format("update T_WarehouseOut set type='{0}'," +
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
        /// <summary>
        /// 更新主表和从表
        /// </summary>
        /// <param name="warehouseOut">主表：只有一行数据</param>
        /// <param name="listModel">从表：多行，用List类型保存多条model的数据</param>
        public void update(WarehouseOut warehouseOut, List<WarehouseOutDetail> listModel)
        {
            //多条数据的
            string sqlToList = @"UPDATE T_WarehouseOutDetail
   SET code = @code
      , materialCode = @materialCode
      , materiaName = @materiaName
      , materiaModel = @materiaModel
      , materiaUnit = @materiaUnit
      , number = @number
      , price = @price
      , money = @money
      , barcode = @barcode
      , rfid = @rfid
      , updateDate = @updateDate
      , state = @state
      , date = @date
      , isClear = @isClear
      , reserved1 = @reserved1
      , reserved2 = @reserved2
      , remark = @remark
      , storageRackName = @storageRackName
      , storageRackCode = @storageRackCode
      , isArrive = @isArrive
      , warehouseCode = @warehouseCode
      , warehouseName = @warehouseName
      , mainCode = @mainCode";
            //多条数据的值  要添加到list里   就像普通的参数集  不过最后添加到list里了
            List<SqlParameter[]> listParameter = new List<SqlParameter[]>();//存储参数集合的list
            SqlParameter[] sps;  //定义一个参数集合
            foreach (var item in listModel)
            {
                sps = new SqlParameter[]
                {
                        new SqlParameter("@code",item.code),
                        new SqlParameter("@materialCode",item.WarehouseCode),
                        new SqlParameter("@materiaModel",item.materiaModel),
                        new SqlParameter("@materiaUnit",item.materiaUnit),
                        new SqlParameter("@number",item.number),
                        new SqlParameter("@price",item.price),
                        new SqlParameter("@barcode",item.barcode),
                        new SqlParameter("@rfid",item.rfid),
                        new SqlParameter("@updateDate",item.updateDate),
                        new SqlParameter("@state",item.state),
                        new SqlParameter("@date",item.date),
                        new SqlParameter("@isClear",item.isClear),
                        new SqlParameter("@reserved1",item.reserved1),
                        new SqlParameter("@reserved2",item.reserved2),
                        new SqlParameter("@remark",item.remark),
                        new SqlParameter("@storageRackName",item.StorageRackName),
                        new SqlParameter("@storageRackCode",item.StorageRackCode),
                        new SqlParameter("@isArrive",item.StorageRackCode),
                        new SqlParameter("@warehouseCode",item.WarehouseCode),
                        new SqlParameter("@warehouseName",item.WarehouseName),
                        new SqlParameter("@mainCode",item.MainCode)
                };
                listParameter.Add(sps);
            }

            Hashtable htKey = new Hashtable();  //参数要求
            string sql = @"UPDATE T_WarehouseOut
SET type = @type
,stock = @stock
,operation = @operation
,examine = @examine
,isClear = @isClear
,updateDate = @updateDate
,state = @state
,salesCode = @salesCode
,date = @date
,checkState = @checkState
,remark = @remark
,reserved1 = @reserved1
,reserved2 = @reserved2
,delivey = @delivey
,clientCode = @clientCode
,expressOdd = @expressOdd
,expressMan = @expressMan
,expressPhone = @expressPhone
,defaultType = @defaultType
 WHERE code = @code";  //主表的
            SqlParameter[] parameters = //主表参数
            {
                            new SqlParameter("@code",warehouseOut.code),
                            new SqlParameter("@type",warehouseOut.type),
                            new SqlParameter("@stock",warehouseOut.stock),
                            new SqlParameter("@operation",warehouseOut.operation),
                            new SqlParameter("@examine",warehouseOut.examine),
                            new SqlParameter("@isClear",warehouseOut.isClear),
                            new SqlParameter("@updateDate",warehouseOut.updateDate),
                            new SqlParameter("@state",warehouseOut.state),
                            new SqlParameter("@salesCode",warehouseOut.salesCode),
                            new SqlParameter("@date",warehouseOut.date),
                            new SqlParameter("@checkState",warehouseOut.checkState),
                            new SqlParameter("@remark",warehouseOut.remark),
                            new SqlParameter("@reserved1",warehouseOut.reserved1),
                            new SqlParameter("@reserved2",warehouseOut.reserved2),
                            new SqlParameter("@delivey",warehouseOut.Delivery),
                            new SqlParameter("@clientCode",warehouseOut.ClientCode),
                            new SqlParameter("@expressOdd",warehouseOut.ExpressOdd),
                            new SqlParameter("@expressMan",warehouseOut.ExpressMan),
                            new SqlParameter("@expressPhone",warehouseOut.ExpressPhone),
                            new SqlParameter("@defaultType",warehouseOut.DefaultType)
            };
            htKey.Add(sql, parameters);//sql语句和主表的参数集合
            try
            {
                DbHelperSQL.ExecuteSqlTran(htKey, sqlToList, listParameter);
            }
            catch (Exception ex)
            {
            }
        }

        public int delete(string code)
        {
            string strSql = "";
            try
            {
                strSql = string.Format("delete from T_WarehouseOut where code={0}", code);
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
