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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;

namespace WSCATProject.Warehouse
{
    public partial class WareHouseAllotForm : TestForm
    {
        public WareHouseAllotForm()
        {
            InitializeComponent();
        }

        #region 调用接口以及加密解密方法
        CodingHelper ch = new CodingHelper();
        EmpolyeeInterface employee = new EmpolyeeInterface();
        StorageInterface storage = new StorageInterface();
        WarehouseMainInterface waremain = new WarehouseMainInterface();
        #endregion

        #region  数据字段
        /// <summary>
        /// 所有业务员
        /// </summary>
        private DataTable _AllEmployee = null;
        /// <summary>
        /// 点击的项,1业务员
        /// </summary>
        private int _Click = 0;
        /// <summary>
        /// 调拨单code
        /// </summary>
        private string _WareHouseAllotCode;
        /// <summary>
        /// 所有商品列表
        /// </summary>
        private DataTable _AllMaterial = null;
        /// <summary>
        /// 所有仓库列表
        /// </summary>
        private DataTable _AllStorage = null;
        /// <summary>
        /// 保存仓库的Code
        /// </summary>
        private string _StorageCode = "";
        /// <summary>
        /// 保存商品Code
        /// </summary>
        private string _MaterialCode = "";
        /// <summary>
        /// 调出仓库列表
        /// </summary>
        private KeyValuePair<string, string> _ClickOutStorageList;
        /// <summary>
        /// 调入仓库列表
        /// </summary>
        private KeyValuePair<string, string> _ClickInStorageList;
        //保存调入仓库
        private string _InStorage;
        /// <summary>
        /// 保存调出仓库
        /// </summary>
        private string _OutStorage;
        /// <summary>
        /// 保存仓库状态 1、调出仓库 2、调入仓库 3、商品列表
        /// </summary>
        private int _StorageState;
        /// <summary>
        /// 统计数量
        /// </summary>
        private decimal _MaterialNumber;
        /// <summary>
        /// 统计调出金额
        /// </summary>
        private decimal _MaterialMoney;
        /// <summary>
        /// 统计调入金额
        /// </summary>
        private decimal _MaterInMoney;
        #endregion

