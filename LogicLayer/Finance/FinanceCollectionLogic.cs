using BaseLayer.Finance;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Finance
{
    public class FinanceCollectionLogic
    {
        FinanceCollectionBase _dal = new FinanceCollectionBase();
        public object AddToMainOrDetail(FinanceCollection model, List<FinanceCollectionDetail> modelDetail)
        {
            return _dal.AddToMainOrDetail(model, modelDetail);
        }
        public object UpdateToMainOrDetail(FinanceCollection model, List<FinanceCollectionDetail> modelDetail)
        {
            return _dal.UpdateToMainOrDetail(model, modelDetail);
        }
    }
}
