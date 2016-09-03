using BaseLayer;
using BaseLayer.Base;
using HelperUtility;
using HelperUtility.Encrypt;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Base
{
    public class StorageRackLogic
    {
        StorageRackBase sr = new StorageRackBase();

        /// <summary>
        /// 根据父级ID查询
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public DataTable SelStorageRackByCode(string parentId)
        {
            DataTable dt = null;
            try
            {
                dt = sr.SelStorageRackByCode(parentId);
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_StorageRack",
                    operationTime = DateTime.Now,
                    objective = "查询货架信息",
                    result = 1,
                    operationContent = "查询T_StorageRack表的数据成功,条件为:ParentId=" + XYEEncoding.strHexDecode(parentId)
                };
                lb.Add(log);
            }
            catch (Exception ex)
            {
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_StorageRack",
                    operationTime = DateTime.Now,
                    objective = "查询货架信息",
                    result = -1,
                    operationContent = "查询T_StorageRack表的数据失败,条件为:ParentId=" + XYEEncoding.strHexDecode(parentId)
                };
                lb.Add(log);
                throw ex;
            }
            return dt;
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public DataTable SelStorageRack()
        {
            DataTable dt = null;
            try
            {
                dt = sr.SelStorageRack();
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_StorageRack",
                    operationTime = DateTime.Now,
                    objective = "查询货架信息",
                    result = 1,
                    operationContent = "查询T_StorageRack表的数据成功"
                };
                lb.Add(log);
            }
            catch (Exception ex)
            {
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_StorageRack",
                    operationTime = DateTime.Now,
                    objective = "查询货架信息",
                    result = -1,
                    operationContent = "查询T_StorageRack表的数据失败"
                };
                lb.Add(log);
                throw ex;
            }
            return dt;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int DelStorageRackByCode(string code)
        {
            int result = 0;
            try
            {
                result = sr.DelStorageRackByCode(code);
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_StorageRack",
                    operationTime = DateTime.Now,
                    objective = "查询货架信息",
                    result = 1,
                    operationContent = "删除T_StorageRack表的数据成功,条件为:code=" + XYEEncoding.strHexDecode(code)
                };
                lb.Add(log);
            }
            catch (Exception ex)
            {
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_StorageRack",
                    operationTime = DateTime.Now,
                    objective = "删除货架信息",
                    result = -1,
                    operationContent = "删除T_StorageRack表的数据失败,条件为:code=" + XYEEncoding.strHexDecode(code)
                };
                lb.Add(log);
                throw ex;
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
            int result = 0;
            try
            {
                result = sr.UpdateStorageRackByCode(fieldName, fieldValue,code);
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_StorageRack",
                    operationTime = DateTime.Now,
                    objective = "修改货架信息",
                    result = 1,
                    operationContent = "修改T_StorageRack表的数据成功,条件为:code=" + XYEEncoding.strHexDecode(code)
                };
                lb.Add(log);
            }
            catch (Exception ex)
            {
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_StorageRack",
                    operationTime = DateTime.Now,
                    objective = "修改货架信息",
                    result = -1,
                    operationContent = "修改T_StorageRack表的数据成功,条件为:code=" + XYEEncoding.strHexDecode(code)
                };
                lb.Add(log);
                throw ex;
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
            int result = 0;
            try
            {
                result = sr.InsStorageRack(srb);
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_StorageRack",
                    operationTime = DateTime.Now,
                    objective = "新增货架信息",
                    result = 1,
                    operationContent = "新增T_StorageRack表的数据成功,条件为:code=" + XYEEncoding.strHexDecode(srb.code)
                };
                lb.Add(log);
            }
            catch (Exception ex)
            {
                LogBase lb = new LogBase();
                log log = new log()
                {
                    code = BuildCode.ModuleCode("log"),
                    operationCode = "操作人code",
                    operationName = "操作人名",
                    operationTable = "T_StorageRack",
                    operationTime = DateTime.Now,
                    objective = "新增货架信息",
                    result = -1,
                    operationContent = "新增T_StorageRack表的数据失败,条件为:code=" + XYEEncoding.strHexDecode(srb.code)
                };
                lb.Add(log);
                throw ex;
            }
            return result;
        }
    }
}