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

namespace LogicLayer.Finance
{
    public class FinanceVoucherEntryLogic
    {
        /// <summary>
        /// 上一单
        /// </summary>
        /// <param name="clientCode">客户code</param>
        /// <returns></returns>
        public DataTable GetLastDetail(string code)
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
                dt = _dal.GetLastDetail(code);
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
    }
}
