using DevComponents.DotNetBar.SuperGrid;
using HelperUtility;
using HelperUtility.Encrypt;
using InterfaceLayer.Base;
using Model.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSCATProject.Sales
{
    public partial class SalesOrderForm : TestForm
    {
        public SalesOrderForm()
        {
            InitializeComponent();
        }

        #region 调用接口以及加密解密方法
        CodingHelper ch = new CodingHelper();
        ClientInterface client = new ClientInterface();//客户
        EmpolyeeInterface employee = new EmpolyeeInterface();//员工  
        StorageInterface storage = new StorageInterface();//交货地点
        MaterialInterface materialInterface = new MaterialInterface();
        #endregion

        #region 数据字段

        /// <summary>
        /// 所有客户
        /// </summary>
        private DataTable _AllClient = null;
        /// <summary>
        /// 所有销售员
        /// </summary>
        private DataTable _AllEmployee = null;
        /// <summary>
        /// 所有仓库
        /// </summary>
        private DataTable _AllStorage = null;
        /// <summary>
        /// 点击的项,1客户  2为销售员  3为仓库   
        /// </summary>
        private int _Click = 0;
        /// <summary>
        /// 保存客户Code
        /// </summary>
        private string _clientCode;
        /// <summary>
        /// 保存销售员Code
        /// </summary>
        private string _employeeCode;
        /// <summary>
        /// 保存仓库Code
        /// </summary>
        private string _storgeCode;
        /// <summary>
        /// 所有商品列表
        /// </summary>
        private DataTable _AllMaterial = null;
        /// <summary>
        /// 商品code
        /// </summary>
        private string _materialCode;
        /// <summary>
        /// 统计数量
        /// </summary>
        private decimal _Materialnumber;
        private decimal _MaterialNumber;
        /// <summary>
        /// 统计金额
        /// </summary>
        private decimal _Money;
        /// <summary>
        /// 统计税额
        /// </summary>
        private decimal _TaxMoney;
        /// <summary>
        /// 统计价税合计
        /// </summary>
        private decimal _PriceAndTaxMoney;
        /// <summary>
        /// 销售订单code
        /// </summary>
        private string _SalesOrderCode;
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
                                 2,
                                 ButtonBorderStyle.Solid,
                                 Color.FromArgb(85, 177, 238),
                                 1,
                                 ButtonBorderStyle.Solid,
                                 Color.FromArgb(85, 177, 238),
                                 2,
                                 ButtonBorderStyle.Solid,
                                 Color.White,
                                 1,
                                 ButtonBorderStyle.Solid);
        }
        #endregion

        #region 初始化数据
        /// <summary>
        /// 统计行数据
        /// </summary>
        private void InitDataGridView()
        {
            //新增一行 用于给客户操作
            superGridControlShangPing.PrimaryGrid.NewRow(true);
            //最后一行做统计行
            GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.
                Rows[superGridControlShangPing.PrimaryGrid.Rows.Count - 1];
            gr.ReadOnly = true;
            gr.CellStyles.Default.Background.Color1 = Color.SkyBlue;
            gr.Cells["material"].Value = "合计";
            gr.Cells["material"].CellStyles.Default.Alignment =
                DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["dinggouNumber"].Value = 0;
            gr.Cells["dinggouNumber"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["dinggouNumber"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["money"].Value = 0;
            gr.Cells["money"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["money"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["TaxMoney"].Value = 0;
            gr.Cells["TaxMoney"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["TaxMoney"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["priceANDtax"].Value = 0;
            gr.Cells["priceANDtax"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["priceANDtax"].CellStyles.Default.Background.Color1 = Color.Orange;
        }

        /// <summary>
        /// 非空验证
        /// </summary>
        private bool isNUllValidate()
        {
            if (labtxtDanJuType.Text.Trim() == null)
            {
                MessageBox.Show("客户不能为空！");
                return false;
            }
            if (labtextboxTop2.Text.Trim() == null)
            {
                MessageBox.Show("联系人不能为空！");
                return false;
            }
            if (labtextboxTop3.Text.Trim() == null)
            {
                MessageBox.Show("电话不能为空！");
                return false;
            }
            if (cboMethod.Text.Trim() == null)
            {
                MessageBox.Show("交货方式不能为空！");
                return false;
            }
            if (labtextboxTop5.Text.Trim() == null)
            {
                MessageBox.Show("交货地点不能为空！");
                return false;
            }
            GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[0];
            if (gr.Cells["material"].Value == null || gr.Cells["material"].Value.ToString() == "")
            {
                MessageBox.Show("商品代码不能为空！");
                return false;
            }
            if (ltxtbSalsMan.Text.Trim() == null || ltxtbSalsMan.Text == "")
            {
                MessageBox.Show("销售员不能为空！");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 初始化客户
        /// </summary>
        private void InitClient()
        {
            try
            {
                if (_Click != 1)
                {
                    _Click = 1;
                    dataGridViewFuJia.DataSource = null;
                    dataGridViewFuJia.Columns.Clear();

                    DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "code";
                    dgvc.HeaderText = "客户编号";
                    dgvc.DataPropertyName = "code";
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "name";
                    dgvc.HeaderText = "客户姓名";
                    dgvc.DataPropertyName = "name";
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "mobilePhone";
                    dgvc.HeaderText = "电话";
                    dgvc.DataPropertyName = "mobilePhone";
                    dgvc.Visible = false;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "linkMan";
                    dgvc.HeaderText = "联系人";
                    dgvc.DataPropertyName = "linkMan";
                    dgvc.Visible = false;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "fax";
                    dgvc.HeaderText = "传真";
                    dgvc.DataPropertyName = "fax";
                    dgvc.Visible = false;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    resizablePanel1.Location = new Point(230, 160);
                    dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllClient);
                    resizablePanel1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化客户数据失败！请检查：" + ex.Message);
            }
        }

        /// <summary>
        /// 初始化销售员
        /// </summary>
        private void InitEmployee()
        {
            try
            {
                if (_Click != 2)
                {
                    _Click = 2;
                    dataGridViewFuJia.DataSource = null;
                    dataGridViewFuJia.Columns.Clear();

                    DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "code";
                    dgvc.HeaderText = "员工工号";
                    dgvc.DataPropertyName = "员工工号";
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "name";
                    dgvc.HeaderText = "姓名";
                    dgvc.DataPropertyName = "姓名";
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllEmployee);
                    resizablePanel1.Visible = true;
                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        resizablePanel1.Location = new Point(220, 670);
                        return;
                    }
                    if (this.WindowState == FormWindowState.Normal)
                    {
                        resizablePanel1.Location = new Point(234, 460);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化销售员数据失败！请检查：" + ex.Message);
            }
        }

        /// <summary>
        /// 初始化仓库
        /// </summary>
        private void InitStorage()
        {
            try
            {
                if (_Click != 3)
                {
                    _Click = 3;
                    dataGridViewFuJia.DataSource = null;
                    dataGridViewFuJia.Columns.Clear();

                    DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "code";
                    dgvc.Visible = true;
                    dgvc.HeaderText = "仓库编号";
                    dgvc.DataPropertyName = "code";
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "name";
                    dgvc.Visible = true;
                    dgvc.HeaderText = "仓库名称";
                    dgvc.DataPropertyName = "name";
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "address";
                    dgvc.Visible = false;
                    dgvc.HeaderText = "仓库地址";
                    dgvc.DataPropertyName = "address";
                    dataGridViewFuJia.Columns.Add(dgvc);

                    resizablePanel1.Location = new Point(560, 190);
                    //查询仓库的方法
                    dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllStorage);
                    resizablePanel1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化仓库数据失败！请检查：" + ex.Message);
            }
        }

        /// <summary>
        /// 初始化商品下拉别表的数据
        /// </summary>
        private void InitMaterialDataGridView()
        {
            try
            {
                dataGridViewShangPing.DataSource = null;
                dataGridViewShangPing.Columns.Clear();
                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "code";
                dgvc.Visible = false;
                dgvc.HeaderText = "code";
                dgvc.DataPropertyName = "code";
                dataGridViewShangPing.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "materialDaima";
                dgvc.HeaderText = "商品代码";
                dgvc.DataPropertyName = "materialDaima";
                dataGridViewShangPing.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "name";
                dgvc.HeaderText = "商品名称";
                dgvc.DataPropertyName = "name";
                dataGridViewShangPing.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "model";
                dgvc.Visible = false;
                dgvc.HeaderText = "规格型号";
                dgvc.DataPropertyName = "model";
                dataGridViewShangPing.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "barCode";
                dgvc.Visible = false;
                dgvc.HeaderText = "条形码";
                dgvc.DataPropertyName = "barCode";
                dataGridViewShangPing.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "unit";
                dgvc.Visible = false;
                dgvc.HeaderText = "单位";
                dgvc.DataPropertyName = "unit";
                dataGridViewShangPing.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "price";
                dgvc.Visible = false;
                dgvc.HeaderText = "单价";
                dgvc.DataPropertyName = "price";
                dataGridViewShangPing.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "remark";
                dgvc.Visible = false;
                dgvc.HeaderText = "备注";
                dgvc.DataPropertyName = "remark";
                dataGridViewShangPing.Columns.Add(dgvc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化商品列表失败，请检查：" + ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SalesOrderForm_Load(object sender, EventArgs e)
        {
            try
            {
                //客户
                _AllClient = client.GetClientByBool(false);
                //销售员
                _AllEmployee = employee.SelSupplierTable(false);
                //仓库
                _AllStorage = storage.GetList(00, "");

                #region 初始化窗体

                cboMethod.SelectedIndex = 0;
                //禁用自动创建列
                dataGridViewShangPing.AutoGenerateColumns = false;
                dataGridViewFuJia.AutoGenerateColumns = false;
                superGridControlShangPing.HScrollBarVisible = true;
                // 将dataGridView中的内容居中显示
                dataGridViewFuJia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //显示行号
                superGridControlShangPing.PrimaryGrid.ShowRowGridIndex = true;
                //内容居中
                superGridControlShangPing.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                //调用合计行数据
                InitDataGridView();

                #endregion

                //订购数量
                GridDoubleInputEditControl gdiecNumber = superGridControlShangPing.PrimaryGrid.Columns["dinggouNumber"].EditControl as GridDoubleInputEditControl;
                gdiecNumber.MinValue = 1;
                gdiecNumber.MaxValue = 999999999;
                //单价
                GridDoubleInputEditControl gdiecPrice = superGridControlShangPing.PrimaryGrid.Columns["price"].EditControl as GridDoubleInputEditControl;
                gdiecNumber.MinValue = 1;
                gdiecNumber.MaxValue = 999999999;
                //折扣率
                GridDoubleInputEditControl gdiecDiscountRate = superGridControlShangPing.PrimaryGrid.Columns["DiscountRate"].EditControl as GridDoubleInputEditControl;
                gdiecNumber.MinValue = 1;
                gdiecNumber.MaxValue = 100;

                //生成销售订单code和显示条形码
                _SalesOrderCode = BuildCode.ModuleCode("SOR");
                textBoxOddNumbers.Text = _SalesOrderCode;
                barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
                _Code.ValueFont = new Font("微软雅黑", 20);
                System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
                pictureBoxBarCode.Image = imgTemp;

                //绑定事件 双击事填充内容并隐藏列表
                dataGridViewFuJia.CellDoubleClick += dataGridViewFuJia_CellDoubleClick;
                dataGridViewShangPing.CellDoubleClick += dataGridViewShangPing_CellDoubleClick;

                toolStripBtnSave.Click += toolStripBtnSave_Click;//保存按钮
                toolStripBtnShengHe.Click += toolStripBtnShengHe_Click;//审核按钮

            }
            catch (Exception ex)
            {
                MessageBox.Show("窗体加载失败！请检查：" + ex.Message);
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripBtnSave_Click(object sender, EventArgs e)
        {
            if (isNUllValidate() == false)
            {
                return;
            }
            //获得界面上的数据,准备传给base层新增数据
            // WarehouseInInterface warehouseInterface = new WarehouseInInterface();
            //销售订单
            SalesOrder salesorder = new SalesOrder();
            //销售订单商品列表
            List<SalesOrderDetail> saleorderList = new List<SalesOrderDetail>();
            try
            {
                salesorder.code = XYEEncoding.strCodeHex(_SalesOrderCode);//销售订单Code
                salesorder.date = this.dateTimePicker1.Value;//开单日期
                salesorder.clientCode = XYEEncoding.strCodeHex(_clientCode);//客户code
                switch (cboMethod.Text.Trim())//交货方式
                {
                    case "提货":
                        salesorder.deliversMethod = 0;
                        break;
                    case "送货":
                        salesorder.deliversMethod = 1;
                        break;
                    case "发货":
                        salesorder.deliversMethod = 2;
                        break;
                }
                salesorder.deliversLocation = XYEEncoding.strCodeHex(labtextboxTop5.Text);//交货地点
                salesorder.deliversDate = dateTimePicker2.Value;//交货日期
                salesorder.remark = XYEEncoding.strCodeHex(labtextboxTop9.Text);//摘要
                salesorder.operation = XYEEncoding.strCodeHex(ltxtbSalsMan.Text);//销售员
                salesorder.makeMan = XYEEncoding.strCodeHex(ltxtbMakeMan.Text);//制单人
                salesorder.examine = XYEEncoding.strCodeHex(ltxtbShengHeMan.Text);//审核人
                salesorder.checkState = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:尝试创建销售订单数据出错,请检查输入" + ex.Message, "销售订单温馨提示");
                return;
            }

            try
            {
                //获得商品列表数据,准备传给base层新增数据
                GridRow g = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[ClickRowIndex];
                GridItemsCollection grs = superGridControlShangPing.PrimaryGrid.Rows;
                int i = 0;
                DateTime nowDataTime = DateTime.Now;
                foreach (GridRow gr in grs)
                {
                    if (gr["material"].Value != null)
                    {

                        i++;
                        SalesOrderDetail salesorderDetail = new SalesOrderDetail();
                        salesorderDetail.mainCode = XYEEncoding.strCodeHex(this.textBoxOddNumbers.Text);//销售订单单据Code
                        salesorderDetail.code = XYEEncoding.strCodeHex(_SalesOrderCode + i.ToString());//销售订单明细Code
                        salesorderDetail.materialCode = XYEEncoding.strCodeHex(_materialCode);//物料code
                        salesorderDetail.materialNumber = Convert.ToDecimal(gr["dinggouNumber"].Value);//数量
                        salesorderDetail.materialPrice = Convert.ToDecimal(gr["price"].Value);//单价
                        salesorderDetail.discountRate = Convert.ToDecimal(gr["DiscountRate"].Value);//折扣率
                        salesorderDetail.discountMoney = Convert.ToDecimal(gr["DiscountMoney"].Value);//折扣额
                        salesorderDetail.VATRate = Convert.ToDecimal(gr["TaxRate"].Value);//增值税税率
                        salesorderDetail.tax = Convert.ToDecimal(gr["TaxMoney"].Value);//税额
                        salesorderDetail.taxTotal = Convert.ToDecimal(gr["priceANDtax"].Value);//价税合计
                        salesorderDetail.remark = XYEEncoding.strCodeHex(gr["remark"].Value.ToString());//备注
                        salesorderDetail.deliveryNumber = Convert.ToDecimal(gr["FaHuoNumber"].Value);//发货数量    

                        GridRow dr = superGridControlShangPing.PrimaryGrid.Rows[0] as GridRow;
                        saleorderList.Add(salesorderDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-尝试创建销售订单详情商品数据出错,请检查输入" + ex.Message, "销售订单温馨提示");
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
        /// 审核按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripBtnShengHe_Click(object sender, EventArgs e)
        {
            if (isNUllValidate() == false)
            {
                return;
            }
        }


        #region 小箭头和表格点击事件以及两个表格双击绑定数据

        /// <summary>
        /// 客户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxDanJuType_Click(object sender, EventArgs e)
        {
            if (_Click != 1)
            {
                InitClient();
            }
            _Click = 4;
        }

        /// <summary>
        /// 销售员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxEmployee_Click(object sender, EventArgs e)
        {
            if (_Click != 2)
            {
                InitEmployee();
            }
            _Click = 5;
        }

        /// <summary>
        /// 交货地点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxAddress_Click(object sender, EventArgs e)
        {
            if (_Click != 3)
            {
                InitStorage();
            }
            _Click = 6;
        }

        /// <summary>
        /// 查询商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControlShangPing_BeginEdit(object sender, GridEditEventArgs e)
        {
            try
            {
                if (labtxtDanJuType.Text.Trim() == null)
                {
                    MessageBox.Show("请先选择客户：");
                    return;
                }
                if (e.GridCell.GridColumn.Name == "material")
                {
                    SelectedElementCollection ge = superGridControlShangPing.PrimaryGrid.GetSelectedCells();
                    GridCell gc = ge[0] as GridCell;
                    string materialDaima = XYEEncoding.strCodeHex(e.EditControl.EditorValue.ToString());
                    if (gc.GridRow.Cells[material].Value != null && (gc.GridRow.Cells[material].Value).ToString() != "")
                    {
                        //模糊查询商品列表
                        _AllMaterial = materialInterface.GetList(0, "" + materialDaima + "");
                        InitMaterialDataGridView();
                    }
                    else
                    {
                        //绑定商品列表
                        _AllMaterial = materialInterface.GetList(999, "");
                        InitMaterialDataGridView();
                    }
                    dataGridViewShangPing.DataSource = ch.DataTableReCoding(_AllMaterial);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询商品数据失败！请检查：" + ex.Message);
            }
        }

        /// <summary>
        /// 双击绑定客户、销售员、仓库数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewFuJia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //客户
                if (_Click == 1 || _Click == 4)
                {
                    _clientCode = dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString();//客户code
                    string name = dataGridViewFuJia.Rows[e.RowIndex].Cells["name"].Value.ToString();//客户名称
                    string linkman = dataGridViewFuJia.Rows[e.RowIndex].Cells["linkMan"].Value.ToString();//联系人
                    string phone = dataGridViewFuJia.Rows[e.RowIndex].Cells["mobilePhone"].Value.ToString();//电话
                    string fax = dataGridViewFuJia.Rows[e.RowIndex].Cells["fax"].Value.ToString();//传真
                    labtxtDanJuType.Text = name;
                    labtextboxTop2.Text = linkman;
                    labtextboxTop3.Text = phone;
                    labtextboxTop8.Text = fax;
                    resizablePanel1.Visible = false;
                }
                //销售员
                if (_Click == 2 || _Click == 5)
                {
                    _employeeCode = dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString();//销售员code
                    string name = dataGridViewFuJia.Rows[e.RowIndex].Cells["name"].Value.ToString();//销售员
                    ltxtbSalsMan.Text = name;
                    resizablePanel1.Visible = false;
                }
                //仓库
                if (_Click == 3 || _Click == 6)
                {
                    _storgeCode = dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString();//仓库code
                    string name = dataGridViewFuJia.Rows[e.RowIndex].Cells["name"].Value.ToString();//仓库
                    labtextboxTop5.Text = name;
                    resizablePanel1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("双击绑定数据错误！请检查：" + ex.Message);
            }
        }

        /// <summary>
        /// 双击绑定商品数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewShangPing_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //是否要新增一行的标记
                bool newAdd = false;
                GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[ClickRowIndex];
                //id字段为空 说明是没有数据的行 不是修改而是新增
                if (gr.Cells["gridColumnid"].Value == null)
                {
                    newAdd = true;
                }
                _materialCode = dataGridViewShangPing.Rows[e.RowIndex].Cells["code"].Value.ToString();//商品code 
                gr.Cells["materialCode"].Value = _materialCode;//商品code 
                gr.Cells["material"].Value = dataGridViewShangPing.Rows[e.RowIndex].Cells["materialDaima"].Value;//商品代码
                gr.Cells["name"].Value = dataGridViewShangPing.Rows[e.RowIndex].Cells["name"].Value;//商品名称
                gr.Cells["model"].Value = dataGridViewShangPing.Rows[e.RowIndex].Cells["model"].Value;//规格型号
                gr.Cells["barcode"].Value = dataGridViewShangPing.Rows[e.RowIndex].Cells["barCode"].Value;//条形码
                gr.Cells["unit"].Value = dataGridViewShangPing.Rows[e.RowIndex].Cells["unit"].Value;//单位
                gr.Cells["dinggouNumber"].Value = 1.00;//数量
                gr.Cells["price"].Value = dataGridViewShangPing.Rows[e.RowIndex].Cells["price"].Value;//单价
                double discount = 100.00;
                gr.Cells["DiscountRate"].Value = discount;//折扣率
                gr.Cells["TaxRate"].Value = 17.00;//增值税税率
                gr.Cells["remark"].Value = dataGridViewShangPing.Rows[e.RowIndex].Cells["remark"].Value;//备注

                ////计算金额
                decimal number = Convert.ToDecimal(gr.Cells["dinggouNumber"].FormattedValue);//订购数量
                decimal price = Convert.ToDecimal(gr.Cells["price"].FormattedValue);//单价               
                decimal money = number * price;//金额
                gr.Cells["money"].Value = money;
                decimal discountRate = Convert.ToDecimal(gr.Cells["DiscountRate"].FormattedValue);//折扣率
                decimal discountAfter = money * (discountRate / 100);
                decimal discountMoney = money - discountAfter;//折扣额
                gr.Cells["DiscountMoney"].Value = discountMoney;
                decimal taxRate = Convert.ToDecimal(gr.Cells["TaxRate"].FormattedValue);//增值税税率
                decimal rateMoney = money * (taxRate / 100);//税额
                gr.Cells["TaxMoney"].Value = rateMoney;
                decimal priceAndtax = money + rateMoney;//价税合计
                gr.Cells["priceANDtax"].Value = priceAndtax;
                resizablePanelData.Visible = false;

                //逐行统计数据总数
                decimal tempAllNumber = 0;
                decimal tempAllMoney = 0;
                decimal tempAllTaxMoney = 0;
                decimal tempAllPriceAndTax = 0;
                for (int i = 0; i < superGridControlShangPing.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControlShangPing.PrimaryGrid.Rows[i] as GridRow;
                    tempAllNumber += Convert.ToDecimal(tempGR["dinggouNumber"].FormattedValue);
                    tempAllMoney += Convert.ToDecimal(tempGR["money"].FormattedValue);
                    tempAllTaxMoney += Convert.ToDecimal(tempGR["TaxMoney"].FormattedValue);
                    tempAllPriceAndTax += Convert.ToDecimal(tempGR["priceANDtax"].FormattedValue);
                }
                _Materialnumber = tempAllNumber;
                _Money = tempAllMoney;
                _TaxMoney = tempAllTaxMoney;
                _PriceAndTaxMoney = tempAllPriceAndTax;
                gr = (GridRow)superGridControlShangPing.PrimaryGrid.LastSelectableRow;
                gr["dinggouNumber"].Value = _Materialnumber.ToString();
                gr["money"].Value = _Money.ToString();
                gr["TaxMoney"].Value = _TaxMoney.ToString();
                gr["priceANDtax"].Value = _PriceAndTaxMoney.ToString();

                //新增一行
                if (newAdd)
                {
                    superGridControlShangPing.PrimaryGrid.NewRow(superGridControlShangPing.PrimaryGrid.Rows.Count);
                    //递增数量和金额 默认为1和单价 
                    gr = (GridRow)superGridControlShangPing.PrimaryGrid.LastSelectableRow;
                    _MaterialNumber += 1;
                    gr.Cells["dinggouNumber"].Value = _Materialnumber;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-绑定商品数据错误" + ex.Message);
            }

            superGridControlShangPing.Focus();
            SendKeys.Send("^{End}{Home}");
        }

        #endregion

        #region 模糊查询

        /// <summary>
        /// 销售员模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ltxtbSalsMan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ltxtbSalsMan.Text.Trim() == "")
                {
                    _Click = 5;
                    InitEmployee();
                    return;
                }
                dataGridViewFuJia.DataSource = null;
                dataGridViewFuJia.Columns.Clear();

                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "code";
                dgvc.HeaderText = "员工工号";
                dgvc.DataPropertyName = "code";
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "name";
                dgvc.HeaderText = "姓名";
                dgvc.DataPropertyName = "name";
                dataGridViewFuJia.Columns.Add(dgvc);

                dataGridViewFuJia.DataSource = ch.DataTableReCoding(employee.GetList(0, "" + XYEEncoding.strCodeHex(ltxtbSalsMan.Text.Trim()) + ""));
                resizablePanel1.Visible = true;
                if (this.WindowState == FormWindowState.Maximized)
                {
                    resizablePanel1.Location = new Point(220, 670);
                    return;
                }
                if (this.WindowState == FormWindowState.Normal)
                {
                    resizablePanel1.Location = new Point(234, 460);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-销售员模糊查询数据失败！" + ex.Message);
            }
        }

        /// <summary>
        /// 客户模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labtxtDanJuType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.labtxtDanJuType.Text.Trim() == "")
                {
                    InitClient();
                    _Click = 4;
                    return;
                }
                dataGridViewFuJia.DataSource = null;
                dataGridViewFuJia.Columns.Clear();

                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "code";
                dgvc.HeaderText = "客户编号";
                dgvc.DataPropertyName = "code";
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "name";
                dgvc.HeaderText = "客户姓名";
                dgvc.DataPropertyName = "name";
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "mobilePhone";
                dgvc.HeaderText = "电话";
                dgvc.DataPropertyName = "mobilePhone";
                dgvc.Visible = false;
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "linkMan";
                dgvc.HeaderText = "联系人";
                dgvc.DataPropertyName = "linkMan";
                dgvc.Visible = false;
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "fax";
                dgvc.HeaderText = "传真";
                dgvc.DataPropertyName = "fax";
                dgvc.Visible = false;
                dataGridViewFuJia.Columns.Add(dgvc);

                resizablePanel1.Location = new Point(230, 160);
                string name = XYEEncoding.strCodeHex(this.labtxtDanJuType.Text.Trim());
                dataGridViewFuJia.DataSource = ch.DataTableReCoding(client.GetList(0, name));
                resizablePanel1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：模糊查询客户数据错误" + ex.Message, "销售订单温馨提示");
            }
        }

        /// <summary>
        /// 商品代码模糊搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControlShangPing_EditorValueChanged(object sender, GridEditEventArgs e)
        {
            try
            {
                string SS = "";
                GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[ClickRowIndex];
                GridCell gc = gr[0] as GridCell;
                string materialDaima = XYEEncoding.strCodeHex(e.EditControl.EditorValue.ToString());
                if (SS == "")
                {
                    //模糊查询商品列表
                    _AllMaterial = materialInterface.GetList(0, "" + materialDaima + "");
                    InitMaterialDataGridView();
                    dataGridViewShangPing.DataSource = ch.DataTableReCoding(_AllMaterial);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:-表格商品模糊查询错误，查询数据错误" + ex.Message, "销售订单温馨提示");
            }
        }

        #endregion

        /// <summary>
        /// 按ESC关闭副表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SalesOrderForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.resizablePanel1.Visible = false;
                this.resizablePanelData.Visible = false;
            }
        }

        /// <summary>
        /// 验证和统计数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControlShangPing_CellValidated(object sender, GridCellValidatedEventArgs e)
        {
            try
            {
                //最后一行做统计行
                GridRow gr = e.GridPanel.Rows[e.GridCell.RowIndex] as GridRow;
                ////计算金额
                decimal number = Convert.ToDecimal(gr.Cells["dinggouNumber"].FormattedValue);//订购数量
                decimal price = Convert.ToDecimal(gr.Cells["price"].FormattedValue);//单价               
                decimal money = number * price;//金额
                gr.Cells["money"].Value = money;
                decimal discountRate = Convert.ToDecimal(gr.Cells["DiscountRate"].FormattedValue);//折扣率
                decimal discountAfter = money * (discountRate / 100);
                decimal discountMoney = money - discountAfter;//折扣额
                gr.Cells["DiscountMoney"].Value = discountMoney;
                decimal taxRate = Convert.ToDecimal(gr.Cells["TaxRate"].FormattedValue);//增值税税率
                decimal rateMoney = money * (taxRate / 100);//税额
                gr.Cells["TaxMoney"].Value = rateMoney;
                decimal priceAndtax = money + rateMoney;//价税合计
                gr.Cells["priceANDtax"].Value = priceAndtax;

                //逐行统计数据总数
                decimal tempAllNumber = 0;
                decimal tempAllMoney = 0;
                decimal tempAllTaxMoney = 0;
                decimal tempAllPriceAndTax = 0;
                for (int i = 0; i < superGridControlShangPing.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControlShangPing.PrimaryGrid.Rows[i] as GridRow;
                    tempAllNumber += Convert.ToDecimal(tempGR["dinggouNumber"].FormattedValue);
                    tempAllMoney += Convert.ToDecimal(tempGR["money"].FormattedValue);
                    tempAllTaxMoney += Convert.ToDecimal(tempGR["TaxMoney"].FormattedValue);
                    tempAllPriceAndTax += Convert.ToDecimal(tempGR["priceANDtax"].FormattedValue);
                }
                _Materialnumber = tempAllNumber;
                _Money = tempAllMoney;
                _TaxMoney = tempAllTaxMoney;
                _PriceAndTaxMoney = tempAllPriceAndTax;
                gr = (GridRow)superGridControlShangPing.PrimaryGrid.LastSelectableRow;
                gr["dinggouNumber"].Value = _Materialnumber.ToString();
                gr["money"].Value = _Money.ToString();
                gr["TaxMoney"].Value = _TaxMoney.ToString();
                gr["priceANDtax"].Value = _PriceAndTaxMoney.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-统计数量出错！请检查：" + ex.Message);
            }
        }

        private void SalesOrderForm_Activated(object sender, EventArgs e)
        {
            labtxtDanJuType.Focus();
        }

        #region 验证只能输入数字和小数点
        /// <summary>
        /// 验证只能输入数字和小数点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ltxtTopYiShouDingJin_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)//小数点
            {
                if (labtextboxTop7.Text.Length < 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(labtextboxTop7.Text, out oldf);
                    b2 = float.TryParse(labtextboxTop7.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }

        }

        private void labtextboxTop7_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(labtextboxTop7.Text)) return;

            // 按千分位逗号格式显示！
            double d = Convert.ToDouble(skipComma(labtextboxTop7.Text));
            //labtextboxTop7.Text = string.Format("{0:#,#}", d);
            labtextboxTop7.Text = string.Format("{0:#,#}", d);
            // 确保输入光标在最右侧
            labtextboxTop7.Select(labtextboxTop7.Text.Length, 0);
        }
        private string skipComma(string str)
        {
            string[] ss = null;
            string strnew = "";
            if (str == "")
            {
                strnew = "0";
            }
            else
            {
                ss = str.Split(',');
                for (int i = 0; i < ss.Length; i++)
                {
                    strnew += ss[i];
                }
            }
            return strnew;
        }
        #endregion
    }
}
