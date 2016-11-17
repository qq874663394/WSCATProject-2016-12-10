using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar.SuperGrid.Style;
using HelperUtility.Encrypt;
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
using WSCATProject.Base;

namespace WSCATProject.Finance
{
    public partial class FinanceVoucherEntryForm : Form
    {
        public FinanceVoucherEntryForm()
        {
            InitializeComponent();
        }

        private object _cellValue;
        private int _indexRows;
        public object _CellValue { get; set; }
        public int _IndexRows { get; set; }

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

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            InitDataGridView();
        }

        private void superGridControlPingZheng_EditorValueChanged(object sender, GridEditEventArgs e)
        {
            e.EditControl.EditorValue = _CellValue;
        }
        private void superGridControlPingZheng_GridPreviewKeyDown(object sender, GridPreviewKeyDownEventArgs e)
        {
            _IndexRows = superGridControlPingZheng.PrimaryGrid.ActiveRow.Index;
            GridColumn[] item1 = { gridColumn1, gridColumn2, gridColumn3, gridColumn4, gridColumn5, gridColumn6, gridColumn7, gridColumn8, gridColumn9, gridColumn10, gridColumn11, gridColumn12, gridColumn13, gridColumn14, gridColumn15, gridColumn16, gridColumn17 };
            GridColumn[] item2 = { gridColumn18, gridColumn19 };
            GridRow gr = (GridRow)superGridControlPingZheng.PrimaryGrid.Rows[_IndexRows];   //获取
            SelectedElementCollection ge = superGridControlPingZheng.PrimaryGrid.GetSelectedCells();
            GridCell gc = null;
            object str = "";
            if (ge.Count <= 0)
            {
                return;
            }
            gc = ge[0] as GridCell;

            //MessageBox.Show(gc.ColumnIndex.ToString());
            //if (true)
            //{

            //    return;
            //}
            #region 验证按键
            switch (e.KeyCode)
            {
                case Keys.Decimal:
                case Keys.OemPeriod:
                case Keys.ProcessKey:
                    str = ".";
                    break;
                case Keys.NumPad0:
                case Keys.D0:
                    str = "0";
                    break;
                case Keys.NumPad1:
                case Keys.D1:
                    str = "1";
                    break;
                case Keys.NumPad2:
                case Keys.D2:
                    str = "2";
                    break;
                case Keys.NumPad3:
                case Keys.D3:
                    str = "3";
                    break;
                case Keys.NumPad4:
                case Keys.D4:
                    str = "4";
                    break;
                case Keys.NumPad5:
                case Keys.D5:
                    str = "5";
                    break;
                case Keys.NumPad6:
                case Keys.D6:
                    str = "6";
                    break;
                case Keys.NumPad7:
                case Keys.D7:
                    str = "7";
                    break;
                case Keys.NumPad8:
                case Keys.D8:
                    str = "8";
                    break;
                case Keys.NumPad9:
                case Keys.D9:
                    str = "9";
                    break;
                case Keys.Back:
                case Keys.Delete:
                    str = "\b";
                    break;
                default:
                    str = null;
                    break;
            }
            #endregion
            if (str == null)
            {
                return;
            }
            if (gc.ColumnIndex >= 2 && gc.ColumnIndex <= 18)//借方左边
            {
                if (e.KeyCode == Keys.ProcessKey)
                {
                    gr["gridColumn18"].SetActive();
                    gr["gridColumn18"].IsSelected = true;
                    gr["gridColumn17"].IsSelected = false;
                    gr["gridColumn17"].Value = _CellValue;
                    return;
                }
                //左删除
                if (gr["gridColumn17"].IsActiveCell == true || gr["gridColumn17"].AllowSelection == true)//焦点在左边输入点
                {
                    if (str.Equals("\b"))//按键是退格键
                    {
                        for (int i = item1.Length - 1; i >= 0; i--)
                        {
                            gr = (GridRow)superGridControlPingZheng.PrimaryGrid.Rows[_IndexRows];
                            if ((i - 1) >= 0)
                            {
                                if (i == item1.Length - 1)
                                {
                                    gr["gridColumn17"].Value = gr["gridColumn16"].Value;
                                }
                                gr[item1[i]].Value = gr[item1[i - 1]].Value;
                                gr[item1[i - 1]].Value = null;
                                _CellValue = null;
                            }
                        }
                        return;
                    }
                }
                //左移动
                if (gr["gridColumn1"].Value != null)
                {
                    return;
                }
                for (int i = 0; i < item1.Length - 1; i++)
                {
                    gr = (GridRow)superGridControlPingZheng.PrimaryGrid.Rows[_IndexRows];   //获取双击进去后处于编辑状态的行
                    if ((i + 1) <= item1.Length - 1)//下一条数组不能大于总长度
                    {
                        if (gr[item1[i + 1]].Value == null)//如果等于null 可以移动
                        {
                            gr[item1[i]].Value = gr[item1[i + 1]].Value;
                            gr[item1[i + 1]].Value = null;
                        }
                        else
                        {
                            if (gr[item1[i + 1]].Value.ToString() == "" || gr[item1[i + 1]].Value.ToString() == ".")//如果等于空字符串  则赋值为null
                            {
                                gr[item1[i]].Value = null;
                                gr[item1[i + 1]].Value = null;
                            }
                            else//如果不为null也不为空字符串  就正常移动
                            {
                                gr[item1[i]].Value = gr[item1[i + 1]].Value;
                                gr[item1[i + 1]].Value = null;
                            }
                        }
                    }
                }
                if (_CellValue != null)//如果全局变量不等于null就赋值给源单元格前一个单元格
                {
                    gr["gridColumn16"].Value = _CellValue;
                }
            }
            if (gc.ColumnIndex >= 19 && gc.ColumnIndex <= 20)//借方右边
            {
                gr = (GridRow)superGridControlPingZheng.PrimaryGrid.Rows[_IndexRows];   //获取双击进去后处于编辑状态的行
                //if (gr["gridColumn18"].Value != null)
                //{
                //    gr["gridColumn19"].Value = gr["gridColumn18"].Value;
                //}
                if (gr["gridColumn18"].Value == null)
                {
                    gr["gridColumn18"].Value = str;
                }
                else
                {
                    gr["gridColumn19"].Value = gr["gridColumn18"].Value;
                    gr["gridColumn18"].Value = str;
                }
            }
            if (gc.ColumnIndex >= 21 && gc.ColumnIndex <= 37)//贷方左边
            {

            }
            if (gc.ColumnIndex >= 38 && gc.ColumnIndex <= 39)//贷方右边
            {

            }
            _CellValue = str;//将获取的字符赋值给全局变量,用于下一次输入后把上一次输入的赋值给前一个单元格
        }
        /// <summary>
        /// 删除当前分录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripBtnShangChu_Click(object sender, EventArgs e)
        {
            int rowindex = Convert.ToInt32(superGridControlPingZheng.PrimaryGrid.GridIndex);//获取行号
            if (superGridControlPingZheng.PrimaryGrid.Rows.Count > 2)
            {
                superGridControlPingZheng.PrimaryGrid.Rows.RemoveAt(rowindex);
            }
        }

