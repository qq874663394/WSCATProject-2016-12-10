using DevComponents.DotNetBar.SuperGrid;
using HelperUtility.Encrypt;
using InterfaceLayer.Warehouse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSCATProject.Warehouse
{
    public partial class WareHouseInventoryReportForm : Form
    {
        public WareHouseInventoryReportForm()
        {
            InitializeComponent();
        }
        #region  数据字段
        /// <summary>
        /// 统计贮存数量
        /// </summary>
        private decimal _Materialzhucun;
        /// <summary>
        /// 统计判定数量
        /// </summary>
        private decimal _Materialpandian;
        /// <summary>
        /// 统计盘盈数量
        /// </summary>
        private decimal _Materialpanying;
        /// <summary>
        /// 统计盘亏数量
        /// </summary>
        private decimal _Materialpankui;
        #endregion

        WarehouseInventoryInterface iface = new WarehouseInventoryInterface();
        CodingHelper codeh = new CodingHelper();

        #region  窗体加载事件
        private void WareHouseInventoryReportForm_Load(object sender, EventArgs e)
        {
            this.labelTitle.BackColor = Color.FromArgb(85, 177, 238);
            this.pictureBox6.BackColor = Color.FromArgb(85, 177, 238);
            this.pictureBox7.BackColor = Color.FromArgb(85, 177, 238);
            this.pictureBox8.BackColor = Color.FromArgb(85, 177, 238);
            //显示行号
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            //不可自动添加列
            this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            //显示行号
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            #region 加载盘点方案的数据
            DataTable dt = null;//codeh.DataTableReCoding(iface.GetList());
            DataRow dr = dt.NewRow();
            dr["name"] = "请选择";
            dt.Rows.InsertAt(dr, 0);

            comboBoxEx1.DisplayMember = "name";
            comboBoxEx1.ValueMember = "code";
            comboBoxEx1.DataSource = dt;

            #endregion

            //调用表格初始化
            superGridControl1.PrimaryGrid.EnsureVisible();
            InitDataGridView();
        }
        #endregion

        #region  下拉框选择改变事件
        private void comboBoxEx1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxEx1.SelectedValue == null || comboBoxEx1.SelectedValue.ToString() == "")
            {
                //绑定dgv   查询全部数据
                DataTable dt = codeh.DataTableReCoding(iface.GetList(999,""));
                if (dt == null)
                {
                    superGridControl1.PrimaryGrid.DataSource = null;
                }
                else
                {
                    superGridControl1.PrimaryGrid.DataSource = dt;
                }
            }
            else
            {
                string a = comboBoxEx1.SelectedValue.ToString();
                DataTable dts = codeh.DataTableReCoding(iface.GetList(3, XYEEncoding.strCodeHex(comboBoxEx1.SelectedValue.ToString())));
                superGridControl1.PrimaryGrid.DataSource = dts;
                //调用表格初始化
                superGridControl1.PrimaryGrid.EnsureVisible();
                InitDataGridView();

            }
        }
        #endregion

        #region  表格初始化
        /// <summary>
        /// supergridControl表格初始化
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
            gr.Cells["name"].Value = "合计";
            gr.Cells["name"].CellStyles.Default.Alignment =
                DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            //贮存数量
            gr.Cells["zhangcunnumber"].Value = 0;
            gr.Cells["zhangcunnumber"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["zhangcunnumber"].CellStyles.Default.Background.Color1 = Color.Orange;
            //盘点数量
            gr.Cells["pandiannumber"].Value = 0;
            gr.Cells["pandiannumber"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["pandiannumber"].CellStyles.Default.Background.Color1 = Color.Orange;
            //盘盈数量
            gr.Cells["panyingnumber"].Value = 0;
            gr.Cells["panyingnumber"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["panyingnumber"].CellStyles.Default.Background.Color1 = Color.Orange;
            //盘亏数量
            gr.Cells["pankuinumber"].Value = 0;
            gr.Cells["pankuinumber"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["pankuinumber"].CellStyles.Default.Background.Color1 = Color.Orange;
        }
        #endregion

        #region superGridControl单元格验证事件
        private void superGridControl1_CellValidated(object sender, GridCellValidatedEventArgs e)
        {
            GridRow gr = e.GridPanel.Rows[e.GridCell.RowIndex] as GridRow;
            //计算盘盈、盘亏的数量
            //try
            //{
            //    decimal tempAllzhucun = 0;
            //    decimal tempAllpandian = 0;
            //    decimal tempAllpanying = 0;
            //    decimal tempAllpankui = 0;
            //    decimal zhucunnumber = Convert.ToDecimal(gr.Cells["gridColumn7"].FormattedValue);
            //    decimal pandiannumber = Convert.ToDecimal(gr.Cells["gridColumn8"].FormattedValue);
            //    decimal panying = pandiannumber - zhucunnumber;
            //    if (panying > 0)
            //    {
            //        gr.Cells["gridColumn9"].Value = panying;
            //        gr.Cells["gridColumn10"].Value = "0.00";
            //    }
            //    if (panying < 0)
            //    {
            //        gr.Cells["gridColumn10"].Value = panying;
            //        gr.Cells["gridColumn9"].Value = "0.00";
            //    }

            //    //逐行统计数据总数
            //    for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
            //    {
            //        GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
            //        tempAllzhucun += Convert.ToDecimal(tempGR["gridColumn7"].FormattedValue);
            //        tempAllpandian += Convert.ToDecimal(tempGR["gridColumn8"].FormattedValue);
            //        tempAllpanying += Convert.ToDecimal(tempGR["gridColumn9"].FormattedValue);
            //        tempAllpankui += Convert.ToDecimal(tempGR["gridColumn10"].FormattedValue);
            //    }
            //    _Materialzhucun = tempAllzhucun;
            //    _Materialpandian = tempAllpandian;
            //    _Materialpanying = tempAllpanying;
            //    _Materialpankui = tempAllpankui;
            //    gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
            //    gr["gridColumn7"].Value = _Materialzhucun.ToString();
            //    gr["gridColumn8"].Value = _Materialpandian.ToString();
            //    gr["gridColumn9"].Value = _Materialpanying.ToString();
            //    gr["gridColumn10"].Value = _Materialpankui.ToString();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("统计数量错误" + ex.Message);
            //}
        }
        #endregion

        #region 设置窗体无边框可以拖动
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void superGridControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion

        #region 最小化、最大化、关闭的点击事件
        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            //当窗体的状态为最大化时，工具提示文本为还原
            if (this.WindowState == FormWindowState.Maximized)
            {
                toolTip1.SetToolTip(pictureBox6, "还原");
                return;
            }
            //当窗体的状态为正常时时，工具提示文本为最大化
            if (this.WindowState == FormWindowState.Normal)
            {
                toolTip1.SetToolTip(pictureBox6, "最大化");
                return;
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                pictureBox6.Image = Properties.Resources.zuidahua1;
                return;
            }
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                pictureBox6.Image = Properties.Resources.zuidahua;
                return;
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //关闭
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion
    }
}
