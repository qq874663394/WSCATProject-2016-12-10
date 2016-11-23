using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;
using HelperUtility.Encrypt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WSCATProject.Finance
{
    public partial class FinanceSummaryLibraryForm : Form
    {

        public FinanceSummaryLibraryForm()
        {
            InitializeComponent();
        }

        private void FinanceSummaryLibraryForm_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 添加类别
        /// </summary>
        /// <param name="xmlFilePath"></param>
        private static void AddCXml(string name)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("../XML/XMLSummary.xml");

                XmlNode root = xmlDoc.SelectSingleNode("Classs");//查找<Employees> 
                XmlElement xe1 = xmlDoc.CreateElement(name);//创建一个<Node>节点 
                xe1.SetAttribute("genre", "doucube");//设置该节点genre属性 
                xe1.SetAttribute("isbn", "2-3631-4");//设置该节点isbn属性 

                XmlElement xesub1 = xmlDoc.CreateElement("title");
                xesub1.InnerText = "CS从入门到精通";//设置文本节点 
                xe1.AppendChild(xesub1);//添加到<Node>节点中 
                XmlElement xesub2 = xmlDoc.CreateElement("author");
                xesub2.InnerText = "候捷";
                xe1.AppendChild(xesub2);
                XmlElement xesub3 = xmlDoc.CreateElement("price");
                xesub3.InnerText = "58.3";
                xe1.AppendChild(xesub3);
                root.AppendChild(xe1);//添加到<Employees>节点中 
                xmlDoc.Save("../XML/XMLSummary.xml");
                MessageBox.Show("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCAdd_Click(object sender, EventArgs e)
        {
            //AddSXml("阿萨德", "123","123");
            //AddCXml("阿萨德", "123", "123");
        }

        /// <summary>
        /// 添加摘要
        /// </summary>
        /// <param name = "xmlFilePath" ></ param >
        private static void AddSXml(string nodeName, string code, string name)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("../XML/XMLSummary.xml");

                XmlNode root = xmlDoc.SelectSingleNode(nodeName);//查找节点 
                XmlElement xe1 = xmlDoc.CreateElement(name);//创建一个<Node>节点 
                XmlElement xesub1 = xmlDoc.CreateElement("code");
                xesub1.InnerText = code+"-"+name;//设置文本节点 
                root.AppendChild(xe1);//添加到<Node>节点中 
                xmlDoc.Save("../XML/XMLSummary.xml");
                MessageBox.Show("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

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
    }
}
