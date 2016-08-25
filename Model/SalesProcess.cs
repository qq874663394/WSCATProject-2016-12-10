using System;
namespace Model
{
	/// <summary>
	/// 销售订单明细表
	/// </summary>
	[Serializable]
	public partial class SalesProcess
	{
		public SalesProcess()
		{}
		#region Model
		private int _process_id;
		private string _process_code;
		private string _process_selllineno;
		private DateTime? _process_datetime;
		private string _process_opt;
		private string _process_ope;
		private string _process_remark;
		private int? _process_clear;
		private DateTime? _process_updatedate;
		/// <summary>
		/// 
		/// </summary>
		public int process_ID
		{
			set{ _process_id=value;}
			get{return _process_id;}
		}
		/// <summary>
		/// 主表code
		/// </summary>
		public string process_Code
		{
			set{ _process_code=value;}
			get{return _process_code;}
		}
		/// <summary>
		/// 栏号(code)
		/// </summary>
		public string process_SellLineno
		{
			set{ _process_selllineno=value;}
			get{return _process_selllineno;}
		}
		/// <summary>
		/// 操作时间
		/// </summary>
		public DateTime? process_Datetime
		{
			set{ _process_datetime=value;}
			get{return _process_datetime;}
		}
		/// <summary>
		/// 所做操作
		/// </summary>
		public string process_Opt
		{
			set{ _process_opt=value;}
			get{return _process_opt;}
		}
		/// <summary>
		/// 操作人
		/// </summary>
		public string process_Ope
		{
			set{ _process_ope=value;}
			get{return _process_ope;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string process_Remark
		{
			set{ _process_remark=value;}
			get{return _process_remark;}
		}
		/// <summary>
		/// 是否清除
		/// </summary>
		public int? process_Clear
		{
			set{ _process_clear=value;}
			get{return _process_clear;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? process_UpdateDate
		{
			set{ _process_updatedate=value;}
			get{return _process_updatedate;}
		}
		#endregion Model

	}
}

