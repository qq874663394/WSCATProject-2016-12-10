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
            #region 递归添加树的节点
            /// <summary>
            /// 递归添加树的节点
            /// </summary>
            /// <param name="ParentID">父级ID：默认为空</param>
            /// <param name="pNode">父级节点：默认为null，可选</param>
            /// <param name="table">表名：默认为City，可选参数：P</param>
            /// <param name="ControlName">控件名：必选</param>
        private void AddTree(string ParentID, Node pNode, string table, ComboTree ControlName)
        {
            string ParentId = "City_ParentId";
            string Code = "City_Code";
            string Name = "City_Name";
            try
            {
                DataTable dt = null;
                //DataTable dt = cm.SelCityTable();
                DataView dvTree = new DataView(dt);
                //过滤ParentID,得到当前的所有子节点
                dvTree.RowFilter = string.Format("{0} = '{1}'", ParentId, ParentID);
                foreach (DataRowView Row in dvTree)
                {
                    Node node = new Node();
                    if (pNode == null)
                    {
                        //添加根节点    
                        node.Text = XYEEncoding.strHexDecode(Row[Name].ToString());//Text
                        node.Tag = XYEEncoding.strHexDecode(Row[Code].ToString());//Tag
                        ControlName.Nodes.Add(node);
                        AddTree(Row[Code].ToString(), node, table, ControlName);//调用本身
                        //展开第一级节点
                        node.Expand();
                    }
                    else
                    {
                        //添加当前节点的子节点                  
                        node.Text = XYEEncoding.strHexDecode(Row[Name].ToString());
                        node.Tag = XYEEncoding.strHexDecode(Row[Code].ToString());
                        pNode.Nodes.Add(node);
                        AddTree(Row[Code].ToString(), node, table, ControlName);     //再次递归 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载数据失败,请检查服务器连接并尝试刷新.错误:" + ex.Message);
            }
        }
        #endregion
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