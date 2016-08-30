
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
		private int? _state;
		private DateTime? _date;
		private int? _isclear;
		private string _reserved1;
		private string _reserved2;
		private string _remark;
        private string _storageRackName;
        private string _storageRackCode;
        private string _mainCode;
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
		/// 物料code
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
		/// 物料类型
		/// </summary>
		public string materiaModel
		{
			set{ _materiamodel=value;}
			get{return _materiamodel;}
		}
		/// <summary>
		/// 物料单位
		/// </summary>
		public string materiaUnit
		{
			set{ _materiaunit=value;}
			get{return _materiaunit;}
		}
		/// <summary>
		/// 数量
		/// </summary>
		public decimal? number
		{
			set{ _number=value;}
			get{return _number;}
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
		/// 金额
		/// </summary>
		public decimal? money
		{
			set{ _money=value;}
			get{return _money;}
		}
		/// <summary>
		/// 条码
		/// </summary>
		public string barcode
		{
			set{ _barcode=value;}
			get{return _barcode;}
		}
		/// <summary>
		/// rfid
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
		/// 状态
		/// </summary>
		public int? state
		{
			set{ _state=value;}
			get{return _state;}
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
		/// 是否清除
		/// </summary>
		public int? isClear
		{
			set{ _isclear=value;}
			get{return _isclear;}
		}
		/// <summary>
		/// 备用字段1
		/// </summary>
		public string reserved1
		{
			set{ _reserved1=value;}
			get{return _reserved1;}
		}
		/// <summary>
		/// 备用字段2
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
        /// <summary>
        /// 货架名称
        /// </summary>
        public string StorageRackName
        {
            get
            {
                return _storageRackName;
            }

            set
            {
                _storageRackName = value;
            }
        }
        /// <summary>
        /// 货架code
        /// </summary>
        public string StorageRackCode
        {
            get
            {
                return _storageRackCode;
            }

            set
            {
                _storageRackCode = value;
            }
        }
        /// <summary>
        /// 主表code
        /// </summary>
        public string MainCode
        {
            get
            {
                return _mainCode;
            }

            set
            {
                _mainCode = value;
            }
        }
        #endregion Model

    }
}

