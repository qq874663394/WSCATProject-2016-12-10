using System;
namespace Model
{
	/// <summary>
	/// 仓库表
	/// </summary>
	[Serializable]
	public partial class WarehouseBreakage
	{
		public WarehouseBreakage()
		{}
		#region Model
		private int _breakage_id;
		private string _breakage_code;
		private string _breakage_stockid;
		private string _breakage_stockname;
		private string _breakage_date;
		private string _breakage_operatio;
		private string _breakage_auditman;
		private string _breakage_remark;
		private string _breakage_satetyone;
		private string _breakage_satetytwo;
		private int? _breakage_clear=1;
		private DateTime? _breakage_updatedate;
		/// <summary>
		/// 
		/// </summary>
		public int Breakage_ID
		{
			set{ _breakage_id=value;}
			get{return _breakage_id;}
		}
		/// <summary>
		/// 编号
		/// </summary>
		public string Breakage_Code
		{
			set{ _breakage_code=value;}
			get{return _breakage_code;}
		}
		/// <summary>
		/// 仓库编号
		/// </summary>
		public string Breakage_StockID
		{
			set{ _breakage_stockid=value;}
			get{return _breakage_stockid;}
		}
		/// <summary>
		/// 仓库名称
		/// </summary>
		public string Breakage_StockName
		{
			set{ _breakage_stockname=value;}
			get{return _breakage_stockname;}
		}
		/// <summary>
		/// 报损日期
		/// </summary>
		public string Breakage_Date
		{
			set{ _breakage_date=value;}
			get{return _breakage_date;}
		}
		/// <summary>
		/// 报损人
		/// </summary>
		public string Breakage_Operatio
		{
			set{ _breakage_operatio=value;}
			get{return _breakage_operatio;}
		}
		/// <summary>
		/// 审核人
		/// </summary>
		public string Breakage_Auditman
		{
			set{ _breakage_auditman=value;}
			get{return _breakage_auditman;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Breakage_Remark
		{
			set{ _breakage_remark=value;}
			get{return _breakage_remark;}
		}
		/// <summary>
		/// 预留字段1
		/// </summary>
		public string Breakage_Satetyone
		{
			set{ _breakage_satetyone=value;}
			get{return _breakage_satetyone;}
		}
		/// <summary>
		/// 预留字段2
		/// </summary>
		public string Breakage_Satetytwo
		{
			set{ _breakage_satetytwo=value;}
			get{return _breakage_satetytwo;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public int? Breakage_Clear
		{
			set{ _breakage_clear=value;}
			get{return _breakage_clear;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? Breakage_UpdateDate
		{
			set{ _breakage_updatedate=value;}
			get{return _breakage_updatedate;}
		}
		#endregion Model

	}
}

