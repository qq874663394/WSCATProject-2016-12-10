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
    public class SupplierLogic
    {
        SupplierBase sb = new SupplierBase();
        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns>所有数据以DataTable的形式返回</returns>
        public DataTable SelSupplierTable()
        {
            DataTable dt = null;
            LogBase lb = new LogBase();
            log logModel = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_BaseSupplier",
                operationTime = DateTime.Now,
                objective = "查询供应商信息",
                operationContent = "查询T_BaseSupplier表的数据成功"
            };
            try
            {
                dt = sb.SelSupplierTable();
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
        public DataTable GetList(int fieldName, string fieldValue)
        {
            string strWhere = "";
            DataTable dt = null;
            LogBase lb = new LogBase();
            log model = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_BaseSupplier",
                operationTime = DateTime.Now,
                objective = "查询员工信息",
                operationContent = "查询T_BaseSupplier表的所有数据,条件:" + strWhere
            };
            try
            {
                switch (fieldName)
                {
                    case 0:
                        strWhere += string.Format("name like '%{0}%'", fieldValue);
                        break;
                    case 1:
                        strWhere += string.Format("cityName like '%{0}%'", fieldValue);
                        break;
                    case 2:
                        strWhere += string.Format("isEnable = {0}", fieldValue);
                        break;
                    case 3:
                        strWhere += string.Format("isClear = {0}", fieldValue);
                        break;
                    default:
                        throw new Exception("-7");
                }
                dt = sb.GetList(strWhere);
                model.result = 1;
            }
            catch (Exception ex)
            {
                model.result = 0;
                throw ex;
            }
            finally
            {
                lb.Add(model);
            }
            return dt;
        }
        public DataTable GetPurchaseList(string code)
        {
            DataTable dt = null;
            LogBase lb = new LogBase();
            log model = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_BaseSupplier/T_PurchaseMain",
                operationTime = DateTime.Now,
                objective = "查询员工信息",
                operationContent = "根据T_BaseSupplier表的code查询T_PurchaseMain表的所有数据,条件:code=" + XYEEncoding.strHexDecode(code)
            };
            try
            {
                if (!string.IsNullOrWhiteSpace(code))
                {
                    dt = sb.GetPurchaseList(code);
                    model.result = 1;
                }
                else
                {
                    model.result = 0;
                    throw new Exception("-7");
                }
            }
            catch (Exception ex)
            {
                model.result = 0;
                throw ex;
            }
            finally
            {
                lb.Add(model);
            }
            return dt;
        }
    }
}
