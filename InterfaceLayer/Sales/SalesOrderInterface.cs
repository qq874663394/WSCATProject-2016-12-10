﻿using LogicLayer.Sales;
using Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Sales
{
    public class SalesOrderInterface
    {
        SalesOrderLogic _dal = new SalesOrderLogic();
        /// <summary>
        /// 事务操作
        /// </summary>
        /// <returns>返回结果,不等于null成功</returns>
        public object AddOrUpdate(SalesOrder model, List<SalesOrderDetail> modelDetail)
        {
            return _dal.AddOrUpdate(model, modelDetail);
        }
    }
}
