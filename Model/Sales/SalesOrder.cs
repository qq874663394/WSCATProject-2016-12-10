using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Sales
{
    public class SalesOrder
    {
        #region Model
        private int _id;
        private string _clientcode;
        private DateTime? _deliversdate;
        private int? _deliversmethod;
        private string _remark;
        private string _code;
        private DateTime? _date;
        private string _deliverslocation;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 客户外键
        /// </summary>
        public string clientCode
        {
            set { _clientcode = value; }
            get { return _clientcode; }
        }
        /// <summary>
        /// 交货日期
        /// </summary>
        public DateTime? deliversDate
        {
            set { _deliversdate = value; }
            get { return _deliversdate; }
        }
        /// <summary>
        /// 交货方式
        /// </summary>
        public int? deliversMethod
        {
            set { _deliversmethod = value; }
            get { return _deliversmethod; }
        }
        /// <summary>
        /// 摘要
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? date
        {
            set { _date = value; }
            get { return _date; }
        }
        /// <summary>
        /// 交货地点
        /// </summary>
        public string deliversLocation
        {
            set { _deliverslocation = value; }
            get { return _deliverslocation; }
        }
        #endregion Model
    }
}
