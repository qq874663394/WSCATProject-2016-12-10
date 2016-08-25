using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public partial class Log
    {
        private int _id;
        private string _code;
        private string _operationCode;
        private string _operationName;
        private string _operationTable;
        private DateTime _operationTime;
        private int _result;
        private string _objective;
        private string operationContent;

        public int Id { get; set; }
        public string Code { get; set; }
        public string OperationCode { get; set; }
        public string OperationName { get; set; }
        public string OperationTable { get; set; }
        public DateTime OperationTime { get; set; }
        public int Result { get; set; }
        public string Objective { get; set; }
        public string OperationContent { get; set; }
    }
}
