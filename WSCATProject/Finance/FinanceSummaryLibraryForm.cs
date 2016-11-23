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
            //AddCxml("阿萨德", "123", "123");
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
    }
}
