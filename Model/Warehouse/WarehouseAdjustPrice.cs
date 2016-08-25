using System;
namespace Model
{
	/// <summary>
	/// 仓库表
	/// </summary>
	[Serializable]
	public partial class WarehouseAdjustPrice
	{
		public WarehouseAdjustPrice()
		{}
		#region Model
		private int _adjustprice_id;
		private string _adjustprice_code;
		private string _adjustprice_stockid;
		private string _adjustprice_stockname;
		private DateTime? _adjustprice_date;
		private string _adjustprice_operation;
		private string _adjustprice_auditman;
		private string _adjustprice_remark;
		private string _adjustprice_satetyone;
		private string _adjustprice_satetytwo;
		private int? _adjustprice_clear=1;
		private DateTime? _adjustprice_updatedata;
		/// <summary>
		/// 自增ID
		/// </summary>
		public int adjustPrice_ID
		{
			set{ _adjustprice_id=value;}
			get{return _adjustprice_id;}
		}
		/// <summary>
		/// 调价单号（生成）
		/// </summary>
		public string adjustPrice_Code
		{
			set{ _adjustprice_code=value;}
			get{return _adjustprice_code;}
		}
		/// <summary>
		/// 仓库编号
		/// </summary>
		public string adjustPrice_StockID
		{
			set{ _adjustprice_stockid=value;}
			get{return _adjustprice_stockid;}
		}
		/// <summary>
		/// 仓库名称
		/// </summary>
		public string adjustPrice_StockName
		{
			set{ _adjustprice_stockname=value;}
			get{return _adjustprice_stockname;}
		}
		/// <summary>
		/// 调价日期
		/// </summary>
		public DateTime? adjustPrice_Date
		{
			set{ _adjustprice_date=value;}
			get{return _adjustprice_date;}
		}
		/// <summary>
		/// 操作人
		/// </summary>
		public string adjustPrice_Operation
		{
			set{ _adjustprice_operation=value;}
			get{return _adjustprice_operation;}
		}
		/// <summary>
		/// 审核人
		/// </summary>
		public string adjustPrice_Auditman
		{
			set{ _adjustprice_auditman=value;}
			get{return _adjustprice_auditman;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string adjustPrice_Remark
		{
			set{ _adjustprice_remark=value;}
			get{return _adjustprice_remark;}
		}
		/// <summary>
		/// 保留字段1
		/// </summary>
		public string adjustPrice_Satetyone
		{
			set{ _adjustprice_satetyone=value;}
			get{return _adjustprice_satetyone;}
		}
		/// <summary>
		/// 保留字段2
		/// </summary>
		public string adjustPrice_Satetytwo
		{
			set{ _adjustprice_satetytwo=value;}
			get{return _adjustprice_satetytwo;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public int? adjustPrice_Clear
		{
			set{ _adjustprice_clear=value;}
			get{return _adjustprice_clear;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? adjustPrice_UpdateData
		{
			set{ _adjustprice_updatedata=value;}
			get{return _adjustprice_updatedata;}
		}
		#endregion Model

	}
}

