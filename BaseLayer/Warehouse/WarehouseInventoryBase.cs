using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLayer.Warehouse
{
    public class WarehouseInventoryBase
    {
        /// <summary>
        /// 获取仓库列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetList()
        {
            string sql = "";
            DataSet ds = null;
            try
            {
                sql = "select code,name from T_BaseStorage";
                ds = DbHelperSQL.Query(sql);
            }
            catch
            {
                throw new Exception("-1");
            }
            return ds.Tables[0];
        }

        public DataTable GetTbList(int num, string code)
        {
            string sql = "";
            DataSet ds = null;
            try
            {
                if (code != null || code != " ")
                {
                    switch (num)
                    {
                        // 1 查全部
                        case 1:
                            sql = "select mains.name, mat.code,mat.name as shopname,mat.model,mat.unit,mains.allNumber,mingx.remark,mat.barCode from T_WarehouseMain mains,T_BaseMaterial mat,T_WarehouseInventoryDetail mingx where mains.materialCode=mat.code and mingx.materialCode=mat.code and mains.materialCode = mingx.materialCode ";
                            break;
                        case 2:
                            sql = "select mains.name, mat.code,mat.name as shopname,mat.model,mat.unit,mains.allNumber,mingx.remark,mat.barCode from T_WarehouseMain mains,T_BaseMaterial mat,T_WarehouseInventoryDetail mingx where mains.storageCode='" + code + "' and  mains.materialCode=mat.code and mingx.materialCode=mat.code and mains.materialCode = mingx.materialCode";
                            break;

                    }
                }
                else
                {
                    sql = "select mains.name, mat.code,mat.name as shopname,mat.model,mat.unit,mains.allNumber,mingx.remark,mat.barCode from T_WarehouseMain mains,T_BaseMaterial mat,T_WarehouseInventoryDetail mingx where mains.storageCode=" + code + "and  mains.materialCode=mat.code and mingx.materialCode=mat.code and mains.materialCode = mingx.materialCode";
                }
                ds = DbHelperSQL.Query(sql);
            }
            catch
            {
                throw new Exception("-1");
            }
            return ds.Tables[0];
        }
    }
}
