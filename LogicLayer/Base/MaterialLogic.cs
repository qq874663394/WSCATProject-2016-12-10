using BaseLayer;
using BaseLayer.Base;
using HelperUtility;
using Model;
using System;
using System.Collections.Generic;
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
    }
}
