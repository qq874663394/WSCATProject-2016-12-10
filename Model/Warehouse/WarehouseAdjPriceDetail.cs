using System;
namespace Model
{
	/// <summary>
	/// 仓库表
	/// </summary>
	[Serializable]
	public partial class WarehouseAdjPriceDetail
	{
		public WarehouseAdjPriceDetail()
		{}
		#region Model
		private string _adjpricedetail_id;
		private int _adjpricedetail_lineno;
		private string _adjpricedetail_maid;
		private string _adjpricedetail_maname;
		private string _adjpricedetail_model;
		private string _adjpricedetail_unit;
		private string _adjpricedetail_curprice;
		private string _adjpricedetail_scale;
		private string _adjpricedetail_price;
		private string _adjpricedetail_lostmoney;
		private string _adjpricedetail_cause;
		private int? _adjpricedetail_clear;
		private string _adjpricedetail_safetyone;
		private string _adjpricedetail_safetytwo;
		private string _adjpricedetail_remark;
		private DateTime? _adjpricedetail_updatedata;
		/// <summary>
		/// 调价编号
		/// </summary>
		public string adjPriceDetail_ID
		{
			set{ _adjpricedetail_id=value;}
			get{return _adjpricedetail_id;}
		}
		/// <summary>
		/// 自增ID
		/// </summary>
		public int adjPriceDetail_Lineno
		{
			set{ _adjpricedetail_lineno=value;}
			get{return _adjpricedetail_lineno;}
		}
		/// <summary>
		/// 物料编号
		/// </summary>
		public string adjPriceDetail_MaID
		{
			set{ _adjpricedetail_maid=value;}
			get{return _adjpricedetail_maid;}
		}
		/// <summary>
		/// 物料名称
		/// </summary>
		public string adjPriceDetail_MaName
		{
			set{ _adjpricedetail_maname=value;}
			get{return _adjpricedetail_maname;}
		}
		/// <summary>
		/// 规格型号
		/// </summary>
		public string adjPriceDetail_Model
		{
			set{ _adjpricedetail_model=value;}
			get{return _adjpricedetail_model;}
		}
		/// <summary>
		/// 单位
		/// </summary>
		public string adjPriceDetail_Unit
		{
			set{ _adjpricedetail_unit=value;}
			get{return _adjpricedetail_unit;}
		}
		/// <summary>
		/// 调前单价
		/// </summary>
		public string adjPriceDetail_CurPrice
		{
			set{ _adjpricedetail_curprice=value;}
			get{return _adjpricedetail_curprice;}
		}
		/// <summary>
		/// 调价比例
		/// </summary>
		public string adjPriceDetail_Scale
		{
			set{ _adjpricedetail_scale=value;}
			get{return _adjpricedetail_scale;}
		}
		/// <summary>
		/// 调后单价
		/// </summary>
		public string adjPriceDetail_Price
		{
			set{ _adjpricedetail_price=value;}
			get{return _adjpricedetail_price;}
		}
		/// <summary>
		/// 差额
		/// </summary>
		public string adjPriceDetail_LostMoney
		{
			set{ _adjpricedetail_lostmoney=value;}
			get{return _adjpricedetail_lostmoney;}
		}
		/// <summary>
		/// 原因
		/// </summary>
		public string adjPriceDetail_Cause
		{
			set{ _adjpricedetail_cause=value;}
			get{return _adjpricedetail_cause;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public int? adjPriceDetail_Clear
		{
			set{ _adjpricedetail_clear=value;}
			get{return _adjpricedetail_clear;}
		}
		/// <summary>
		/// 预留字段
		/// </summary>
		public string adjPriceDetail_Safetyone
		{
			set{ _adjpricedetail_safetyone=value;}
			get{return _adjpricedetail_safetyone;}
		}
		/// <summary>
		/// 预留字段
		/// </summary>
		public string adjPriceDetail_Safetytwo
		{
			set{ _adjpricedetail_safetytwo=value;}
			get{return _adjpricedetail_safetytwo;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string adjPriceDetail_Remark
		{
			set{ _adjpricedetail_remark=value;}
			get{return _adjpricedetail_remark;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? adjPriceDetail_UpdateData
		{
			set{ _adjpricedetail_updatedata=value;}
			get{return _adjpricedetail_updatedata;}
		}
		#endregion Model

	}
}

