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
            DataSet ds = null;
            try
            {
                sql = string.Format("SELECT * FROM T_BaseStorageRack where isClear=1 and parentId='{0}' order by id", parentId);
                ds = DbHelperSQL.Query(sql);
            }
            catch
            {
                throw new Exception("-1");
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
            DataSet ds = null;
            try
            {
                sql = string.Format("SELECT * FROM T_BaseStorageRack where isClear=1 order by id");
                ds = DbHelperSQL.Query(sql);
            }
            catch
            {
                throw new Exception("-1");
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
            int result = 0;
            try
            {
                sql = string.Format("update from T_BaseStorageRack set isClear=0 where code='{1}'", code);
                result = DbHelperSQL.ExecuteSql(sql);
            }
            catch
            {
                throw new Exception("-1");
            }
            return result;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int UpdateStorageRackByCode(string fieldName, string fieldValue, string code)
        {
            string sql = "";
            int result = 0;
            try
            {
                sql = string.Format("update from T_BaseStorageRack set {0}='{1}' where code='{2}'", fieldName, fieldValue, code);
                result = DbHelperSQL.ExecuteSql(sql);
            }
            catch
            {
                throw new Exception("-1");
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
            int result = 0;
            SqlParameter[] sps;
            try
            {
                sql = "insert into T_BaseStorageRack values(@code,@name,@parentId,@isEnable,@isClear,@updateDate)";
                sps = new SqlParameter[]
                {
                    new SqlParameter("@code", srb.code),
                    new SqlParameter("@name", srb.name),
                    new SqlParameter("@parentId", srb.parentId),
                    new SqlParameter("@isEnable", srb.isEnable),
                    new SqlParameter("@isClear", srb.isClear),
                    new SqlParameter("@updateDate", srb.updateDate)
                };
                result=DbHelperSQL.ExecuteSql(sql, sps);
            }
            catch
            {
                throw new Exception("-1");
            }
            return result;
        }
    }
}