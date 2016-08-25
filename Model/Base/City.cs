using System;
namespace Model
{
	/// <summary>
	/// 借款类型表
	/// </summary>
	[Serializable]
	public partial class City
	{
		public City()
		{}
		#region Model
		private int _city_id;
		private string _city_code;
		private string _city_name;
		private string _city_parentid;
		private int? _city_enable;
		private int? _city_clear;
		private DateTime? _city_updatedate;
		/// <summary>
		/// 
		/// </summary>
		public int City_ID
		{
			set{ _city_id=value;}
			get{return _city_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string City_Code
		{
			set{ _city_code=value;}
			get{return _city_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string City_Name
		{
			set{ _city_name=value;}
			get{return _city_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string City_ParentId
		{
			set{ _city_parentid=value;}
			get{return _city_parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? City_Enable
		{
			set{ _city_enable=value;}
			get{return _city_enable;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? City_Clear
		{
			set{ _city_clear=value;}
			get{return _city_clear;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? City_UpdateDate
		{
			set{ _city_updatedate=value;}
			get{return _city_updatedate;}
		}
		#endregion Model

	}
}

