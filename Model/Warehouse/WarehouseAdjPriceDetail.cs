
using System;
namespace Model
{
	/// <summary>
	/// 销售订单明细表
	/// </summary>
	[Serializable]
	public partial class WarehouseAdjPriceDetail
	{
		public WarehouseAdjPriceDetail()
		{}
		#region Model
		private int _id;
		private string _code;
		private string _materialcode;
		private string _materianame;
		private string _materiaunitmodel;
		private string _materiaunitunit;
		private decimal? _curprice;
		private int? _scale;
		private decimal? _price;
		private decimal? _lostmoney;
		private string _cause;
		private int? _isclear;
		private DateTime? _updatedata;
		private string _reserved1;
		private string _reserved2;
		private string _remark;
		/// <summary>
		/// 自增ID
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 调价编号
		/// </summary>
		public string code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 物料编号
		/// </summary>
		public string materialCode
		{
			set{ _materialcode=value;}
			get{return _materialcode;}
		}
		/// <summary>
		/// 物料名称
		/// </summary>
		public string materiaName
		{
			set{ _materianame=value;}
			get{return _materianame;}
		}
		/// <summary>
		/// 规格型号
		/// </summary>
		public string materiaunitmodel
		{
			set{ _materiaunitmodel=value;}
			get{return _materiaunitmodel;}
		}
		/// <summary>
		/// 单位
		/// </summary>
		public string materiaunitunit
		{
			set{ _materiaunitunit=value;}
			get{return _materiaunitunit;}
		}
		/// <summary>
		/// 调前单价
		/// </summary>
		public decimal? curPrice
		{
			set{ _curprice=value;}
			get{return _curprice;}
		}
		/// <summary>
		/// 调价比例
		/// </summary>
		public int? scale
		{
			set{ _scale=value;}
			get{return _scale;}
		}
		/// <summary>
		/// 调后单价
		/// </summary>
		public decimal? price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 差额
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
		public DateTime? updateData
		{
			set{ _updatedata=value;}
			get{return _updatedata;}
		}
		/// <summary>
		/// 预留字段
		/// </summary>
		public string reserved1
		{
			set{ _reserved1=value;}
			get{return _reserved1;}
		}
		/// <summary>
		/// 预留字段
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

