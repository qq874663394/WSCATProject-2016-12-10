
using System;
namespace Model
{
	/// <summary>
	/// 销售订单明细表
	/// </summary>
	[Serializable]
	public partial class WarehouseOutDetail
	{
		public WarehouseOutDetail()
		{}
		#region Model
		private string _code;
		private int _id;
		private string _materialcode;
		private string _materianame;
		private string _materiamodel;
		private string _materiaunit;
		private decimal? _number;
		private decimal? _price;
		private decimal? _money;
		private string _barcode;
		private string _rfid;
		private DateTime? _updatedate;
		private int? _warehouseoutdetailstate;
		private DateTime? _date;
		private int? _isclear;
		private string _reserved1;
		private string _reserved2;
		private string _remark;
		/// <summary>
		/// 出库编号
		/// </summary>
		public string code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 自增ID
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string materialCode
		{
			set{ _materialcode=value;}
			get{return _materialcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string materiaName
		{
			set{ _materianame=value;}
			get{return _materianame;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string materiaModel
		{
			set{ _materiamodel=value;}
			get{return _materiamodel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string materiaUnit
		{
			set{ _materiaunit=value;}
			get{return _materiaunit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? number
		{
			set{ _number=value;}
			get{return _number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? money
		{
			set{ _money=value;}
			get{return _money;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string barcode
		{
			set{ _barcode=value;}
			get{return _barcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string rfid
		{
			set{ _rfid=value;}
			get{return _rfid;}
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
		/// 
		/// </summary>
		public int? warehouseOutDetailState
		{
			set{ _warehouseoutdetailstate=value;}
			get{return _warehouseoutdetailstate;}
		}
		/// <summary>
		/// 出库时间
		/// </summary>
		public DateTime? date
		{
			set{ _date=value;}
			get{return _date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? isClear
		{
			set{ _isclear=value;}
			get{return _isclear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string reserved1
		{
			set{ _reserved1=value;}
			get{return _reserved1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string reserved2
		{
			set{ _reserved2=value;}
			get{return _reserved2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

