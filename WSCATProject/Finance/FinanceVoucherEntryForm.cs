using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar.SuperGrid.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSCATProject.Finance
{
    public partial class FinanceVoucherEntryForm : Form
    {
        public FinanceVoucherEntryForm()
        {
            InitializeComponent();
        }


        #region 设置窗体无边框可以拖动
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void FinanceVoucherEntryForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion

        #region 最小化、最大化、关闭的点击事件
        private void pictureBoxMax_MouseEnter(object sender, EventArgs e)
        {
            //当窗体的状态为最大化时，工具提示文本为还原
            if (this.WindowState == FormWindowState.Maximized)
            {
                toolTip1.SetToolTip(pictureBoxMax, "还原");
                return;
            }
            //当窗体的状态为正常时时，工具提示文本为最大化
            if (this.WindowState == FormWindowState.Normal)
            {
                toolTip1.SetToolTip(pictureBoxMax, "最大化");
                return;
            }
        }

        /// <summary>
        /// 最大化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxMax_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                pictureBoxMax.Image = Properties.Resources.zuidahua1;
                return;
            }
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                pictureBoxMax.Image = Properties.Resources.zuidahua;
                return;
            }
        }

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion

        #region 改变边框颜色
        /// <summary>
        /// 改变边框颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                                 this.panel2.ClientRectangle,
                                 Color.FromArgb(85, 177, 238),
                                 1,
                                ButtonBorderStyle.Solid,
                                 Color.FromArgb(85, 177, 238),
                                 1,
                                 ButtonBorderStyle.Solid,
                                  Color.White,
                                 1,
                                 ButtonBorderStyle.Solid,
                                  Color.FromArgb(85, 177, 238),
                                 1,
                                 ButtonBorderStyle.Solid);
        }
        #endregion

        #region 获取dateTimePicker年份月份的值
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = DateTime.Parse(dateTimePicker1.Text);
            string year = date.Year.ToString();
            string month = date.Month.ToString();
            lblyear.Text = year + " 年";
            lblmonth.Text = "第 " + month + " 期";

        }
        #endregion

        #region 合并表头
        private ColumnGroupHeader GetSlCompanyInfoHeader(GridColumnCollection columns, string name, string headerText, string startName, string endName)
        {
            ColumnGroupHeader cgh = new ColumnGroupHeader();

            cgh.Name = name;
            cgh.HeaderText = headerText;

            cgh.MinRowHeight = 36;
            cgh.RowHeight = 50;

            cgh.StartDisplayIndex = GetDisplayIndex(columns, startName);
            cgh.EndDisplayIndex = GetDisplayIndex(columns, endName);

            return (cgh);
        }
        private int GetDisplayIndex(GridColumnCollection columns, string name)
        {
            return (columns.GetDisplayIndex(columns[name]));
        }
        #endregion

        #region 统计合计行
        /// <summary>
        /// 统计行数据
        /// </summary>
        private void InitDataGridView()
        {
            //新增一行 用于给客户操作
            superGridControlPingZheng.PrimaryGrid.NewRow(true);
            //最后一行做统计行
            GridRow gr = (GridRow)superGridControlPingZheng.PrimaryGrid.
                Rows[superGridControlPingZheng.PrimaryGrid.Rows.Count - 1];
            gr.ReadOnly = true;
            gr.AllowSelection = false;
            gr.CellStyles.Default.Background.Color1 = Color.Yellow;
        }
        #endregion

        private void FinanceVoucherEntryForm_Load(object sender, EventArgs e)
        {
            #region 加载时，初始化表格

            GridPanel panel = superGridControlPingZheng.PrimaryGrid;
            GridColumnCollection columns = panel.Columns;
            panel.ColumnHeader.GroupHeaders.Add(GetSlCompanyInfoHeader(columns, "gridColumnSubject", "借方", "gridColumn1", "gridColumn19"));
            panel.ColumnHeader.GroupHeaders.Add(GetSlCompanyInfoHeader(columns, "gridColumn19", "贷方", "gridColumn20", "gridColumn38"));

            superGridControlPingZheng.PrimaryGrid.ColumnHeader.ShowGroupColumnHeaders = false;//合并表头后，是否显示原有的列
            superGridControlPingZheng.PrimaryGrid.AutoGenerateColumns = false;//禁止自动创建列
            superGridControlPingZheng.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;//内容居中           
            superGridControlPingZheng.PrimaryGrid.ShowRowGridIndex = true;//显示行号
            pictureBoxMin.Parent = pictureBoxtitle;
            pictureBoxMax.Parent = pictureBoxtitle;
            pictureBoxClose.Parent = pictureBoxtitle;
            cboPingZhengZi.SelectedIndex = 0;
            superGridControlPingZheng.PrimaryGrid.GridLines = GridLines.Horizontal;//只允许显示行线
            //排序方式范围
            superGridControlPingZheng.PrimaryGrid.SortCycle = SortCycle.AscDesc;
            //设置排序列和排序方式
            superGridControlPingZheng.PrimaryGrid.AddSort(superGridControlPingZheng.PrimaryGrid.Columns[0], SortDirection.Ascending);
            InitDataGridView();//调用统计行

            //GridTextBoxXEditControl gdiec1 = superGridControlPingZheng.PrimaryGrid.Columns["gridColumn1"].EditControl as GridTextBoxXEditControl;
            // gdiec1.EditorValue = "";
            //GridTextBoxXEditControl gdiec2 = superGridControlPingZheng.PrimaryGrid.Columns["gridColumn2"].EditControl as GridTextBoxXEditControl;
            //gdiec2.MaxValue = 9;
            //gdiec2.MinValue = 0;

            #region  获取日期值
            DateTime date = DateTime.Parse(dateTimePicker1.Text);
            string year = date.Year.ToString();
            string month = date.Month.ToString();
            lblyear.Text = year + " 年";
            lblmonth.Text = "第 " + month + " 期";
            #endregion

            #endregion

        }

        /// <summary>
        /// 计算器点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripBtnJiSuanQi_Click(object sender, EventArgs e)
        {
            Process sysCalculator = null;
            sysCalculator = Process.Start("calc.exe");
        }

        /// <summary>
        /// 还原点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripBtnHuanYuan_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("凭证已经修改，是否保存？", "信息提示", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.OK)
            {
                MessageBox.Show("暂不能进行保存！");
            }
            if (result == DialogResult.No)
            {
                numericUpDownFuJian.Value = 0;
                numericUpDownXuHao.Value = 0;
                superGridControlPingZheng.PrimaryGrid.Rows.Clear();
                FinanceVoucherEntryForm_Load(sender, e);
            }
            return;
        }

        /// <summary>
        /// 插入点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonChaRu_Click(object sender, EventArgs e)
        {
            FinanceVoucherEntryForm_Load(sender, e);
        }

        private void superGridControlPingZheng_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || e.KeyChar == 13 || e.KeyChar == 8 || e.KeyChar == 46)
            {
                e.Handled = true;
            }
        }

        private void superGridControlPingZheng_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void superGridControlPingZheng_BeginEdit_1(object sender, GridEditEventArgs e)
        {
        }

        private void superGridControlPingZheng_KeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void superGridControlPingZheng_Validated(object sender, EventArgs e)
        {
        }

        private void superGridControlPingZheng_TextChanged(object sender, EventArgs e)
        {
        }

        private void superGridControlPingZheng_CellInfoLeave(object sender, GridCellInfoLeaveEventArgs e)
        {
        }

        private void superGridControlPingZheng_CellMouseDown(object sender, GridCellMouseEventArgs e)
        {
            //MessageBox.Show("Test");
        }

        private void superGridControlPingZheng_CellValueChanged(object sender, GridCellValueChangedEventArgs e)
        {
            //MessageBox.Show(e.GridCell.GridColumn.PrevVisibleColumn.Name);
            GridColumn[] item1 = { gridColumn1, gridColumn2, gridColumn3, gridColumn4, gridColumn5, gridColumn6, gridColumn7, gridColumn8, gridColumn9, gridColumn10, gridColumn11, gridColumn12, gridColumn13, gridColumn14};
            int rowsIndex = (superGridControlPingZheng.GetSelectedCells()[0] as GridCell).GridRow.RowIndex;  //获取行号
            GridRow gr = (GridRow)superGridControlPingZheng.PrimaryGrid.Rows[rowsIndex];   //获取

            if (gr[e.GridCell.GridColumn.PrevVisibleColumn.Name].Value != null)
            {
                for (int i = 0; i < 14; i++)//有18列
                {
                    if (gr[item1[i]].Value != null)//如果下一列的值不等于null，则位移
                    {
                        gr[item1[i - 1]].Value = gr[item1[i]].Value;
                    }
                }
            }
            if (e.OldValue != null)
            {
                gr[e.GridCell.GridColumn.PrevVisibleColumn.Name].Value = e.OldValue;
            }
        }

        private void superGridControlPingZheng_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //MessageBox.Show("Test");
        }

        private void superGridControlPingZheng_EditorValueChanged(object sender, GridEditEventArgs e)
        {

        }
    }
}
