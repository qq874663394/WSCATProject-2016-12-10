﻿using BaseLayer;
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
        public DataTable SelEmpolyeeTable(bool isflag)
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
                lb.Add(model);
            }
            catch (Exception ex)
            {
                model.result = 0;
                lb.Add(model);
                throw ex;
            }
            return dt;
        }
        public DataTable GetList(string strWhere)
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
                operationContent = "查询T_BaseEmpolyee表的所有数据,条件:" + strWhere
            };
            try
            {
                model.result = 1;
                dt = eb.GetList(strWhere);
            }
            catch (Exception ex)
            {
                model.result = 0;
                throw ex;
            }
            lb.Add(model);
            return dt;
        }
    }
}