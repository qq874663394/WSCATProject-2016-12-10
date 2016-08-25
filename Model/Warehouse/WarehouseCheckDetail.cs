using System;
namespace Model
{
	/// <summary>
	/// 仓库表
	/// </summary>
	[Serializable]
	public partial class WarehouseCheckDetail
	{
		public WarehouseCheckDetail()
		{}
		#region Model
		private string _checkdetail_id;
		private int _checkdetail_lineno;
		private int? _checkdetail_maid;
		private string _checkdetail_maname;
		private string _checkdetail_model;
		private string _checkdetail_unit;
		private decimal? _checkdetail_curqty;
		private decimal? _checkdetail_checkqty;
		private decimal? _checkdetail_lostqty;
		private decimal? _checkdetail_price;
		private decimal? _checkdetail_lostmoney;
		private string _checkdetail_cause;
		private int? _checkdetail_clear=1;
		private string _checkdetail_safetyone;
		private string _checkdetail_safetytwo;
		private string _checkdetail_remark;
		private DateTime? _checkdetail_updatedate;
		/// <summary>
		/// 盘点编号
		/// </summary>
		public string checkDetail_ID
		{
			set{ _checkdetail_id=value;}
			get{return _checkdetail_id;}
		}
		/// <summary>
		/// 栏号(自增)
		/// </summary>
		public int checkDetail_Lineno
		{
			set{ _checkdetail_lineno=value;}
			get{return _checkdetail_lineno;}
		}
		/// <summary>
		/// 物料ID
		/// </summary>
		public int? checkDetail_MaID
		{
			set{ _checkdetail_maid=value;}
			get{return _checkdetail_maid;}
		}
		/// <summary>
		/// 物料名称
		/// </summary>
		public string checkDetail_MaName
		{
			set{ _checkdetail_maname=value;}
			get{return _checkdetail_maname;}
		}
		/// <summary>
		/// 规格型号
		/// </summary>
		public string checkDetail_Model
		{
			set{ _checkdetail_model=value;}
			get{return _checkdetail_model;}
		}
		/// <summary>
		/// 单位
		/// </summary>
		public string checkDetail_Unit
		{
			set{ _checkdetail_unit=value;}
			get{return _checkdetail_unit;}
		}
		/// <summary>
		/// 账面数量
		/// </summary>
		public decimal? checkDetail_CurQty
		{
			set{ _checkdetail_curqty=value;}
			get{return _checkdetail_curqty;}
		}
		/// <summary>
		/// 盘点数量
		/// </summary>
		public decimal? checkDetail_CheckQty
		{
			set{ _checkdetail_checkqty=value;}
			get{return _checkdetail_checkqty;}
		}
		/// <summary>
		/// 盈亏数量
		/// </summary>
		public decimal? checkDetail_LostQty
		{
			set{ _checkdetail_lostqty=value;}
			get{return _checkdetail_lostqty;}
		}
		/// <summary>
		/// 单价
		/// </summary>
		public decimal? checkDetail_Price
		{
			set{ _checkdetail_price=value;}
			get{return _checkdetail_price;}
		}
		/// <summary>
		/// 盈亏金额
		/// </summary>
		public decimal? checkDetail_LostMoney
		{
			set{ _checkdetail_lostmoney=value;}
			get{return _checkdetail_lostmoney;}
		}
		/// <summary>
		/// 原因
		/// </summary>
		public string checkDetail_Cause
		{
			set{ _checkdetail_cause=value;}
			get{return _checkdetail_cause;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public int? checkDetail_Clear
		{
			set{ _checkdetail_clear=value;}
			get{return _checkdetail_clear;}
		}
		/// <summary>
		/// 预留字段1
		/// </summary>
		public string checkDetail_Safetyone
		{
			set{ _checkdetail_safetyone=value;}
			get{return _checkdetail_safetyone;}
		}
		/// <summary>
		/// 预留字段2
		/// </summary>
		public string checkDetail_Safetytwo
		{
			set{ _checkdetail_safetytwo=value;}
			get{return _checkdetail_safetytwo;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string checkDetail_Remark
		{
			set{ _checkdetail_remark=value;}
			get{return _checkdetail_remark;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? checkDetail_UpdateDate
		{
			set{ _checkdetail_updatedate=value;}
			get{return _checkdetail_updatedate;}
		}
		#endregion Model

	}
}

