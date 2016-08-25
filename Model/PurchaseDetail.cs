using System;
namespace Model
{
	/// <summary>
	/// 其他支出项目表
	/// </summary>
	[Serializable]
	public partial class PurchaseDetail
	{
		public PurchaseDetail()
		{}
		#region Model
		private int _detail_lineno;
		private string _detail_code;
		private string _detail_linecode;
		private string _detail_stockcode;
		private string _detail_stockname;
		private string _detail_materialcode;
		private string _detail_maname;
		private string _detail_model;
		private string _detail_unit;
		private string _detail_curnumber;
		private string _detail_discountaprice;
		private string _detail_discount;
		private string _detail_discountbprice;
		private string _detail_amountmoney;
		private int? _detail_clear;
		private string _detail_safetyone;
		private string _detail_safetytwo;
		private string _detail_remark;
		private DateTime? _detail_updatedate;
		/// <summary>
		/// 栏号自增
		/// </summary>
		public int detail_Lineno
		{
			set{ _detail_lineno=value;}
			get{return _detail_lineno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string detail_Code
		{
			set{ _detail_code=value;}
			get{return _detail_code;}
		}
		/// <summary>
		/// 仓库code
		/// </summary>
		public string detail_LineCode
		{
			set{ _detail_linecode=value;}
			get{return _detail_linecode;}
		}
		/// <summary>
		/// 仓库名称
		/// </summary>
		public string detail_StockCode
		{
			set{ _detail_stockcode=value;}
			get{return _detail_stockcode;}
		}
		/// <summary>
		/// 物料ID
		/// </summary>
		public string detail_StockName
		{
			set{ _detail_stockname=value;}
			get{return _detail_stockname;}
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
		public string detail_MaName
		{
			set{ _detail_maname=value;}
			get{return _detail_maname;}
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
		/// 数量
		/// </summary>
		public string detail_CurNumber
		{
			set{ _detail_curnumber=value;}
			get{return _detail_curnumber;}
		}
		/// <summary>
		/// 折扣后价格
		/// </summary>
		public string detail_DiscountAPrice
		{
			set{ _detail_discountaprice=value;}
			get{return _detail_discountaprice;}
		}
		/// <summary>
		/// 折扣
		/// </summary>
		public string detail_Discount
		{
			set{ _detail_discount=value;}
			get{return _detail_discount;}
		}
		/// <summary>
		/// 折扣前价格
		/// </summary>
		public string detail_DiscountBPrice
		{
			set{ _detail_discountbprice=value;}
			get{return _detail_discountbprice;}
		}
		/// <summary>
		/// 金额
		/// </summary>
		public string detail_AmountMoney
		{
			set{ _detail_amountmoney=value;}
			get{return _detail_amountmoney;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public int? detail_Clear
		{
			set{ _detail_clear=value;}
			get{return _detail_clear;}
		}
		/// <summary>
		/// 预留字段1
		/// </summary>
		public string detail_Safetyone
		{
			set{ _detail_safetyone=value;}
			get{return _detail_safetyone;}
		}
		/// <summary>
		/// 预留字段2
		/// </summary>
		public string detail_Safetytwo
		{
			set{ _detail_safetytwo=value;}
			get{return _detail_safetytwo;}
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
		/// 更新时间
		/// </summary>
		public DateTime? detail_UpdateDate
		{
			set{ _detail_updatedate=value;}
			get{return _detail_updatedate;}
		}
		#endregion Model

	}
}

