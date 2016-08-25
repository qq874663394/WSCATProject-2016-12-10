using System;
namespace Model
{
	/// <summary>
	/// 仓库表
	/// </summary>
	[Serializable]
	public partial class WarehouseBreakageDetail
	{
		public WarehouseBreakageDetail()
		{}
		#region Model
		private string _breakagedetail_id;
		private int _breakagedetail_lineno;
		private string _breakagedetail_maid;
		private string _breakagedetail_maname;
		private string _breakagedetail_model;
		private string _breakagedetail_unit;
		private string _breakagedetail_curqty;
		private string _breakagedetail_checkqty;
		private string _breakagedetail_lostqty;
		private string _breakagedetail_price;
		private string _breakagedetail_lostmoney;
		private string _breakagedetail_cause;
		private int? _breakagedetail_clear;
		private string _breakagedetail_safetyone;
		private string _breakagedetail_safetytwo;
		private string _breakagedetail_remark;
		private DateTime? _breakagedetail_updatedate;
		/// <summary>
		/// 报损编号
		/// </summary>
		public string breakageDetail_ID
		{
			set{ _breakagedetail_id=value;}
			get{return _breakagedetail_id;}
		}
		/// <summary>
		/// 栏号(自增)
		/// </summary>
		public int breakageDetail_Lineno
		{
			set{ _breakagedetail_lineno=value;}
			get{return _breakagedetail_lineno;}
		}
		/// <summary>
		/// 物料编号
		/// </summary>
		public string breakageDetail_MaID
		{
			set{ _breakagedetail_maid=value;}
			get{return _breakagedetail_maid;}
		}
		/// <summary>
		/// 物料名称
		/// </summary>
		public string breakageDetail_MaName
		{
			set{ _breakagedetail_maname=value;}
			get{return _breakagedetail_maname;}
		}
		/// <summary>
		/// 规格型号
		/// </summary>
		public string breakageDetail_Model
		{
			set{ _breakagedetail_model=value;}
			get{return _breakagedetail_model;}
		}
		/// <summary>
		/// 单位
		/// </summary>
		public string breakageDetail_Unit
		{
			set{ _breakagedetail_unit=value;}
			get{return _breakagedetail_unit;}
		}
		/// <summary>
		/// 现有数量
		/// </summary>
		public string breakageDetail_CurQty
		{
			set{ _breakagedetail_curqty=value;}
			get{return _breakagedetail_curqty;}
		}
		/// <summary>
		/// 损坏数量
		/// </summary>
		public string breakageDetail_CheckQty
		{
			set{ _breakagedetail_checkqty=value;}
			get{return _breakagedetail_checkqty;}
		}
		/// <summary>
		/// 剩余数量
		/// </summary>
		public string breakageDetail_LostQty
		{
			set{ _breakagedetail_lostqty=value;}
			get{return _breakagedetail_lostqty;}
		}
		/// <summary>
		/// 单价
		/// </summary>
		public string breakageDetail_Price
		{
			set{ _breakagedetail_price=value;}
			get{return _breakagedetail_price;}
		}
		/// <summary>
		/// 盈亏金额
		/// </summary>
		public string breakageDetail_LostMoney
		{
			set{ _breakagedetail_lostmoney=value;}
			get{return _breakagedetail_lostmoney;}
		}
		/// <summary>
		/// 原因
		/// </summary>
		public string breakageDetail_Cause
		{
			set{ _breakagedetail_cause=value;}
			get{return _breakagedetail_cause;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public int? breakageDetail_Clear
		{
			set{ _breakagedetail_clear=value;}
			get{return _breakagedetail_clear;}
		}
		/// <summary>
		/// 预留字段
		/// </summary>
		public string breakageDetail_Safetyone
		{
			set{ _breakagedetail_safetyone=value;}
			get{return _breakagedetail_safetyone;}
		}
		/// <summary>
		/// 预留字段
		/// </summary>
		public string breakageDetail_Safetytwo
		{
			set{ _breakagedetail_safetytwo=value;}
			get{return _breakagedetail_safetytwo;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string breakageDetail_Remark
		{
			set{ _breakagedetail_remark=value;}
			get{return _breakagedetail_remark;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? breakageDetail_UpdateDate
		{
			set{ _breakagedetail_updatedate=value;}
			get{return _breakagedetail_updatedate;}
		}
		#endregion Model

	}
}

