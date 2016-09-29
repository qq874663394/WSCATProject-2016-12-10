﻿using LogicLayer.Warehouse;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Warehouse
{
    public class WarehouseAssemblyInterface
    {
        WarehouseAssemblyLogic _dal = new WarehouseAssemblyLogic();
        /// <summary>
        /// 复合查询
        /// </summary>
        /// <param name="fieldName">0:code,1:mainCode,2:materialDaima,3:materialName</param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public DataTable GetList(int fieldName, string fieldValue)
        {
            return _dal.GetList(fieldName, fieldValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="listModel"></param>
        /// <returns></returns>
        public object AddAndModify(WarehouseAssembly model, List<WarehouseAssemblyDetail> listModel)
        {
            return _dal.AddAndModify(model, listModel);
        }
    }
}
