using System;
namespace Model
{
	/// <summary>
	/// 其他支出项目表
	/// </summary>
	[Serializable]
	public partial class Sales
	{
		public Sales()
		{}
		#region Model
		private int _id;
		private string _code;
		private string _type;
		private DateTime? _date= DateTime.Now;
		private string _transporttype;
		private int? _review=0;
		private DateTime? _changedate;
		private string _operation;
		private string _auditman;
		private string _remark;
		private string _satetyone;
		private string _satetytwo;
		private int? _ispay;
		private int? _isputwarehouse;
		private int? _paymathod;
		private DateTime? _getdate;
		private string _logistics;
		private string _logisticscode;
		private string _logisticsphone;
		private int? _clear=1;
		private string _oddmoney;
		private string _accountcode;
		private string _inmoney;
		private string _lastmoney;
		private string _address;
		private string _clientname;
		private string _clientphone;
		private string _linkman;
		private string _salesmancode;
		private int? _oddstatus=0;
		private int? _jiajistate=0;
		private DateTime? _zuiwanshijian;
		private string _fukuanfangshi;
		private DateTime? _updatedate;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 销售编号
		/// </summary>
		public string code
		{
			set{ _code=value;}
			get{return _code;}
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
		/// 订单日期
		/// </summary>
		public DateTime? date
		{
			set{ _date=value;}
			get{return _date;}
		}
		/// <summary>
		/// 运送方式
		/// </summary>
		public string transportType
		{
			set{ _transporttype=value;}
			get{return _transporttype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? review
		{
			set{ _review=value;}
			get{return _review;}
		}
		/// <summary>
		/// 调价日期
		/// </summary>
		public DateTime? changeDate
		{
			set{ _changedate=value;}
			get{return _changedate;}
		}
		/// <summary>
		/// 操作人
		/// </summary>
		public string operation
		{
			set{ _operation=value;}
			get{return _operation;}
		}
		/// <summary>
		/// 审核人
		/// </summary>
		public string auditman
		{
			set{ _auditman=value;}
			get{return _auditman;}
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
		/// 保留字段1
		/// </summary>
		public string satetyone
		{
			set{ _satetyone=value;}
			get{return _satetyone;}
		}
		/// <summary>
		/// 保留字段2
		/// </summary>
		public string satetytwo
		{
			set{ _satetytwo=value;}
			get{return _satetytwo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? isPay
		{
			set{ _ispay=value;}
			get{return _ispay;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? isPutwarehouse
		{
			set{ _isputwarehouse=value;}
			get{return _isputwarehouse;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? payMathod
		{
			set{ _paymathod=value;}
			get{return _paymathod;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? getDate
		{
			set{ _getdate=value;}
			get{return _getdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string logistics
		{
			set{ _logistics=value;}
			get{return _logistics;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string logisticsCode
		{
			set{ _logisticscode=value;}
			get{return _logisticscode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string logisticsPhone
		{
			set{ _logisticsphone=value;}
			get{return _logisticsphone;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public int? clear
		{
			set{ _clear=value;}
			get{return _clear;}
		}
		/// <summary>
		/// 本单总额
		/// </summary>
		public string oddMoney
		{
			set{ _oddmoney=value;}
			get{return _oddmoney;}
		}
		/// <summary>
		/// 收款账号Code
		/// </summary>
		public string accountCode
		{
			set{ _accountcode=value;}
			get{return _accountcode;}
		}
		/// <summary>
		/// 本次收款
		/// </summary>
		public string inMoney
		{
			set{ _inmoney=value;}
			get{return _inmoney;}
		}
		/// <summary>
		/// 剩余尾款
		/// </summary>
		public string lastMoney
		{
			set{ _lastmoney=value;}
			get{return _lastmoney;}
		}
		/// <summary>
		/// 送货地址
		/// </summary>
		public string address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 客户姓名
		/// </summary>
		public string clientName
		{
			set{ _clientname=value;}
			get{return _clientname;}
		}
		/// <summary>
		/// 联系电话
		/// </summary>
		public string clientPhone
		{
			set{ _clientphone=value;}
			get{return _clientphone;}
		}
		/// <summary>
		/// 联系人
		/// </summary>
		public string linkMan
		{
			set{ _linkman=value;}
			get{return _linkman;}
		}
		/// <summary>
		/// 业务员
		/// </summary>
		public string salesmanCode
		{
			set{ _salesmancode=value;}
			get{return _salesmancode;}
		}
		/// <summary>
		/// 发货状态0未审核，1缺货，2发货中，3，已完成
		/// </summary>
		public int? oddStatus
		{
			set{ _oddstatus=value;}
			get{return _oddstatus;}
		}
		/// <summary>
		/// 加急状态
		/// </summary>
		public int? jiajiState
		{
			set{ _jiajistate=value;}
			get{return _jiajistate;}
		}
		/// <summary>
		/// 最晚到货时间
		/// </summary>
		public DateTime? zuiwanshijian
		{
			set{ _zuiwanshijian=value;}
			get{return _zuiwanshijian;}
		}
		/// <summary>
		/// 付款方式
		/// </summary>
		public string fukuanfangshi
		{
			set{ _fukuanfangshi=value;}
			get{return _fukuanfangshi;}
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

