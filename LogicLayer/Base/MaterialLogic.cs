﻿using BaseLayer;
using BaseLayer.Base;
using HelperUtility;
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
    public class MaterialLogic
    {
        MaterialBase _dal = new MaterialBase();
        LogBase _logDal = new LogBase();
        BaseUpdataManager baseUpdate = new BaseUpdataManager();
        public int SetMaterialNumber(string materialCode, string price)
        {
            int result = 0;
            Log model = new Log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_BaseMaterial",
                operationTime = DateTime.Now,
                objective = "修改物料信息",
                operationContent = "修改T_BastMaiterial表的数据,条件:materialCode=" + materialCode
            };
            try
            {
                if ((materialCode == null || materialCode == "") && string.IsNullOrWhiteSpace(price))
                {
                    throw new Exception("-2");
                }
                result = _dal.SetMaterialNumber(materialCode, price);
                if (result <= 0)
                {
                    throw new Exception("-3");
                }
                model.result = 1;
                baseUpdate.add(materialCode, model.operationTable, result, "", model.operationTime);
            }
            catch (Exception ex)
            {
                model.result = 0;
                throw ex;
            }
            finally
            {
                _logDal.Add(model);
            }
            return result;
        }
        /// <summary>
        /// 复合查询
        /// </summary>
        /// <param name="fieldName">012模糊查询0:materialDaima,1:name,2:zhujima,3:code</param>
        /// <param name="fieldValue">条件值</param>
        /// <returns></returns>
        public DataTable GetList(int fieldName, string fieldValue)
        {
            string strWhere = "";
            DataTable dt = null;
            LogBase lb = new LogBase();
            Log model = new Log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_BaseMaterial",
                operationTime = DateTime.Now,
                objective = "查询客户信息"
            };
            try
            {
                switch (fieldName)
                {
                    case 0:
                        strWhere += string.Format("materialDaima like '%{0}%'", fieldValue);
                        break;
                    case 1:
                        strWhere += string.Format("name like '%{0}%'", fieldValue);
                        break;
                    case 2:
                        strWhere += string.Format("zhujima like '%{0}%'", fieldValue);
                        break;
                    case 3:
                        strWhere += string.Format("code = '{0}'", fieldValue);
                        break;
                }
                model.operationContent = "查询T_BaseMaterial表的所有数据,条件:" + strWhere;
                dt = _dal.GetList(strWhere);
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
