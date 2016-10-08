using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Sales
{
    public class SalesMainBase
    {
        /// <summary>
        /// 复合查询
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetList(string strWhere)
        {
            DataTable dt = null;
            string sql = "select * from T_SalesMain";
            try
            {
                if (!string.IsNullOrWhiteSpace(strWhere))
                {
                    sql += " where " + strWhere;
                }
                dt = DbHelperSQL.Query(sql).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        /// <summary>
        /// 查询id和code列
        /// </summary>
        /// <param name="clientCode">客户code</param>
        /// <returns></returns>
        public DataTable GetTableByClientCode(string clientCode)
        {
            string sql = "";
            DataTable dt = null;
            try
            {
                sql = string.Format("select id,code from T_SalesMain where clientCode='{0}'", clientCode);
                dt = DbHelperSQL.Query(sql).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelDetail"></param>
        /// <returns></returns>
        public object AddSalesOrToDetail(SalesMain model, List<SalesDetail> modelDetail)
        {
            List<SqlParameter[]> list = new List<SqlParameter[]>();
            Hashtable hashTable = new Hashtable();
            object result = null;
            string sqlDetail = "";
            string sqlMain = "";
            try
            {

                sqlMain = @"INSERT INTO [T_SalesMain] VALUES (@isClear,@code,@type,@date,@transportMathod,@salesOrderState,@salesManCode,@checkState,@changeDate,@operationMan,@checkMan,@salesMan,@payState,@inWarehouseState,@payMathod,@deliveryDate,@logistics,@logisticsOddCode,@logisticsPhone,@oddAllMoney,@accountCode,@collectMoney,@lastMoney,@clientAddress@,@clientCode,@clientName,@clientPhone,@linkMan,@urgentState,@expireDate,@updateDate,@remark,@reserved1,@reserved2);select scope_identity()";
                SqlParameter[] spsMain =
                {
                    new SqlParameter("@changeDate", SqlDbType.DateTime),
                    new SqlParameter("@operationMan", SqlDbType.NVarChar,-1),
                    new SqlParameter("@checkMan", SqlDbType.NVarChar,-1),
                    new SqlParameter("@salesMan", SqlDbType.NVarChar,-1),
                    new SqlParameter("@payState", SqlDbType.Int,4),
                    new SqlParameter("@inWarehouseState", SqlDbType.Int,4),
                    new SqlParameter("@payMathod", SqlDbType.NVarChar,-1),
                    new SqlParameter("@deliveryDate", SqlDbType.DateTime),
                    new SqlParameter("@logistics", SqlDbType.NVarChar,-1),
                    new SqlParameter("@logisticsOddCode", SqlDbType.NVarChar,-1),
                    new SqlParameter("@isClear", SqlDbType.Int,4),
                    new SqlParameter("@logisticsPhone", SqlDbType.NVarChar,-1),
                    new SqlParameter("@oddAllMoney", SqlDbType.Decimal),
                    new SqlParameter("@accountCode", SqlDbType.NVarChar,-1),
                    new SqlParameter("@collectMoney", SqlDbType.Decimal),
                    new SqlParameter("@lastMoney", SqlDbType.Decimal),
                    new SqlParameter("@clientAddress", SqlDbType.NVarChar,-1),
                    new SqlParameter("@clientCode", SqlDbType.NVarChar,-1),
                    new SqlParameter("@clientName", SqlDbType.NVarChar,-1),
                    new SqlParameter("@clientPhone", SqlDbType.NVarChar,-1),
                    new SqlParameter("@linkMan", SqlDbType.NVarChar,-1),
                    new SqlParameter("@code", SqlDbType.NVarChar,-1),
                    new SqlParameter("@urgentState", SqlDbType.Int,4),
                    new SqlParameter("@expireDate", SqlDbType.DateTime),
                    new SqlParameter("@updateDate", SqlDbType.DateTime),
                    new SqlParameter("@remark", SqlDbType.NVarChar,-1),
                    new SqlParameter("@reserved1", SqlDbType.NVarChar,-1),
                    new SqlParameter("@reserved2", SqlDbType.NVarChar,-1),
                    new SqlParameter("@type", SqlDbType.NVarChar,-1),
                    new SqlParameter("@date", SqlDbType.DateTime),
                    new SqlParameter("@transportMathod", SqlDbType.NVarChar,-1),
                    new SqlParameter("@salesOrderState", SqlDbType.Int,4),
                    new SqlParameter("@salesManCode", SqlDbType.NVarChar,-1),
                    new SqlParameter("@checkState", SqlDbType.Int,4)
                };
                spsMain[0].Value = model.checkState;
                spsMain[1].Value =model.changeDate;
                spsMain[2].Value =model.operationMan;
                spsMain[3].Value =model.checkMan;
                spsMain[4].Value =model.salesMan;
                spsMain[5].Value =model.payState;
                spsMain[6].Value =model.inWarehouseState;
                spsMain[7].Value =model.payMathod;
                spsMain[8].Value =model.deliveryDate;
                spsMain[9].Value = model.logistics;
                spsMain[10].Value =model.logisticsOddCode;
                spsMain[11].Value =model.isClear;
                spsMain[12].Value =model.logisticsPhone;
                spsMain[13].Value =model.oddAllMoney;
                spsMain[14].Value =model.accountCode;
                spsMain[15].Value =model.collectMoney;
                spsMain[16].Value =model.lastMoney;
                spsMain[17].Value =model.clientAddress;
                spsMain[18].Value =model.clientCode;
                spsMain[19].Value =model.clientName;
                spsMain[20].Value =model.clientPhone;
                spsMain[21].Value =model.linkMan;
                spsMain[22].Value =model.code;
                spsMain[23].Value =model.urgentState;
                spsMain[24].Value =model.expireDate;
                spsMain[25].Value =model.updateDate;
                spsMain[26].Value =model.remark;
                spsMain[27].Value =model.reserved1;
                spsMain[28].Value =model.reserved2;
                spsMain[29].Value =model.type;
                spsMain[30].Value =model.date;
                spsMain[31].Value =model.transportMathod;
                spsMain[32].Value =model.salesOrderState;
                spsMain[33].Value =model.salesManCode;
                hashTable.Add(sqlMain, spsMain);
                sqlDetail = @"INSERT INTO [T_SalesDetail] VALUES (@isClear,@code,@salesMainCode,@storageCode,@storageName,@materialDaima,@materialCode,@materialName,@materiaModel,@unit,@needNumber,@actualNumber,@lostNumber,@discountBeforePrice,@discount,@discountAfterPrice,@money,@reserved1,@reserved2,@remark,@updateDate,@productionDate,@qualityDate,@effectiveDate,@zhujima,@barCode)";

                foreach (var item in modelDetail)
                {
                    SqlParameter[] spsDetail =
                    {
                    new SqlParameter("@materiaModel", SqlDbType.NVarChar,-1),
                    new SqlParameter("@unit", SqlDbType.NVarChar,-1),
                    new SqlParameter("@needNumber", SqlDbType.Decimal),
                    new SqlParameter("@actualNumber", SqlDbType.Decimal),
                    new SqlParameter("@lostNumber", SqlDbType.Decimal),
                    new SqlParameter("@discountBeforePrice", SqlDbType.Decimal),
                    new SqlParameter("@discount", SqlDbType.Decimal),
                    new SqlParameter("@discountAfterPrice", SqlDbType.Decimal),
                    new SqlParameter("@money", SqlDbType.Decimal),
                    new SqlParameter("@reserved1", SqlDbType.NVarChar,-1),
                    new SqlParameter("@isClear", SqlDbType.Int,4),
                    new SqlParameter("@reserved2", SqlDbType.NVarChar,-1),
                    new SqlParameter("@remark", SqlDbType.NVarChar,-1),
                    new SqlParameter("@updateDate", SqlDbType.DateTime),
                    new SqlParameter("@productionDate", SqlDbType.DateTime),
                    new SqlParameter("@qualityDate", SqlDbType.Decimal),
                    new SqlParameter("@effectiveDate", SqlDbType.DateTime),
                    new SqlParameter("@zhujima", SqlDbType.NVarChar,-1),
                    new SqlParameter("@barCode", SqlDbType.NVarChar,-1),
                    new SqlParameter("@code", SqlDbType.NVarChar,-1),
                    new SqlParameter("@salesMainCode", SqlDbType.NVarChar,-1),
                    new SqlParameter("@storageCode", SqlDbType.NVarChar,-1),
                    new SqlParameter("@storageName", SqlDbType.NVarChar,-1),
                    new SqlParameter("@materialDaima", SqlDbType.NVarChar,-1),
                    new SqlParameter("@materialCode", SqlDbType.NVarChar,-1),
                    new SqlParameter("@materialName", SqlDbType.NVarChar,-1)
                    };
                    spsDetail[0].Value =item.materialName;
                    spsDetail[1].Value =item.materiaModel;
                    spsDetail[2].Value =item.unit;
                    spsDetail[3].Value =item.needNumber;
                    spsDetail[4].Value =item.actualNumber;
                    spsDetail[5].Value =item.lostNumber;
                    spsDetail[6].Value =item.discountBeforePrice;
                    spsDetail[7].Value =item.discount;
                    spsDetail[8].Value =item.discountAfterPrice;
                    spsDetail[9].Value = item.money;
                    spsDetail[10].Value = item.reserved1;
                    spsDetail[11].Value =item.isClear;
                    spsDetail[12].Value =item.reserved2;
                    spsDetail[13].Value =item.remark;
                    spsDetail[14].Value =item.updateDate;
                    spsDetail[15].Value =item.productionDate;
                    spsDetail[16].Value =item.qualityDate;
                    spsDetail[17].Value =item.effectiveDate;
                    spsDetail[18].Value =item.zhujima;
                    spsDetail[19].Value =item.barCode;
                    spsDetail[20].Value =item.code;
                    spsDetail[21].Value =item.salesMainCode;
                    spsDetail[22].Value =item.storageCode;
                    spsDetail[23].Value =item.storageName;
                    spsDetail[24].Value =item.materialDaima;
                    spsDetail[25].Value =item.materialCode;
                    list.Add(spsDetail);
                }
                result = DbHelperSQL.ExecuteSqlTranScalar(hashTable, sqlDetail, list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}