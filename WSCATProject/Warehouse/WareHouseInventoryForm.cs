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
using Model;

namespace WSCATProject.Warehouse
{
    public partial class WareHouseInventoryForm : Form
    {
        public WareHouseInventoryForm()
        {
            InitializeComponent();
        }
        #region 调用接口以及加密解密的方法
        StorageInterface si = new StorageInterface();
        CodingHelper codeh = new CodingHelper();
        WarehouseMainInterface warehousemain = new WarehouseMainInterface();
        #endregion

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
        /// 盘点code
        /// </summary>
        private string _PanDianCode;
        #endregion

        #region 设置窗体无边框可以拖动
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void WareHouseInventoryForm_MouseDown(object sender, MouseEventArgs e)
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

        private void WareHouseInventoryForm_Load(object sender, EventArgs e)
        {
            CodingHelper ch = new CodingHelper();
            WarehouseInventoryInterface wii = new WarehouseInventoryInterface();
            this.labelTitle.BackColor = Color.FromArgb(85, 177, 238);
            this.pictureBoxMax.BackColor = Color.FromArgb(85, 177, 238);
            this.pictureBoxMin.BackColor = Color.FromArgb(85, 177, 238);
            this.pictureBoxClose.BackColor = Color.FromArgb(85, 177, 238);    

            //设置盘点数量可输入的最大值和最小值
            GridDoubleInputEditControl gdiecNumber = superGridControl1.PrimaryGrid.Columns["pandiannumber"].EditControl as GridDoubleInputEditControl;
            gdiecNumber.MinValue = 0;
            gdiecNumber.MaxValue = 999999999;

            //调用表格初始化
            superGridControl1.HScrollBarVisible = true;
            superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            superGridControl1.DefaultVisualStyles.CellStyles.Default.Alignment =
            DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            InitDataGridView();

            //生成code 和显示条形码
            _PanDianCode = BuildCode.ModuleCode("WII");
            textBoxpandiancode.Text = _PanDianCode;
            barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
            _Code.ValueFont = new Font("微软雅黑", 20);
            System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxpandiancode.Text, barcodeXYE.Code128.Encode.Code128A);
            picbpandianBarCode.Image = imgTemp;

