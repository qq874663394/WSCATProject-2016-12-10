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
		private int _detail_lineno;
		private string _detail_code;
		private string _detail_stockcode;
		private string _detail_stockname;
		private string _detail_linecode;
		private string _detail_materialcode;
		private string _detaill_materialname;
		private string _detail_model;
		private string _detail_unit;
		private decimal? _detail_number;
		private decimal? _detail_discountaprice;
		private decimal? _detail_discount;
		private decimal? _detail_discountbprice;
		private decimal? _detail_money;
		private int? _detaill_clear=1;
		private string _detail_safetyone;
		private string _detaill_safetytwo;
		private string _detail_remark;
		private decimal? _detail_renumber;
		private decimal? _detail_lostnumber;
		private DateTime? _detail_updatedate;
		/// <summary>
		/// 栏号(自增)ID
		/// </summary>
		public int detail_Lineno
		{
			set{ _detail_lineno=value;}
			get{return _detail_lineno;}
		}
		/// <summary>
		/// 销售订单编号
		/// </summary>
		public string detail_Code
		{
			set{ _detail_code=value;}
			get{return _detail_code;}
		}
		/// <summary>
		/// 仓库code
		/// </summary>
		public string detail_StockCode
		{
			set{ _detail_stockcode=value;}
			get{return _detail_stockcode;}
		}
		/// <summary>
		/// 仓库名称
		/// </summary>
		public string detail_StockName
		{
			set{ _detail_stockname=value;}
			get{return _detail_stockname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string detail_LineCode
		{
			set{ _detail_linecode=value;}
			get{return _detail_linecode;}
		}
		/// <summary>
		/// 物料编号
		/// </summary>
		public string detail_MaterialCode
		{
			set{ _detail_materialcode=value;}
			get{return _detail_materialcode;}
		}
		/// <summary>
		/// 物料名称
		/// </summary>
		public string detaill_MaterialName
		{
			set{ _detaill_materialname=value;}
			get{return _detaill_materialname;}
		}
		/// <summary>
		/// 规格型号
		/// </summary>
		public string detail_Model
		{
			set{ _detail_model=value;}
			get{return _detail_model;}
		}
		/// <summary>
		/// 单位
		/// </summary>
		public string detail_Unit
		{
			set{ _detail_unit=value;}
			get{return _detail_unit;}
		}
		/// <summary>
		/// 需求数量
		/// </summary>
		public decimal? detail_Number
		{
			set{ _detail_number=value;}
			get{return _detail_number;}
		}
		/// <summary>
		/// 单价(折扣后)
		/// </summary>
		public decimal? detail_DiscountAPrice
		{
			set{ _detail_discountaprice=value;}
			get{return _detail_discountaprice;}
		}
		/// <summary>
		/// 折扣
		/// </summary>
		public decimal? detail_Discount
		{
			set{ _detail_discount=value;}
			get{return _detail_discount;}
		}
		/// <summary>
		/// 折扣前单价
		/// </summary>
		public decimal? detail_DiscountBPrice
		{
			set{ _detail_discountbprice=value;}
			get{return _detail_discountbprice;}
		}
		/// <summary>
		/// 金额
		/// </summary>
		public decimal? detail_Money
		{
			set{ _detail_money=value;}
			get{return _detail_money;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public int? detaill_Clear
		{
			set{ _detaill_clear=value;}
			get{return _detaill_clear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string detail_Safetyone
		{
			set{ _detail_safetyone=value;}
			get{return _detail_safetyone;}
		}
		/// <summary>
		/// 预留字段2
		/// </summary>
		public string detaill_Safetytwo
		{
			set{ _detaill_safetytwo=value;}
			get{return _detaill_safetytwo;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string detail_Remark
		{
			set{ _detail_remark=value;}
			get{return _detail_remark;}
		}
		/// <summary>
		/// 实际数量
		/// </summary>
		public decimal? detail_ReNumber
		{
			set{ _detail_renumber=value;}
			get{return _detail_renumber;}
		}
		/// <summary>
		/// 欠缺数量
		/// </summary>
		public decimal? detail_LostNumber
		{
			set{ _detail_lostnumber=value;}
			get{return _detail_lostnumber;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? detail_UpdateDate
		{
			set{ _detail_updatedate=value;}
			get{return _detail_updatedate;}
		}
		#endregion Model

	}
}

