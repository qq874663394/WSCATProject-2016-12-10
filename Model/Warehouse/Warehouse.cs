﻿using System;
namespace Model
{
	/// <summary>
	/// 仓库表
	/// </summary>
	[Serializable]
	public partial class Warehouse
	{
		public Warehouse()
		{}
		#region Model
		private int _id;
		private string _code;
		private string _materialcode;
		private string _name;
		private string _storagecode;
		private decimal? _ininumber;
		private decimal? _iniprice;
		private decimal? _currentnumber;
		private decimal? _currentprice;
		private decimal? _allnumber;
		private decimal? _allprice;
		private decimal? _enanumber;
		private decimal? _floornumber;
		private int? _clear;
		private string _remark;
		private string _safetyone;
		private string _safetytwo;
		private DateTime? _updatedate;
		/// <summary>
		/// 自增ID
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 物料ID
		/// </summary>
		public string materialCode
		{
			set{ _materialcode=value;}
			get{return _materialcode;}
		}
		/// <summary>
		/// 仓库名
		/// </summary>
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 仓库ID
		/// </summary>
		public string storageCode
		{
			set{ _storagecode=value;}
			get{return _storagecode;}
		}
		/// <summary>
		/// 期初数量
		/// </summary>
		public decimal? iniNumber
		{
			set{ _ininumber=value;}
			get{return _ininumber;}
		}
		/// <summary>
		/// 期初总值
		/// </summary>
		public decimal? iniPrice
		{
			set{ _iniprice=value;}
			get{return _iniprice;}
		}
		/// <summary>
		/// 本期收入数量
		/// </summary>
		public decimal? currentNumber
		{
			set{ _currentnumber=value;}
			get{return _currentnumber;}
		}
		/// <summary>
		/// 本期收入总值
		/// </summary>
		public decimal? currentPrice
		{
			set{ _currentprice=value;}
			get{return _currentprice;}
		}
		/// <summary>
		/// 现有总数量
		/// </summary>
		public decimal? allNumber
		{
			set{ _allnumber=value;}
			get{return _allnumber;}
		}
		/// <summary>
		/// 现有总价值
		/// </summary>
		public decimal? allPrice
		{
			set{ _allprice=value;}
			get{return _allprice;}
		}
		/// <summary>
		/// 可用总量
		/// </summary>
		public decimal? enaNumber
		{
			set{ _enanumber=value;}
			get{return _enanumber;}
		}
		/// <summary>
		/// 安全数量
		/// </summary>
		public decimal? floorNumber
		{
			set{ _floornumber=value;}
			get{return _floornumber;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public int? clear
		{
			set{ _clear=value;}
			get{return _clear;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 保留字段1
		/// </summary>
		public string safetyone
		{
			set{ _safetyone=value;}
			get{return _safetyone;}
		}
		/// <summary>
		/// 保留字段2
		/// </summary>
		public string safetytwo
		{
			set{ _safetytwo=value;}
			get{return _safetytwo;}
		}
		/// <summary>
		/// 更改时间
		/// </summary>
		public DateTime? updateDate
		{
			set{ _updatedate=value;}
			get{return _updatedate;}
		}
		#endregion Model

	}
}

