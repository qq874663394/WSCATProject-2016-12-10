using BaseLayer;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.SuperGrid;
using HelperUtility.Encrypt;
using InterfaceLayer.Sales;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSCATProject.Warehouse
{
    public partial class TestVoidForm : Form
    {
        SalesOrderInterface soif = new SalesOrderInterface();
        public TestVoidForm()
        {
            InitializeComponent();
        }

        private void TestVoidForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Show(textBox1);
            //Dictionary<int, string> dc = new Dictionary<int, string>();
            //for (int i = 0; i < 20; i++)
            //{
            //    dc.Add(i,string.Format("{0}",i));
            //}
            //string[] str = new string[3];
            //str[1] = "123";
            //str[2] = "1";

            //Hashtable hashtable = new Hashtable();
            //hashtable.Add("2", "1");
            //hashtable.Add("3", 1);

            //ArrayList arrayList = new ArrayList();
            //arrayList.Add("a");
            //arrayList.Add(1);

            //List<string> list = new List<string>();
            //list.Add("a");
            //list.Add(1);
            //Console.WriteLine("123");
            //SqlCommand cmd = new SqlCommand("proc_FVEManagment", DbHelperSQL.Conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@code", "asdasd6");  //给输入参数赋值


            //SqlParameter parReturn = new SqlParameter("@return", SqlDbType.Int);
            //parReturn.Direction = ParameterDirection.ReturnValue;   //参数类型为ReturnValue                    
            //cmd.Parameters.Add(parReturn);

            //cmd.ExecuteNonQuery();

            //MessageBox.Show(parReturn.Value.ToString());
        }
    }
}