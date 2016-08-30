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
        /// <summary>
        /// 根据父级ID查询
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public DataTable SelStorageRackByCode(string parentId)
        {
            string sql = "";
            try
            {
                sql = string.Format("SELECT * FROM T_BaseStorageRack where isClear=1 and isEnable=1 and parentId='{0}' order by id", parentId);
            }
            catch
            {
                throw new Exception("-1");
            }
            DataSet ds = null;
            try
            {
                SqlDataAdapter dapter = new SqlDataAdapter(sql, DbHelperSQL.connectionString);
                ds = new DataSet();
                dapter.Fill(ds, "T_StorageRack");
            }
            catch
            {
                throw new Exception("-2");
            }
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public DataTable SelStorageRack()
        {
            string sql = "";
            try
            {
                sql = string.Format("SELECT * FROM T_BaseStorageRack where isClear=1 and isEnable=1 order by id");
            }
            catch
            {
                throw new Exception("-1");
            }
            DataSet ds = null;
            try
            {
                SqlDataAdapter dapter = new SqlDataAdapter(sql, DbHelperSQL.connectionString);
                ds = new DataSet();
                dapter.Fill(ds, "T_StorageRack");
            }
            catch
            {
                throw new Exception("-2");
            }
            return ds.Tables[0];
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int DelStorageRackByCode(string code)
        {
            string sql = "";
            try
            {
                sql = string.Format("update from T_BaseStorageRack set isClear=0 where code='{1}'", code);
            }
            catch
            {
                throw new Exception("-1");
            }
            int result;
            try
            {
                result = DbHelperSQL.ExecuteSql(sql);
            }
            catch
            {
                throw new Exception("-2");
            }
            return result;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int UpdateStorageRackByCode(string code)
        {
            string sql = "";
            try
            {
                sql = string.Format("update from T_BaseStorageRack set name='{0}' where code='{1}'", code);
            }
            catch
            {
                throw new Exception("-1");
            }
            int result;
            try
            {
                result = DbHelperSQL.ExecuteSql(sql);
            }
            catch
            {
                throw new Exception("-2");
            }
            return result;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="srb"></param>
        /// <returns></returns>
        public int InsStorageRack(BaseStorageRack srb)
        {
            string sql = "";
            SqlParameter[] sps;
            try
            {
                sql = "insert into T_BaseStorageRack values(@code,@name,@parentId,@isEnable,@isClear,@updateDate)";
            }
            catch
            {
                throw new Exception("-1");
            }
            try
            {
                sps = new SqlParameter[]
                    {
                        new SqlParameter("@code", srb.code),
                        new SqlParameter("@name", srb.name),
                        new SqlParameter("@parentId", srb.parentId),
                        new SqlParameter("@isEnable", srb.isEnable),
                        new SqlParameter("@isClear", srb.isClear),
                        new SqlParameter("@updateDate", srb.updateDate)
                    };
            }
            catch
            {
                throw new Exception("-2");
            }
            return DbHelperSQL.ExecuteSql(sql, sps);
        }
    }
}