using System;
namespace Model
{
	/// <summary>
	/// 供应商类别表
	/// </summary>
	[Serializable]
	public partial class Profession
	{
		public Profession()
		{}
		#region Model
		private int _st_id;
		private string _st_code;
		private string _st_name;
		private string _st_parentid;
		private int? _st_enable=1;
		private int? _st_clear=1;
		private DateTime? _st_updatdate;
		/// <summary>
		/// 
		/// </summary>
		public int ST_ID
		{
			set{ _st_id=value;}
			get{return _st_id;}
		}
		/// <summary>
		/// 类别编号
		/// </summary>
		public string ST_Code
		{
			set{ _st_code=value;}
			get{return _st_code;}
		}
		/// <summary>
		/// 类别名称
		/// </summary>
		public string ST_Name
		{
			set{ _st_name=value;}
			get{return _st_name;}
		}
		/// <summary>
		/// 保留字段1
		/// </summary>
		public string ST_ParentId
		{
			set{ _st_parentid=value;}
			get{return _st_parentid;}
		}
		/// <summary>
		/// 是否启用
		/// </summary>
		public int? ST_Enable
		{
			set{ _st_enable=value;}
			get{return _st_enable;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public int? ST_Clear
		{
			set{ _st_clear=value;}
			get{return _st_clear;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? ST_UpdatDate
		{
			set{ _st_updatdate=value;}
			get{return _st_updatdate;}
		}
		#endregion Model

	}
}

