using BaseLayer;
using BaseLayer.Base;
using BaseLayer.Warehouse;
using HelperUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Warehouse
{
    public class WarehouseMainLogic
    {
        WarehouseMainBase wo = new WarehouseMainBase();
        /// <summary>
        /// 减少库存
        /// </summary>
        /// <param name="number"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public int updateReduce(int number, string code)
        {
            int result = 0;
            LogBase lb = new LogBase();
            Log logModel = new Log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseMain",
                operationTime = DateTime.Now,
                objective = "减少库存",
                operationContent = "修改T_WarehouseMain表的数据,number为" + number + "，code为:" + code
            };
            try
            {
                if (string.IsNullOrWhiteSpace(code))
                {
                    throw new Exception("2");
                }
                result = wo.updateReduce(number, code);
                if (result <= 0)
                {
                    throw new Exception("-3");
                }
            }
            catch (Exception ex)
            {
                logModel.result = 0;
                throw ex;
            }
            finally
            {
                lb.Add(logModel);
            }
            return result;
        }
        /// <summary>
        /// 增加库存
        /// </summary>
        /// <param name="number"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public int updateAug(int number, string code)
        {
            int result = 0;
            LogBase lb = new LogBase();
            Log logModel = new Log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_WarehouseMain",
                operationTime = DateTime.Now,
                objective = "增加库存",
                operationContent = "修改T_WarehouseMain表的数据,number为" + number + "，code为:" + code
            };
            try
            {
                if (string.IsNullOrWhiteSpace(code))
                {
                    throw new Exception("2");
                }
                result = wo.updateAug(number, code);
                if (result <= 0)
                {
                    throw new Exception("-3");
                }
                logModel.result = 1;
            }
            catch (Exception ex)
            {
                logModel.result = 0;
                throw ex;
            }
            finally
            {
                lb.Add(logModel);
            }
            return result;
        }
        /// <summary>
        /// 自定义条件获取列表
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public DataTable GetMaterialDetail(string fieldValue)
        {
            DataTable dt = null;

            LogBase lb = new LogBase();
            Log logModel = new Log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_BaseStorage",
                operationTime = DateTime.Now,
                objective = "商品盘点报告表",
                operationContent = "查询T_BaseMaterial、T_WarehouseMain、T_WarehouseDetail表的数据,条件:storageCode=" + fieldValue
            };
            try
            {
                dt = wo.GetMaterialDetail(fieldValue);
                logModel.result = 1;
            }
            catch (Exception ex)
            {
                logModel.result = 0;
                throw ex;

            }
            finally
            {
                lb.Add(logModel);
            }
            return dt;
        }

        /// <summary>
        /// 根据查出来的仓库code查库存表，再根据库存表的商品code查物料信息表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable GetMaterialByMain(string code)
        {
            DataTable dt = null;
            LogBase lb = new LogBase();
            Log logModel = new Log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_BaseMaterial/T_WarehouseMain",
                operationTime = DateTime.Now,
                objective = "查询盘点表中List数据",
                operationContent = "查询T_BaseMaterial、T_WarehouseMain表的数据,条件:code=" + code
            };
            try
            {
                if (string.IsNullOrWhiteSpace(code))
                {
                    throw new Exception("-2");
                }
                dt = wo.GetMaterialByMain(code);
                logModel.result = 1;
            }
            catch (Exception ex)
            {
                logModel.result = 0;
                throw ex;
            }
            finally
            {
                lb.Add(logModel);
            }
            return dt;
        }
        public DataTable GetWMainAndMaterialByWMCode(int fieldName, string fieldValue,string storageCode)
        {
            string strWhere = "";
            DataTable dt = null;
            LogBase lb = new LogBase();
            Log logModel = new Log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_BaseStorage",
                operationTime = DateTime.Now,
                objective = "查询盘点表中List数据"
            };
            try
            {
                switch (fieldName)
                {
                    case 0:
                        strWhere += string.Format("materialDaima like '%{0}%'",fieldValue);
                        break;
                    case 1:
                        strWhere += string.Format("name like '%{0}%'",fieldValue);
                        break;
                    case 2:
                        strWhere += string.Format("barCode like '%{0}%'",fieldValue);
                        break;
                    case 3:
                        strWhere += string.Format("zhujima like '%{0}%'",fieldValue);
                        break;
                }
                dt = wo.GetWMainAndMaterialByWMCode(strWhere, storageCode);
                logModel.operationContent = "查询T_BaseMaterial、T_WarehouseMain表的数据,条件:where=" + strWhere;
                logModel.result = 1;
            }
            catch (Exception ex)
            {
                logModel.result = 0;
                throw ex;
            }
            finally
            {
                lb.Add(logModel);
            }
            return dt;
        }
    }
}
