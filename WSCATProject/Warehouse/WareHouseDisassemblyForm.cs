using DevComponents.DotNetBar.SuperGrid;
using HelperUtility.Encrypt;
using InterfaceLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSCATProject.Warehouse
{
    public partial class WareHouseDisassemblyForm : TestForm
    {
        public WareHouseDisassemblyForm()
        {
            InitializeComponent();
        }

        #region 数据字段
        /// <summary>
        /// 所有业务员
        /// </summary>
        private DataTable _AllEmployee = null;
        #endregion

        #region 实例化接口层 以及解密方法

        CodingHelper ch = new CodingHelper();
        EmpolyeeInterface employee = new EmpolyeeInterface();

        #endregion

        #region 初始化数据
        /// <summary>
        /// 初始化业务员
        /// </summary>
        private void InitEmployee()
        {

            dataGridViewFujia.DataSource = null;
            dataGridViewFujia.Columns.Clear();

            DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "code";
            dgvc.HeaderText = "员工工号";
            dgvc.DataPropertyName = "员工工号";
            dataGridViewFujia.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "name";
            dgvc.HeaderText = "姓名";
            dgvc.DataPropertyName = "姓名";
            dataGridViewFujia.Columns.Add(dgvc);

            resizablePanel1.Location = new Point(234, 440);
            dataGridViewFujia.DataSource = ch.DataTableReCoding(_AllEmployee);
            resizablePanel1.Visible = true;

            if (this.WindowState == FormWindowState.Maximized)
            {
                resizablePanel1.Location = new Point(230, 670);
                return;
            }
            if (this.WindowState == FormWindowState.Normal)
            {
                resizablePanel1.Location = new Point(230, 450);
                return;
            }

        }

        /// <summary>
        /// 统计行数据
        /// </summary>
        private void InitDataGridView()
        {
            //新增一行 用于给客户操作
            superGridControl1.PrimaryGrid.NewRow(true);
            //最后一行做统计行
            GridRow gr = (GridRow)superGridControl1.PrimaryGrid.
                Rows[superGridControl1.PrimaryGrid.Rows.Count - 1];
            gr.ReadOnly = true;
            gr.CellStyles.Default.Background.Color1 = Color.SkyBlue;
            gr.Cells["material"].Value = "合计";
            gr.Cells["material"].CellStyles.Default.Alignment =
                DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumn7"].Value = 0;
            gr.Cells["gridColumn7"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumn7"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumn8"].Value = 0;
            gr.Cells["gridColumn8"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumn8"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumn9"].Value = 0;
            gr.Cells["gridColumn9"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumn9"].CellStyles.Default.Background.Color1 = Color.Orange;

        }
        #endregion

        private void WareHouseDisassemblyForm_Load(object sender, EventArgs e)
        {
            //业务员
            _AllEmployee = employee.SelSupplierTable(false);

            //禁用自动创建列
            dataGridView1.AutoGenerateColumns = false;
            dataGridViewFujia.AutoGenerateColumns = false;
            superGridControl1.HScrollBarVisible = true;

            //绑定事件 双击事填充内容并隐藏列表
            dataGridViewFujia.CellDoubleClick += DataGridViewFujia_CellDoubleClick;
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;

            InitDataGridView();
        }

        #region 两个副框的双击事件和下拉箭头的点击事件

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridViewFujia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
            labtextboxBotton1.Text = name;
            resizablePanel1.Visible = false;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            InitEmployee();
        }



        #endregion

        private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
        {

        }

        private void superGridControl2_BeginEdit(object sender, GridEditEventArgs e)
        {
            if(e.GridCell.GridColumn.Name == "material")
            {
                ClickRowIndex = e.GridCell.RowIndex;
                resizablePanelData.Visible = true;
                resizablePanelData.Location = new Point(e.GridCell.UnMergedBounds.X,
                    e.GridCell.UnMergedBounds.Bottom + panel3.Location.Y);
            }
        }

        private void labtextboxTop6_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

         
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)//小数点
            {
                if (labtextboxTop6.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(labtextboxTop6.Text, out oldf);
                    b2 = float.TryParse(labtextboxTop6.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
            }
            catch (Exception)
            {

                throw;
            }
        }
        

    }
}
