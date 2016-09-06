using BaseLayer;
using InterfaceLayer.Base;
using InterfaceLayer.Warehouse;
using LogicLayer;
using LogicLayer.Base;
using LogicLayer.Warehouse;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSCATProject.Warehouse
{
    public partial class TestVoidForm : Form
    {
        public TestVoidForm()
        {
            InitializeComponent();
        }

        private void TestVoidForm_Load(object sender, EventArgs e)
        {

        }
        public string ExSwitch(string warehouseInResult)
        {
            switch (warehouseInResult)
            {
                case "-1":
                    return "错误代码:4001;拼接连接字符串时出现异常,请尝试重新插入数据.";
                case "-2":
                    return "错误代码:4002;建立查询字符串参数时出现异常";
                case "-3":
                    return "错误代码:4003;对参数赋值时出现异常,请检查输入";
                case "-4":
                    return "错误代码:4004;尝试打开数据库连接时出错,请检查服务器连接";
                case "-5":
                    return "错误代码:4005;对数据库新增数据时未能增加任何数据";
                case "-6":
                    return "错误代码:4006;对数据库新增数据的方法失效,未能增加任何行";
                case "-7":
                    return "错误代码:4007;检查到传入的参数为空,无法进行新增操作";
                default:
                    return "未知错误";
            }
        }
        public void main()
        {
            MessageBox.Show(Void1().ToString());
        }
        public int Void1()
        {
            try
            {
                return 1;
            }
            catch (Exception)
            {

                throw new Exception("-1");
            }
        }
        public int Void2()
        {
            try
            {
                return 000;
            }
            catch (Exception)
            {
                throw new Exception("-2");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region 查询
            ////ClientInterface ba = new ClientInterface();
            ////try
            ////{
            ////    DataTable dt = ba.SelClient(false);
            ////}
            ////catch (Exception ex)
            ////{
            ////    MessageBox.Show(ExSwitch(ex.Message));
            ////}
            #endregion

            #region 新增
            ////多条数据的
            //string sqlToList = "insert into T_BaseArea(code,name,parentId,isEnable) values(@code, @name, @parentId,@isEnable)";
            ////多条数据的值  要添加到list里   就像普通的参数集  不过最后添加到list里了
            //List<SqlParameter[]> list = new List<SqlParameter[]>();//存储参数集合的list
            //SqlParameter[] sps;  //定义一个参数集合
            //sps = new SqlParameter[]
            //{
            //                new SqlParameter("@code","duotiao"),
            //                new SqlParameter("@name","duotiao"),
            //                new SqlParameter("@parentId","duotiao"),
            //                new SqlParameter("@isEnable",1)
            //};
            //list.Add(sps);

            //sps = new SqlParameter[]
            //{
            //                new SqlParameter("@code","duotiao"),
            //                new SqlParameter("@name","duotiao"),
            //                new SqlParameter("@parentId","duotiao"),
            //                new SqlParameter("@isEnable",2)
            //};
            //list.Add(sps);

            //sps = new SqlParameter[]
            //{
            //                new SqlParameter("@code","duotiao"),
            //                new SqlParameter("@name","duotiao"),
            //                new SqlParameter("@parentId","duotiao"),
            //                new SqlParameter("@isEnable",3)
            //};
            //list.Add(sps);

            //sps = new SqlParameter[]
            //{
            //                new SqlParameter("@code","duotiao"),
            //                new SqlParameter("@name","duotiao"),
            //                new SqlParameter("@parentId","duotiao"),
            //                new SqlParameter("@isEnable",4)
            //};
            //list.Add(sps);

            //sps = new SqlParameter[]
            //{
            //                new SqlParameter("@code","duotiao"),
            //                new SqlParameter("@name","duotiao"),
            //                new SqlParameter("@parentId","duotiao"),
            //                new SqlParameter("@isEnable",5)
            //};
            //list.Add(sps);

            //Hashtable htKey = new Hashtable();  //参数要求

            //string sql = @"insert into T_BaseArea(code,name,parentId) 
            //values(@code,@name,@parentId)";  //主表的
            //SqlParameter[] parameters = //主表参数
            //{
            //                new SqlParameter("@code","duotiao"),
            //                new SqlParameter("@name","duotiao"),
            //                new SqlParameter("@parentId","duotiao")
            //            };


            //htKey.Add(sql, parameters);//sql语句和主表的参数集合


            //try
            //{
            //    DbHelperSQL.ExecuteSqlTran(htKey, sqlToList, list);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ExSwitch(ex.Message));
            //}
            #endregion

            WarehouseInInterface wi = new WarehouseInInterface();
            WarehouseOutInterface wo = new WarehouseOutInterface();
            DataTable widt = wi.GetListToIn(0).Tables[0];
            DataTable wodt = wo.GetListToOut(0).Tables[0];
            
            wodt.Merge(widt);
        }
    }
}