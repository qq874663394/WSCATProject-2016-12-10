using BaseLayer;
using BaseLayer.Base;
using HelperUtility.Encrypt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Base
{
    public class StorageLogic
    {
        CodingHelper ch = new CodingHelper();
        StorageBase srb = new StorageBase();
        public DataTable SelStorage()
        {
            return ch.DataTableReCoding(srb.SelStorage());
        }
    }
}
