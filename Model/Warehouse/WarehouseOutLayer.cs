using System;
namespace Model
{
	/// <summary>
	/// 仓库表
	/// </summary>
	[Serializable]
	public partial class WarehouseOut
	{
		public WarehouseOut()
		{}
		#region Model
		private int _out_id;
		private string _out_code;
		private string _out_type;
		private string _out_stock;
		private string _out_operation;
		private string _out_examine;
		private string _out_remark;
		private string _out_satetyone;
		private string _out_satetytwo;
		private int? _out_clea;
		private DateTime? _outsto_updatedate;
        private int? _out_State;
        private string _out_SalesCode;
        private DateTime? _out_Date;
        private int? _out_review;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int out_ID
		{
			set{ _out_id=value;}
			get{return _out_id;}
		}
		/// <summary>
		/// 出货单号（生成）
		/// </summary>
		public string out_Code
		{
			set{ _out_code=value;}
			get{return _out_code;}
		}
		/// <summary>
		/// 出货类型
		/// </summary>
		public string out_Type
		{
			set{ _out_type=value;}
			get{return _out_type;}
		}
		/// <summary>
		/// 仓库
		/// </summary>
		public string out_Stock
		{
			set{ _out_stock=value;}
			get{return _out_stock;}
		}
		/// <summary>
		/// 出库员
		/// </summary>
		public string out_Operation
		{
			set{ _out_operation=value;}
			get{return _out_operation;}
		}
		/// <summary>
		/// 审查人
		/// </summary>
		public string out_Examine
		{
			set{ _out_examine=value;}
			get{return _out_examine;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string out_Remark
		{
			set{ _out_remark=value;}
			get{return _out_remark;}
		}
		/// <summary>
		/// 保留字段1
		/// </summary>
		public string out_Satetyone
		{
			set{ _out_satetyone=value;}
			get{return _out_satetyone;}
		}
		/// <summary>
		/// 保留字段2
		/// </summary>
		public string out_Satetytwo
		{
			set{ _out_satetytwo=value;}
			get{return _out_satetytwo;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public int? out_Clea
		{
			set{ _out_clea=value;}
			get{return _out_clea;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? outSto_UpdateDate
		{
			set{ _outsto_updatedate=value;}
			get{return _outsto_updatedate;}
		}
        /// <summary>
        /// 单据状态
        /// </summary>
        public int? out_State
        {
            get
            {
                return _out_State;
            }

            set
            {
                _out_State = value;
            }
        }
        /// <summary>
        /// 销售单时间
        /// </summary>
        public string out_SalesCode
        {
            get
            {
                return _out_SalesCode;
            }

            set
            {
                _out_SalesCode = value;
            }
        }
        /// <summary>
        /// 开单时间
        /// </summary>
        public DateTime? out_Date
        {
            get
            {
                return _out_Date;
            }

            set
            {
                _out_Date = value;
            }
        }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int? out_review
        {
            get
            {
                return _out_review;
            }

            set
            {
                _out_review = value;
            }
        }
        #endregion Model

    }
}

