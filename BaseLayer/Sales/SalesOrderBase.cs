using Model;
using Model.Sales;
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
    public class SalesOrderBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelDetail"></param>
        /// <returns></returns>
        public object AddSalesOrToDetail(SalesOrder model, List<SalesOrderDetail> modelDetail)
        {
            List<SqlParameter[]> list = new List<SqlParameter[]>();
            Hashtable hashTable = new Hashtable();
            object result = null;
            string sqlDetail = "";
            string sqlMain = "";
            try
            {

                sqlMain = @"INSERT INTO [T_SalesOrder] VALUES (@clientCode,@deliversDate,@deliversMethod,@remark,@code,@date,@deliversLocation);select scope_identity()";
                SqlParameter[] spsMain =
                {
                    new SqlParameter("@clientCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@deliversDate", SqlDbType.DateTime,3),
                    new SqlParameter("@deliversMethod", SqlDbType.Int,4),
                    new SqlParameter("@remark", SqlDbType.NVarChar,200),
                    new SqlParameter("@code", SqlDbType.NVarChar,50),
                    new SqlParameter("@date", SqlDbType.DateTime,3),
                    new SqlParameter("@deliversLocation", SqlDbType.NVarChar,50)
                };
                spsMain[0].Value = model.clientCode;
                spsMain[1].Value = model.deliversDate;
                spsMain[2].Value = model.deliversMethod;
                spsMain[3].Value = model.remark;
                spsMain[4].Value = model.code;
                spsMain[5].Value = model.date;
                spsMain[6].Value = model.deliversLocation;
                hashTable.Add(sqlMain, spsMain);
                sqlDetail = @"INSERT INTO [T_SalesOrderDetail] VALUES (@code,@materialCode,@materialNumber,@materialPrice,@discountRate,@VATRate,@discountMoney,@tax,@taxTotal,@remark,@deliveryNumber,@mainCode)";

                foreach (var item in modelDetail)
                {
                    SqlParameter[] spsDetail =
                    {
                    new SqlParameter("@materialCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@materialNumber", SqlDbType.Decimal,9),
                    new SqlParameter("@materialPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@discountRate", SqlDbType.Decimal,9),
                    new SqlParameter("@VATRate", SqlDbType.Decimal,9),
                    new SqlParameter("@discountMoney", SqlDbType.Decimal,9),
                    new SqlParameter("@tax", SqlDbType.Decimal,9),
                    new SqlParameter("@taxTotal", SqlDbType.Decimal,9),
                    new SqlParameter("@remark", SqlDbType.NVarChar,200),
                    new SqlParameter("@deliveryNumber", SqlDbType.Decimal,9),
                    new SqlParameter("@mainCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@code",SqlDbType.NVarChar,50)
                    };
                    spsDetail[0].Value = item.materialCode;
                    spsDetail[1].Value = item.materialNumber;
                    spsDetail[2].Value = item.materialPrice;
                    spsDetail[3].Value = item.discountRate;
                    spsDetail[4].Value = item.VATRate;
                    spsDetail[5].Value = item.discountMoney;
                    spsDetail[6].Value = item.tax;
                    spsDetail[7].Value = item.taxTotal;
                    spsDetail[8].Value = item.remark;
                    spsDetail[9].Value = item.deliveryNumber;
                    spsDetail[10].Value = item.mainCode;
                    spsDetail[11].Value = item.code;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelDetail"></param>
        /// <returns></returns>
        public object UpdateSalesOrToDetail(SalesOrder model, List<SalesOrderDetail> modelDetail)
        {
            List<SqlParameter[]> list = new List<SqlParameter[]>();
            Hashtable hashTable = new Hashtable();
            object result = null;
            string sqlDetail = "";
            string sqlMain = "";
            try
            {
                sqlMain = @"update [T_SalesOrder] set 
[clientCode] = @clientCode,
[deliversDate] = @deliversDate,
[deliversMethod] = @deliversMethod,
[remark] = @remark,
[date] = @date,
[deliversLocation] = @deliversLocation where [code] = @code;select scope_identity()";
                SqlParameter[] spsMain =
                {
                    new SqlParameter("@clientCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@deliversDate", SqlDbType.DateTime,3),
                    new SqlParameter("@deliversMethod", SqlDbType.Int,4),
                    new SqlParameter("@remark", SqlDbType.NVarChar,200),
                    new SqlParameter("@code", SqlDbType.NVarChar,50),
                    new SqlParameter("@date", SqlDbType.DateTime,3),
                    new SqlParameter("@deliversLocation", SqlDbType.NVarChar,50)
                };
                spsMain[0].Value = model.clientCode;
                spsMain[1].Value = model.deliversDate;
                spsMain[2].Value = model.deliversMethod;
                spsMain[3].Value = model.remark;
                spsMain[4].Value = model.code;
                spsMain[5].Value = model.date;
                spsMain[6].Value = model.deliversLocation;
                hashTable.Add(sqlMain, spsMain);
                sqlDetail = @"update [T_SalesOrderDetail] set 
[id] = @id,
[code]=@code,
[materialCode] = @materialCode,
[materialNumber] = @materialNumber,
[materialPrice] = @materialPrice,
[discountRate] = @discountRate,
[VATRate] = @VATRate,
[discountMoney] = @discountMoney,
[tax] = @tax,
[taxTotal] = @taxTotal,
[remark] = @remark,
[deliveryNumber] = @deliveryNumber where mainCode=@mainCode";

                foreach (var item in modelDetail)
                {
                    SqlParameter[] spsDetail =
                    {
                    new SqlParameter("@materialCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@materialNumber", SqlDbType.Decimal,9),
                    new SqlParameter("@materialPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@discountRate", SqlDbType.Decimal,9),
                    new SqlParameter("@VATRate", SqlDbType.Decimal,9),
                    new SqlParameter("@discountMoney", SqlDbType.Decimal,9),
                    new SqlParameter("@tax", SqlDbType.Decimal,9),
                    new SqlParameter("@taxTotal", SqlDbType.Decimal,9),
                    new SqlParameter("@remark", SqlDbType.NVarChar,200),
                    new SqlParameter("@DeliveryNumber", SqlDbType.Decimal,9),
                    new SqlParameter("@mainCode", SqlDbType.NVarChar,50)
                    };
                    spsDetail[0].Value = item.materialCode;
                    spsDetail[1].Value = item.materialNumber;
                    spsDetail[2].Value = item.materialPrice;
                    spsDetail[3].Value = item.discountRate;
                    spsDetail[4].Value = item.VATRate;
                    spsDetail[5].Value = item.discountMoney;
                    spsDetail[6].Value = item.tax;
                    spsDetail[7].Value = item.taxTotal;
                    spsDetail[8].Value = item.remark;
                    spsDetail[9].Value = item.deliveryNumber;
                    spsDetail[10].Value = item.mainCode;
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
