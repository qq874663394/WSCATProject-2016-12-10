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
using UpdateManagerLayer;

namespace LogicLayer.Base
{
    public class StorageRackLogic
    {
        StorageRackBase sr = new StorageRackBase();
        BaseUpdataManager bum = new BaseUpdataManager();
        /// <summary>
        /// 根据父级ID查询
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public DataTable SelStorageRackByCode(string parentId)
        {
            DataTable dt = null;
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_StorageRack",
                operationTime = DateTime.Now,
                objective = "查询货架信息",
                operationContent = "查询T_StorageRack表的数据,条件为:ParentId=" + parentId
            };
            if (!string.IsNullOrWhiteSpace(parentId))
            {
                try
                {
                    dt = sr.SelStorageRackByCode(parentId);
                    logModel.result = 1;
                }
                catch (Exception ex)
                {
                    logModel.result = 0;
                    throw ex;
                }
            }
            else
            {
                logModel.result = 0;
                throw new Exception("-7");
            }
            lb.Add(logModel);
            return dt;
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public DataTable SelStorageRack()
        {
            DataTable dt = null;
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_StorageRack",
                operationTime = DateTime.Now,
                objective = "查询货架信息",
                operationContent = "查询T_StorageRack表的数据"
            };
            try
            {
                dt = sr.SelStorageRack();
                logModel.result = 1;
            }
            catch (Exception ex)
            {
                logModel.result = 0;
                throw ex;
            }
            lb.Add(logModel);
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
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_StorageRack",
                operationTime = DateTime.Now,
                objective = "删除货架信息",
                operationContent = "删除T_StorageRack表的数据,条件为:code=" + code
            };
            if (!string.IsNullOrWhiteSpace(code))
            {
                try
                {
                    result = sr.DelStorageRackByCode(code);
                    if (result > 0)
                    {
                        logModel.result = 1;
                        bum.add(code, logModel.operationTable, result, logModel.operationContent, logModel.operationTime);
                    }
                    else
                    {
                        logModel.result = 0;
                    }
                }
                catch (Exception ex)
                {
                    logModel.result = 0;
                    throw ex;
                }
            }
            else
            {
                logModel.result = 0;
                throw new Exception("-7");
            }
            lb.Add(logModel);
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
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_StorageRack",
                operationTime = DateTime.Now,
                objective = "修改货架信息",
                operationContent = string.Format("修改T_StorageRack表的数据,条件为:fieldName={1},fieldValue={2},code={3}", fieldName, fieldValue, code)
            };
            if (!string.IsNullOrWhiteSpace(fieldName) && !string.IsNullOrWhiteSpace(fieldValue) && !string.IsNullOrWhiteSpace(code))
            {
                try
                {
                    result = sr.UpdateStorageRackByCode(fieldName, fieldValue, code);
                    if (result > 0)
                    {
                        logModel.result = 1;
                        bum.add(code, logModel.operationTable, result, logModel.operationContent, logModel.operationTime);
                    }
                    else
                    {
                        logModel.result = 1;
                    }
                }
                catch (Exception ex)
                {
                    logModel.result = 0;
                    throw ex;
                }
            }
            else
            {
                logModel.result = 0;
                throw new Exception("-7");
            }
            lb.Add(logModel);
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
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_StorageRack",
                operationTime = DateTime.Now,
                objective = "新增货架信息",
                operationContent = "新增T_StorageRack表的数据,条件为:code=" + srb.code
            };
            if (!string.IsNullOrWhiteSpace(srb.code) && srb != null)
            {
                try
                {
                    result = sr.InsStorageRack(srb);
                    if (result > 0)
                    {
                        logModel.result = 1;
                        bum.add(srb.code, logModel.operationTable, result, logModel.operationContent, logModel.operationTime);
                    }
                    else
                    {
                        logModel.result = 0;
                    }
                }
                catch (Exception ex)
                {
                    logModel.result = 0;
                    throw ex;
                }
            }
            else
            {
                logModel.result = 0;
                throw new Exception("-7");
            }
            lb.Add(logModel);
            return result;
        }
    }
}