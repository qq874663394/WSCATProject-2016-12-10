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
