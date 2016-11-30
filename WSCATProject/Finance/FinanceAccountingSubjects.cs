using DevComponents.Tree;
using HelperUtility.Encrypt;
using InterfaceLayer.Finance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSCATProject.Finance
{
    public partial class FinanceAccountingSubjects : Form
    {
        public FinanceAccountingSubjects()
        {
            InitializeComponent();
        }

        private void FinanceAccountingSubjects_Load(object sender, EventArgs e)
        {
            FinanceAccountingSubjectsInterface _dal = new FinanceAccountingSubjectsInterface();
            DataTable dt = _dal.GetList(9999, "");
            AddTree(null, null, dt, treeView1);
            AddTree(null, null, dt, treeView2);
            AddTree(null, null, dt, treeView3);
            AddTree(null, null, dt, treeView4);
            AddTree(null, null, dt, treeView5);
        }
        #region 递归添加树的节点
        /// <summary>
        /// 递归添加树的节点
        /// </summary>
        /// <param name="ParentID">父级ID：默认为空</param>
        /// <param name="pNode">父级节点：默认为null，可选</param>
        /// <param name="table">表名：默认为City，可选参数：P</param>
        /// <param name="ControlName">控件名：必选</param>
        private void AddTree(object ParentCodeIn, int nodeType,TreeNode pNode, DataTable dt, TreeView ControlName)
        {
            string ParentCode = "parentCode";
            string Code = "code";
            string Name = "name";
            string HotKey = "hotkey";
            try
            {
                //DataTable dt = cm.SelCityTable();
                DataView dvTree = new DataView(dt);
                //过滤ParentID,得到当前的所有子节点
                if (ParentCodeIn == null)
                {
                    dvTree.RowFilter = string.Format("{0} is NULL and nodeType=1", ParentCode);
                }
                else
                {
                    dvTree.RowFilter = string.Format("{0} = '{1}'", ParentCode, ParentCodeIn);
                }
                
                foreach (DataRowView Row in dvTree)
                {
                    TreeNode node = new TreeNode();
                    if (pNode == null)
                    {
                        //添加根节点    
                        node.Text = Row[HotKey].ToString()+" - "+Row[Name].ToString();//Text
                        node.Tag = Row[Code].ToString();//Tag
                        ControlName.Nodes.Add(node);
                        AddTree(Row[Code].ToString(), node, dt, ControlName);//调用本身
                        //展开第一级节点
                        node.Expand();
                    }
                    else
                    {
                        //添加当前节点的子节点                  
                        node.Text = Row[HotKey].ToString() + " - " + Row[Name].ToString();
                        node.Tag = Row[Code].ToString();
                        pNode.Nodes.Add(node);
                        AddTree(Row[Code].ToString(), node, dt, ControlName);     //再次递归 
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
