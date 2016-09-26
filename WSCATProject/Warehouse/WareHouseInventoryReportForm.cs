using DevComponents.DotNetBar.SuperGrid;
using HelperUtility;
using HelperUtility.Encrypt;
using InterfaceLayer.Base;
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
        /// 统计账存数量
        /// </summary>
        private decimal _ZhangCunShuLiang;
        /// <summary>
        /// 统计判定数量
        /// </summary>
        private decimal _PanDianShuLiang;
        /// <summary>
        /// 统计盘盈数量
        /// </summary>
        private decimal _PanYingShuLiang;
        /// <summary>
        /// 统计盘亏数量
        /// </summary>
        private decimal _PanKuiShuLiang;

        /// <summary>
        /// 盘点Code
        /// </summary>
        private string _inventoryCode;
        public string InventoryCode
        {
            get {return _inventoryCode;}
            set { _inventoryCode = value;}
        }
        /// <summary>
        /// 仓库Code
        /// </summary>
        private string _storageCode;
        public string StorageCode
        {
            get{  return _storageCode; }
            set { _storageCode = value; }
        }

        /// <summary>
        /// 仓库Name
        /// </summary>
        private string _storageName;
        public string StorageName
        {
            get{ return _storageName;}
            set {  _storageName = value; }
        }

        #endregion

        #region 调用接口以及加密解密的方法
        StorageInterface si = new StorageInterface();
        WarehouseInventoryDetailInterface whidi = new WarehouseInventoryDetailInterface();
        CodingHelper codeh = new CodingHelper();
        #endregion

        #region  窗体加载事件
        private void WareHouseInventoryReportForm_Load(object sender, EventArgs e)
        {
            this.labelTitle.BackColor = Color.FromArgb(85, 177, 238);
            this.pictureBoxMax.BackColor = Color.FromArgb(85, 177, 238);
            this.pictureBoxMin.BackColor = Color.FromArgb(85, 177, 238);
            this.pictureBoxClose.BackColor = Color.FromArgb(85, 177, 238);
            //显示行号
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            //不可自动添加列
            this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            superGridControl1.HScrollBarVisible = true;
            //表格内容居中
            superGridControl1.DefaultVisualStyles.CellStyles.Default.Alignment =
            DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            InitDataGridView();//表格初始化
            
            this.textBoxpandiancode.Text = _inventoryCode;//盘点单号
            barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
            _Code.ValueFont = new Font("微软雅黑", 20);
            System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxpandiancode.Text, barcodeXYE.Code128.Encode.Code128A);
            picbpandianBarCode.Image = imgTemp;//条形码

            #region 加载盘点方案的数据
            DataTable dt = codeh.DataTableReCoding(si.GetList(999, ""));
            cbopandianidea.DisplayMember = "name";
            cbopandianidea.ValueMember = "code";
            cbopandianidea.DataSource = dt;
            this.cbopandianidea.Text = _storageName;//仓库名称
            superGridControl1.PrimaryGrid.DataSource = codeh.DataTableReCoding(whidi.Search(2, (XYEEncoding.strCodeHex(_storageCode))));
            superGridControl1.PrimaryGrid.EnsureVisible();
            InitDataGridView();
            try
            {
                GridRow gr = new GridRow();
                decimal tempAllzhucun = 0;
                decimal tempAllpandian = 0;
                decimal tempAllpanying = 0;
                decimal tempAllpankui = 0;
                //逐行统计数据总数
                for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                    tempAllzhucun += Convert.ToDecimal(tempGR["zhangcunnumber"].FormattedValue);
                    tempAllpandian += Convert.ToDecimal(tempGR["pandiannumber"].FormattedValue);
                    tempAllpanying += Convert.ToDecimal(tempGR["panyingnumber"].FormattedValue);
                    tempAllpankui += Convert.ToDecimal(tempGR["pankuinumber"].FormattedValue);
                }
                _ZhangCunShuLiang = tempAllzhucun;
                _PanDianShuLiang = tempAllpandian;
                _PanYingShuLiang = tempAllpanying;
                _PanKuiShuLiang = tempAllpankui;
                gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                gr["zhangcunnumber"].Value = _ZhangCunShuLiang.ToString();
                gr["pandiannumber"].Value = _PanDianShuLiang.ToString();
                gr["panyingnumber"].Value = _PanYingShuLiang.ToString();
                gr["pankuinumber"].Value = _PanKuiShuLiang.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("商品盘点表统计数量错误" + ex.Message);
            }
            #endregion

        }
        #endregion

        #region  下拉框选择改变事件
        private void comboBoxEx1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbopandianidea.Text == "")
            {
                return;
            }

            if (cbopandianidea.Text != "")
            {
                string code = cbopandianidea.SelectedValue.ToString();
                this.superGridControl1.PrimaryGrid.DataSource = null;
                superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
                superGridControl1.PrimaryGrid.DataSource = codeh.DataTableReCoding(whidi.Search(2,(XYEEncoding.strCodeHex(cbopandianidea.SelectedValue.ToString()))));
                superGridControl1.PrimaryGrid.EnsureVisible();
                InitDataGridView();
                try
                {
                    GridRow gr = new GridRow();
                    decimal tempAllzhucun = 0;
                    decimal tempAllpandian = 0;
                    decimal tempAllpanying = 0;
                    decimal tempAllpankui = 0;
                    //逐行统计数据总数
                    for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
                    {
                        GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                        tempAllzhucun += Convert.ToDecimal(tempGR["zhangcunnumber"].FormattedValue);
                        tempAllpandian += Convert.ToDecimal(tempGR["pandiannumber"].FormattedValue);
                        tempAllpanying += Convert.ToDecimal(tempGR["panyingnumber"].FormattedValue);
                        tempAllpankui += Convert.ToDecimal(tempGR["pankuinumber"].FormattedValue);
                    }
                    _ZhangCunShuLiang = tempAllzhucun;
                    _PanDianShuLiang = tempAllpandian;
                    _PanYingShuLiang = tempAllpanying;
                    _PanKuiShuLiang = tempAllpankui;
                    gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                    gr["zhangcunnumber"].Value = _ZhangCunShuLiang.ToString();
                    gr["pandiannumber"].Value = _PanDianShuLiang.ToString();
                    gr["panyingnumber"].Value = _PanYingShuLiang.ToString();
                    gr["pankuinumber"].Value = _PanKuiShuLiang.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("商品盘点报告表统计数量错误" + ex.Message);
                }
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
        /// <summary>
        /// 生成盘亏单的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonLoss_Click(object sender, EventArgs e)
        {
            WareHouseInventoryLossForm wareinventloss = new WareHouseInventoryLossForm();
            wareinventloss.StorageCode = _storageCode;
            wareinventloss.ShowDialog();
        }
        /// <summary>
        /// 生成盘盈单的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonProfit_Click(object sender, EventArgs e)
        {
            WareHouseInventoryProfit warehouseprofit = new WareHouseInventoryProfit();
            warehouseprofit.StorageCode = _storageCode;
            warehouseprofit.ShowDialog();
        }
    }
}
