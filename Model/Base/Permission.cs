using System;
namespace Model
{
	/// <summary>
	/// 单据类型表
	/// </summary>
	[Serializable]
	public partial class Permission
	{
		public Permission()
		{}
		#region Model
		private int _per_id;
		private string _per_code;
		private string _per_modulename;
		private int? _per_readstate;
		private int? _per_writestate;
		private int? _per_auditstate;
		private int? _per_clear;
		private string _per_type;
		private string _per_rolecode;
		private DateTime? _per_updatedate;
		/// <summary>
		/// 
		/// </summary>
		public int Per_ID
		{
			set{ _per_id=value;}
			get{return _per_id;}
		}
		/// <summary>
		/// 编号
		/// </summary>
		public string Per_Code
		{
			set{ _per_code=value;}
			get{return _per_code;}
		}
		/// <summary>
		/// 模块名称
		/// </summary>
		public string Per_ModuleName
		{
			set{ _per_modulename=value;}
			get{return _per_modulename;}
		}
		/// <summary>
		/// 读取权限状态
		/// </summary>
		public int? Per_ReadState
		{
			set{ _per_readstate=value;}
			get{return _per_readstate;}
		}
		/// <summary>
		/// 写入权限状态
		/// </summary>
		public int? Per_WriteState
		{
			set{ _per_writestate=value;}
			get{return _per_writestate;}
		}
		/// <summary>
		/// 审核权限状态
		/// </summary>
		public int? Per_AuditState
		{
			set{ _per_auditstate=value;}
			get{return _per_auditstate;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public int? Per_Clear
		{
			set{ _per_clear=value;}
			get{return _per_clear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Per_Type
		{
			set{ _per_type=value;}
			get{return _per_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Per_RoleCode
		{
			set{ _per_rolecode=value;}
			get{return _per_rolecode;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? Per_UpdateDate
		{
			set{ _per_updatedate=value;}
			get{return _per_updatedate;}
		}
		#endregion Model

	}
}

