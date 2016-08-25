using System;
namespace Model
{
	/// <summary>
	/// 其他支出项目表
	/// </summary>
	[Serializable]
	public partial class SalesCancel
	{
		public SalesCancel()
		{}
		#region Model
		private int _cancel_id;
		private string _cancel_code;
		private string _cancel_clientcode;
		private string _cancel_clientname;
		private string _cancel_clientphone;
		private DateTime? _cancel_outdate= DateTime.Now;
		private string _cancel_salemanid;
		private string _cancel_manname;
		private int? _cancel_enable=1;
		private int? _cancel_againin;
		private string _cancel_operation;
		private string _cancel_auditman;
		private string _cancel_remark;
		private string _cancel_satetyone;
		private string _cancel_satetytwo;
		private int? _cancel_clear=1;
		private DateTime? _cancel_updatedate;
		/// <summary>
		/// 
		/// </summary>
		public int cancel_ID
		{
			set{ _cancel_id=value;}
			get{return _cancel_id;}
		}
		/// <summary>
		/// 退货编号
		/// </summary>
		public string cancel_Code
		{
			set{ _cancel_code=value;}
			get{return _cancel_code;}
		}
		/// <summary>
		/// 客户ID，散客选择散客ID
		/// </summary>
		public string cancel_ClientCode
		{
			set{ _cancel_clientcode=value;}
			get{return _cancel_clientcode;}
		}
		/// <summary>
		/// 客户名称
		/// </summary>
		public string cancel_ClientName
		{
			set{ _cancel_clientname=value;}
			get{return _cancel_clientname;}
		}
		/// <summary>
		/// 客户电话
		/// </summary>
		public string cancel_ClientPhone
		{
			set{ _cancel_clientphone=value;}
			get{return _cancel_clientphone;}
		}
		/// <summary>
		/// 退货时间
		/// </summary>
		public DateTime? cancel_OutDate
		{
			set{ _cancel_outdate=value;}
			get{return _cancel_outdate;}
		}
		/// <summary>
		/// 业务员编号
		/// </summary>
		public string cancel_SalemanID
		{
			set{ _cancel_salemanid=value;}
			get{return _cancel_salemanid;}
		}
		/// <summary>
		/// 业务员姓名
		/// </summary>
		public string cancel_ManName
		{
			set{ _cancel_manname=value;}
			get{return _cancel_manname;}
		}
		/// <summary>
		/// 是否已审核
		/// </summary>
		public int? cancel_Enable
		{
			set{ _cancel_enable=value;}
			get{return _cancel_enable;}
		}
		/// <summary>
		/// 是否重新入库
		/// </summary>
		public int? cancel_AgainIn
		{
			set{ _cancel_againin=value;}
			get{return _cancel_againin;}
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

