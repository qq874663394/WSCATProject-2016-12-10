using BaseLayer;
using DevComponents.DotNetBar.SuperGrid;
using InterfaceLayer.Sales;
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
            SqlCommand cmd = new SqlCommand("proc_FVEManagment", DbHelperSQL.Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@code", "asdasd6");  //给输入参数赋值
            
            
            SqlParameter parReturn = new SqlParameter("@return", SqlDbType.Int);
            parReturn.Direction = ParameterDirection.ReturnValue;   //参数类型为ReturnValue                    
            cmd.Parameters.Add(parReturn);

            cmd.ExecuteNonQuery();

            MessageBox.Show(parReturn.Value.ToString());
        }
    }
}