﻿
using System;
namespace Model
{
	/// <summary>
	/// 销售订单明细表
	/// </summary>
	[Serializable]
	public partial class SalesMain
	{
		public SalesMain()
		{}
		#region Model
		private int _id;
		private int? _isclear=1;
		private string _code;
		private string _type;
		private DateTime? _date= DateTime.Now;
		private string _transportmathod;
		private int? _salesorderstate=0;
		private string _salesmancode;
		private int? _checkstate=0;
		private DateTime? _changedate;
		private string _operationman;
		private string _checkman;
		private string _salesman;
		private int? _paystate;
		private int? _inwarehousestate;
		private string _paymathod;
		private DateTime? _deliverydate;
		private string _logistics;
		private string _logisticsoddcode;
		private string _logisticsphone;
		private decimal? _oddallmoney;
		private string _accountcode;
		private decimal? _collectmoney;
		private decimal? _lastmoney;
		private string _clientaddress;
		private string _clientname;
		private string _clientphone;
		private string _linkman;
		private int? _urgentstate=0;
		private DateTime? _expiredate;
		private DateTime? _updatedate;
		private string _remark;
		private string _reserved1;
		private string _reserved2;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
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
		public string transportMathod
		{
			set{ _transportmathod=value;}
			get{return _transportmathod;}
		}
		/// <summary>
		/// 发货状态0未审核，1缺货，2发货中，3，已完成
		/// </summary>
		public int? SalesOrderState
		{
			set{ _salesorderstate=value;}
			get{return _salesorderstate;}
		}
		/// <summary>
		/// 业务员
		/// </summary>
		public string salesManCode
		{
			set{ _salesmancode=value;}
			get{return _salesmancode;}
		}
		/// <summary>
		/// 0未审核,1已审核
		/// </summary>
		public int? checkState
		{
			set{ _checkstate=value;}
			get{return _checkstate;}
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
		public string operationMan
		{
			set{ _operationman=value;}
			get{return _operationman;}
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
		/// 
		/// </summary>
		public string salesMan
		{
			set{ _salesman=value;}
			get{return _salesman;}
		}
		/// <summary>
		/// 是否已付款(0为未付款,1部分付款,2已完成)
		/// </summary>
		public int? payState
		{
			set{ _paystate=value;}
			get{return _paystate;}
		}
		/// <summary>
		/// 入库状态(0未入库,1已入库)
		/// </summary>
		public int? inWarehouseState
		{
			set{ _inwarehousestate=value;}
			get{return _inwarehousestate;}
		}
		/// <summary>
		/// 付款方式
		/// </summary>
		public string payMathod
		{
			set{ _paymathod=value;}
			get{return _paymathod;}
		}
		/// <summary>
		/// 到达时间
		/// </summary>
		public DateTime? deliveryDate
		{
			set{ _deliverydate=value;}
			get{return _deliverydate;}
		}
		/// <summary>
		/// 物流
		/// </summary>
		public string logistics
		{
			set{ _logistics=value;}
			get{return _logistics;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string logisticsOddCode
		{
			set{ _logisticsoddcode=value;}
			get{return _logisticsoddcode;}
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
		/// 本单总额
		/// </summary>
		public decimal? oddAllMoney
		{
			set{ _oddallmoney=value;}
			get{return _oddallmoney;}
		}
		/// <summary>
		/// 账号Code
		/// </summary>
		public string accountCode
		{
			set{ _accountcode=value;}
			get{return _accountcode;}
		}
		/// <summary>
		/// 本次收款
		/// </summary>
		public decimal? collectMoney
		{
			set{ _collectmoney=value;}
			get{return _collectmoney;}
		}
		/// <summary>
		/// 剩余尾款
		/// </summary>
		public decimal? lastMoney
		{
			set{ _lastmoney=value;}
			get{return _lastmoney;}
		}
		/// <summary>
		/// 客户地址
		/// </summary>
		public string clientAddress
		{
			set{ _clientaddress=value;}
			get{return _clientaddress;}
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
		/// 加急状态
		/// </summary>
		public int? urgentState
		{
			set{ _urgentstate=value;}
			get{return _urgentstate;}
		}
		/// <summary>
		/// 最晚送达时间
		/// </summary>
		public DateTime? expireDate
		{
			set{ _expiredate=value;}
			get{return _expiredate;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? updateDate
		{
			set{ _updatedate=value;}
			get{return _updatedate;}
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
		public string reserved1
		{
			set{ _reserved1=value;}
			get{return _reserved1;}
		}
		/// <summary>
		/// 保留字段2
		/// </summary>
		public string reserved2
		{
			set{ _reserved2=value;}
			get{return _reserved2;}
		}
		#endregion Model

	}
}

