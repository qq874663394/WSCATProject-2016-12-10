using System;
namespace Model
{
	/// <summary>
	/// 其他支出项目表
	/// </summary>
	[Serializable]
	public partial class Purchase
	{
		public Purchase()
		{}
		#region Model
		private int _id;
		private string _code;
		private DateTime? _data= DateTime.Now;
		private string _suppliercode;
		private string _suppliername;
		private int? _purchasestatus=0;
		private int? _auditstatus;
		private string _purchasercode;
		private string _salesman;
		private string _operation;
		private string _auditman;
		private string _remark;
		private string _satetyone;
		private string _satetytwo;
		private int? _clear=1;
		private int? _ispay=0;
		private int? _paymethod=0;
		private int? _isputsto=0;
		private string _type;
		private DateTime? _getdate;
		private string _logistics;
		private string _logisticscode;
		private string _logisticsphone;
		private string _oddmoney;
		private string _accountcode;
		private string _inmoney;
		private string _lastmoney;
		private DateTime? _updatedate;
		private string _jiajistate;
		/// <summary>
		/// 自增ID
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 采购单号
		/// </summary>
		public string code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 采购日期
		/// </summary>
		public DateTime? data
		{
			set{ _data=value;}
			get{return _data;}
		}
		/// <summary>
		/// 供应商code
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
		/// 是否已完成,1已完成,0未完成
		/// </summary>
		public int? purchaseStatus
		{
			set{ _purchasestatus=value;}
			get{return _purchasestatus;}
		}
		/// <summary>
		/// 审核情况,1已审核,0未审核
		/// </summary>
		public int? auditStatus
		{
			set{ _auditstatus=value;}
			get{return _auditstatus;}
		}
		/// <summary>
		/// 采购人员ID
		/// </summary>
		public string purchaserCode
		{
			set{ _purchasercode=value;}
			get{return _purchasercode;}
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
		public string auditMan
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
		/// 是否删除
		/// </summary>
		public int? clear
		{
			set{ _clear=value;}
			get{return _clear;}
		}
		/// <summary>
		/// 是否已付款(0为未完成,1为已完成)
		/// </summary>
		public int? isPay
		{
			set{ _ispay=value;}
			get{return _ispay;}
		}
		/// <summary>
		/// 付款方式(0~100，对应实际的数值为百分数。如：50等于付了50%)
		/// </summary>
		public int? payMethod
		{
			set{ _paymethod=value;}
			get{return _paymethod;}
		}
		/// <summary>
		/// 入库状态(0未入库,1已入库)
		/// </summary>
		public int? isPutSto
		{
			set{ _isputsto=value;}
			get{return _isputsto;}
		}
		/// <summary>
		/// 单据类别
		/// </summary>
		public string type
		{
			set{ _type = value;}
			get{return _type; }
		}
		/// <summary>
		/// 到货日期
		/// </summary>
		public DateTime? getDate
		{
			set{ _getdate=value;}
			get{return _getdate;}
		}
		/// <summary>
		/// 物流单号
		/// </summary>
		public string logistics
		{
			set{ _logistics=value;}
			get{return _logistics;}
		}
		/// <summary>
		/// 物流单号
		/// </summary>
		public string logisticsCode
		{
			set{ _logisticscode=value;}
			get{return _logisticscode;}
		}
		/// <summary>
		/// 快递电话
		/// </summary>
		public string logisticsPhone
		{
			set{ _logisticsphone=value;}
			get{return _logisticsphone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string oddMoney
		{
			set{ _oddmoney=value;}
			get{return _oddmoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string accountCode
		{
			set{ _accountcode=value;}
			get{return _accountcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string inMoney
		{
			set{ _inmoney=value;}
			get{return _inmoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string lastMoney
		{
			set{ _lastmoney=value;}
			get{return _lastmoney;}
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
		/// 加急状态
		/// </summary>
		public string jiajiState
		{
			set{ _jiajistate=value;}
			get{return _jiajistate;}
		}
		#endregion Model

	}
}

