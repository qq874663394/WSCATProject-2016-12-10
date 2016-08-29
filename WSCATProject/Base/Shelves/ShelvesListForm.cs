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

namespace WSCATProject.Base.Shelves
{
    public partial class ShelvesListForm : Form
    {
        public ShelvesListForm()
        {
            InitializeComponent();
        }
        #region 绑定TreeView
        /// <summary>
        /// 绑定TreeView
        /// </summary>
        private void BindTreeViewList()
        {
            try
            {
                DataTable dts = new DataTable();
                //DataTable dts = cm.SelCityTable();
                AddTree("D4", null, dts, "C");
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载数据失败,请检查服务器连接并尝试刷新.错误:" + ex.Message);
            }
        }
        #endregion

        #region 递归添加树的节点
        /// <summary>
        /// //递归添加树的节点
        /// </summary>
        /// <param name="ParentID">条件ID:默认D4</param>
        /// <param name="pNode">父级ID：null</param>
        /// <param name="dt">查询结果集:DataTable</param>
        protected void AddTree(string ParentID, TreeNode pNode, DataTable dts, string tableName)
        {
            if (ParentID == "")
            {
                ParentID = "D4";
            }
            string ParentId = "PIC_ParentId";
            string Code = "PIC_Code";
            string Name = "PIC_Name";
            if (tableName == "C")
            {
                ParentId = "City_ParentId";
                Code = "City_Code";
                Name = "City_Name";
            }

            DataTable dt = dts;
            DataView dvTree = new DataView(dt);

            //过滤ParentID,得到当前的所有子节点
            dvTree.RowFilter = string.Format("{0} = '{1}'", ParentId, ParentID);

            foreach (DataRowView Row in dvTree)
            {
                TreeNode Node = new TreeNode();
                if (pNode == null)
                {
                    //添加根节点    
                    Node.Text = XYEEncoding.strHexDecode(Row[Name].ToString());
                    Node.Tag = XYEEncoding.strHexDecode(Row[Code].ToString());

                    treeView1.Nodes.Add(Node);
                    AddTree(Row[Code].ToString(), Node, dts, tableName);
                    //展开第一级节点
                    Node.Expand();
                }
                else
                {
                    //添加当前节点的子节点                  
                    Node.Text = XYEEncoding.strHexDecode(Row[Name].ToString());
                    Node.Tag = XYEEncoding.strHexDecode(Row[Code].ToString());
                    pNode.Nodes.Add(Node);
                    AddTree(Row[Code].ToString(), Node, dts, tableName);     //再次递归 
                }
            }
        }
        #endregion
    }
}