        #region 修改Panel的边框颜色
        /// <summary>
        /// 修改Panel的边框颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                               this.panel2.ClientRectangle,
                               Color.White,
                               1,
                               ButtonBorderStyle.Solid,
                               Color.FromArgb(85, 177, 238),
                               1,
                               ButtonBorderStyle.Solid,
                               Color.White,
                               1,
                               ButtonBorderStyle.Solid,
                               Color.White,
                               1,
                               ButtonBorderStyle.Solid);
        }

        #endregion

        private void WareHouseAllotForm_Load(object sender, EventArgs e)
        {
            try
            {
                //业务员
                _AllEmployee = employee.SelSupplierTable(false);
                //仓库
                _AllStorage = storage.GetList(00, "");

                //调入单价
                GridDoubleInputEditControl diaoruprice = superGridControl1.PrimaryGrid.Columns["gridColumnpricein"].EditControl as GridDoubleInputEditControl;
                diaoruprice.MinValue = 0;
                diaoruprice.MaxValue = 999999999;
                //数量
                GridDoubleInputEditControl gdiecNumber = superGridControl1.PrimaryGrid.Columns["gridColumnnumber"].EditControl as GridDoubleInputEditControl;
                gdiecNumber.MinValue = 0;
                gdiecNumber.MaxValue = 999999999;

                //禁用自动创建列
                dataGridView1.AutoGenerateColumns = false;
                dataGridViewFujia.AutoGenerateColumns = false;
                superGridControl1.HScrollBarVisible = true;
                //绑定事件 双击事填充内容并隐藏列表
                dataGridViewFujia.CellDoubleClick += DataGridViewFujia_CellDoubleClick;
                dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
                // 将dataGridView中的内容居中显示
                dataGridViewFujia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //显示行号
                superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
                //内容居中
                superGridControl1.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                //调用合计行数据
                InitDataGridView();
                cbotype.SelectedIndex = 0;
                //生成code 和显示条形码
                _WareHouseAllotCode = BuildCode.ModuleCode("WIA");
                textBoxOddNumbers.Text = _WareHouseAllotCode;
                barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
                _Code.ValueFont = new Font("微软雅黑", 20);
                System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
                pictureBox9.Image = imgTemp;

                toolStripButtonsave.Click += ToolStripButtonsave_Click;//保存按钮
                toolStripButtonshen.Click += ToolStripButtonshen_Click;//审核按钮

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-初始化调拨窗体数据错误" + ex.Message);
            }
        }
        /// <summary>
        /// 审核按钮的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonshen_Click(object sender, EventArgs e)
        {
            //非空验证
            isNUllValidate();
            //获得界面上的数据,准备传给base层新增数据
            //WarehouseInInterface warehouseInterface = new WarehouseInInterface();
            //调拨单
            WarehouseAllot warehouseallot = new WarehouseAllot();
            //调拨商品列表
            List<WarehouseAllotDetail> wareHouseallList = new List<WarehouseAllotDetail>();
            try
            {
                warehouseallot.allotGap = labtextboxTop7.Text == "" ? 0.0M : Convert.ToDecimal(labtextboxTop7.Text);
                warehouseallot.allotType = XYEEncoding.strCodeHex(cbotype.Text);
                warehouseallot.cause = labtextboxTop6.Text == "" ? "" : XYEEncoding.strCodeHex(labtextboxTop6.Text);
                warehouseallot.checkMan = ltxtbShengHeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbShengHeMan.Text);
                warehouseallot.checkState = 1;
                warehouseallot.code = XYEEncoding.strCodeHex(textBoxOddNumbers.Text);
                warehouseallot.date = dateTimePicker1.Value;
                warehouseallot.isClear = 1;
                warehouseallot.makeMan = ltxtbSalsMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbSalsMan.Text);
                warehouseallot.operationMan = ltxtbMakeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbMakeMan.Text);
                warehouseallot.remark = "";
                warehouseallot.reserved1 = "";
                warehouseallot.reserved2 = "";
                warehouseallot.updateDate = DateTime.Now;

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:2104;尝试创建入库单商品数据出错,请检查输入" + ex.Message, "入库单温馨提示");
                return;
            }
            try
            {
                //获得商品列表数据,准备传给base层新增数据
                GridRow g = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
                GridItemsCollection grs = superGridControl1.PrimaryGrid.Rows;
                int i = 0;
                DateTime nowDataTime = DateTime.Now;
                foreach (GridRow gr in grs)
                {
                    if (gr["gridColumnname"].Value != null)
                    {

                        i++;
                        WarehouseAllotDetail warehousealld = new WarehouseAllotDetail();
                        warehousealld.allotInPrice = gr["gridColumnpricein"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnpricein"].Value.ToString());
                        warehousealld.allotInSummoney = gr["gridColumnmoneyin"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnmoneyin"].Value.ToString());
                        warehousealld.allotOutPrice = gr["gridColumnpriceout"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnpriceout"].Value.ToString());
                        warehousealld.allotOutSummoney = gr["gridColumnmoneyout"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnmoneyout"].Value.ToString());
                        warehousealld.barcode = gr["gridColumntiaoxingma"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumntiaoxingma"].Value.ToString());
                        warehousealld.code = XYEEncoding.strCodeHex(textBoxOddNumbers.Text) + i.ToString();
                        warehousealld.curNumber = gr["gridColumnnumber"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnnumber"].Value.ToString());
                        warehousealld.effectiveDate = gr["gridColumnyouxiao"].Value == DBNull.Value ? Convert.ToDateTime("1990-01-01") : Convert.ToDateTime(gr["gridColumnyouxiao"].Value);
                        warehousealld.isClear = 1;
                        warehousealld.mainCode = XYEEncoding.strCodeHex(textBoxOddNumbers.Text);
                        warehousealld.materialCode = gr["gridColumnMaterialcode"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnMaterialcode"].Value.ToString());
                        warehousealld.materialDaima = gr["material"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["material"].Value.ToString());
                        warehousealld.materiaModel = gr["gridColumnmodel"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnmodel"].Value.ToString());
                        warehousealld.materiaName = gr["gridColumnname"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnname"].Value.ToString());
                        warehousealld.materiaUnit = gr["gridColumnunit"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnunit"].Value.ToString());
                        warehousealld.productionDate = gr["gridColumnshengchandate"].Value == DBNull.Value ? Convert.ToDateTime("1990-01-01") : Convert.ToDateTime(gr["gridColumnshengchandate"].Value);
                        warehousealld.qualityDate = gr["gridColumnbaozhi"].Value.ToString() == "" ? 0.0M : Convert.ToDecimal(gr["gridColumnbaozhi"].Value.ToString());
                        warehousealld.remark = gr["gridColumnremark"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnremark"].Value.ToString());
                        warehousealld.reserved1 = "";
                        warehousealld.reserved2 = "";
                        warehousealld.stoIncode = _InStorage == "" ? "" : XYEEncoding.strCodeHex(_InStorage);
                        warehousealld.stoInName = gr["gridColumnStockIn"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnStockIn"].Value.ToString());
                        warehousealld.stoOutcode = _OutStorage == "" ? "" : XYEEncoding.strCodeHex(_OutStorage);
                        warehousealld.stoOutName = gr["gridColumnStock"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnStock"].Value.ToString());
                        warehousealld.updateDate = DateTime.Now;
                        GridRow dr = superGridControl1.PrimaryGrid.Rows[0] as GridRow;
                        wareHouseallList.Add(warehousealld);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2105-尝试创建入库单详商品数据出错,请检查输入" + ex.Message, "入库单温馨提示");
                return;
            }
            //增加一条入库单和入库单详细数据
            //object warehouseInResult = warehouseInterface.AddWarehouseOrToDetail(warehouseIn, wareHouseInList);
            //this.textBoxid.Text = warehouseInResult.ToString();
            //if (warehouseInResult != null)
            //{
            //    MessageBox.Show("新增入库数据成功", "入库单温馨提示");
            //}
        }

        /// <summary>
        /// 保存按钮的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonsave_Click(object sender, EventArgs e)
        {
            //非空验证
            isNUllValidate();
            //获得界面上的数据,准备传给base层新增数据
            //WarehouseInInterface warehouseInterface = new WarehouseInInterface();
            //调拨单
            WarehouseAllot warehouseallot = new WarehouseAllot();
            //调拨商品列表
            List<WarehouseAllotDetail> wareHouseallList = new List<WarehouseAllotDetail>();
            try
            {
                warehouseallot.allotGap = labtextboxTop7.Text == "" ? 0.0M : Convert.ToDecimal(labtextboxTop7.Text);
                warehouseallot.allotType = XYEEncoding.strCodeHex(cbotype.Text);
                warehouseallot.cause = labtextboxTop6.Text == "" ? "" : XYEEncoding.strCodeHex(labtextboxTop6.Text);
                warehouseallot.checkMan = ltxtbShengHeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbShengHeMan.Text);
                warehouseallot.checkState = 0;
                warehouseallot.code = XYEEncoding.strCodeHex(textBoxOddNumbers.Text);
                warehouseallot.date = dateTimePicker1.Value;
                warehouseallot.isClear = 1;
                warehouseallot.makeMan = ltxtbSalsMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbSalsMan.Text);
                warehouseallot.operationMan = ltxtbMakeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbMakeMan.Text);
                warehouseallot.remark = "";
                warehouseallot.reserved1 = "";
                warehouseallot.reserved2 = "";
                warehouseallot.updateDate = DateTime.Now;

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:2104;尝试创建入库单商品数据出错,请检查输入" + ex.Message, "入库单温馨提示");
                return;
            }
            try
            {
                //获得商品列表数据,准备传给base层新增数据
                GridRow g = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
                GridItemsCollection grs = superGridControl1.PrimaryGrid.Rows;
                int i = 0;
                DateTime nowDataTime = DateTime.Now;
                foreach (GridRow gr in grs)
                {
                    if (gr["gridColumnname"].Value != null)
                    {
                        i++;
                        WarehouseAllotDetail warehousealld = new WarehouseAllotDetail();
                        warehousealld.allotInPrice = gr["gridColumnpricein"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnpricein"].Value.ToString());
                        warehousealld.allotInSummoney = gr["gridColumnmoneyin"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnmoneyin"].Value.ToString());
                        warehousealld.allotOutPrice = gr["gridColumnpriceout"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnpriceout"].Value.ToString());
                        warehousealld.allotOutSummoney = gr["gridColumnmoneyout"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnmoneyout"].Value.ToString());
                        warehousealld.barcode = gr["gridColumntiaoxingma"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumntiaoxingma"].Value.ToString());
                        warehousealld.code = XYEEncoding.strCodeHex(textBoxOddNumbers.Text) + i.ToString();
                        warehousealld.curNumber = gr["gridColumnnumber"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnnumber"].Value.ToString());
                        warehousealld.effectiveDate = gr["gridColumnyouxiao"].Value == DBNull.Value ? Convert.ToDateTime("1990-01-01") : Convert.ToDateTime(gr["gridColumnyouxiao"].Value);
                        warehousealld.isClear = 1;
                        warehousealld.mainCode = XYEEncoding.strCodeHex(textBoxOddNumbers.Text);
                        warehousealld.materialCode = gr["gridColumnMaterialcode"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnMaterialcode"].Value.ToString());
                        warehousealld.materialDaima = gr["material"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["material"].Value.ToString());
                        warehousealld.materiaModel = gr["gridColumnmodel"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnmodel"].Value.ToString());
                        warehousealld.materiaName = gr["gridColumnname"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnname"].Value.ToString());
                        warehousealld.materiaUnit = gr["gridColumnunit"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnunit"].Value.ToString());
                        warehousealld.productionDate = gr["gridColumnshengchandate"].Value == DBNull.Value ? Convert.ToDateTime("1990-01-01") : Convert.ToDateTime(gr["gridColumnshengchandate"].Value);
                        warehousealld.qualityDate = gr["gridColumnbaozhi"].Value.ToString() == "" ? 0.0M : Convert.ToDecimal(gr["gridColumnbaozhi"].Value.ToString());
                        warehousealld.remark = gr["gridColumnremark"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnremark"].Value.ToString());
                        warehousealld.reserved1 = "";
                        warehousealld.reserved2 = "";
                        warehousealld.stoIncode = _InStorage == "" ? "" : XYEEncoding.strCodeHex(_InStorage);
                        warehousealld.stoInName = gr["gridColumnStockIn"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnStockIn"].Value.ToString());
                        warehousealld.stoOutcode = _OutStorage == "" ? "" : XYEEncoding.strCodeHex(_OutStorage);
                        warehousealld.stoOutName = gr["gridColumnStock"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnStock"].Value.ToString());
                        warehousealld.updateDate = DateTime.Now;
                        GridRow dr = superGridControl1.PrimaryGrid.Rows[0] as GridRow;
                        wareHouseallList.Add(warehousealld);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2105-尝试创建入库单详商品数据出错,请检查输入" + ex.Message, "入库单温馨提示");
                return;
            }
            //增加一条入库单和入库单详细数据
            //object warehouseInResult = warehouseInterface.AddWarehouseOrToDetail(warehouseIn, wareHouseInList);
            //this.textBoxid.Text = warehouseInResult.ToString();
            //if (warehouseInResult != null)
            //{
            //    MessageBox.Show("新增入库数据成功", "入库单温馨提示");
            //}
        }

        #region  初始化数据

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
            gr.Cells["gridColumnStock"].Value = "合计";
            gr.Cells["gridColumnStock"].CellStyles.Default.Alignment =
                DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            //数量
            gr.Cells["gridColumnnumber"].Value = 0;
            gr.Cells["gridColumnnumber"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnnumber"].CellStyles.Default.Background.Color1 = Color.Orange;
            //调出金额
            gr.Cells["gridColumnmoneyout"].Value = 0;
            gr.Cells["gridColumnmoneyout"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnmoneyout"].CellStyles.Default.Background.Color1 = Color.Orange;
            //调入金额
            gr.Cells["gridColumnmoneyin"].Value = 0;
            gr.Cells["gridColumnmoneyin"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnmoneyin"].CellStyles.Default.Background.Color1 = Color.Orange;
        }

        /// <summary>
        /// 非空验证
        /// </summary>
        private void isNUllValidate()
        {
            //if (comboBoxEx1.Text.Trim() == null)
            //{
            //    MessageBox.Show("出库类别不能为空！");
            //}
            if (ltxtbSalsMan.Text.Trim() == null)
            {
                MessageBox.Show("业务员不能为空！");
            }

        }

        /// <summary>
        /// 初始化业务员
        /// </summary>
        private void InitEmployee()
        {
            try
            {
                if (_Click != 1)
                {
                    _Click = 1;
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

                    resizablePanel1.Location = new Point(234, 420);
                    dataGridViewFujia.DataSource = ch.DataTableReCoding(_AllEmployee);
                    resizablePanel1.Visible = true;

                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        resizablePanel1.Location = new Point(230, 670);
                        return;
                    }
                    if (this.WindowState == FormWindowState.Normal)
                    {
                        resizablePanel1.Location = new Point(230, 420);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-初始化业务员数据错误" + ex.Message);
            }
        }

        /// <summary>
        /// 初始化仓库列和数据
        /// </summary>
        private void InitStorageList()
        {
            if (_Click != 2)
            {
                _Click = 2;
                dataGridViewFujia.DataSource = null;
                dataGridViewFujia.Columns.Clear();

                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "code";
                dgvc.Visible = false;
                dgvc.HeaderText = "code";
                dgvc.DataPropertyName = "code";
                dataGridViewFujia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "name";
                dgvc.Visible = true;
                dgvc.HeaderText = "仓库名称";
                dgvc.DataPropertyName = "name";
                dataGridViewFujia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "address";
                dgvc.Visible = true;
                dgvc.HeaderText = "仓库地址";
                dgvc.DataPropertyName = "address";
                dataGridViewFujia.Columns.Add(dgvc);

                //查询仓库的方法
                dataGridViewFujia.DataSource = ch.DataTableReCoding(_AllStorage);
            }
        }

        /// <summary>
        /// 初始化商品下拉别表的数据
        /// </summary>
        private void InitMaterialDataGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();

            DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "remark";
            dgvc.Visible = false;
            dgvc.HeaderText = "备注";
            dgvc.DataPropertyName = "remark";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "price";
            dgvc.Visible = false;
            dgvc.HeaderText = "单价";
            dgvc.DataPropertyName = "price";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "currentNumber";
            dgvc.Visible = false;
            dgvc.HeaderText = "数量";
            dgvc.DataPropertyName = "currentNumber";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "unit";
            dgvc.Visible = false;
            dgvc.HeaderText = "单位";
            dgvc.DataPropertyName = "unit";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "productionDate";
            dgvc.Visible = false;
            dgvc.HeaderText = "生产/采购日期";
            dgvc.DataPropertyName = "productionDate";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "qualityDate";
            dgvc.Visible = false;
            dgvc.HeaderText = "保质期";
            dgvc.DataPropertyName = "qualityDate";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "effectiveDate";
            dgvc.Visible = false;
            dgvc.HeaderText = "有效期至";
            dgvc.DataPropertyName = "effectiveDate";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "code";
            dgvc.Visible = false;
            dgvc.HeaderText = "商品code";
            dgvc.DataPropertyName = "code";
            dataGridView1.Columns.Add(dgvc);
            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "storageCode";
            dgvc.Visible = false;
            dgvc.HeaderText = "仓库code";
            dgvc.DataPropertyName = "storageCode";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "storageName";
            dgvc.Visible = false;
            dgvc.HeaderText = "仓库名称";
            dgvc.DataPropertyName = "storageName";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "materialDaima";
            dgvc.Visible = true;
            dgvc.HeaderText = "商品代码";
            dgvc.DataPropertyName = "materialDaima";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "name";
            dgvc.Visible = true;
            dgvc.HeaderText = "商品名称";
            dgvc.DataPropertyName = "name";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "model";
            dgvc.Visible = true;
            dgvc.HeaderText = "规格型号";
            dgvc.DataPropertyName = "model";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "barCode";
            dgvc.Visible = true;
            dgvc.HeaderText = "条码";
            dgvc.DataPropertyName = "barCode";
            dataGridView1.Columns.Add(dgvc);

            dataGridView1.DataSource = ch.DataTableReCoding(_AllMaterial);

        }
        /// <summary>
        /// 初始化窗体控件可用不可用
        /// </summary>
        private void InitForm()
        {
            cbotype.Enabled = false;
            labtextboxTop6.ReadOnly = true;
            superGridControl1.PrimaryGrid.ReadOnly = true;
            dateTimePicker1.Enabled = false;
            textBoxOddNumbers.ReadOnly = true;
            ltxtbSalsMan.ReadOnly = true;
            pictureBox5.Enabled = false;
            ltxtbMakeMan.ReadOnly = true;
            ltxtbShengHeMan.ReadOnly = true;
            this.pictureBox6.Image = Properties.Resources.审核;
        }
        #endregion

        #region 小箭头图标和表格数据的点击事件
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (_Click != 1)
            {
                InitEmployee();
            }
            _Click = 3;
        }

        private void DataGridViewFujia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //业务员
                if (_Click == 1 || _Click == 3)
                {
                    string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                    ltxtbSalsMan.Text = name;
                    resizablePanel1.Visible = false;
                }
                //仓库信息
                if (_Click == 2)
                {

                    GridRow gr = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
                    //判断是调入还是调出仓库
                    if (_StorageState == 1)
                    {
                        string codeout = dataGridViewFujia.Rows[e.RowIndex].Cells["code"].Value.ToString();
                        string OutName = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                        gr.Cells["gridColumnStock"].Value = OutName;
                        gr.Cells["gridColumnoutcode"].Value = codeout;
                        _ClickOutStorageList = new KeyValuePair<string, string>(codeout, OutName);
                        _OutStorage = codeout;

                    }
                    else
                    {
                        string codein = dataGridViewFujia.Rows[e.RowIndex].Cells["code"].Value.ToString();
                        string Inname = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                        gr.Cells["gridColumnStockIn"].Value = Inname;
                        gr.Cells["gridColumnincode"].Value = codein;
                        _ClickInStorageList = new KeyValuePair<string, string>(codein, Inname);
                        _InStorage = codein;
                    }
                    resizablePanel1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("双击绑定数据错误！请检查：" + ex.Message);
            }
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //是否要新增一行的标记
                bool newAdd = false;
                GridRow gr = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
                //id字段为空 说明是没有数据的行 不是修改而是新增
                if (gr.Cells["gridColumnid"].Value == null)
                {
                    newAdd = true;
                }
                gr.Cells["material"].Value = dataGridView1.Rows[e.RowIndex].Cells["materialDaima"].Value;//商品代码
                gr.Cells["gridColumnname"].Value = dataGridView1.Rows[e.RowIndex].Cells["name"].Value;//商品名称
                gr.Cells["gridColumnmodel"].Value = dataGridView1.Rows[e.RowIndex].Cells["model"].Value;//规格型号
                gr.Cells["gridColumntiaoxingma"].Value = dataGridView1.Rows[e.RowIndex].Cells["barCode"].Value;//条形码
                gr.Cells["gridColumnunit"].Value = dataGridView1.Rows[e.RowIndex].Cells["unit"].Value;//单位
                gr.Cells["gridColumnnumber"].Value = dataGridView1.Rows[e.RowIndex].Cells["currentNumber"].Value;//数量
                gr.Cells["gridColumnpriceout"].Value = dataGridView1.Rows[e.RowIndex].Cells["price"].Value;//单价
                gr.Cells["gridColumnshengchandate"].Value = dataGridView1.Rows[e.RowIndex].Cells["productionDate"].Value;//生产/采购日期
                gr.Cells["gridColumnbaozhi"].Value = dataGridView1.Rows[e.RowIndex].Cells["qualityDate"].Value;//保质期
                gr.Cells["gridColumnyouxiao"].Value = dataGridView1.Rows[e.RowIndex].Cells["effectiveDate"].Value;//有效期
                gr.Cells["gridColumnremark"].Value = dataGridView1.Rows[e.RowIndex].Cells["remark"].Value;//备注
                gr.Cells["gridColumnincode"].Value = _InStorage;//调入仓库code
                gr.Cells["gridColumnoutcode"].Value = _OutStorage;//调出仓库code
                gr.Cells["gridColumnMaterialcode"].Value = dataGridView1.Rows[e.RowIndex].Cells["code"].Value;//商品code 

                gr.Cells["gridColumnnumber"].Value = 1;

                decimal price = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["price"].Value.Equals("") ?
                    0 : dataGridView1.Rows[e.RowIndex].Cells["price"].Value);
                gr.Cells["gridColumnpriceout"].Value = price;
                gr.Cells["gridColumnmoneyout"].Value = price;
                resizablePanelData.Visible = false;

                //当上一次有选择仓库时 默认本次也为上次选择仓库
                if (!string.IsNullOrEmpty(_ClickOutStorageList.Value) && !string.IsNullOrEmpty(_ClickOutStorageList.Key))
                {
                    if (!string.IsNullOrEmpty(_ClickInStorageList.Value) && !string.IsNullOrEmpty(_ClickInStorageList.Key))
                    {
                        gr.Cells["gridColumnoutcode"].Value = _ClickOutStorageList.Key;
                        gr.Cells["gridColumnStock"].Value = _ClickOutStorageList.Value;
                        gr.Cells["gridColumnincode"].Value = _ClickInStorageList.Key;
                        gr.Cells["gridColumnStockIn"].Value = _ClickInStorageList.Value;
                    }
                }
                //新增一行 
                if (newAdd)
                {
                    superGridControl1.PrimaryGrid.NewRow(superGridControl1.PrimaryGrid.Rows.Count);
                    //递增数量和金额 默认为1和单价 
                    gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                    _MaterialNumber += 1;
                    _MaterialMoney += price;
                    gr.Cells["gridColumnnumber"].Value = _MaterialNumber;
                    gr.Cells["gridColumnmoneyout"].Value = _MaterialMoney;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-绑定商品数据错误" + ex.Message);
            }

            superGridControl1.Focus();
            SendKeys.Send("^{End}{Home}");
        }

        #endregion

        /// <summary>
        /// 第一个表格选择调出出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
        {
            try
            {
                if (e.GridCell.GridColumn.Name == "gridColumnStock")
                {
                    //绑定仓库列表
                    InitStorageList();
                    _StorageState = 1;
                    return;
                }
                if (e.GridCell.GridColumn.Name == "gridColumnStockIn")
                {
                    //绑定仓库列表
                    InitStorageList();
                    _StorageState = 2;
                    return;
                }
                if (e.GridCell.GridColumn.Name == "material")
                {
                    if (_OutStorage != null || _InStorage != null)
                    {
                        _AllMaterial = waremain.GetWMainAndMaterialByWMCode(999,"",XYEEncoding.strCodeHex(_OutStorage));
                        InitMaterialDataGridView();
                        _StorageState = 3;
                    }
                    else
                    {
                        this.resizablePanelData.Visible = false;
                        MessageBox.Show("请先选择调入和调出仓库!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-选择表格第一格数据错误！" + ex.Message);
            }

        }

        /// <summary>
        /// 验证完成后，统计单元格数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_CellValidated(object sender, GridCellValidatedEventArgs e)
        {
            GridRow gr = e.GridPanel.Rows[e.GridCell.RowIndex] as GridRow;
            //若是没数据的行则不做处理
            if (gr.Cells["material"].Value == null)
            {
                return;
            }

            if (e.GridCell.GridColumn.Name == "gridColumnnumber" ||
                e.GridCell.GridColumn.Name == "gridColumnpriceout" ||
                e.GridCell.GridColumn.Name == "gridColumnpricein")
            {
                //添加对应的单价和总价
                if (e.GridCell.GridColumn.Name == "gridColumnnumber" &&
                    !string.IsNullOrEmpty(e.GridCell.FormattedValue))
                {
                    _MaterialNumber += decimal.Parse(e.GridCell.FormattedValue);
                }
                if (e.GridCell.GridColumn.Name == "gridColumnpriceout" &&
                    !string.IsNullOrEmpty(e.GridCell.FormattedValue))
                {
                    _MaterialMoney += decimal.Parse(e.GridCell.FormattedValue);
                }
                //调入价格单价
                if (e.GridCell.GridColumn.Name == "gridColumnpricein" &&
                 !string.IsNullOrEmpty(e.GridCell.FormattedValue))
                {
                    _MaterInMoney += decimal.Parse(e.GridCell.FormattedValue);
                }
                //计算金额
                try
                {
                    decimal number = 0;
                    decimal tempAllNumber = 0;//统计数量
                    decimal tempAllMoney = 0;//调出金额
                    decimal temAllInMoney = 0;//调入金额
                    if (cbotype.Text == "同价调拨")
                    {
                        number = Convert.ToDecimal(gr.Cells["gridColumnnumber"].FormattedValue);
                        decimal priceout = Convert.ToDecimal(gr.Cells["gridColumnpriceout"].FormattedValue);
                        decimal allOutPrice = number * priceout;
                        gr.Cells["gridColumnmoneyout"].Value = allOutPrice;

                        //逐行统计数据总数
                        for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
                        {
                            GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                            tempAllNumber += Convert.ToDecimal(tempGR["gridColumnnumber"].FormattedValue);
                            tempAllMoney += Convert.ToDecimal(tempGR["gridColumnmoneyout"].FormattedValue);
                        }
                        _MaterialNumber = tempAllNumber;
                        _MaterialMoney = tempAllMoney;
                        gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                        gr["gridColumnnumber"].Value = _MaterialNumber.ToString();
                        gr["gridColumnmoneyout"].Value = _MaterialMoney.ToString();
                    }
                    if (cbotype.Text == "异价调拨")
                    {
                        number = Convert.ToDecimal(gr.Cells["gridColumnnumber"].FormattedValue);
                        decimal pricein = Convert.ToDecimal(gr.Cells["gridColumnpricein"].FormattedValue);
                        decimal allInmoney = number * pricein;
                        gr.Cells["gridColumnmoneyin"].Value = allInmoney;

                        for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
                        {
                            GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                            tempAllNumber += Convert.ToDecimal(tempGR["gridColumnnumber"].FormattedValue);
                            temAllInMoney += Convert.ToDecimal(tempGR["gridColumnmoneyin"].FormattedValue);
                        }
                        _MaterialNumber = tempAllNumber;
                        _MaterInMoney = temAllInMoney;
                        gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                        gr["gridColumnnumber"].Value = _MaterialNumber.ToString();
                        gr["gridColumnmoneyin"].Value = _MaterInMoney.ToString();
                    }
                    labtextboxTop7.Text = (_MaterInMoney - _MaterialMoney).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("金额转换错误" + ex.Message);
                }
            }
        }

        /// <summary>
        ///下拉框的选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (cbotype.Text.Trim())
                {
                    case "同价调拨":
                        superGridControl1.PrimaryGrid.Columns["gridColumnpricein"].Visible = false;
                        superGridControl1.PrimaryGrid.Columns["gridColumnmoneyin"].Visible = false;
                        labTop9.Visible = false;
                        labtextboxTop7.Visible = false;

                        break;
                    case "异价调拨":
                        superGridControl1.PrimaryGrid.Columns["gridColumnpricein"].Visible = true;
                        superGridControl1.PrimaryGrid.Columns["gridColumnmoneyin"].Visible = true;
                        labTop9.Visible = true;
                        labtextboxTop7.Visible = true;
                        superGridControl1.PrimaryGrid.Columns["gridColumnmoneyin"].ReadOnly = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-选择调拨类型数据错误" + ex.Message);
            }

        }

        /// <summary>
        /// 点击panel隐藏panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel2_Click(object sender, EventArgs e)
        {
            resizablePanel1.Visible = false;
        }

        /// <summary>
        /// 业务员模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labtextboxBotton1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ltxtbSalsMan.Text.Trim() == "")
                {
                    _Click = 3;
                    InitEmployee();
                    return;
                }
                dataGridViewFujia.DataSource = null;
                dataGridViewFujia.Columns.Clear();

                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "code";
                dgvc.HeaderText = "员工工号";
                dgvc.DataPropertyName = "code";
                dataGridViewFujia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "name";
                dgvc.HeaderText = "姓名";
                dgvc.DataPropertyName = "name";
                dataGridViewFujia.Columns.Add(dgvc);

                resizablePanel1.Location = new Point(234, 420);
                dataGridViewFujia.DataSource = ch.DataTableReCoding(employee.GetList(0, "" + XYEEncoding.strCodeHex(ltxtbSalsMan.Text.Trim()) + ""));
                resizablePanel1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-业务员模糊查询数据失败！" + ex.Message);
            }

        }
    }
}
