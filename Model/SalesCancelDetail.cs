using System;
namespace Model
{
	/// <summary>
	/// 退货明细表
	/// </summary>
	[Serializable]
	public partial class SalesCancelDetail
	{
		public SalesCancelDetail()
		{}
		#region Model
		private int _canceldetail_lineno;
		private string _canceldetail_code;
		private string _canceldetail_materialcode;
		private string _canceldetail_materianame;
		private string _canceldetail_model;
		private string _canceldetail_unit;
		private string _canceldetail_number;
		private string _canceldetail_afterprice;
		private string _canceldetail_discount;
		private string _canceldetail_beforediscount;
		private string _canceldetail_price;
		private int? _canceldetail_clear=1;
		private string _canceldetail_safetyone;
		private string _canceldetail_safetytwo;
		private string _canceldetail_remark;
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
		public string cancelDetail_MaterialCode
		{
			set{ _canceldetail_materialcode=value;}
			get{return _canceldetail_materialcode;}
		}
		/// <summary>
		/// 物料名称
		/// </summary>
		public string cancelDetail_MateriaName
		{
			set{ _canceldetail_materianame=value;}
			get{return _canceldetail_materianame;}
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
		public string cancelDetail_Number
		{
			set{ _canceldetail_number=value;}
			get{return _canceldetail_number;}
		}
		/// <summary>
		/// 折扣后的单价
		/// </summary>
		public string cancelDetail_AfterPrice
		{
			set{ _canceldetail_afterprice=value;}
			get{return _canceldetail_afterprice;}
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
		public string cancelDetail_BeforeDiscount
		{
			set{ _canceldetail_beforediscount=value;}
			get{return _canceldetail_beforediscount;}
		}
		/// <summary>
		/// 金额
		/// </summary>
		public string cancelDetail_Price
		{
			set{ _canceldetail_price=value;}
			get{return _canceldetail_price;}
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

