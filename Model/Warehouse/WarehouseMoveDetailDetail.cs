using System;
namespace Model
{
	/// <summary>
	/// 仓库表
	/// </summary>
	[Serializable]
	public partial class WarehouseMoveDetailDetail
	{
		public WarehouseMoveDetailDetail()
		{}
		#region Model
		private string _movedetail_id;
		private int _movedetai_lineno;
		private string _movedetai_maid;
		private string _movedetai_maname;
		private string _movedetai_model;
		private string _movedetai_unit;
		private string _movedetai_curnumber;
		private int? _movedetai_clear;
		private string _movedetai_safetyone;
		private string _movedetai_safetytwo;
		private string _movedetai_remark;
		private DateTime? _movedetai_updatedate;
		/// <summary>
		/// 调拨编号
		/// </summary>
		public string moveDetail_ID
		{
			set{ _movedetail_id=value;}
			get{return _movedetail_id;}
		}
		/// <summary>
		/// 栏号(自增)
		/// </summary>
		public int moveDetai_Lineno
		{
			set{ _movedetai_lineno=value;}
			get{return _movedetai_lineno;}
		}
		/// <summary>
		/// 物料编号
		/// </summary>
		public string moveDetai_MaID
		{
			set{ _movedetai_maid=value;}
			get{return _movedetai_maid;}
		}
		/// <summary>
		/// 物料名称
		/// </summary>
		public string moveDetai_MaName
		{
			set{ _movedetai_maname=value;}
			get{return _movedetai_maname;}
		}
		/// <summary>
		/// 规格型号
		/// </summary>
		public string moveDetai_Model
		{
			set{ _movedetai_model=value;}
			get{return _movedetai_model;}
		}
		/// <summary>
		/// 单位
		/// </summary>
		public string moveDetai_Unit
		{
			set{ _movedetai_unit=value;}
			get{return _movedetai_unit;}
		}
		/// <summary>
		/// 数量
		/// </summary>
		public string moveDetai_CurNumber
		{
			set{ _movedetai_curnumber=value;}
			get{return _movedetai_curnumber;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public int? moveDetai_Clear
		{
			set{ _movedetai_clear=value;}
			get{return _movedetai_clear;}
		}
		/// <summary>
		/// 预留字段1
		/// </summary>
		public string moveDetai_Safetyone
		{
			set{ _movedetai_safetyone=value;}
			get{return _movedetai_safetyone;}
		}
		/// <summary>
		/// 预留字段2
		/// </summary>
		public string moveDetai_Safetytwo
		{
			set{ _movedetai_safetytwo=value;}
			get{return _movedetai_safetytwo;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string moveDetai_Remark
		{
			set{ _movedetai_remark=value;}
			get{return _movedetai_remark;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? moveDetai_UpdateDate
		{
			set{ _movedetai_updatedate=value;}
			get{return _movedetai_updatedate;}
		}
		#endregion Model

	}
}

