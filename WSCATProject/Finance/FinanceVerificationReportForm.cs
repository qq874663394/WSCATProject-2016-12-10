using DevComponents.DotNetBar.SuperGrid;
using HelperUtility.Encrypt;
using InterfaceLayer.Finance;
using InterfaceLayer.Purchase;
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

namespace WSCATProject.Finance
{
    public partial class FinanceVerificationReportForm : Form
    {
        public FinanceVerificationReportForm()
        {
            InitializeComponent();
        }

        #region 初始化接口
        CodingHelper ch = new CodingHelper();
        FinanceCollectionInterface financeInterface = new FinanceCollectionInterface();
        SalesMainInterface salesMain = new SalesMainInterface();
        PurchaseMainInterface purchase = new PurchaseMainInterface();
        FinancePaymentInterface financePay = new FinancePaymentInterface();
        #endregion

        #region 数据字段
        /// <summary>
        /// 客户code
        /// </summary>
        private string _clientCode;

        public string ClientCode
        {
            get
            {
                return _clientCode;
            }

            set
            {
                _clientCode = value;
            }
        }

        /// <summary>
        /// 供应商code
        /// </summary>
        public string SupplierCode
        {
            get
            {
                return _supplierCode;
            }

            set
            {
                _supplierCode = value;
            }
        }



        private string _supplierCode;
        /// <summary>
        /// 销售单主表code
        /// </summary>
        private string _salerMainCode;

        public string SalerMainCode
        {
            get
            {
                return _salerMainCode;
            }

            set
            {
                _salerMainCode = value;
            }
        }

        public string PurchaseCode
        {
            get
            {
                return _purchaseCode;
            }

            set
            {
                _purchaseCode = value;
            }
        }

        private string _purchaseCode;
        DataTable dt = null;
        #endregion

        #region 初始化数据

        /// <summary>
        /// 初始化客户列表
        /// </summary>
        private void InitClientDataGridView()
        {
            GridColumn gc = null;

            superGridControlShangPing.PrimaryGrid.DataSource = null;
            superGridControlShangPing.PrimaryGrid.Columns.Clear();
            gc = new GridColumn();
            gc.DataPropertyName = "type";
            gc.Name = "type";
            gc.HeaderText = "单据类型";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "code";
            gc.Name = "code";
            gc.HeaderText = "单据编号";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "clientName";
            gc.Name = "clientName";
            gc.HeaderText = "客户名称";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "date";
            gc.Name = "date";
            gc.HeaderText = "日期";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "totalCollection";
            gc.Name = "totalCollection";
            gc.HeaderText = "总金额";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "totalCollection";
            gc.Name = "totalCollection";
            gc.HeaderText = "已核销金额";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.Name = "operation";
            gc.HeaderText = "未核销金额";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "remark";
            gc.Name = "remark";
            gc.HeaderText = "摘要";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "salesCode";
            gc.Name = "salesCode";
            gc.Visible = false;
            gc.HeaderText = "采购单code";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

        }

        private void InitShowDataGridView()
        {
            GridColumn gc = null;

            superGridControlShangPing.PrimaryGrid.DataSource = null;
            superGridControlShangPing.PrimaryGrid.Columns.Clear();
            gc = new GridColumn();
            gc.DataPropertyName = "type";
            gc.Name = "type";
            gc.HeaderText = "单据类型";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "code";
            gc.Name = "code";
            gc.HeaderText = "单据编号";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "clientName";
            gc.Name = "clientName";
            gc.HeaderText = "客户名称";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "date";
            gc.Name = "date";
            gc.HeaderText = "日期";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "oddAllMoney";
            gc.Name = "oddAllMoney";
            gc.HeaderText = "总金额";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "collectMoney";
            gc.Name = "collectMoney";
            gc.HeaderText = "已核销金额";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "lastMoney";
            gc.Name = "lastMoney";
            gc.HeaderText = "未核销金额";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "remark";
            gc.Name = "remark";
            gc.HeaderText = "摘要";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "salesCode";
            gc.Name = "salesCode";
            gc.Visible = false;
            gc.HeaderText = "采购单code";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);
        }

