using System;
namespace Model
{
	/// <summary>
	/// ���ݱ����
	/// </summary>
	[Serializable]
	public partial class Serialization
	{
		public Serialization()
		{}
		#region Model
		private int _ser_id;
		private string _ser_code;
		private string _ser_description;
		private string _ser_prelenght;
		private int? _ser_alllenght;
		private string _ser_prefix;
		private DateTime? _ser_currentdate= DateTime.Now;
		private int? _ser_segno;
		private int? _ser_clear=1;
		private DateTime? _ser_updatedate;
		/// <summary>
		/// ���ݱ��
		/// </summary>
		public int Ser_ID
		{
			set{ _ser_id=value;}
			get{return _ser_id;}
		}
		/// <summary>
		/// �������
		/// </summary>
		public string Ser_Code
		{
			set{ _ser_code=value;}
			get{return _ser_code;}
		}
		/// <summary>
		/// ����˵��
		/// </summary>
		public string Ser_Description
		{
			set{ _ser_description=value;}
			get{return _ser_description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ser_PreLenght
		{
			set{ _ser_prelenght=value;}
			get{return _ser_prelenght;}
		}
		/// <summary>
		/// �ܱ��볤��
		/// </summary>
		public int? Ser_AllLenght
		{
			set{ _ser_alllenght=value;}
			get{return _ser_alllenght;}
		}
		/// <summary>
		/// �������
		/// </summary>
		public string Ser_Prefix
		{
			set{ _ser_prefix=value;}
			get{return _ser_prefix;}
		}
		/// <summary>
		/// ��ǰ����
		/// </summary>
		public DateTime? Ser_CurrentDate
		{
			set{ _ser_currentdate=value;}
			get{return _ser_currentdate;}
		}
		/// <summary>
		/// �¸����ֵ
		/// </summary>
		public int? Ser_SegNo
		{
			set{ _ser_segno=value;}
			get{return _ser_segno;}
		}
		/// <summary>
		/// �Ƿ�ͣ��
		/// </summary>
		public int? Ser_Clear
		{
			set{ _ser_clear=value;}
			get{return _ser_clear;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? Ser_UpdateDate
		{
			set{ _ser_updatedate=value;}
			get{return _ser_updatedate;}
		}
		#endregion Model

	}
}

