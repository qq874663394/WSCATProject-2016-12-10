
using System;
namespace Model
{
	/// <summary>
	/// 销售订单明细表
	/// </summary>
	[Serializable]
	public partial class SalesDetail
	{
		public SalesDetail()
		{}
		#region Model
		private int _id;
		private int? _isclear=1;
		private string _code;
		private string _salesmaincode;
		private string _storagecode;
		private string _storagename;
		private string _materialcode;
		private string _materialname;
		private string _materiamodel;
		private string _unit;
		private decimal? _neednumber;
		private decimal? _actualnumber;
		private decimal? _lostnumber;
		private decimal? _discountbeforeprice;
		private decimal? _discount;
		private decimal? _discountafterprice;
		private decimal? _money;
		private string _reserved1;
		private string _reserved2;
		private string _remark;
		private DateTime? _detail_updatedate;
        private string _materialdaima;
        private DateTime? _productiondate;
        private DateTime? _qualitydate;
        private DateTime? _effectivedate;
        /// <summary>
        /// 栏号(自增)ID
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
		/// 销售订单详情code
		/// </summary>
		public string code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 主表code
		/// </summary>
		public string salesMainCode
		{
			set{ _salesmaincode = value;}
			get{return _salesmaincode; }
		}
		/// <summary>
		/// 仓库code
		/// </summary>
		public string storageCode
		{
			set{ _storagecode=value;}
			get{return _storagecode;}
		}
		/// <summary>
		/// 仓库名称
		/// </summary>
		public string storageName
		{
			set{ _storagename=value;}
			get{return _storagename;}
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
		public string materialName
		{
			set{ _materialname=value;}
			get{return _materialname;}
		}
		/// <summary>
		/// 规格型号
		/// </summary>
		public string materiaModel
		{
			set{ _materiamodel=value;}
			get{return _materiamodel;}
		}
		/// <summary>
		/// 单位
		/// </summary>
		public string unit
		{
			set{ _unit=value;}
			get{return _unit;}
		}
		/// <summary>
		/// 需求数量
		/// </summary>
		public decimal? needNumber
		{
			set{ _neednumber=value;}
			get{return _neednumber;}
		}
		/// <summary>
		/// 实际数量
		/// </summary>
		public decimal? actualNumber
		{
			set{ _actualnumber=value;}
			get{return _actualnumber;}
		}
		/// <summary>
		/// 欠缺数量
		/// </summary>
		public decimal? lostNumber
		{
			set{ _lostnumber=value;}
			get{return _lostnumber;}
		}
		/// <summary>
		/// 单价(折扣后)
		/// </summary>
		public decimal? discountBeforePrice
		{
			set{ _discountbeforeprice=value;}
			get{return _discountbeforeprice;}
		}
		/// <summary>
		/// 折扣
		/// </summary>
		public decimal? discount
		{
			set{ _discount=value;}
			get{return _discount;}
		}
		/// <summary>
		/// 折扣前单价
		/// </summary>
		public decimal? discountAfterPrice
		{
			set{ _discountafterprice=value;}
			get{return _discountafterprice;}
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
		/// 
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
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? detail_UpdateDate
		{
			set{ _detail_updatedate=value;}
			get{return _detail_updatedate;}
		}
        /// <summary>
        /// 商品代码
        /// </summary>
        public string materialDaima
        {
            set { _materialdaima = value; }
            get { return _materialdaima; }
        }
        /// <summary>
		/// 生产/采购日期
		/// </summary>
		public DateTime? productionDate
        {
            set { _productiondate = value; }
            get { return _productiondate; }
        }
        /// <summary>
		/// 保质期
		/// </summary>
		public DateTime? qualityDate
        {
            set { _qualitydate = value; }
            get { return _qualitydate; }
        }
        /// <summary>
		/// 有效期至
		/// </summary>
		public DateTime? effectiveDate
        {
            set { _effectivedate = value; }
            get { return _effectivedate; }
        }
        #endregion Model

    }
}

