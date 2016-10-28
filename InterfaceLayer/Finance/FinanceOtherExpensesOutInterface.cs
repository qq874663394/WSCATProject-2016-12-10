using LogicLayer.Finance;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Finance
{
    public class FinanceOtherExpensesOutInterface
    {
        FinanceOtherExpensesOutLogic _dal = new FinanceOtherExpensesOutLogic();
        public object AddOrUpdateToMainOrDetail(FinanceOtherExpensesOut model, List<FinanceOtherExpensesOutDetail> modelDetail)
        {
            return _dal.AddOrUpdateToMainOrDetail(model, modelDetail);
        }
        public bool Exists(string code)
        {
            return _dal.Exists(code);
        }
    }
}
