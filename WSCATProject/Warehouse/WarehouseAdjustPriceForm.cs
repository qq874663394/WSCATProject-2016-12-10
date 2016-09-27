using DevComponents.DotNetBar.SuperGrid;
using HelperUtility;
using HelperUtility.Encrypt;
using InterfaceLayer.Base;
using InterfaceLayer.Warehouse;
using Model;
using Model.Warehouse;
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
    public partial class WareHouseAdjustPriceForm : TestForm
    {
        public WareHouseAdjustPriceForm()
        {
            InitializeComponent();
        }

        #region 调用接口以及加密解密方法
        CodingHelper ch = new CodingHelper();
        EmpolyeeInterface employee = new EmpolyeeInterface();
        StorageInterface storage = new StorageInterface();
        WarehouseMainInterface waremain = new WarehouseMainInterface();
        WarehouseAdjustPriceInterface warehouseAdjust = new WarehouseAdjustPriceInterface();
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
        /// 调价单code
        /// </summary>
        private string _WareHouseAdjCode;
        /// <summary>
        /// 所有商品列表
        /// </summary>
        private DataTable _AllMaterial = null;
        /// <summary>
        /// 保存商品Code
        /// </summary>
        private string _MaterialCode = "";
        /// <summary>
        /// 所有仓库
        /// </summary>
        private DataTable _AllStorage = null;
        /// <summary>
        /// 仓库列表
        /// </summary>
        private KeyValuePair<string, string> _ClickStorageList;
        /// <summary>
        /// 保存仓库的Code
        /// </summary>
        private string _StorageCode = "";
        /// <summary>
        /// 保存仓库
        /// </summary>
        private string _Storage;
        /// <summary>
        /// 统计调前金额
        /// </summary>
        private decimal _MaterialBeforeMoney;
        /// <summary>
        /// 统计调后金额
        /// </summary>
        private decimal _MaterialAfterMoney;
        /// <summary>
        /// 统计调价金额
        /// </summary>
        private decimal _MaterialAdjMoney;
        /// <summary>
        /// 统计数量
        /// </summary>
        private decimal _MaterialNumber;
        ///// <summary>
        ///// 保存仓库状态 1、仓库 2、商品列表
        ///// </summary>
        private int _StorageState;
        /// <summary>
        /// 统计调前金额
        /// </summary>
        private decimal _beforeMoney;
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

        private void WareHouseAdjustPriceForm_Load(object sender, EventArgs e)
        {
            try
            {
                //业务员
                _AllEmployee = employee.SelSupplierTable(false);
                //仓库
                _AllStorage = storage.GetList(00, "");

                //调后单价
                GridDoubleInputEditControl diaoruprice = superGridControl1.PrimaryGrid.Columns["gridColumnafterprice"].EditControl as GridDoubleInputEditControl;
                diaoruprice.MinValue = 0;
                diaoruprice.MaxValue = 999999999;            
                //禁用自动创建列
                dataGridView1.AutoGenerateColumns = false;
                dataGridViewFujia.AutoGenerateColumns = false;
                superGridControl1.HScrollBarVisible = true;
                //绑定事件 双击事填充内容并隐藏列表
                dataGridViewFujia.CellDoubleClick += DataGridViewFujia_CellDoubleClick;
                dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
                toolStripButtonsave.Click += ToolStripButtonsave_Click;//保存按钮
                toolStripButtonshen.Click += ToolStripButtonshen_Click;//审核按钮

                // 将dataGridView中的内容居中显示
                dataGridViewFujia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //显示行号
                superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
                //内容居中
                superGridControl1.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                //调用合计行数据
                InitDataGridView();
                //生成code 和显示条形码
                _WareHouseAdjCode = BuildCode.ModuleCode("WAP");
                textBoxOddNumbers.Text = _WareHouseAdjCode;
                barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
                _Code.ValueFont = new Font("微软雅黑", 20);
                System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
                pictureBox9.Image = imgTemp;
            }
            catch (Exception ex)
            {
                MessageBox.Show("窗体加载时出错！请检查：" + ex.Message, "调价单温馨提示！");
            }
        }

        /// <summary>
        /// 审核按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonshen_Click(object sender, EventArgs e)
        {
            //非空验证
            isNUllValidate();
            //获得界面上的数据,准备传给base层新增数据
            WarehouseAdjustPriceInterface warehouseAdjpriceinterface = new WarehouseAdjustPriceInterface();
            //调价单
            WarehouseAdjustPrice warehouseADJprice = new WarehouseAdjustPrice();
            //调价单商品列表
            List<WarehouseAdjustPriceDetail> warehouseADJpriceList = new List<WarehouseAdjustPriceDetail>();
            try
            {
                warehouseADJprice.code = textBoxOddNumbers.Text == "" ? "" : XYEEncoding.strCodeHex(textBoxOddNumbers.Text);//单据code
                warehouseADJprice.date = dateTimePicker1.Value;//单据日期
                warehouseADJprice.type = cboadjType.Text == "" ? "" : XYEEncoding.strCodeHex(cboadjType.Text);//调价类别
                warehouseADJprice.operationMan = ltxtbSalsMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbSalsMan.Text);//调价员
                warehouseADJprice.makeMan = ltxtbMakeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbMakeMan.Text);//制单人
                warehouseADJprice.checkMan = ltxtbShengHeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbShengHeMan.Text);//审核人
                warehouseADJprice.remark = labtextboxTop9.Text == "" ? "" : XYEEncoding.strCodeHex(labtextboxTop9.Text);//摘要
                warehouseADJprice.checkState = 1; //审核状态
                warehouseADJprice.isClear = 1;
                warehouseADJprice.updateDate = DateTime.Now;
                warehouseADJprice.reserved1 = "";
                warehouseADJprice.reserved2 = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:；尝试创建调价单商品数据出错,请检查输入" + ex.Message, "调价单温馨提示");
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
                        warehouseAdjDetail.stockName = gr["gridColumnStock"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnStock"].Value.ToString());//仓库名称
                        warehouseAdjDetail.code = XYEEncoding.strCodeHex(textBoxOddNumbers.Text) + i.ToString();//单据code
                        warehouseAdjDetail.mainCode = XYEEncoding.strCodeHex(textBoxOddNumbers.Text);//主表code
                        warehouseAdjDetail.materiaDaima = gr["material"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["material"].Value.ToString());//商品代码
                        warehouseAdjDetail.materialCode = gr["gridColumnmaterialcode"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnmaterialcode"].Value.ToString());//商品code
                        warehouseAdjDetail.barCode = gr["gridColumntiaoma"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumntiaoma"].Value.ToString());//条形码
                        warehouseAdjDetail.materialName = gr["gridColumnname"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnname"].Value.ToString());//物料名称
                        warehouseAdjDetail.materialModel = gr["gridColumnmodel"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnmodel"].Value.ToString());//规格型号
                        warehouseAdjDetail.materialUnit = gr["gridColumnunit"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnunit"].Value.ToString());//单位
                        warehouseAdjDetail.number = Convert.ToDecimal(gr["gridColumnnumber"].Value.ToString());//数量
                        warehouseAdjDetail.curPrice = Convert.ToDecimal(gr["gridColumnbeforeprice"].Value.ToString() == "" ? null : gr["gridColumnbeforeprice"].Value);//调前单价
                        warehouseAdjDetail.curMoney = Convert.ToDecimal(gr["gridColumnbeforemoney"].Value.ToString() == "" ? null : gr["gridColumnbeforemoney"].Value);//调前金额
                        warehouseAdjDetail.price = Convert.ToDecimal(gr["gridColumnafterprice"].Value.ToString() == "" ? null : gr["gridColumnafterprice"].Value);//调后单价
                        warehouseAdjDetail.money = Convert.ToDecimal(gr["gridColumnaftermoney"].Value.ToString() == "" ? null : gr["gridColumnaftermoney"].Value);//调后金额
                        warehouseAdjDetail.lostMoney = Convert.ToDecimal(gr["gridColumnmoneyadj"].Value.ToString() == "" ? null : gr["gridColumnmoneyadj"].Value);//调价差额
                        warehouseAdjDetail.remark = gr["gridColumnremark"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnremark"].Value.ToString());//备注
                        warehouseAdjDetail.isClear = 1;
                        warehouseAdjDetail.updateDate = DateTime.Now;
                        warehouseAdjDetail.reserved1 = "";
                        warehouseAdjDetail.reserved2 = "";
                        warehouseAdjDetail.scale = 1;
                        warehouseAdjDetail.cause = "";
                        GridRow dr = superGridControl1.PrimaryGrid.Rows[0] as GridRow;
                        warehouseADJpriceList.Add(warehouseAdjDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2105-尝试创建调价单详情商品数据出错,请检查输入" + ex.Message, "调价单温馨提示");
                return;
            }
         
            //增加一条调价单和调价详细数据
            object warehouseAdjResult = warehouseAdjpriceinterface.AddAndModify(warehouseADJprice, warehouseADJpriceList);

            if (warehouseAdjResult != null)
            {
                MessageBox.Show("新增或审核调价单数据成功", "调价单温馨提示");
            }
            cboadjType.SelectedItem = 0;
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonsave_Click(object sender, EventArgs e)
        {
            //非空验证
            isNUllValidate();
            //获得界面上的数据,准备传给base层新增数据
            WarehouseAdjustPriceInterface warehouseAdjpriceinterface = new WarehouseAdjustPriceInterface();
            //调价单
            WarehouseAdjustPrice warehouseADJprice = new WarehouseAdjustPrice();
            //调价单商品列表
            List<WarehouseAdjustPriceDetail> warehouseADJpriceList = new List<WarehouseAdjustPriceDetail>();
            try
            {
                warehouseADJprice.code = textBoxOddNumbers.Text == "" ? "" : XYEEncoding.strCodeHex(textBoxOddNumbers.Text);//单据code
                warehouseADJprice.date = dateTimePicker1.Value;//单据日期
                warehouseADJprice.type = cboadjType.Text == "" ? "" : XYEEncoding.strCodeHex(cboadjType.Text);//调价类别
                warehouseADJprice.operationMan = ltxtbSalsMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbSalsMan.Text);//调价员
                warehouseADJprice.makeMan = ltxtbMakeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbMakeMan.Text);//制单人
                warehouseADJprice.checkMan = ltxtbShengHeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbShengHeMan.Text);//审核人
                warehouseADJprice.remark = labtextboxTop9.Text == "" ? "" : XYEEncoding.strCodeHex(labtextboxTop9.Text);//摘要
                warehouseADJprice.checkState = 0; //审核状态
                warehouseADJprice.isClear = 1;
                warehouseADJprice.updateDate = DateTime.Now;
                warehouseADJprice.reserved1 = "";
                warehouseADJprice.reserved2 = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:；尝试创建调价单商品数据出错,请检查输入" + ex.Message, "调价单温馨提示");
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
                        warehouseAdjDetail.stockName = gr["gridColumnStock"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnStock"].Value.ToString());//仓库名称
                        warehouseAdjDetail.code = XYEEncoding.strCodeHex(textBoxOddNumbers.Text) + i.ToString();//单据code
                        warehouseAdjDetail.mainCode = XYEEncoding.strCodeHex(textBoxOddNumbers.Text);//主表code
                        warehouseAdjDetail.materiaDaima = gr["material"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["material"].Value.ToString());//商品代码
                        warehouseAdjDetail.materialCode = gr["gridColumnmaterialcode"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnmaterialcode"].Value.ToString());//商品code
                        warehouseAdjDetail.barCode = gr["gridColumntiaoma"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumntiaoma"].Value.ToString());//条形码
                        warehouseAdjDetail.materialName = gr["gridColumnname"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnname"].Value.ToString());//物料名称
                        warehouseAdjDetail.materialModel = gr["gridColumnmodel"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnmodel"].Value.ToString());//规格型号
                        warehouseAdjDetail.materialUnit = gr["gridColumnunit"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnunit"].Value.ToString());//单位
                        warehouseAdjDetail.number = Convert.ToDecimal(gr["gridColumnnumber"].Value.ToString());//数量
                        warehouseAdjDetail.curPrice = Convert.ToDecimal(gr["gridColumnbeforeprice"].Value.ToString() == "" ? null : gr["gridColumnbeforeprice"].Value);//调前单价
                        warehouseAdjDetail.curMoney = Convert.ToDecimal(gr["gridColumnbeforemoney"].Value.ToString() == "" ? null : gr["gridColumnbeforemoney"].Value);//调前金额
                        warehouseAdjDetail.price = Convert.ToDecimal(gr["gridColumnafterprice"].Value.ToString() == "" ? null : gr["gridColumnafterprice"].Value);//调后单价
                        warehouseAdjDetail.money = Convert.ToDecimal(gr["gridColumnaftermoney"].Value.ToString() == "" ? null : gr["gridColumnaftermoney"].Value);//调后金额
                        warehouseAdjDetail.lostMoney = Convert.ToDecimal(gr["gridColumnmoneyadj"].Value.ToString() == "" ? null : gr["gridColumnmoneyadj"].Value);//调价差额
                        warehouseAdjDetail.remark = gr["gridColumnremark"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnremark"].Value.ToString());//备注
                        warehouseAdjDetail.isClear = 1;
                        warehouseAdjDetail.updateDate = DateTime.Now;
                        warehouseAdjDetail.reserved1 = "";
                        warehouseAdjDetail.reserved2 = "";
                        warehouseAdjDetail.scale = 1;
                        warehouseAdjDetail.cause = "";
                        GridRow dr = superGridControl1.PrimaryGrid.Rows[0] as GridRow;
                        warehouseADJpriceList.Add(warehouseAdjDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2105-尝试创建调价单详情商品数据出错,请检查输入" + ex.Message, "调价单温馨提示");
                return;
            }

            //增加一条调价单和调价详细数据
            object warehouseAdjResult = warehouseAdjpriceinterface.AddAndModify(warehouseADJprice, warehouseADJpriceList);
            //this.textBoxid.Text = warehouseProfitResult.ToString();
            if (warehouseAdjResult != null)
            {
                MessageBox.Show("新增调价单数据成功", "调价单温馨提示");
            }
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
            gr.Cells["material"].Value = "合计";
            gr.Cells["material"].CellStyles.Default.Alignment =
                DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            //数量
            gr.Cells["gridColumnnumber"].Value = 0;
            gr.Cells["gridColumnnumber"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnnumber"].CellStyles.Default.Background.Color1 = Color.Orange;
            //调前金额
            gr.Cells["gridColumnbeforemoney"].Value = 0;
            gr.Cells["gridColumnbeforemoney"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnbeforemoney"].CellStyles.Default.Background.Color1 = Color.Orange;
            //调后金额
            gr.Cells["gridColumnaftermoney"].Value = 0;
            gr.Cells["gridColumnaftermoney"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnaftermoney"].CellStyles.Default.Background.Color1 = Color.Orange;
            //调价金额
            gr.Cells["gridColumnmoneyadj"].Value = 0;
            gr.Cells["gridColumnmoneyadj"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnmoneyadj"].CellStyles.Default.Background.Color1 = Color.Orange;
        }

        /// <summary>
        /// 非空验证
        /// </summary>
        private void isNUllValidate()
        {
            if (cboadjType.Text.Trim() == null)
            {
                MessageBox.Show("调价科目不能为空！");
            }
            if (ltxtbSalsMan.Text.Trim() == null)
            {
                MessageBox.Show("调价员不能为空！");
            }
            GridRow gr = (GridRow)superGridControl1.PrimaryGrid.
               Rows[superGridControl1.PrimaryGrid.Rows.Count];
            if (gr.Cells["material"].Value == null)
            {
                MessageBox.Show("商品代码不能为空！");
            }
            if (gr.Cells["gridColumnStock"].Value == null)
            {
                MessageBox.Show("仓库不能为空！");
            }

        }

        /// <summary>
        /// 初始化调价员
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
                MessageBox.Show("初始化调价员出错！请检查：" + ex.Message);
            }
        }

        /// <summary>
        /// 初始化仓库列和数据
        /// </summary>
        private void InitStorageList()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("初始化仓库出错！请检查：" + ex.Message);
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

        /// <summary>
        /// 双击调价员或者仓库信息填写在表格里面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewFujia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //调价员
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
                    string code = dataGridViewFujia.Rows[e.RowIndex].Cells["code"].Value.ToString();
                    string Name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                    gr.Cells["gridColumnStock"].Value = Name;
                    //gr.Cells["gridColumncode"].Value = code;
                    _ClickStorageList = new KeyValuePair<string, string>(code, Name);
                    _StorageCode = code;
                    resizablePanel1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("双击绑定调价员或仓库信息数据错误！请检查：" + ex.Message);
            }
        }

        /// <summary>
        /// 双击物料信息填写在表格里面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                gr.Cells["gridColumntiaoma"].Value = dataGridView1.Rows[e.RowIndex].Cells["barCode"].Value;//条码
                gr.Cells["gridColumnunit"].Value = dataGridView1.Rows[e.RowIndex].Cells["unit"].Value;//单位
                gr.Cells["gridColumnnumber"].Value = dataGridView1.Rows[e.RowIndex].Cells["currentNumber"].Value;//商品数量
                gr.Cells["gridColumnbeforeprice"].Value = dataGridView1.Rows[e.RowIndex].Cells["price"].Value;//单价
                gr.Cells["gridColumnremark"].Value = dataGridView1.Rows[e.RowIndex].Cells["remark"].Value;//备注
                gr.Cells["gridColumnmaterialcode"].Value = dataGridView1.Rows[e.RowIndex].Cells["code"].Value;//商品code

                decimal number = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["currentNumber"].Value);//数量
                decimal beforeprice = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["price"].Value);//调前单价
                _MaterialBeforeMoney = number * beforeprice;
                gr.Cells["gridColumnbeforemoney"].Value = _MaterialBeforeMoney;//调前金额

                //当上一次有选择仓库时 默认本次也为上次选择仓库
                if (!string.IsNullOrEmpty(_ClickStorageList.Value) && !string.IsNullOrEmpty(_ClickStorageList.Key))
                {
                    gr.Cells["gridColumnstockcode"].Value = _ClickStorageList.Key;
                    gr.Cells["gridColumnStock"].Value = _ClickStorageList.Value;
                }
                //新增一行 
                if (newAdd)
                {
                    superGridControl1.PrimaryGrid.NewRow(superGridControl1.PrimaryGrid.Rows.Count);
                    //递增数量和金额 默认为1和单价 
                    gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                    _MaterialNumber += 1;
                    _beforeMoney += _MaterialBeforeMoney;
                    gr.Cells["gridColumnnumber"].Value = _MaterialNumber;
                    gr.Cells["gridColumnbeforemoney"].Value = _beforeMoney;
                }
                superGridControl1.Focus();
                SendKeys.Send("^{End}{Home}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("双击绑定物料信息数据错误！请检查：" + ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// 第一个表格选择仓库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "gridColumnStock")
            {
                InitStorageList();
                return;
            }
            if (e.GridCell.GridColumn.Name == "material")
            {
                //查询商品列表
                _AllMaterial = waremain.GetWMainAndMaterialByWMCode(XYEEncoding.strCodeHex(_StorageCode));
                InitMaterialDataGridView();
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
            //计算调后金额以及调价金额
            try
            {
                decimal tempAllBeforemoney = 0;
                decimal tempAllAftermoney = 0;
                decimal tempALLadjmoney = 0;
                decimal tempAllnumber = 0;
                //数量
                decimal number = Convert.ToDecimal(gr.Cells["gridColumnnumber"].FormattedValue);
                //调前单价
                decimal tiaoqianPrice = Convert.ToDecimal(gr.Cells["gridColumnbeforeprice"].FormattedValue);
                //调前金额
                decimal tiaoqianMoney = Convert.ToDecimal(gr.Cells["gridColumnbeforemoney"].FormattedValue);
                //调后单价
                decimal tiaohouPrice = Convert.ToDecimal(gr.Cells["gridColumnafterprice"].FormattedValue);
                //调后金额
                decimal tiaohouMoney = number * tiaohouPrice;
                gr.Cells["gridColumnaftermoney"].Value = tiaohouMoney;
                //调价金额
                decimal ADJmoney = tiaohouMoney - tiaoqianMoney;
                gr.Cells["gridColumnmoneyadj"].Value = ADJmoney;
                labtextboxTop7.Text = ADJmoney.ToString();

                //逐行统计数据总数
                for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                    tempAllBeforemoney += Convert.ToDecimal(tempGR["gridColumnbeforemoney"].FormattedValue);
                    tempAllAftermoney += Convert.ToDecimal(tempGR["gridColumnaftermoney"].FormattedValue);
                    tempALLadjmoney += Convert.ToDecimal(tempGR["gridColumnmoneyadj"].FormattedValue);
                    tempAllnumber += Convert.ToDecimal(tempGR["gridColumnnumber"].FormattedValue);
                }
                _MaterialBeforeMoney = tempAllBeforemoney;
                _MaterialAfterMoney = tempAllAftermoney;
                _MaterialAdjMoney = tempALLadjmoney;
                _MaterialNumber = tempAllnumber;
                gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                gr["gridColumnbeforemoney"].Value = _MaterialBeforeMoney.ToString();
                gr["gridColumnaftermoney"].Value = _MaterialAfterMoney.ToString();
                gr["gridColumnmoneyadj"].Value = _MaterialAdjMoney.ToString();
                gr["gridColumnnumber"].Value = _MaterialNumber.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("统计数量错误！请检查：" + ex.Message);
            }
        }

        /// <summary>
        /// 调价员模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labtextboxBotton1_TextChanged(object sender, EventArgs e)
        {
            if (ltxtbSalsMan.Text.Trim() == "")
            {
                _Click = 3;
                InitEmployee();
                return;
            }
            try
            {
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
                MessageBox.Show("模糊查询调价员出错！请检查：" + ex.Message);
            }
        }

        /// <summary>
        /// 窗体任意位置隐藏resizablePanel1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void panel6_Click(object sender, EventArgs e)
        {
            resizablePanel1.Visible = false;
        }

        /// <summary>
        /// 按下ESC按钮关闭子窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WareHouseAdjustPriceForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.resizablePanel1.Visible = false;
            }
        }

    }
}
