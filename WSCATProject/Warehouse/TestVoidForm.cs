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
        public string ExSwitch(int number, string warehouseInResult)
        {
            switch (warehouseInResult)
            {
                case "-1":
                    return "错误代码:4093;拼接连接字符串时出现异常,请重试";
                case "-2":
                    return "错误代码:4094;检查到传入参数为空,请检查输入";
                case "-3":
                    return "错误代码:4095;操作数据库失败,请重试";
                case "-4":
                    return "错误代码:4096;尝试打开数据库连接时出错,请检查服务器连接";
                case "-5":
                    return "错误代码:4097;对数据库新增数据时未能增加任何数据";
                case "-6":
                    return "错误代码:4098;对数据库新增数据的方法失效,未能增加任何行";
                case "-7":
                    return "错误代码:4099;检查到传入的参数错误,无法进行新增操作";
                case "-7.1":
                    return "错误代码:4099;检查到主表操作错误";
                case "-7.2":
                    return "";
                default:
                    return "未知错误";
            }
        }

        public static readonly string connectionString = ConfigurationManager.AppSettings["WSCAT"];
        public static SqlConnection Conn
        {
            get
            {
                SqlConnection conn = null;
                try
                {
                    conn = new SqlConnection(connectionString);
                    conn.Open();
                }
                catch
                {
                    throw new Exception("-0");
                }
                return conn;
            }
        }
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    #region dgv子父级添加行
        //    DataTable dt1 = soif.GetSalesJoinSearch("225525");
        //    DataTable dt2 = soif.GetSalesDetailJoinSearch();
        //    for (int i = 0; i < dt1.Rows.Count; i++)
        //    {
        //        sgCustomers.PrimaryGrid.Rows.Add(new GridRow(dt1.Rows[i].ItemArray));
        //        for (int j = 0; j < dt2.Rows.Count; j++)
        //        {
        //            if (dt1.Rows[i]["code"].Equals(dt2.Rows[j]["mainCode"]))
        //            {
        //                sgCustomers.PrimaryGrid.Rows.Add(new GridRow(dt2.Rows[j].ItemArray));
        //                (sgCustomers.PrimaryGrid.Rows[i] as GridRow).Cells["mainCode"].Value = dt2.Rows[j]["MainCode"];
        //                //sgCustomers.PrimaryGrid.Rows[i]//dt1.Rows[i]["code"] = "";
        //            }
        //            else
        //            {
        //                continue;
        //            }
        //        }
        //    }
        //    #endregion
        //}
        public static object ExecuteSqlTranScalar(Hashtable SQLStringList, string sqlstr,string sqlInsert,
            List<SqlParameter[]> paraList)
        {
            object val1 = null;
            object val2 = null;
            using (SqlConnection conn = Conn)
            {
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    if (paraList.Count > 0)
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            val1 = cmd.ExecuteScalar();
                            if (val1 == null || val1 == DBNull.Value)
                            {
                                throw new Exception("-3");
                            }
                            cmd.Parameters.Clear();
                        }
                        foreach (SqlParameter[] para in paraList)
                        {
                            string cmdText = sqlstr;
                            SqlParameter[] cmdParms = para;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            val2 = cmd.ExecuteScalar();
                            if (val2 == null || val2 == DBNull.Value)
                            {
                                cmdText = sqlInsert;
                                cmdParms = para;
                                PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                                val2 = cmd.ExecuteScalar();
                            }
                            cmd.Parameters.Clear();
                        }
                    }
                    trans.Commit();
                }
                return val1;
            }
        }
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn,
            SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
        public int test()
        {
            int i = 10;
            int y = 0;
            int x = 0 - 10;
            return x;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void sgContracts_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                MessageBox.Show("Test");
                throw;
            }
        }
    }
}