        /// <summary>
        /// 查看代码点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripBtnDaiMa_Click(object sender, EventArgs e)
        {
            LookDaiMa();
        }

        /// <summary>
        /// 查看代码函数
        /// </summary>
        private void LookDaiMa()
        {
            try
            {
                SelectedElementCollection ge = superGridControlPingZheng.PrimaryGrid.GetSelectedCells();
                GridCell gc = ge[0] as GridCell;
                if (gc.GridColumn.Name == "gridColumnSubject")
                {
                    AccountingSubjectsForm accountSubject = new AccountingSubjectsForm();
                    accountSubject.ShowDialog(this);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 快捷方式设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinanceVoucherEntryForm_KeyDown(object sender, KeyEventArgs e)
        {
            //上一站凭证
            if (e.KeyCode == Keys.B)
            {
                MessageBox.Show("上一站凭证！");
                return;
            }
            //下一张凭证
            if (e.KeyCode == Keys.A)
            {
                MessageBox.Show("下一张凭证！");
                return;
            }
            //新增凭证
            if (e.KeyCode == Keys.N)
            {
                MessageBox.Show("新增凭证！");
                return;
            }
            //保存凭证
            if (e.KeyCode == Keys.S)
            {
                MessageBox.Show("保存凭证！");
                return;
            }
            //打印凭证
            if (e.KeyCode == Keys.P)
            {
                MessageBox.Show("保存凭证！");
                return;
            }
            //取消所做的更改
            if (e.KeyCode == Keys.Z)
            {
                toolStripBtnHuanYuan_Click(sender, e);
                return;
            }
            //插入一条分录
            if (e.KeyCode == Keys.I)
            {
                InitDataGridView();
                return;
            }
            //删除一条分录
            if (e.KeyCode == Keys.D)
            {
                toolStripBtnShangChu_Click(sender, e);
                return;
            }
            //查看代码
            if (e.KeyCode == Keys.F7)
            {
                LookDaiMa();
                return;
            }
            //关闭
            if (e.KeyCode == Keys.X)
            {
                this.Close();
                this.Dispose();
            }
        }

        /// <summary>
        /// 光标定位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinanceVoucherEntryForm_Activated(object sender, EventArgs e)
        {
            superGridControlPingZheng.Focus();
        }

        private void superGridControlPingZheng_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectedElementCollection ge = superGridControlPingZheng.PrimaryGrid.GetSelectedCells();
                GridCell gc = ge[0] as GridCell;
                if (gc.GridColumn.Name == "gridColumnSubject")
                {
                    MessageBox.Show("科目无权使用或不存在！");
                }
            }
        }

        private void superGridControlPingZheng_CellClick(object sender, GridCellClickEventArgs e)
        {
            //MessageBox.Show(_IndexRows.ToString());
            
        }
    }
}