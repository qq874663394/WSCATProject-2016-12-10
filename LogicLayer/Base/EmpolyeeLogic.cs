using BaseLayer;
using BaseLayer.Base;
using HelperUtility;
using Model;
using System;
using System.Data;

namespace LogicLayer.Base
{
    public class EmpolyeeLogic
    {
        EmpolyeeBase eb = new EmpolyeeBase();
        public DataTable GetEmpByBool(bool isflag)
        {
            DataTable dt = null;
            LogBase lb = new LogBase();
            log model = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_BaseEmpolyee",
                operationTime = DateTime.Now,
                objective = "查询员工信息",
                operationContent = "查询T_BaseEmpolyee表的所有数据,显示全部:" + isflag.ToString()
            };
            try
            {
                dt = eb.SelEmpolyee(isflag);
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
                operationTable = "T_BaseEmpolyee",
                operationTime = DateTime.Now,
                objective = "查询员工信息"
            };
            try
            {
                if (string.IsNullOrWhiteSpace(fieldValue))
                {
                    throw new Exception("-2");
                }
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
                    case 4:
                        strWhere += string.Format("roleCode = '{0}'", fieldValue);
                        break;
                    case 5:
                        strWhere += string.Format("passWord = '{0}'", fieldValue);
                        break;
                }
                model.operationContent = "查询T_BaseEmpolyee表的所有数据,条件:" + strWhere;
                dt = eb.GetList(strWhere);
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
    }
}