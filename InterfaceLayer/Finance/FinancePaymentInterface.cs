using LogicLayer.Finance;
using Model.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Finance
{
    public class FinancePaymentInterface
    {
        FinancePaymentLogic _dal = new FinancePaymentLogic();
        public object AddOrUpdateToMainOrDetail(FinancePayment model, List<FinancePaymentDetail> modelDetail)
        {
            return _dal.AddOrUpdateToMainOrDetail(model, modelDetail);
        }
        public bool Exists(string code)
        {
            return _dal.Exists(code);
        }
    }
}
