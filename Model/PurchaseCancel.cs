using System;
namespace Model
{
	/// <summary>
	/// 其他支出项目表
	/// </summary>
	[Serializable]
	public partial class PurchaseCancel
	{
		public PurchaseCancel()
		{}
		#region Model
		private int _cancel_id;
		private string _cancel_code;
		private string _cancel_stockcode;
		private string _cancel_stockname;
		private DateTime? _cancel_date= DateTime.Now;
		private int? _cancel_returnstatus=0;
		private int? _cancel_auditstatus=0;
		private string _cancel_salesman;
		private string _cancel_operation;
		private string _cancel_auditman;
		private string _cancel_remark;
		private string _cancel_satetyone;
		private string _cancel_satetytwo;
		private int? _cancel_clear=1;
		private DateTime? _cancel_updatedate;
		/// <summary>
		/// 自增ID
		/// </summary>
		public int cancel_ID
		{
			set{ _cancel_id=value;}
			get{return _cancel_id;}
		}
		/// <summary>
		/// 退货单号（生成）
		/// </summary>
		public string cancel_Code
		{
			set{ _cancel_code=value;}
			get{return _cancel_code;}
		}
		/// <summary>
		/// 供应商编号
		/// </summary>
		public string cancel_StockCode
		{
			set{ _cancel_stockcode=value;}
			get{return _cancel_stockcode;}
		}
		/// <summary>
		/// 供应商名称
		/// </summary>
		public string cancel_StockName
		{
			set{ _cancel_stockname=value;}
			get{return _cancel_stockname;}
		}
		/// <summary>
		/// 退货日期
		/// </summary>
		public DateTime? cancel_Date
		{
			set{ _cancel_date=value;}
			get{return _cancel_date;}
		}
		/// <summary>
		/// 是否已完成,1已完成,0未完成
		/// </summary>
		public int? cancel_ReturnStatus
		{
			set{ _cancel_returnstatus=value;}
			get{return _cancel_returnstatus;}
		}
		/// <summary>
		/// 是否已完成,1已完成,0未完成
		/// </summary>
		public int? cancel_AuditStatus
		{
			set{ _cancel_auditstatus=value;}
			get{return _cancel_auditstatus;}
		}
		/// <summary>
		/// 业务员
		/// </summary>
		public string cancel_SalesMan
		{
			set{ _cancel_salesman=value;}
			get{return _cancel_salesman;}
		}
		/// <summary>
		/// 操作人
		/// </summary>
		public string cancel_Operation
		{
			set{ _cancel_operation=value;}
			get{return _cancel_operation;}
		}
		/// <summary>
		/// 审核人
		/// </summary>
		public string cancel_Auditman
		{
			set{ _cancel_auditman=value;}
			get{return _cancel_auditman;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string cancel_Remark
		{
			set{ _cancel_remark=value;}
			get{return _cancel_remark;}
		}
		/// <summary>
		/// 保留字段1
		/// </summary>
		public string cancel_Satetyone
		{
			set{ _cancel_satetyone=value;}
			get{return _cancel_satetyone;}
		}
		/// <summary>
		/// 保留字段2
		/// </summary>
		public string cancel_Satetytwo
		{
			set{ _cancel_satetytwo=value;}
			get{return _cancel_satetytwo;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public int? cancel_Clear
		{
			set{ _cancel_clear=value;}
			get{return _cancel_clear;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? cancel_UpdateDate
		{
			set{ _cancel_updatedate=value;}
			get{return _cancel_updatedate;}
		}
		#endregion Model

	}
}

