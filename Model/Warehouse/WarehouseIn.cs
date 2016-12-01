using System;
namespace Model
{
	/// <summary>
	/// T_WarehouseIn:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public partial class WarehouseIn
	{
		public WarehouseIn()
		{}
		#region Model
		private int _id=0;
		private string _code;
		private string _type;
		private string _stock;
		private string _operation;
        private string _makeman;
		private string _examine;
		private int? _state;
		private DateTime? _date;
		private string _purchasecode;
		private int? _checkstate;
		private int? _isclear;
		private DateTime? _updatedate;
		private string _reserved1;
		private string _reserved2;
		private string _remark;
        private string _goodsCode;
        private string _defaultType;
        private string _suppliercode;
        private string _suppliername;
        private string _supplierphone;
        /// <summary>
        /// ����ID 
        /// </summary>
        public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// �������(����)
		/// </summary>
		public string code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// �������
		/// </summary>
		public string type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// �ֿ�
		/// </summary>
		public string stock
		{
			set{ _stock=value;}
			get{return _stock;}
		}
		/// <summary>
		/// ���Ա
		/// </summary>
		public string operation
		{
			set{ _operation=value;}
			get{return _operation;}
		}
        /// <summary>
        /// �Ƶ���
        /// </summary>
        public string makeMan
        {
            set { _makeman = value; }
            get { return _makeman; }
        }
        /// <summary>
        /// �����
        /// </summary>
        public string examine
		{
			set{ _examine=value;}
			get{return _examine;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? state
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? date
		{
			set{ _date=value;}
			get{return _date;}
		}
		/// <summary>
		/// �ɹ���Code
		/// </summary>
		public string purchaseCode
		{
			set{ _purchasecode=value;}
			get{return _purchasecode;}
		}
		/// <summary>
		/// ���״̬ 0��δ��ˣ�1�������
		/// </summary>
		public int? checkState
		{
			set{ _checkstate=value;}
			get{return _checkstate;}
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
		/// ����ʱ��
		/// </summary>
		public DateTime? updateDate
		{
			set{ _updatedate=value;}
			get{return _updatedate;}
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
		/// ��ע
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
        /// <summary>
        /// ��Ӧ�̵���code
        /// </summary>
        public string goodsCode
        {
            get
            {
                return _goodsCode;
            }

            set
            {
                _goodsCode = value;
            }
        }
        /// <summary>
        /// Ĭ�ϵ�������
        /// </summary>
        public string defaultType
        {
            get
            {
                return _defaultType;
            }

            set
            {
                _defaultType = value;
            }
        }

        /// <summary>
        /// ��Ӧ��code
        /// </summary>
        public string supplierCode
        {
            get
            {
                return _suppliercode;
            }

            set
            {
                _suppliercode = value;
            }
        }
        /// <summary>
        /// ��Ӧ������
        /// </summary>
        public string supplierName
        {
            get
            {
                return _suppliername;
            }

            set
            {
                _suppliername = value;
            }
        }
        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        public string supplierPhone
        {
            get
            {
                return _supplierphone;
            }

            set
            {
                _supplierphone = value;
            }
        }
        #endregion Model

    }
}

