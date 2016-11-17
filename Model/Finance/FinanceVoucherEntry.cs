using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Finance
{
    public class FinanceVoucherEntry
    {
        #region Model
        private int _id;
        private string _code;
        private DateTime? _date;
        private string _summary;
        private string _subject;
        private string _examine;
        private string _posting;
        private string _makeman;
        private int? _examinestate;
        private decimal? _debitamount;
        private decimal? _creditamount;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
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
        /// 摘要
        /// </summary>
        public string summary
        {
            set { _summary = value; }
            get { return _summary; }
        }
        /// <summary>
        /// 科目
        /// </summary>
        public string subject
        {
            set { _subject = value; }
            get { return _subject; }
        }
        /// <summary>
        /// 审核人
        /// </summary>
        public string examine
        {
            set { _examine = value; }
            get { return _examine; }
        }
        /// <summary>
        /// 过账人
        /// </summary>
        public string posting
        {
            set { _posting = value; }
            get { return _posting; }
        }
        /// <summary>
        /// 制单人
        /// </summary>
        public string makeMan
        {
            set { _makeman = value; }
            get { return _makeman; }
        }
        /// <summary>
        /// 审核状态0未审核,1已审核
        /// </summary>
        public int? examineState
        {
            set { _examinestate = value; }
            get { return _examinestate; }
        }
        /// <summary>
        /// 借方金额
        /// </summary>
        public decimal? debitAmount
        {
            set { _debitamount = value; }
            get { return _debitamount; }
        }
        /// <summary>
        /// 贷方金额
        /// </summary>
        public decimal? creditAmount
        {
            set { _creditamount = value; }
            get { return _creditamount; }
        }
        #endregion Model
    }
}
