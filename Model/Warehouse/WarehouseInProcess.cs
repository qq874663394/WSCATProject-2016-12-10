using System;
namespace Model
{
	/// <summary>
	/// T_WarehouseProcess:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public partial class WarehouseInProcess
	{
		public WarehouseInProcess()
		{}
		#region Model
		private int _id;
		private int? _isclear;
		private string _code;
		private string _warehouseindetailcode;
		private DateTime? _createdatetime;
		private string _operator;
		private string _operatorman;
		private string _remark;
		private DateTime? _updatedate;
		/// <summary>
		/// ����ID
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// �Ƿ�ɾ��
		/// </summary>
		public int? isClear
		{
			set{ _isclear=value;}
			get{return _isclear;}
		}
		/// <summary>
		/// ���Code
		/// </summary>
		public string code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// �����ϸCode
		/// </summary>
		public string warehouseInDetailCode
		{
			set{ _warehouseindetailcode=value;}
			get{return _warehouseindetailcode;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? createDatetime
		{
			set{ _createdatetime=value;}
			get{return _createdatetime;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string Operator
		{
			set{ _operator=value;}
			get{return _operator;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string operatorMan
		{
			set{ _operatorman=value;}
			get{return _operatorman;}
		}
		/// <summary>
		/// ��ע
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? updateDate
		{
			set{ _updatedate=value;}
			get{return _updatedate;}
		}
		#endregion Model

	}
}

