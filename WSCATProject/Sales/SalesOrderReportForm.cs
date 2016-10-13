using DevComponents.DotNetBar.SuperGrid;
using HelperUtility.Encrypt;
using InterfaceLayer.Sales;
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

namespace WSCATProject.Sales
{
    public partial class SalesOrderReportForm : Form
    {
        public SalesOrderReportForm()
        {
            InitializeComponent();
        }

        #region 初始化数据
        /// <summary>
        /// 保存客户code
        /// </summary>
        private string _clientCode;

        public string clientCode
        {
            get { return _clientCode; }
            set { _clientCode = value; }
        }
        #endregion
        CodingHelper ch = new CodingHelper();

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SalesOrderReportForm_Load(object sender, EventArgs e)
        {
            this.labelTitle.BackColor = Color.FromArgb(85, 177, 238);
            this.pictureBoxMax.BackColor = Color.FromArgb(85, 177, 238);
            this.pictureBoxMin.BackColor = Color.FromArgb(85, 177, 238);
            this.pictureBoxClose.BackColor = Color.FromArgb(85, 177, 238);

            //不可自动添加列
            this.superGridControlShangPing.PrimaryGrid.AutoGenerateColumns = false;
            superGridControlShangPing.HScrollBarVisible = true;
            //表格内容居中
            superGridControlShangPing.DefaultVisualStyles.CellStyles.Default.Alignment =
            DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;

            SalesOrderInterface salesOrder = new SalesOrderInterface();

            DataTable dt1 = ch.DataTableReCoding(salesOrder.GetSalesJoinSearch());
            DataTable dt2 = ch.DataTableReCoding(salesOrder.GetSalesDetailJoinSearch());
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                superGridControlShangPing.PrimaryGrid.Rows.Add(new GridRow(dt1.Rows[i].ItemArray));
                GridRow gr = superGridControlShangPing.PrimaryGrid.Rows[i] as GridRow;
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    if (dt1.Rows[i]["code"].Equals(dt2.Rows[j]["mainCode"]))
                    {
                        #region 绑定列
                        // gr.Cells["JiaoHuoMethod"].Value = dt2.Rows[j]["code"] + "++" + dt2.Rows[j]["mainCode"];
                        //gr.Cells["DanJuDate"].Value =
                        //gr.Cells["ClientCode"].Value =
                        //gr.Cells["ClientName"].Value =
                        //gr.Cells["LinkMan"].Value =
                        //gr.Cells["MobilPhone"].Value =
                        //gr.Cells["fax"].Value =
                        //gr.Cells["JiaoHuoMethod"].Value =
                        //gr.Cells["JiaoHuoAddress"].Value =
                        //gr.Cells["JiaoHuoDate"].Value =
                        //gr.Cells["YiShouDingJin"].Value =
                        //gr.Cells["SaleMan"].Value =
                        //gr.Cells["makeMan"].Value =
                        //gr.Cells["shengheMan"].Value =
                        //gr.Cells["ZhaiYao"].Value =
                        //gr.Cells["shengheState"].Value =
                        //+dt2.Rows[j]["mainCode"])
                        gr.Cells.Add(new GridCell(dt2.Rows[j]["mainCode"]));
                        gr.Cells.Add(new GridCell(dt2.Rows[j]["materialDaima"]));
                        gr.Cells.Add(new GridCell(dt2.Rows[j]["name"]));
                        gr.Cells.Add(new GridCell(dt2.Rows[j]["model"]));
                        gr.Cells.Add(new GridCell(dt2.Rows[j]["barCode"]));
                        gr.Cells.Add(new GridCell(dt2.Rows[j]["unit"]));
                        gr.Cells.Add(new GridCell(dt2.Rows[j]["number"]));
                        gr.Cells.Add(new GridCell(dt2.Rows[j]["materialPrice"]));
                        gr.Cells.Add(new GridCell(dt2.Rows[j]["discountMoney"]));
                        gr.Cells.Add(new GridCell(dt2.Rows[j]["discountRate"]));
                        gr.Cells.Add(new GridCell(dt2.Rows[j]["discountMoney"]));



                        //dt2.Rows[j]["materialDaima"];
                        //dt2.Rows[j]["name"];
                        //dt2.Rows[j]["model"];
                        //dt2.Rows[j]["barCode"];
                        //dt2.Rows[j]["unit"];
                        //dt2.Rows[j]["number"];
                        //dt2.Rows[j]["materialPrice"];
                        //dt2.Rows[j]["discountMoney"];
                        //dt2.Rows[j]["discountRate"];
                        //dt2.Rows[j]["discountMoney"];
                        ////gr.Cells["shiJiPrice"].Value =
                        //gr.Cells["taxRate"].Value = dt2.Rows[j]["tax"];
                        ////gr.Cells["taxMoney"].Value =
                        ////gr.Cells["priceANDtax"].Value =
                        ////gr.Cells["shiJiTaxPrice"].Value =
                        //gr.Cells["fahuoNumber"].Value = dt2.Rows[j]["deliveryNumber"];
                        //gr.Cells["ZongKuCun"].Value = dt2.Rows[j]["allNumber"];
                        ////gr.Cells["CaiGouZaiDingNumber"].Value =
                        ////gr.Cells["SaleZaiDingNumber"].Value =
                        ////gr.Cells["materialRemark"].Value =
                        #endregion
                       
                    }
                    else
                    {
                        
                        continue;
                    }
                }
            }
        }
        #region 设置窗体无边框可以拖动
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;



        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void SalesOrderReportForm_MouseDown(object sender, MouseEventArgs e)
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

        private void pictureBoxMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //关闭
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion

    }
}
