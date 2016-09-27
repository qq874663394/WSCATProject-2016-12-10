
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
        { }
        #region Model
        private int _id;
        private string _code;
        private string _maincode;
        private string _materialdaima;
        private string _barcode;
        private string _materialcode;
        private string _materianame;
        private string _materiaunitmodel;
        private string _materiaunitunit;
        private string _stockcode;
        private string _stockname;
        private decimal? _number;
        private decimal? _curprice;
        private decimal? _curmoney;
        private int? _scale;
        private decimal? _price;
        private decimal? _money;
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
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 调价编号
        /// </summary>
        public string code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
		/// 主表code
		/// </summary>
		public string mainCode
        {
            set { _maincode = value; }
            get { return _maincode; }
        }
        /// <summary>
		/// 商品代码
		/// </summary>
		public string materiaDaima
        {
            set { _materialdaima = value; }
            get { return _materialdaima; }
        }
        /// <summary>
		/// 条形码
		/// </summary>
		public string barCode
        {
            set { _barcode = value; }
            get { return _barcode; }
        }
        /// <summary>
        /// 物料编号
        /// </summary>
        public string materialCode
        {
            set { _materialcode = value; }
            get { return _materialcode; }
        }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string materiaName
        {
            set { _materianame = value; }
            get { return _materianame; }
        }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string materiaunitmodel
        {
            set { _materiaunitmodel = value; }
            get { return _materiaunitmodel; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string materiaunitunit
        {
            set { _materiaunitunit = value; }
            get { return _materiaunitunit; }
        }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string stockcode
        {
            set { _stockcode = value; }
            get { return _stockcode; }
        }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string stockName
        {
            set { _stockname = value; }
            get { return _stockname; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal? number
        {
            set { _number = value; }
            get { return _number; }
        }
        /// <summary>
        /// 调前单价
        /// </summary>
        public decimal? curPrice
        {
            set { _curprice = value; }
            get { return _curprice; }
        }
        /// <summary>
        /// 调前金额
        /// </summary>
        public decimal? curMoney
        {
            set { _curmoney = value; }
            get { return _curmoney; }
        }
        /// <summary>
        /// 调价比例
        /// </summary>
        public int? scale
        {
            set { _scale = value; }
            get { return _scale; }
        }
        /// <summary>
        /// 调后单价
        /// </summary>
        public decimal? price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 调后金额
        /// </summary>
        public decimal? money
        {
            set { _money = value; }
            get { return _money; }
        }
        /// <summary>
        /// 差额
        /// </summary>
        public decimal? lostMoney
        {
            set { _lostmoney = value; }
            get { return _lostmoney; }
        }
        /// <summary>
        /// 原因
        /// </summary>
        public string cause
        {
            set { _cause = value; }
            get { return _cause; }
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int? isClear
        {
            set { _isclear = value; }
            get { return _isclear; }
        }
        /// <summary>
        /// 更改时间
        /// </summary>
        public DateTime? updateData
        {
            set { _updatedata = value; }
            get { return _updatedata; }
        }
        /// <summary>
        /// 预留字段
        /// </summary>
        public string reserved1
        {
            set { _reserved1 = value; }
            get { return _reserved1; }
        }
        /// <summary>
        /// 预留字段
        /// </summary>
        public string reserved2
        {
            set { _reserved2 = value; }
            get { return _reserved2; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
}

