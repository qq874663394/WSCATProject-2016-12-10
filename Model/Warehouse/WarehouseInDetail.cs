using System;
namespace Model
{
	/// <summary>
	/// T_WarehouseInDetail:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public partial class WarehouseInDetail
	{
		public WarehouseInDetail()
		{}
		#region Model
		private int _id;
		private string _code;
		private string _materianame;
		private string _materiamodel;
		private string _materiaunit;
		private decimal? _number;
		private decimal? _price;
		private decimal? _money;
		private string _barcode;
		private string _rfid;
		private DateTime? _updatedate;
		private int? _warehouseindetailstate;
		private DateTime? _date;
		private int? _isclear;
		private string _remark;
		private string _reserved1;
		private string _reserved2;
        private string _storageRackName;
        private string _storageRackCode;
        private int? _isArrive;
        /// <summary>
        /// ����(����)
        /// </summary>
        public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// ��ⵥ��
		/// </summary>
		public string code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string materiaName
		{
			set{ _materianame=value;}
			get{return _materianame;}
		}
		/// <summary>
		/// ����ͺ�
		/// </summary>
		public string materiaModel
		{
			set{ _materiamodel=value;}
			get{return _materiamodel;}
		}
		/// <summary>
		/// ��λ
		/// </summary>
		public string materiaUnit
		{
			set{ _materiaunit=value;}
			get{return _materiaunit;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public decimal? number
		{
			set{ _number=value;}
			get{return _number;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public decimal? price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// ���
		/// </summary>
		public decimal? money
		{
			set{ _money=value;}
			get{return _money;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string barcode
		{
			set{ _barcode=value;}
			get{return _barcode;}
		}
		/// <summary>
		/// RFID
		/// </summary>
		public string rfid
		{
			set{ _rfid=value;}
			get{return _rfid;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? updateDate
		{
			set{ _updatedate=value;}
			get{return _updatedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? warehouseInDetailState
		{
			set{ _warehouseindetailstate=value;}
			get{return _warehouseindetailstate;}
		}
		/// <summary>
		/// ���ʱ��
		/// </summary>
		public DateTime? date
		{
			set{ _date=value;}
			get{return _date;}
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
		/// ��ע
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// �����ֶ�1
		/// </summary>
		public string reserved1
		{
			set{ _reserved1=value;}
			get{return _reserved1;}
		}
		/// <summary>
		/// �����ֶ�2
		/// </summary>
		public string reserved2
		{
			set{ _reserved2=value;}
			get{return _reserved2;}
		}
        /// <summary>
        /// ��������
        /// </summary>
        public string StorageRackName
        {
            get
            {
                return _storageRackName;
            }

            set
            {
                _storageRackName = value;
            }
        }
        /// <summary>
        /// ����code
        /// </summary>
        public string StorageRackCode
        {
            get
            {
                return _storageRackCode;
            }

            set
            {
                _storageRackCode = value;
            }
        }
        /// <summary>
        /// �����Ƿ�ִ�
        /// </summary>
        public int? IsArrive
        {
            get
            {
                return _isArrive;
            }

            set
            {
                _isArrive = value;
            }
        }
        #endregion Model

    }
}

