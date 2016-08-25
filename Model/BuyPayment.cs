using System;
namespace Model
{
	/// <summary>
	/// 借款类型表
	/// </summary>
	[Serializable]
	public partial class BuyPayment
	{
		public BuyPayment()
		{}
		#region Model
		private int _payment_id;
		private string _payment_code;
		private string _payment_suppliercode;
		private string _payment_suppliername;
		private string _payment_purchasecode;
		private string _payment_name;
		private string _payment_accountcode;
		private string _payment_accountname;
		private DateTime? _payment_date= DateTime.Now;
		private string _payment_amountpay;
		private string _payment_accountpaid;
		private string _payment_moneyowed;
		private string _payment_salesman;
		private string _payment_salescode;
		private string _payment_auditman;
		private string _payment_remark;
		private string _payment_satetyone;
		private string _payment_satetytwo;
		private int? _payment_clear=1;
		private int? _payment_states=0;
		private string _payment_premoney;
		private string _payment_actmoney;
		private int? _payment_zhuangtai=0;
		private string _payment_class;
		private DateTime? _payment_updatedate;
		/// <summary>
		/// 自增ID
		/// </summary>
		public int payment_ID
		{
			set{ _payment_id=value;}
			get{return _payment_id;}
		}
		/// <summary>
		/// 采购付款编号
		/// </summary>
		public string payment_Code
		{
			set{ _payment_code=value;}
			get{return _payment_code;}
		}
		/// <summary>
		/// 供应商编号
		/// </summary>
		public string payment_SupplierCode
		{
			set{ _payment_suppliercode=value;}
			get{return _payment_suppliercode;}
		}
		/// <summary>
		/// 供应商名称
		/// </summary>
		public string payment_SupplierName
		{
			set{ _payment_suppliername=value;}
			get{return _payment_suppliername;}
		}
		/// <summary>
		/// 采购单单号
		/// </summary>
		public string payment_PurchaseCode
		{
			set{ _payment_purchasecode=value;}
			get{return _payment_purchasecode;}
		}
		/// <summary>
		/// 采购单名称
		/// </summary>
		public string payment_Name
		{
			set{ _payment_name=value;}
			get{return _payment_name;}
		}
		/// <summary>
		/// 账户编号
		/// </summary>
		public string payment_AccountCode
		{
			set{ _payment_accountcode=value;}
			get{return _payment_accountcode;}
		}
		/// <summary>
		/// 账户名称
		/// </summary>
		public string payment_AccountName
		{
			set{ _payment_accountname=value;}
			get{return _payment_accountname;}
		}
		/// <summary>
		/// 开单日期
		/// </summary>
		public DateTime? payment_Date
		{
			set{ _payment_date=value;}
			get{return _payment_date;}
		}
		/// <summary>
		/// 应付金额
		/// </summary>
		public string payment_AmountPay
		{
			set{ _payment_amountpay=value;}
			get{return _payment_amountpay;}
		}
		/// <summary>
		/// 已付金额
		/// </summary>
		public string payment_AccountPaid
		{
			set{ _payment_accountpaid=value;}
			get{return _payment_accountpaid;}
		}
		/// <summary>
		/// 尚欠
		/// </summary>
		public string payment_moneyOwed
		{
			set{ _payment_moneyowed=value;}
			get{return _payment_moneyowed;}
		}
		/// <summary>
		/// 业务员
		/// </summary>
		public string payment_SalesMan
		{
			set{ _payment_salesman=value;}
			get{return _payment_salesman;}
		}
		/// <summary>
		/// 业务员编号
		/// </summary>
		public string payment_SalesCode
		{
			set{ _payment_salescode=value;}
			get{return _payment_salescode;}
		}
		/// <summary>
		/// 审核人
		/// </summary>
		public string payment_AuditMan
		{
			set{ _payment_auditman=value;}
			get{return _payment_auditman;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string payment_Remark
		{
			set{ _payment_remark=value;}
			get{return _payment_remark;}
		}
		/// <summary>
		/// 保留字段
		/// </summary>
		public string payment_Satetyone
		{
			set{ _payment_satetyone=value;}
			get{return _payment_satetyone;}
		}
		/// <summary>
		/// 保留字段
		/// </summary>
		public string payment_Satetytwo
		{
			set{ _payment_satetytwo=value;}
			get{return _payment_satetytwo;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public int? payment_Clear
		{
			set{ _payment_clear=value;}
			get{return _payment_clear;}
		}
		/// <summary>
		/// 单据状态
		/// </summary>
		public int? payment_States
		{
			set{ _payment_states=value;}
			get{return _payment_states;}
		}
		/// <summary>
		/// 预付金额
		/// </summary>
		public string payment_Premoney
		{
			set{ _payment_premoney=value;}
			get{return _payment_premoney;}
		}
		/// <summary>
		/// 实付金额
		/// </summary>
		public string payment_Actmoney
		{
			set{ _payment_actmoney=value;}
			get{return _payment_actmoney;}
		}
		/// <summary>
		/// 付款状态
		/// </summary>
		public int? payment_Zhuangtai
		{
			set{ _payment_zhuangtai=value;}
			get{return _payment_zhuangtai;}
		}
		/// <summary>
		/// 单据类型
		/// </summary>
		public string payment_Class
		{
			set{ _payment_class=value;}
			get{return _payment_class;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? payment_UpdateDate
		{
			set{ _payment_updatedate=value;}
			get{return _payment_updatedate;}
		}
		#endregion Model

	}
}

