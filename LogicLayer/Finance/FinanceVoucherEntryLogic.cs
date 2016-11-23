using BaseLayer;
using BaseLayer.Base;
using HelperUtility;
using Model;
using Model.Finance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateManagerLayer;

namespace LogicLayer.Finance
{
    public class FinanceVoucherEntryLogic
    {
        private readonly FinanceVoucherEntryBase _dal = new FinanceVoucherEntryBase();
        private readonly FinanceUpdataManager _updateDal = new FinanceUpdataManager();
        private readonly LogBase _logDal = new LogBase();
        private Log logModel = new Log()
        {
            code = BuildCode.ModuleCode("log"),
            operationCode = "操作人code",
            operationName = "操作人名"
            //,
            //objective = "",
            //operationContent = "",
            //operationTable = "",
            //operationTime = "",
            //result = 0;
        };
        /// <summary>
        /// 上一单
        /// </summary>
        /// <param name="clientCode">客户code</param>
        /// <returns></returns>
        public DataTable GetLastDetail(string code)
        {
            logModel.objective = "上一单数据查询";
            logModel.operationContent = "";
            logModel.operationTable = "T_FinanceVoucherEntry";
            logModel.operationTime = DateTime.Now;
            logModel.result = 0;
            DataTable dt = null;
            try
            {
                dt = _dal.GetLastDetail(code);
                if (dt.Rows.Count<=0)
                {
                    //失败
                    throw new Exception("-3");
                }
                else
                {
                    //成功
                    logModel.result = 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _logDal.Add(logModel);
            }
            return dt;
        }

        /// <summary>
        /// 默认的上一单
        /// </summary>
        /// <param name="clientCode">客户code</param>
        /// <returns></returns>
        public DataTable GetFLastDetail(string code)
        {
            FinanceVoucherEntryBase _dal = new FinanceVoucherEntryBase();
            FinanceVoucherEntryMain main = new FinanceVoucherEntryMain()
            {
                code = BuildCode.ModuleCode("financeVoucherEntry"),
                date = DateTime.Now,
                examine = "审核人",
                makeMan = "制单人",
                posting = "过账人"
            };
            FinanceVoucherEntryDetail detail = new Model.Finance.FinanceVoucherEntryDetail()
            {
                code = BuildCode.ModuleCode("financeVoucherEntry"),
                mainCode = main.code,
                creditAmount = 0,
                debitAmount = 0,
                subject = "科目",
                summary = "摘要"
            };
            DataTable dt = null;
            try
            {
                dt = _dal.GetFLastDetail(code);
                //main.examineState = 
            }
            catch (Exception ex)
            {
                //model.result = 0;
                throw ex;
            }
            finally
            {
                //lb.Add(model);
            }
            return dt;
        }

        /// <summary>
        /// 下一单
        /// </summary>
        /// <param name="clientCode">客户code</param>
        /// <returns></returns>
        public DataTable GetNextDetail(string code)
        {
            FinanceVoucherEntryBase _dal = new FinanceVoucherEntryBase();
            FinanceVoucherEntryMain main = new FinanceVoucherEntryMain()
            {
                code = BuildCode.ModuleCode("financeVoucherEntry"),
                date = DateTime.Now,
                examine = "审核人",
                makeMan = "制单人",
                posting = "过账人"
            };
            FinanceVoucherEntryDetail detail = new Model.Finance.FinanceVoucherEntryDetail()
            {
                code = BuildCode.ModuleCode("financeVoucherEntry"),
                mainCode = main.code,
                creditAmount = 0,
                debitAmount = 0,
                subject = "科目",
                summary = "摘要"
            };
            DataTable dt = null;
            try
            {
                dt = _dal.GetNextDetail(code);
                //main.examineState = 
            }
            catch (Exception ex)
            {
                //model.result = 0;
                throw ex;
            }
            finally
            {
                //lb.Add(model);
            }
            return dt;
        }

        /// <summary>
        /// 获取最新的code
        /// </summary>
        /// <returns></returns>
        public string GetNewCode()
        {
            FinanceVoucherEntryBase _dal = new FinanceVoucherEntryBase();
            return _dal.GetNewCode();
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelDetail"></param>
        /// <returns></returns>
        public object AddToMainOrDetail(FinanceVoucherEntryMain model, List<FinanceVoucherEntryDetail> modelDetail)
        {
            object result = null;
            logModel.operationTime = DateTime.Now;
            logModel.operationTable = "T_FinanceVoucherEntryMain/T_FinanceVoucherEntryDetail";
            logModel.objective = "凭证录入";
            logModel.operationContent = "操作内容";
            logModel.result = 0;
            try
            {
                if (_dal.Exists(model.code))
                {
                    //存在,则修改
                    result = _dal.AddToMainOrDetail(model, modelDetail);
                    if (result == null)
                    {
                        //失败
                    }
                    else
                    {
                        //成功
                        logModel.result = 1;//操作结果
                        _updateDal.add(model.code, logModel.operationTable,
                            modelDetail.Count + 1, "修改数据", logModel.operationTime);
                    }
                }
                else
                {
                    result = _dal.AddToMainOrDetail(model, modelDetail);
                    if (result == null)
                    {
                        //失败
                    }
                    else
                    {
                        //成功
                        logModel.result = 1;//操作结果
                        _updateDal.add(model.code, logModel.operationTable,
                            modelDetail.Count + 1, "新增数据", logModel.operationTime);
                    }
                }
                return logModel.result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _logDal.Add(logModel);
            }
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelDetail"></param>
        /// <returns></returns>
        //public object AddToMainOrDetail(FinanceVoucherEntryMain model, List<FinanceVoucherEntryDetail> modelDetail)
        //{
        //    object result = null;
        //    logModel.operationTime = DateTime.Now;
        //    logModel.operationTable = "T_FinanceVoucherEntryMain/T_FinanceVoucherEntryDetail";
        //    logModel.objective = "凭证录入";
        //    logModel.operationContent = "操作内容";
        //    logModel.result = 0;
        //    try
        //    {
        //        if (_dal.Exists(model.code))
        //        {
        //            //存在,则修改
        //            result = _dal.AddToMainOrDetail(model, modelDetail);
        //            if (result == null)
        //            {
        //                //失败
        //            }
        //            else
        //            {
        //                //成功
        //                logModel.result = 1;//操作结果
        //                _updateDal.add(model.code, logModel.operationTable,
        //                    modelDetail.Count + 1, "修改数据", logModel.operationTime);
        //            }
        //        }
        //        else
        //        {
        //            result = _dal.AddToMainOrDetail(model, modelDetail);
        //            if (result == null)
        //            {
        //                //失败
        //            }
        //            else
        //            {
        //                //成功
        //                logModel.result = 1;//操作结果
        //                _updateDal.add(model.code, logModel.operationTable,
        //                    modelDetail.Count + 1, "新增数据", logModel.operationTime);
        //            }
        //        }
        //        return logModel.result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        _logDal.Add(logModel);
        //    }
        //}
    }
}