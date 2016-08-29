using System;
namespace Model
{
	/// <summary>
	/// 借款类型表
	/// </summary>
	[Serializable]
	public partial class BaseStorageRack
    {
		public BaseStorageRack()
		{}
		#region Model
		private int id;
		private string code;
		private string name;
		private string parentid;
		private int? enable;
		private int? clear;
		private DateTime? updatedate;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ id=value;}
			get{return id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Code
		{
			set{ code=value;}
			get{return code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ name=value;}
			get{return name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ParentId
		{
			set{ parentid=value;}
			get{return parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Enable
		{
			set{ enable=value;}
			get{return enable;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Clear
		{
			set{ clear=value;}
			get{return clear;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? UpdateDate
		{
			set{ updatedate=value;}
			get{return updatedate;}
		}
		#endregion Model

	}
}

