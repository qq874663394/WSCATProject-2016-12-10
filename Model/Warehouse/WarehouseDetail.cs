using System;
namespace Model
{
	/// <summary>
	/// 仓库表
	/// </summary>
	[Serializable]
	public partial class WarehouseDetail
	{
		public WarehouseDetail()
		{}
		#region Model
		private int _detail_lineno;
		private string _detail_code;
		private string _detail_linecode;
		private string _detail_number;
		private DateTime? _detail_date;
		private string _detail_location;
		private int? _detail_clear;
		private string _detail_remark;
		private string _detail_safetyone;
		private string _detail_safetytwo;
		private DateTime? _detail_updatedate;
		/// <summary>
		/// 栏号(自增)
		/// </summary>
		public int detail_Lineno
		{
			set{ _detail_lineno=value;}
			get{return _detail_lineno;}
		}
		/// <summary>
		/// 主表code
		/// </summary>
		public string detail_Code
		{
			set{ _detail_code=value;}
			get{return _detail_code;}
		}
		/// <summary>
		/// 物料编码
		/// </summary>
		public string detail_LineCode
		{
			set{ _detail_linecode=value;}
			get{return _detail_linecode;}
		}
		/// <summary>
		/// 物料规格型号
		/// </summary>
		public string detail_Number
		{
			set{ _detail_number=value;}
			get{return _detail_number;}
		}
		/// <summary>
		/// 时间
		/// </summary>
		public DateTime? detail_Date
		{
			set{ _detail_date=value;}
			get{return _detail_date;}
		}
		/// <summary>
		/// 货架位置
		/// </summary>
		public string detail_Location
		{
			set{ _detail_location=value;}
			get{return _detail_location;}
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
		/// 备注
		/// </summary>
		public string detail_Remark
		{
			set{ _detail_remark=value;}
			get{return _detail_remark;}
		}
		/// <summary>
		/// 保留字段1
		/// </summary>
		public string detail_Safetyone
		{
			set{ _detail_safetyone=value;}
			get{return _detail_safetyone;}
		}
		/// <summary>
		/// 保留字段2
		/// </summary>
		public string detail_Safetytwo
		{
			set{ _detail_safetytwo=value;}
			get{return _detail_safetytwo;}
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

