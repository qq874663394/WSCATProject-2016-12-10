using System;
namespace Model
{
	/// <summary>
	/// 仓库表
	/// </summary>
	[Serializable]
	public partial class WarehouseCheck
	{
		public WarehouseCheck()
		{}
		#region Model
		private int _check_id;
		private string _check_code;
		private string _check_stockid;
		private string _check_stockname;
		private DateTime? _check_date;
		private string _check_operation;
		private string _check_auditman;
		private string _check_remark;
		private string _check_satetyone;
		private string _check_satetytwo;
		private int? _check_clear;
		private DateTime? _check_updatedate;
		/// <summary>
		/// 自增ID
		/// </summary>
		public int check_ID
		{
			set{ _check_id=value;}
			get{return _check_id;}
		}
		/// <summary>
		/// 盘点单号（生成）
		/// </summary>
		public string check_Code
		{
			set{ _check_code=value;}
			get{return _check_code;}
		}
		/// <summary>
		/// 仓库编号
		/// </summary>
		public string check_StockID
		{
			set{ _check_stockid=value;}
			get{return _check_stockid;}
		}
		/// <summary>
		/// 仓库名称
		/// </summary>
		public string check_StockName
		{
			set{ _check_stockname=value;}
			get{return _check_stockname;}
		}
		/// <summary>
		/// 盘点日期
		/// </summary>
		public DateTime? check_Date
		{
			set{ _check_date=value;}
			get{return _check_date;}
		}
		/// <summary>
		/// 盘点人
		/// </summary>
		public string check_Operation
		{
			set{ _check_operation=value;}
			get{return _check_operation;}
		}
		/// <summary>
		/// 审核人
		/// </summary>
		public string check_Auditman
		{
			set{ _check_auditman=value;}
			get{return _check_auditman;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string check_Remark
		{
			set{ _check_remark=value;}
			get{return _check_remark;}
		}
		/// <summary>
		/// 保留字段1
		/// </summary>
		public string check_Satetyone
		{
			set{ _check_satetyone=value;}
			get{return _check_satetyone;}
		}
		/// <summary>
		/// 保留字段2
		/// </summary>
		public string check_Satetytwo
		{
			set{ _check_satetytwo=value;}
			get{return _check_satetytwo;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public int? check_Clear
		{
			set{ _check_clear=value;}
			get{return _check_clear;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? check_UpdateDate
		{
			set{ _check_updatedate=value;}
			get{return _check_updatedate;}
		}
		#endregion Model

	}
}