        /// <summary>
        /// 初始化供应商列表
        /// </summary>
        private void InitSupplierDataGridView()
        {
            GridColumn gc = null;

            superGridControlShangPing.PrimaryGrid.DataSource = null;
            superGridControlShangPing.PrimaryGrid.Columns.Clear();
            gc = new GridColumn();
            gc.DataPropertyName = "type";
            gc.Name = "type";
            gc.HeaderText = "单据类型";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "code";
            gc.Name = "code";
            gc.HeaderText = "单据编号";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "supplierName";
            gc.Name = "supplierName";
            gc.HeaderText = "供应商姓名";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "date";
            gc.Name = "date";
            gc.HeaderText = "日期";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "totalCollection";
            gc.Name = "totalCollection";
            gc.HeaderText = "总金额";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "totalCollection";
            gc.Name = "totalCollection";
            gc.HeaderText = "已核销金额";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.Name = "operation";
            gc.HeaderText = "未核销金额";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "remark";
            gc.Name = "remark";
            gc.HeaderText = "摘要";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "purchaseCode";
            gc.Name = "purchaseCode";
            gc.Visible = false;
            gc.HeaderText = "采购单code";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);
        }

        private void InitSupplierShowDataGridView()
        {
            GridColumn gc = null;

            superGridControlShangPing.PrimaryGrid.DataSource = null;
            superGridControlShangPing.PrimaryGrid.Columns.Clear();
            gc = new GridColumn();
            gc.DataPropertyName = "type";
            gc.Name = "type";
            gc.HeaderText = "单据类型";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "code";
            gc.Name = "code";
            gc.HeaderText = "单据编号";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "supplierName";
            gc.Name = "supplierName";
            gc.HeaderText = "供应商姓名";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "date";
            gc.Name = "date";
            gc.HeaderText = "日期";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "oddMoney";
            gc.Name = "oddMoney";
            gc.HeaderText = "总金额";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "inMoney";
            gc.Name = "inMoney";
            gc.HeaderText = "已核销金额";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "lastMoney";
            gc.Name = "lastMoney";
            gc.HeaderText = "未核销金额";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "remark";
            gc.Name = "remark";
            gc.HeaderText = "摘要";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "purchaseCode";
            gc.Name = "purchaseCode";
            gc.Visible = false;
            gc.HeaderText = "采购单code";
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);
        }

        #endregion

        private void FinanceVerificationReportForm_Load(object sender, EventArgs e)
        {
            superGridControlShangPing.PrimaryGrid.AutoGenerateColumns = false;
            //内容居中
            superGridControlShangPing.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            superGridControlShangPing.PrimaryGrid.SelectionGranularity = SelectionGranularity.Row;
            //显示行号
            superGridControlShangPing.PrimaryGrid.ShowRowGridIndex = true;
            if ( _clientCode != null)
            {
                InitClientDataGridView();
                dt = ch.DataTableReCoding(financeInterface.GetList(0, XYEEncoding.strCodeHex(_clientCode)));
                if (dt.Rows.Count > 0)
                {
                    superGridControlShangPing.PrimaryGrid.DataSource = dt;
                    return;
                }
                else
                {
                    MessageBox.Show("查无数据!");
                    this.Close();
                }
            }
            if (_salerMainCode != null)
            {
                InitShowDataGridView();
                dt = ch.DataTableReCoding(salesMain.GetList(1, XYEEncoding.strCodeHex(_salerMainCode)));
                if (dt.Rows.Count > 0)
                {
                    superGridControlShangPing.PrimaryGrid.DataSource = dt;
                    return;
                }
                else
                {
                    MessageBox.Show("查无数据!");
                    this.Close();
                }
            }

            if (_supplierCode!=null)
            {
                InitSupplierDataGridView();
                dt = ch.DataTableReCoding(financePay.GetList(0, XYEEncoding.strCodeHex(_supplierCode)));
                if (dt.Rows.Count > 0)
                {
                    superGridControlShangPing.PrimaryGrid.DataSource = dt;
                    return;
                }
                else
                {
                    MessageBox.Show("查无数据!");
                    this.Close();
                }
            }

            if (_purchaseCode!=null)
            {
                InitSupplierShowDataGridView();
                dt = ch.DataTableReCoding(purchase.GetList(3, XYEEncoding.strCodeHex(_purchaseCode)));
                if (dt.Rows.Count > 0)
                {
                    superGridControlShangPing.PrimaryGrid.DataSource = dt;
                    return;
                }
                else
                {
                    MessageBox.Show("查无数据!");
                    this.Close();
                }
            }

        }

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

        #region 设置窗体无边框可以拖动
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;



        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void PurchaseOrderReportForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion

        /// <summary>
        /// 双击行的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControlShangPing_CellDoubleClick(object sender, GridCellDoubleClickEventArgs e)
        {
            try
            {
                if (superGridControlShangPing.PrimaryGrid.GetSelectedRows() != null)
                {
                    SelectedElementCollection col = superGridControlShangPing.PrimaryGrid.GetSelectedRows();
                    if (col.Count > 0)
                    {
                        GridRow row = col[0] as GridRow;
                        string mainCode = row.Cells["code"].Value.ToString();
                        FinanceVerificationForm financeVerifict = (FinanceVerificationForm)this.Owner;
                        if (_clientCode!=null)
                        {
                            financeVerifict.FuKuanCode = mainCode;
                        }
                        if (_supplierCode!=null)
                        {
                            financeVerifict.ShouKuanCode = mainCode;
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("请先选择要操作的行！");
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：3202-尝试双击表格选中行失败！请检查：" + ex.Message, "销售订单序时薄温馨提示！");
            }
        }
    }
}
