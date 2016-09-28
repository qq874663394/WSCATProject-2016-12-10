using LogicLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Base
{
    public class MaterialInterface
    {
        MaterialLogic _dal = new MaterialLogic();
        public int SetMaterialNumber(string materialCode, string price)
        {
            return _dal.SetMaterialNumber(materialCode, price);
        }
    }
}
