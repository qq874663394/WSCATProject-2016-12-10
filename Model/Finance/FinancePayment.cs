
using System;
namespace Model
{
	/// <summary>
	/// 上下班时刻表
	/// </summary>
	[Serializable]
	public partial class FinancePayment
	{
		public FinancePayment()
		{}
		#region Model
		private int _id;
		private string _code;
		private string _suppliercode;
		private string _suppliername;
		private string _purchasecode;
		private string _purchasename;
		private string _accountcode;
		private string _accountname;
		private DateTime? _date= DateTime.Now;
		private decimal? _amountpay;
		private decimal? _amountpaid;
		private decimal? _amountunpaid;
		private string _salesman;
		private string _salescode;
		private string _checkman;
		private string _remark;
		private string _reserved1;
		private string _reserved2;
		private int? _isclear=1;
		private int? _financepaymentstate=0;
		private decimal? _amountprepaid;
		private decimal? _amountactual;
		private int? _paymentstate=0;
		private string _type;
		private DateTime? _updatedate;
		/// <summary>
		/// 自增ID
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 采购付款编号
		/// </summary>
		public string code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 供应商编号
		/// </summary>
		public string supplierCode
		{
			set{ _suppliercode=value;}
			get{return _suppliercode;}
		}
		/// <summary>
		/// 供应商名称
		/// </summary>
		public string supplierName
		{
			set{ _suppliername=value;}
			get{return _suppliername;}
		}
		/// <summary>
		/// 采购单单号
		/// </summary>
		public string purchaseCode
		{
			set{ _purchasecode=value;}
			get{return _purchasecode;}
		}
		/// <summary>
		/// 采购单名称
		/// </summary>
		public string purchaseName
		{
			set{ _purchasename=value;}
			get{return _purchasename;}
		}
		/// <summary>
		/// 账户编号
		/// </summary>
		public string accountCode
		{
			set{ _accountcode=value;}
			get{return _accountcode;}
		}
		/// <summary>
		/// 账户名称
		/// </summary>
		public string accountName
		{
			set{ _accountname=value;}
			get{return _accountname;}
		}
		/// <summary>
		/// 开单日期
		/// </summary>
		public DateTime? date
		{
			set{ _date=value;}
			get{return _date;}
		}
		/// <summary>
		/// 应付金额
		/// </summary>
		public decimal? amountPay
		{
			set{ _amountpay=value;}
			get{return _amountpay;}
		}
		/// <summary>
		/// 已付金额
		/// </summary>
		public decimal? amountPaid
		{
			set{ _amountpaid=value;}
			get{return _amountpaid;}
		}
		/// <summary>
		/// 未付金额
		/// </summary>
		public decimal? amountUnpaid
		{
			set{ _amountunpaid=value;}
			get{return _amountunpaid;}
		}
		/// <summary>
		/// 业务员
		/// </summary>
		public string salesMan
		{
			set{ _salesman=value;}
			get{return _salesman;}
		}
		/// <summary>
		/// 业务员编号
		/// </summary>
		public string salesCode
		{
			set{ _salescode=value;}
			get{return _salescode;}
		}
		/// <summary>
		/// 审核人
		/// </summary>
		public string checkMan
		{
			set{ _checkman=value;}
			get{return _checkman;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 保留字段
		/// </summary>
		public string Reserved1
		{
			set{ _reserved1=value;}
			get{return _reserved1;}
		}
		/// <summary>
		/// 保留字段
		/// </summary>
		public string Reserved2
		{
			set{ _reserved2=value;}
			get{return _reserved2;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public int? isClear
		{
			set{ _isclear=value;}
			get{return _isclear;}
		}
		/// <summary>
		/// 单据状态
		/// </summary>
		public int? financePaymentState
		{
			set{ _financepaymentstate=value;}
			get{return _financepaymentstate;}
		}
		/// <summary>
		/// 预付金额
		/// </summary>
		public decimal? amountPrepaid
		{
			set{ _amountprepaid=value;}
			get{return _amountprepaid;}
		}
		/// <summary>
		/// 实付金额
		/// </summary>
		public decimal? amountActual
		{
			set{ _amountactual=value;}
			get{return _amountactual;}
		}
		/// <summary>
		/// 付款状态
		/// </summary>
		public int? paymentState
		{
			set{ _paymentstate=value;}
			get{return _paymentstate;}
		}
		/// <summary>
		/// 单据类型
		/// </summary>
		public string type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? updateDate
		{
			set{ _updatedate=value;}
			get{return _updatedate;}
		}
		#endregion Model

	}
}

