﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Base
{
    public class StorageRackBase
    {
        public DataTable SelStorageRackByCode(string parentId)
        {
            string sql = string.Format("SELECT * FROM T_BaseStorageRack where parentId='{0}' order by id",parentId);
            SqlDataAdapter dapter = new SqlDataAdapter(sql, DbHelperSQL.connectionString);
            DataSet ds = new DataSet();
            dapter.Fill(ds, "T_StorageRack");
            return ds.Tables[0];
        }
    }
}