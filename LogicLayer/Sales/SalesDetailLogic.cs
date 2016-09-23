using BaseLayer;
using BaseLayer.Sales;
using HelperUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Sales
{
    public class SalesDetailLogic
    {
        private SalesDetailBase sdb = new SalesDetailBase();
        public DataTable GetList(int fieldName, string fieldValue)
        {
            string strWhere = "";
            DataTable dt = null;
            LogBase lb = new LogBase();
            log model = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_SalesMain",
                operationTime = DateTime.Now,
                objective = "查询采购单信息"
            };
            try
            {
                switch (fieldName)
                {
                    case 0:
                        strWhere += string.Format("salesMainCode='{0}'", fieldValue);
                        break;
                        //case 1:
                        //    strWhere += string.Format("materialDaima='{0}'", fieldValue);
                        //    break;
                        //case 2:
                        //    strWhere += string.Format("barcode='{0}'",fieldValue);
                        //    break;
                        //case 3:
                        //    strWhere += string.Format("materialName='{0}'",fieldValue);
                        //    break;
                }
                dt = sdb.GetList(strWhere);
                model.result = 1;
            }
            catch (Exception ex)
            {
                model.result = 0;
                throw ex;
            }
            finally
            {
                lb.Add(model);
            }
            return dt;
        }

        public DataTable GetDetailByMainCode(string SalesCode, int fieldName, string fieldValue)
        {
            string strWhere = "";
            DataTable dt = null;
            LogBase lb = new LogBase();
            log model = new log()
            {
                code = BuildCode.ModuleCode("log"),
                operationCode = "操作人code",
                operationName = "操作人名",
                operationTable = "T_SalesMain",
                operationTime = DateTime.Now,
                objective = "查询采购单数据",
                operationContent = "查询T_SalesDetail表的所有数据,条件:SalesCode=" + SalesCode
            };
            try
            {
                switch (fieldName)
                {
                    case 0:
                        strWhere += string.Format(" bm.name = '{0}'", fieldValue);
                        break;
                    case 1:
                        strWhere += string.Format("bm.barCode='{0}'", fieldValue);
                        break;
                    case 2:
                        strWhere += string.Format("bm.zhujima='{0}'", fieldValue);
                        break;
                    case 3:
                        strWhere += string.Format("bm.materialDaima='{0}'", fieldValue);
                        break;
                }
                sdb.GetDetailByMainCode(SalesCode, strWhere);
                model.result = 1;
            }
            catch (Exception ex)
            {
                model.result = 0;
                throw ex;
            }
            finally
            {
                lb.Add(model);
            }
            return dt;
        }
    }
}
