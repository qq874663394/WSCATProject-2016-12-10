using System;
namespace Model
{
	/// <summary>
	/// 仓库表
	/// </summary>
	[Serializable]
	public partial class WarehouseOutDetail
    {
		public WarehouseOutDetail()
		{}
		#region Model
		private string _outdetail_id;
		private int _outdetail_lineno;
		private string _outdetail_maid;
		private string _outdetail_maname;
		private string _outdetail_model;
		private string _outdetail_unit;
		private string _outdetail_number;
		private string _outdetail_price;
		private string _outdetail_money;
		private string _outdetail_satetyone;
		private string _outdetail_satetytwo;
		private int? _outdetail_clear;
		private string _outdetail_barcode;
		private string _outdetail_rfid;
		private DateTime? _outdetail_updatedate;
        private int? _outDetail_State;
        private DateTime? _outDetail_Date;
        /// <summary>
        /// 出库编号
        /// </summary>
        public string outDetail_ID
		{
			set{ _outdetail_id=value;}
			get{return _outdetail_id;}
		}
		/// <summary>
		/// 自增ID
		/// </summary>
		public int outDetail_Lineno
		{
			set{ _outdetail_lineno=value;}
			get{return _outdetail_lineno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string outDetail_MaID
		{
			set{ _outdetail_maid=value;}
			get{return _outdetail_maid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string outDetail_MaName
		{
			set{ _outdetail_maname=value;}
			get{return _outdetail_maname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string outDetail_Model
		{
			set{ _outdetail_model=value;}
			get{return _outdetail_model;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string outDetail_Unit
		{
			set{ _outdetail_unit=value;}
			get{return _outdetail_unit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string outDetail_Number
		{
			set{ _outdetail_number=value;}
			get{return _outdetail_number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string outDetail_Price
		{
			set{ _outdetail_price=value;}
			get{return _outdetail_price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string outDetail_Money
		{
			set{ _outdetail_money=value;}
			get{return _outdetail_money;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string outDetail_Satetyone
		{
			set{ _outdetail_satetyone=value;}
			get{return _outdetail_satetyone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string outDetail_Satetytwo
		{
			set{ _outdetail_satetytwo=value;}
			get{return _outdetail_satetytwo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? outDetail_Clear
		{
			set{ _outdetail_clear=value;}
			get{return _outdetail_clear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string outDetail_barcode
		{
			set{ _outdetail_barcode=value;}
			get{return _outdetail_barcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string outDetail_rfid
		{
			set{ _outdetail_rfid=value;}
			get{return _outdetail_rfid;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? outDetail_UpdateDate
		{
			set{ _outdetail_updatedate=value;}
			get{return _outdetail_updatedate;}
		}
        /// <summary>
        /// 单据状态
        /// </summary>
        public int? outDetail_State
        {
            get
            {
                return _outDetail_State;
            }

            set
            {
                _outDetail_State = value;
            }
        }
        /// <summary>
        /// 开单时间
        /// </summary>
        public DateTime? outDetail_Date
        {
            get
            {
                return _outDetail_Date;
            }

            set
            {
                _outDetail_Date = value;
            }
        }
        #endregion Model

    }
}

