using Model;
using System;
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
            string sql = string.Format("SELECT * FROM T_BaseStorageRack where  isClear=1 and isEnable=1 and parentId='{0}' order by id", parentId);
            SqlDataAdapter dapter = new SqlDataAdapter(sql, DbHelperSQL.connectionString);
            DataSet ds = new DataSet();
            dapter.Fill(ds, "T_StorageRack");
            return ds.Tables[0];
        }
        public DataTable SelStorageRack()
        {
            string sql = string.Format("SELECT * FROM T_BaseStorageRack where isClear=1 and isEnable=1 order by id");
            SqlDataAdapter dapter = new SqlDataAdapter(sql, DbHelperSQL.connectionString);
            DataSet ds = new DataSet();
            dapter.Fill(ds, "T_StorageRack");
            return ds.Tables[0];
        }

        public int DelStorageRackByCode(string code)
        {
            string sql = string.Format("update from T_BaseStorageRack set isClear=0 where code='{1}'",code);
            return DbHelperSQL.ExecuteSql(sql);
        }
        public int UpdateStorageRackByCode(string code)
        {
            string sql = string.Format("update from T_BaseStorageRack set name='{0}' where code='{1}'",code);
            return DbHelperSQL.ExecuteSql(sql);
        }
        public int InsStorageRack(BaseStorageRack srb)
        {
            string sql = "insert into T_BaseStorageRack values(@code,@name,@parentId,@isEnable,@isClear,@updateDate)";
            SqlParameter[] sps =
            {
                new SqlParameter("@code",srb.code),
                new SqlParameter("@name",srb.name),
                new SqlParameter("@parentId",srb.parentId),
                new SqlParameter("@isEnable",srb.isEnable),
                new SqlParameter("@isClear",srb.isClear),
                new SqlParameter("@updateDate",srb.updateDate)
            };
            return DbHelperSQL.ExecuteSql(sql, sps);
        }
    }
}