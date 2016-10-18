using Model.Purchase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Purchase
{
    public class PurchaseOrderBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelDetail"></param>
        /// <returns></returns>
        public object AddPurchaseOrderOrToDetail(PurchaseOrder model, List<PurchaseOrderDetail> modelDetail)
        {
            List<SqlParameter[]> list = new List<SqlParameter[]>();
            Hashtable hashTable = new Hashtable();
            object result = null;
            StringBuilder sqlMain = new StringBuilder();
            StringBuilder sqlDetail = new StringBuilder();
            try
            {

                sqlMain.Append("insert into [T_PurchaseOrder] (");
                sqlMain.Append("supplierCode,deliversDate,deliversMethod,remark,code,date,deliversLocation,operation,makeMan,examine,checkState,depositReceived,examineDate)");
                sqlMain.Append(" values (");
                sqlMain.Append("@supplierCode,@deliversDate,@deliversMethod,@remark,@code,@date,@deliversLocation,@operation,@makeMan,@examine,@checkState,@depositReceived,@examineDate)");
                sqlMain.Append(";select @@IDENTITY");
                SqlParameter[] spsMain =
                {
                    new SqlParameter("@supplierCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@deliversDate", SqlDbType.Date,3),
                    new SqlParameter("@deliversMethod", SqlDbType.Int,4),
                    new SqlParameter("@remark", SqlDbType.NVarChar,200),
                    new SqlParameter("@code", SqlDbType.NVarChar,50),
                    new SqlParameter("@date", SqlDbType.Date,3),
                    new SqlParameter("@deliversLocation", SqlDbType.NVarChar,50),
                    new SqlParameter("@operation", SqlDbType.NVarChar,40),
                    new SqlParameter("@makeMan", SqlDbType.NVarChar,40),
                    new SqlParameter("@examine", SqlDbType.NVarChar,40),
                    new SqlParameter("@checkState", SqlDbType.Int,4),
                    new SqlParameter("@depositReceived", SqlDbType.Decimal,9),
                    new SqlParameter("@examineDate", SqlDbType.DateTime)
                };
                spsMain[0].Value = model.supplierCode;
                spsMain[1].Value = model.deliversDate;
                spsMain[2].Value = model.deliversMethod;
                spsMain[3].Value = model.remark;
                spsMain[4].Value = model.code;
                spsMain[5].Value = model.date;
                spsMain[6].Value = model.deliversLocation;
                spsMain[7].Value = model.operation;
                spsMain[8].Value = model.makeMan;
                spsMain[9].Value = model.examine;
                spsMain[10].Value = model.checkState;
                spsMain[11].Value = model.depositReceived;
                spsMain[12].Value = model.examineDate;

                hashTable.Add(sqlMain, spsMain);
                sqlDetail.Append("insert into [T_PurchaseOrderDetail] (");
                sqlDetail.Append("materialCode,materialNumber,materialPrice,materialMoney,discountRate,VATRate,discountMoney,tax,taxTotal,remark,deliveryNumber,mainCode,code)");
                sqlDetail.Append(" values (");
                sqlDetail.Append("@materialCode,@materialNumber,@materialPrice,@materialMoney,@discountRate,@VATRate,@discountMoney,@tax,@taxTotal,@remark,@deliveryNumber,@mainCode,@code)");
                sqlDetail.Append(";select @@IDENTITY");

                foreach (var item in modelDetail)
                {
                    SqlParameter[] spsDetail =
                    {
                        new SqlParameter("@materialCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@materialNumber", SqlDbType.Decimal,9),
                    new SqlParameter("@materialPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@materialMoney", SqlDbType.Decimal,9),
                    new SqlParameter("@discountRate", SqlDbType.Decimal,9),
                    new SqlParameter("@VATRate", SqlDbType.Decimal,9),
                    new SqlParameter("@discountMoney", SqlDbType.Decimal,9),
                    new SqlParameter("@tax", SqlDbType.Decimal,9),
                    new SqlParameter("@taxTotal", SqlDbType.Decimal,9),
                    new SqlParameter("@remark", SqlDbType.NVarChar,200),
                    new SqlParameter("@deliveryNumber", SqlDbType.Decimal,9),
                    new SqlParameter("@mainCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@code", SqlDbType.NVarChar,50)
                    };
                    spsDetail[0].Value = item.materialCode;
                    spsDetail[1].Value = item.materialNumber;
                    spsDetail[2].Value = item.materialPrice;
                    spsDetail[3].Value = item.materialMoney;
                    spsDetail[4].Value = item.discountRate;
                    spsDetail[5].Value = item.VATRate;
                    spsDetail[6].Value = item.discountMoney;
                    spsDetail[7].Value = item.tax;
                    spsDetail[8].Value = item.taxTotal;
                    spsDetail[9].Value = item.remark;
                    spsDetail[10].Value = item.deliveryNumber;
                    spsDetail[11].Value = item.mainCode;
                    spsDetail[12].Value = item.code;
                    list.Add(spsDetail);
                }
                result = DbHelperSQL.ExecuteSqlTranScalar(hashTable, sqlDetail.ToString(), list);
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
        public object UpdateSalesOrToDetail(PurchaseOrder model, List<PurchaseOrderDetail> modelDetail)
        {
            List<SqlParameter[]> list = new List<SqlParameter[]>();
            Hashtable hashTable = new Hashtable();
            object result = null;
            StringBuilder sqlMain = new StringBuilder();
            StringBuilder sqlDetail = new StringBuilder();
            try
            {
                sqlMain.Append("update [T_PurchaseOrder] set ");
                sqlMain.Append("supplierCode=@supplierCode,");
                sqlMain.Append("deliversDate=@deliversDate,");
                sqlMain.Append("deliversMethod=@deliversMethod,");
                sqlMain.Append("remark=@remark,");
                sqlMain.Append("date=@date,");
                sqlMain.Append("deliversLocation=@deliversLocation,");
                sqlMain.Append("operation=@operation,");
                sqlMain.Append("makeMan=@makeMan,");
                sqlMain.Append("examine=@examine,");
                sqlMain.Append("checkState=@checkState,");
                sqlMain.Append("depositReceived=@depositReceived,");
                sqlMain.Append("examineDate=@examineDate");
                sqlMain.Append(" where code=@code ");
                sqlMain.Append(";select @@IDENTITY");
                SqlParameter[] spsMain =
                {
                    new SqlParameter("@supplierCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@deliversDate", SqlDbType.Date,3),
                    new SqlParameter("@deliversMethod", SqlDbType.Int,4),
                    new SqlParameter("@remark", SqlDbType.NVarChar,200),
                    new SqlParameter("@code", SqlDbType.NVarChar,50),
                    new SqlParameter("@date", SqlDbType.Date,3),
                    new SqlParameter("@deliversLocation", SqlDbType.NVarChar,50),
                    new SqlParameter("@operation", SqlDbType.NVarChar,40),
                    new SqlParameter("@makeMan", SqlDbType.NVarChar,40),
                    new SqlParameter("@examine", SqlDbType.NVarChar,40),
                    new SqlParameter("@checkState", SqlDbType.Int,4),
                    new SqlParameter("@depositReceived", SqlDbType.Decimal,9),
                    new SqlParameter("@examineDate", SqlDbType.DateTime),
                };
                spsMain[0].Value = model.supplierCode;
                spsMain[1].Value = model.deliversDate;
                spsMain[2].Value = model.deliversMethod;
                spsMain[3].Value = model.remark;
                spsMain[4].Value = model.code;
                spsMain[5].Value = model.date;
                spsMain[6].Value = model.deliversLocation;
                spsMain[7].Value = model.operation;
                spsMain[8].Value = model.makeMan;
                spsMain[9].Value = model.examine;
                spsMain[10].Value = model.checkState;
                spsMain[11].Value = model.depositReceived;
                spsMain[12].Value = model.examineDate;

                hashTable.Add(sqlMain, spsMain);
                sqlDetail.Append("update [T_PurchaseOrderDetail] set ");
                sqlDetail.Append("materialCode=@materialCode,");
                sqlDetail.Append("materialNumber=@materialNumber,");
                sqlDetail.Append("materialPrice=@materialPrice,");
                sqlDetail.Append("materialMoney=@materialMoney,");
                sqlDetail.Append("discountRate=@discountRate,");
                sqlDetail.Append("VATRate=@VATRate,");
                sqlDetail.Append("discountMoney=@discountMoney,");
                sqlDetail.Append("tax=@tax,");
                sqlDetail.Append("taxTotal=@taxTotal,");
                sqlDetail.Append("remark=@remark,");
                sqlDetail.Append("deliveryNumber=@deliveryNumber,");
                sqlDetail.Append(" where mainCode=@mainCode and code=@code");

                StringBuilder sqlInsert = new StringBuilder();
                sqlInsert.Append("insert into [T_PurchaseOrderDetail] (");
                sqlInsert.Append("materialCode,materialNumber,materialPrice,materialMoney,discountRate,VATRate,discountMoney,tax,taxTotal,remark,deliveryNumber,mainCode,code)");
                sqlInsert.Append(" values (");
                sqlInsert.Append("@materialCode,@materialNumber,@materialPrice,@materialMoney,@discountRate,@VATRate,@discountMoney,@tax,@taxTotal,@remark,@deliveryNumber,@mainCode,@code)");
                sqlInsert.Append(";select @@IDENTITY");

                foreach (var item in modelDetail)
                {
                    SqlParameter[] spsDetail =
                    {
                    new SqlParameter("@materialCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@materialNumber", SqlDbType.Decimal,9),
                    new SqlParameter("@materialPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@materialMoney", SqlDbType.Decimal,9),
                    new SqlParameter("@discountRate", SqlDbType.Decimal,9),
                    new SqlParameter("@VATRate", SqlDbType.Decimal,9),
                    new SqlParameter("@discountMoney", SqlDbType.Decimal,9),
                    new SqlParameter("@tax", SqlDbType.Decimal,9),
                    new SqlParameter("@taxTotal", SqlDbType.Decimal,9),
                    new SqlParameter("@remark", SqlDbType.NVarChar,200),
                    new SqlParameter("@deliveryNumber", SqlDbType.Decimal,9),
                    new SqlParameter("@mainCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@code", SqlDbType.NVarChar,50)
                    };
                    spsDetail[0].Value = item.materialCode;
                    spsDetail[1].Value = item.materialNumber;
                    spsDetail[2].Value = item.materialPrice;
                    spsDetail[3].Value = item.materialMoney;
                    spsDetail[4].Value = item.discountRate;
                    spsDetail[5].Value = item.VATRate;
                    spsDetail[6].Value = item.discountMoney;
                    spsDetail[7].Value = item.tax;
                    spsDetail[8].Value = item.taxTotal;
                    spsDetail[9].Value = item.remark;
                    spsDetail[10].Value = item.deliveryNumber;
                    spsDetail[11].Value = item.mainCode;
                    spsDetail[12].Value = item.code;
                    list.Add(spsDetail);
                }
                result = DbHelperSQL.ExecuteSqlTranScalar(hashTable, sqlDetail.ToString(), sqlInsert.ToString(), list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public bool Exists(string code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [T_PurchaseOrder]");
            strSql.Append(" where code=@code ");

            SqlParameter[] parameters = {
                    new SqlParameter("@code", SqlDbType.NVarChar,50)};
            parameters[0].Value = code;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}