            #region 盘点方案
            DataTable dt = codeh.DataTableReCoding(si.GetList(999, ""));
            cbopandianidea.DisplayMember = "name";
            cbopandianidea.ValueMember = "code";
            cbopandianidea.DataSource = dt;
            string code = cbopandianidea.SelectedValue.ToString();
            superGridControl1.PrimaryGrid.DataSource = codeh.DataTableReCoding(warehousemain.GetMaterialByMain(XYEEncoding.strCodeHex(code)));
            superGridControl1.PrimaryGrid.EnsureVisible();
            InitDataGridView();
            try
            {
                GridRow gr = new GridRow();
                decimal tempAllzhucun = 0;
                decimal tempAllpandian = 0;
                //decimal tempAllpanying = 0;
                //decimal tempAllpankui = 0;
                //逐行统计数据总数
                for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                    tempAllzhucun += Convert.ToDecimal(tempGR["zhangcunnumber"].FormattedValue);
                    tempAllpandian += Convert.ToDecimal(tempGR["pandiannumber"].FormattedValue);
                    //tempAllpanying += Convert.ToDecimal(tempGR["panyingnumber"].FormattedValue);
                    //tempAllpankui += Convert.ToDecimal(tempGR["pankuinumber"].FormattedValue);
                }
                _ZhangCunShuLiang = tempAllzhucun;
                _PanDianShuLiang = tempAllpandian;
                //_PanYingShuLiang = tempAllpanying;
                //_PanKuiShuLiang = tempAllpankui;
                gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                gr["zhangcunnumber"].Value = _ZhangCunShuLiang.ToString();
                gr["pandiannumber"].Value = _PanDianShuLiang.ToString();
                //gr["panyingnumber"].Value = _PanYingShuLiang.ToString();
                //gr["pankuinumber"].Value = _PanKuiShuLiang.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("商品盘点表统计数量错误" + ex.Message);
            }
            #endregion

        }

        #region  下拉框选择改变事件
        private void cbopandianidea_SelectedValueChanged(object sender, EventArgs e)
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
                superGridControl1.PrimaryGrid.DataSource = codeh.DataTableReCoding(warehousemain.GetMaterialByMain(XYEEncoding.strCodeHex(cbopandianidea.SelectedValue.ToString())));
                superGridControl1.PrimaryGrid.EnsureVisible();
                InitDataGridView();
                //计算数量
                try
                {
                    GridRow gr = new GridRow();
                    decimal tempAllzhucun = 0;
                    decimal tempAllpandian = 0;
                    //decimal tempAllpanying = 0;
                    //decimal tempAllpankui = 0;
                    //逐行统计数据总数
                    for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
                    {
                        GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                        tempAllzhucun += Convert.ToDecimal(tempGR["zhangcunnumber"].FormattedValue);
                        tempAllpandian += Convert.ToDecimal(tempGR["pandiannumber"].FormattedValue);
                        //tempAllpanying += Convert.ToDecimal(tempGR["panyingnumber"].FormattedValue);
                        //tempAllpankui += Convert.ToDecimal(tempGR["pankuinumber"].FormattedValue);
                    }
                    _ZhangCunShuLiang = tempAllzhucun;
                    _PanDianShuLiang = tempAllpandian;
                    //_PanYingShuLiang = tempAllpanying;
                    //_PanKuiShuLiang = tempAllpankui;
                    gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                    gr["zhangcunnumber"].Value = _ZhangCunShuLiang.ToString();
                    gr["pandiannumber"].Value = _PanDianShuLiang.ToString();
                    //gr["panyingnumber"].Value = _PanYingShuLiang.ToString();
                    //gr["pankuinumber"].Value = _PanKuiShuLiang.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("商品盘点表统计数量错误" + ex.Message);
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
            //账存数量
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

        private void InitSupGrid()
        {
            GridColumn gc = null;

            gc = new GridColumn();
            gc.DataPropertyName = "storageName";
            gc.Name = "storageName";
            gc.HeaderText = "仓库";
            gc.Width = 80;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "materialDaima";
            gc.Name = "materialDaima";
            gc.HeaderText = "商品代码";
            gc.Width = 120;
            gc.AutoSizeMode = ColumnAutoSizeMode.Fill;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "name";
            gc.Name = "name";
            gc.HeaderText = "商品名称";
            gc.Width = 140;
            gc.AutoSizeMode = ColumnAutoSizeMode.Fill;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "model";
            gc.Name = "model";
            gc.HeaderText = "规格型号";
            gc.Width = 130;
            gc.AutoSizeMode = ColumnAutoSizeMode.Fill;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "barcode";
            gc.Name = "barcode";
            gc.HeaderText = "条形码";
            gc.Width = 150;
            gc.AutoSizeMode = ColumnAutoSizeMode.Fill;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "productionDate";
            gc.Name = "productionDate";
            gc.HeaderText = "采购/生产日期";
            gc.Width = 70;
            gc.HeaderStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "qualityDate";
            gc.Name = "qualityDate";
            gc.HeaderText = "保质期(天)";
            gc.Width = 50;
            gc.HeaderStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "unit";
            gc.Name = "unit";
            gc.HeaderText = "单位";
            gc.Width = 70;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "number";
            gc.Name = "number";
            gc.HeaderText = "账面数量";
            gc.Width = 80;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.Name = "effectiveDate";
            gc.HeaderText = "盘点数量";
            gc.Width = 80;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "remark";
            gc.Name = "remark";
            gc.HeaderText = "备注";
            gc.Width = 110;
            superGridControl1.PrimaryGrid.Columns.Add(gc);
        }

        #endregion

        #region superGridControl单元格验证事件
        private void superGridControl1_CellValidated(object sender, GridCellValidatedEventArgs e)
        {
            GridRow gr = e.GridPanel.Rows[e.GridCell.RowIndex] as GridRow;
            decimal tempAllzhucun = 0;
            decimal tempAllpandian = 0;
            //decimal tempAllpanying = 0;
            //decimal tempAllpankui = 0;
            //计算盘盈、盘亏的数量
            try
            {
                decimal zhucunnumber = Convert.ToDecimal(gr.Cells["zhangcunnumber"].FormattedValue);
                decimal pandiannumber = Convert.ToDecimal(gr.Cells["pandiannumber"].FormattedValue);
                decimal panying = pandiannumber - zhucunnumber;
                if (panying > 0)
                {
                    gr.Cells["panyingnumber"].Value = panying;
                    gr.Cells["pankuinumber"].Value = "0.00";
                }
                if (panying < 0)
                {
                    gr.Cells["pankuinumber"].Value = panying;
                    gr.Cells["panyingnumber"].Value = "0.00";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("计算盘盈，盘亏数量有误！请检查：" + ex.Message);
            }

            try
            {
                //逐行统计数据总数
                for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                    tempAllzhucun += Convert.ToDecimal(tempGR["zhangcunnumber"].FormattedValue);
                    tempAllpandian += Convert.ToDecimal(tempGR["pandiannumber"].FormattedValue);
                    //tempAllpanying += Convert.ToDecimal(tempGR["panyingnumber"].FormattedValue);
                    //tempAllpankui += Convert.ToDecimal(tempGR["pankuinumber"].FormattedValue);
                }
                _ZhangCunShuLiang = tempAllzhucun;
                _PanDianShuLiang = tempAllpandian;
                //_PanYingShuLiang = tempAllpanying;
                //_PanKuiShuLiang = tempAllpankui;
                gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                gr["zhangcunnumber"].Value = _ZhangCunShuLiang.ToString();
                gr["pandiannumber"].Value = _PanDianShuLiang.ToString();
                //gr["panyingnumber"].Value = _PanYingShuLiang.ToString();
                //gr["pankuinumber"].Value = _PanKuiShuLiang.ToString();

                //获取一行数据，添加进数据库
                GridRow gr1 = e.GridPanel.Rows[e.GridCell.RowIndex] as GridRow;
                WarehouseInventoryDetail warehouseinv = new WarehouseInventoryDetail();
                //warehouseinv.cause = "";
                //warehouseinv.checkNumber = "";
                //warehouseinv.code = "";
                //warehouseinv.curNumber = "";
                //warehouseinv.isClear = 1;
                //warehouseinv.lostMoney = "";
                //warehouseinv.lostNumber = "";
                //warehouseinv.materialCode = "";
                //warehouseinv.materiaModel = "";
                //warehouseinv.materiaName = "";
                //warehouseinv.materiaUnit = "";
                //warehouseinv.price = "";
                //warehouseinv.remark = "";
                //warehouseinv.reserved1 = "";
                //warehouseinv.reserved2 = "";
                //warehouseinv.updateDate = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("商品盘点表统计数量错误" + ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 盘点编制的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonbianzhi_Click(object sender, EventArgs e)
        {
            WareHouseInventoryReportForm warehouseinvent = new WareHouseInventoryReportForm();
            warehouseinvent.StorageCode = cbopandianidea.SelectedValue.ToString();
            warehouseinvent.StorageName = cbopandianidea.Text;
            warehouseinvent.InventoryCode = this.textBoxpandiancode.Text;
            warehouseinvent.Show();
        }
    }
}