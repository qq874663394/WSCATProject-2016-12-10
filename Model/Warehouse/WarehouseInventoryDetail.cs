
using System;
namespace Model
{
	/// <summary>
	/// 销售订单明细表
	/// </summary>
	[Serializable]
	public partial class WarehouseInventoryDetail
	{
		public WarehouseInventoryDetail()
		{}
		#region Model
		private string _code;
		private int _id;
		private string _materialcode;
		private string _materialname;
		private string _materialmodel;
		private string _materialunit;
		private decimal? _curnumber;
		private decimal? _checknumber;
		private decimal? _lostnumber;
		private decimal? _price;
		private decimal? _lostmoney;
		private string _cause;
		private int? _isclear=1;
		private DateTime? _updatedate;
		private string _reserved1;
		private string _reserved2;
		private string _remark;
		/// <summary>
		/// 盘点编号
		/// </summary>
		public string code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 栏号(自增)
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 物料ID
		/// </summary>
		public string materialCode
		{
			set{ _materialcode=value;}
			get{return _materialcode;}
		}
		/// <summary>
		/// 物料名称
		/// </summary>
		public string materialName
		{
			set{ _materialname=value;}
			get{return _materialname;}
		}
		/// <summary>
		/// 规格型号
		/// </summary>
		public string materialModel
		{
			set{ _materialmodel=value;}
			get{return _materialmodel;}
		}
		/// <summary>
		/// 单位
		/// </summary>
		public string materialUnit
		{
			set{ _materialunit=value;}
			get{return _materialunit;}
		}
		/// <summary>
		/// 账面数量
		/// </summary>
		public decimal? curNumber
		{
			set{ _curnumber=value;}
			get{return _curnumber;}
		}
		/// <summary>
		/// 盘点数量
		/// </summary>
		public decimal? checkNumber
		{
			set{ _checknumber=value;}
			get{return _checknumber;}
		}
		/// <summary>
		/// 盈亏数量
		/// </summary>
		public decimal? lostNumber
		{
			set{ _lostnumber=value;}
			get{return _lostnumber;}
		}
		/// <summary>
		/// 单价
		/// </summary>
		public decimal? price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 盈亏金额
		/// </summary>
		public decimal? lostMoney
		{
			set{ _lostmoney=value;}
			get{return _lostmoney;}
		}
		/// <summary>
		/// 原因
		/// </summary>
		public string cause
		{
			set{ _cause=value;}
			get{return _cause;}
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
		/// 更改时间
		/// </summary>
		public DateTime? updateDate
		{
			set{ _updatedate=value;}
			get{return _updatedate;}
		}
		/// <summary>
		/// 预留字段1
		/// </summary>
		public string reserved1
		{
			set{ _reserved1=value;}
			get{return _reserved1;}
		}
		/// <summary>
		/// 预留字段2
		/// </summary>
		public string reserved2
		{
			set{ _reserved2=value;}
			get{return _reserved2;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

