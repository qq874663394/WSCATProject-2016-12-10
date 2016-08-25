
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Model;
namespace BaseLayer
{
	/// <summary>
	/// 数据访问类:T_WarehouseInDetail
	/// </summary>
	public partial class WarehouseInDetailBase
    {
		public WarehouseInDetailBase()
		{}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(WarehouseInDetail model)
		{
			StringBuilder strSql=new StringBuilder();
            try
            {
                strSql.Append("insert into T_WarehouseInDetail(");
                strSql.Append("code,materiaName,materiaModel,materiaUnit,number,price,money,remark,reserved1,reserved2,isClear,barcode,rfid,updateDate,warehouseInDetailState,date)");
                strSql.Append(" values (");
                strSql.Append("@code,@materiaName,@materiaModel,@materiaUnit,@number,@price,@money,@remark,@reserved1,@reserved2,@isClear,@barcode,@rfid,@updateDate,@warehouseInDetailState,@date)");
                strSql.Append(";select @@IDENTITY");
            }
            catch
            {
                return -1;
            }
            SqlParameter[] parameters = new SqlParameter[16];
            try
            {
                parameters[0] = new SqlParameter("@code", SqlDbType.NVarChar, 45);
                parameters[1] = new SqlParameter("@materiaName", SqlDbType.NVarChar, 100);
                parameters[2] = new SqlParameter("@materiaModel", SqlDbType.NVarChar, 100);
                parameters[3] = new SqlParameter("@materiaUnit", SqlDbType.NVarChar, 10);
                parameters[4] = new SqlParameter("@number", SqlDbType.Decimal, 9);
                parameters[5] = new SqlParameter("@price", SqlDbType.Decimal, 9);
                parameters[6] = new SqlParameter("@money", SqlDbType.Decimal, 9);
                parameters[7] = new SqlParameter("@remark", SqlDbType.NVarChar, 400);
                parameters[8] = new SqlParameter("@reserved1", SqlDbType.NVarChar, 50);
                parameters[9] = new SqlParameter("@reserved2", SqlDbType.NVarChar, 50);
                parameters[10] = new SqlParameter("@isClear", SqlDbType.Int, 4);
                parameters[11] = new SqlParameter("@barcode", SqlDbType.NVarChar, 100);
                parameters[12] = new SqlParameter("@rfid", SqlDbType.NVarChar, 100);
                parameters[13] = new SqlParameter("@updateDate", SqlDbType.DateTime);
                parameters[14] = new SqlParameter("@warehouseInDetailState", SqlDbType.Int, 4);
                parameters[15] = new SqlParameter("@date", SqlDbType.DateTime);

            }
            catch(Exception ex)
            {
                return -2;
            }
            try
            {
                parameters[0].Value = model.code;
                parameters[1].Value = model.materiaName;
                parameters[2].Value = model.materiaModel;
                parameters[3].Value = model.materiaUnit;
                parameters[4].Value = model.number;
                parameters[5].Value = model.price;
                parameters[6].Value = model.money;
                parameters[7].Value = model.remark;
                parameters[8].Value = model.reserved1;
                parameters[9].Value = model.reserved2;
                parameters[10].Value = model.isClear;
                parameters[11].Value = model.barcode;
                parameters[12].Value = model.rfid;
                parameters[13].Value = model.updateDate;
                parameters[14].Value = model.warehouseInDetailState;
                parameters[15].Value = model.date;
            }
            catch
            {
                return -3;
            }
			int result = DbHelperSQL.GetSingle(strSql.ToString(),parameters);

            return result;
        }
	}
}

