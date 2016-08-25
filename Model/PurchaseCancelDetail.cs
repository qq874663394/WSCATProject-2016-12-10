using System;
namespace Model
{
	/// <summary>
	/// 其他支出项目表
	/// </summary>
	[Serializable]
	public partial class PurchaseCancelDetail
	{
		public PurchaseCancelDetail()
		{}
		#region Model
		private int _canceldetail_lineno;
		private string _canceldetail_code;
		private string _canceldetail_materialid;
		private string _canceldetail_materialname;
		private string _canceldetail_model;
		private string _canceldetail_unit;
		private string _canceldetail_curnumber;
		private int? _canceldetail_clear=1;
		private string _canceldetail_safetyone;
		private string _canceldetail_safetytwo;
		private string _canceldetail_remark;
		private string _canceldetail_discountaprice;
		private string _canceldetail_discount;
		private string _canceldetail_discountbprice;
		private string _canceldetail_amountmoney;
		private DateTime? _canceldetail_updatedate;
		/// <summary>
		/// 栏号(自增)
		/// </summary>
		public int cancelDetail_Lineno
		{
			set{ _canceldetail_lineno=value;}
			get{return _canceldetail_lineno;}
		}
		/// <summary>
		/// 主表编号
		/// </summary>
		public string cancelDetail_Code
		{
			set{ _canceldetail_code=value;}
			get{return _canceldetail_code;}
		}
		/// <summary>
		/// 物料编号
		/// </summary>
		public string cancelDetail_MaterialID
		{
			set{ _canceldetail_materialid=value;}
			get{return _canceldetail_materialid;}
		}
		/// <summary>
		/// 物料名称
		/// </summary>
		public string cancelDetail_MaterialName
		{
			set{ _canceldetail_materialname=value;}
			get{return _canceldetail_materialname;}
		}
		/// <summary>
		/// 规格型号
		/// </summary>
		public string cancelDetail_Model
		{
			set{ _canceldetail_model=value;}
			get{return _canceldetail_model;}
		}
		/// <summary>
		/// 单位
		/// </summary>
		public string cancelDetail_Unit
		{
			set{ _canceldetail_unit=value;}
			get{return _canceldetail_unit;}
		}
		/// <summary>
		/// 数量
		/// </summary>
		public string cancelDetail_CurNumber
		{
			set{ _canceldetail_curnumber=value;}
			get{return _canceldetail_curnumber;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public int? cancelDetail_Clear
		{
			set{ _canceldetail_clear=value;}
			get{return _canceldetail_clear;}
		}
		/// <summary>
		/// 预留字段1
		/// </summary>
		public string cancelDetail_Safetyone
		{
			set{ _canceldetail_safetyone=value;}
			get{return _canceldetail_safetyone;}
		}
		/// <summary>
		/// 预留字段2
		/// </summary>
		public string cancelDetail_Safetytwo
		{
			set{ _canceldetail_safetytwo=value;}
			get{return _canceldetail_safetytwo;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string cancelDetail_Remark
		{
			set{ _canceldetail_remark=value;}
			get{return _canceldetail_remark;}
		}
		/// <summary>
		/// 单价(折扣后)
		/// </summary>
		public string cancelDetail_DiscountAPrice
		{
			set{ _canceldetail_discountaprice=value;}
			get{return _canceldetail_discountaprice;}
		}
		/// <summary>
		/// 折扣
		/// </summary>
		public string cancelDetail_Discount
		{
			set{ _canceldetail_discount=value;}
			get{return _canceldetail_discount;}
		}
		/// <summary>
		/// 折扣前单价
		/// </summary>
		public string cancelDetail_DiscountBPrice
		{
			set{ _canceldetail_discountbprice=value;}
			get{return _canceldetail_discountbprice;}
		}
		/// <summary>
		/// 金额
		/// </summary>
		public string cancelDetail_AmountMoney
		{
			set{ _canceldetail_amountmoney=value;}
			get{return _canceldetail_amountmoney;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? cancelDetail_UpdateDate
		{
			set{ _canceldetail_updatedate=value;}
			get{return _canceldetail_updatedate;}
		}
		#endregion Model

	}
}

