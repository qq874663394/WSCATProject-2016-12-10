﻿using LogicLayer.Warehouse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.Warehouse
{
    public class WarehouseMainInterface
    {
        WarehouseMainLogic wo = new WarehouseMainLogic();
        /// <summary>
        /// 减少库存
        /// </summary>
        /// <param name="number"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public int updateReduce(int number, string code)
        {
            return wo.updateReduce(number, code);
        }
        /// <summary>
        /// 增加库存
        /// </summary>
        /// <param name="number"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public int updateAug(int number, string code)
        {
            return wo.updateAug(number, code);
        }
        /// <summary>
        /// 自定义条件获取列表
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public DataTable GetMaterialDetail(string fieldValue)
        {
            return wo.GetMaterialDetail(fieldValue);
        }
    }
}
