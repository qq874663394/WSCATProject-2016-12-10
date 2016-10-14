using DevComponents.DotNetBar.SuperGrid;
using HelperUtility;
using HelperUtility.Encrypt;
using InterfaceLayer.Base;
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
using InterfaceLayer.Sales;

namespace WSCATProject.Sales
{
    public partial class SalesTicketForm : TestForm
    {
        public SalesTicketForm()
        {
            InitializeComponent();
        }

        #region 调用接口以及加密解密方法
        CodingHelper ch = new CodingHelper();
        ClientInterface client = new ClientInterface();//客户
        EmpolyeeInterface employee = new EmpolyeeInterface();//员工  
        StorageInterface storage = new StorageInterface();//仓库
        BankAccountInterface bank = new BankAccountInterface();//结算账户

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
        /// 所有结算账户
        /// </summary>
        private DataTable _AllBank = null;
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
        /// 保存账户code
        /// </summary>
        private string _bankCode;
        /// <summary>
        /// 所有商品列表
        /// </summary>
        private DataTable _AllMaterial = null;
        /// <summary>
        /// 统计数量
        /// </summary>
        private decimal _Materialnumber;
        /// <summary>
        /// 销售订单code
        /// </summary>
        private string _SalesOrderCode;
        /// <summary>
        /// 保存仓库的字典集合
        /// </summary>
        private KeyValuePair<string, string> _ClickStorageList;
        /// <summary>
        /// 销售订单的主表code
        /// </summary>
        private string _salesOrderMainCode;
        /// <summary>
        /// 统计发货数量
        /// </summary>
        private decimal _FaHuoShuLiang;
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
        private string _shangPinCode;
        /// <summary>
        /// 成本金额
        /// </summary>
        private decimal _chengBenJinE;

        public string SalesOrderMainCode
        {
            get { return _salesOrderMainCode; }
            set { _salesOrderMainCode = value; }
        }

        /// <summary>
        /// 销售订单详细code
        /// </summary>
        public string SalesOrderCode
        {
            get { return _salesOrderCode; }
            set { _salesOrderCode = value; }
        }
        /// <summary>
        /// 商品code
        /// </summary>
        public string ShangPinCode
        {
            get
            {
                return _shangPinCode;
            }

            set
            {
                _shangPinCode = value;
            }
        }

        private string _salesOrderCode;
        private decimal _MaterialNumber;
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
            gr.Cells["gridColumndinggoushu"].Value = 0;
            gr.Cells["gridColumndinggoushu"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumndinggoushu"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumnfahuoshu"].Value = 0;
            gr.Cells["gridColumnfahuoshu"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnfahuoshu"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumnmoney"].Value = 0;
            gr.Cells["gridColumnmoney"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnmoney"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumnshuie"].Value = 0;
            gr.Cells["gridColumnshuie"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnshuie"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumnjiashuiheji"].Value = 0;
            gr.Cells["gridColumnjiashuiheji"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnjiashuiheji"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumnchengbenjine"].Value = 0;
            gr.Cells["gridColumnchengbenjine"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnchengbenjine"].CellStyles.Default.Background.Color1 = Color.Orange;
        }

        /// <summary>
        /// 非空验证
        /// </summary>
        private bool isNUllValidate()
        {
            if (labtextboxTop2.Text.Trim() == null || labtextboxTop2.Text == "")
            {
                MessageBox.Show("客户不能为空！");
                return false;
            }
            GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[0];
            if (gr.Cells["gridColumnStock"].Value == null || gr.Cells["gridColumnStock"].Value.ToString() == "")
            {
                MessageBox.Show("仓库不能为空！");
                return false;
            }
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

                resizablePanel1.Location = new Point(470, 160);
                dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllClient);
                resizablePanel1.Visible = true;
            }
        }

        /// <summary>
        /// 初始化销售员
        /// </summary>
        private void InitEmployee()
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
                    resizablePanel1.Location = new Point(220, 520);
                    return;
                }
            }
        }

        /// <summary>
        /// 初始化仓库
        /// </summary>
        private void InitStorage()
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

                //查询仓库的方法
                dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllStorage);
                resizablePanel1.Visible = true;
            }
        }

        /// <summary>
        /// 初始化结算账户
        /// </summary>
        private void InitBank()
        {
            try
            {
                if (_Click != 4)
                {
                    _Click = 4;
                    dataGridViewFuJia.DataSource = null;
                    dataGridViewFuJia.Columns.Clear();

                    DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "code";
                    dgvc.HeaderText = "账户编号";
                    dgvc.DataPropertyName = "code";
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "openBank";
                    dgvc.HeaderText = "开户行";
                    dgvc.DataPropertyName = "openBank";
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "bankCard";
                    dgvc.HeaderText = "银行账户";
                    dgvc.DataPropertyName = "bankCard";
                    dgvc.Visible = false;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "cardHolder";
                    dgvc.HeaderText = "持卡人";
                    dgvc.DataPropertyName = "cardHolder";
                    dgvc.Visible = false;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "remark";
                    dgvc.HeaderText = "备注";
                    dgvc.DataPropertyName = "remark";
                    dgvc.Visible = false;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "availableBalance";
                    dgvc.HeaderText = "可用额度";
                    dgvc.DataPropertyName = "availableBalance";
                    dgvc.Visible = false;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    resizablePanel1.Location = new Point(470, 230);
                    dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllBank);
                    resizablePanel1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化结算账户数据失败！请检查：" + ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SalesTicketForm_Load(object sender, EventArgs e)
        {
            try
            {
                toolStripButtonXuanYuanDan.Visible = true;
                //客户
                _AllClient = client.GetClientByBool(false);
                //销售员
                _AllEmployee = employee.SelSupplierTable(false);
                //仓库
                _AllStorage = storage.GetList(00, "");
                //结算账户
                _AllBank = bank.GetList(999, "", false, false);

                //绑定事件 双击事填充内容并隐藏列表
                dataGridViewFuJia.CellDoubleClick += dataGridViewFuJia_CellDoubleClick;
                dataGridViewShangPing.CellDoubleClick += dataGridViewShangPing_CellDoubleClick;

                toolStripButtonXuanYuanDan.Visible = true;
                toolStripBtnSave.Click += ToolStripBtnSave_Click;//保存按钮
                toolStripBtnShengHe.Click += ToolStripBtnShengHe_Click;//审核按钮
                toolStripButtonXuanYuanDan.Click += ToolStripButtonXuanYuanDan_Click;//选源单的点击事件

                #region 初始化窗体

                comboBoxType.SelectedIndex = 0;//单据类型
                comboBoxfapiaotype.SelectedIndex = 0;//发票类型
                cboJiesuanMethod.SelectedIndex = 0;//结算方式

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

                //生成销售单code和显示条形码
                _SalesOrderCode = BuildCode.ModuleCode("ST");
                textBoxOddNumbers.Text = _SalesOrderCode;
                barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
                _Code.ValueFont = new Font("微软雅黑", 20);
                System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
                pictureBoxtiaoxingma.Image = imgTemp;

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-初始化数据错误" + ex.Message);
            }


        }

        /// <summary>
        /// 选源单的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonXuanYuanDan_Click(object sender, EventArgs e)
        {
            if (_clientCode == "" || labtextboxTop2.Text == "")
            {
                MessageBox.Show("请先选择客户!");
                return;
            }
            SalesOrderReportForm salesOrder = new SalesOrderReportForm();
            salesOrder.clientCode = _clientCode;
            salesOrder.ShowDialog(this);
            GridItemsCollection grs = superGridControlShangPing.PrimaryGrid.Rows;
            GridRow grid = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[ClickRowIndex];
            SalesOrderInterface salesorder = new SalesOrderInterface();
            DataTable dt = ch.DataTableReCoding(salesorder.GetSelectedDetail(XYEEncoding.strCodeHex(_salesOrderMainCode), XYEEncoding.strCodeHex(_salesOrderCode)));
            foreach (GridRow g in grs)
            {
                if (g.Cells["material"].Value == null || g.Cells["material"].Value.ToString() == "合计")
                {
                    continue;
                }
                if (g.Cells["gridColumncode"].Value.Equals(dt.Rows[0]["materialCode"]))
                {

                    decimal shuliang = Convert.ToDecimal(g.Cells["gridColumndinggoushu"].Value);
                    decimal gridColumnmoney = Convert.ToDecimal(g.Cells["gridColumnmoney"].Value);
                    decimal gridColumnshuie = Convert.ToDecimal(g.Cells["gridColumnshuie"].Value);
                    decimal gridColumnjiashuiheji = Convert.ToDecimal(g.Cells["gridColumnjiashuiheji"].Value);
                    decimal gridColumnchengbenjine = Convert.ToDecimal(g.Cells["gridColumnchengbenjine"].Value);
                    shuliang += 1;
                    gridColumnmoney += gridColumnmoney;
                    gridColumnshuie += gridColumnshuie;
                    gridColumnjiashuiheji += gridColumnjiashuiheji;
                    gridColumnchengbenjine += gridColumnchengbenjine;
                    g.Cells["gridColumndinggoushu"].Value = shuliang;
                    g.Cells["gridColumnmoney"].Value = gridColumnmoney;
                    g.Cells["gridColumnshuie"].Value = gridColumnshuie;
                    g.Cells["gridColumnjiashuiheji"].Value = gridColumnjiashuiheji;
                    g.Cells["gridColumnchengbenjine"].Value = gridColumnchengbenjine;
                    //逐行统计数据总数
                    decimal tempAllMaterialNumber = 0;
                    decimal tempAllMoney = 0;
                    decimal tempAllTaxMoney = 0;
                    decimal tempAllPriceAndTax = 0;
                    decimal tempAllChengBenJine = 0;
                    for (int i = 0; i < superGridControlShangPing.PrimaryGrid.Rows.Count - 1; i++)
                    {
                        GridRow tempGR = superGridControlShangPing.PrimaryGrid.Rows[i] as GridRow;
                        tempAllMaterialNumber += Convert.ToDecimal(tempGR["gridColumndinggoushu"].FormattedValue);
                        tempAllMoney += Convert.ToDecimal(tempGR["gridColumnmoney"].FormattedValue);
                        tempAllTaxMoney += Convert.ToDecimal(tempGR["gridColumnshuie"].FormattedValue);
                        tempAllPriceAndTax += Convert.ToDecimal(tempGR["gridColumnjiashuiheji"].FormattedValue);
                        tempAllChengBenJine += Convert.ToDecimal(tempGR["gridColumnchengbenjine"].FormattedValue);
                    }
                    _MaterialNumber = tempAllMaterialNumber;
                    _Money = tempAllMoney;
                    _TaxMoney = tempAllTaxMoney;
                    _PriceAndTaxMoney = tempAllPriceAndTax;
                    _chengBenJinE = tempAllChengBenJine;
                    grid = (GridRow)superGridControlShangPing.PrimaryGrid.LastSelectableRow;
                    grid["gridColumndinggoushu"].Value = _MaterialNumber.ToString();
                    grid["gridColumnmoney"].Value = _Money.ToString();
                    grid["gridColumnshuie"].Value = _TaxMoney.ToString();
                    grid["gridColumnjiashuiheji"].Value = _PriceAndTaxMoney.ToString();
                    grid["gridColumnchengbenjine"].Value = _chengBenJinE.ToString();
                    resizablePanelData.Visible = false;
                    return;
                }
                continue;
            }
            superGridControlShangPing.PrimaryGrid.Rows.Add(new GridRow("", dt.Rows[0]["materialDaima"],
                dt.Rows[0]["name"],
                dt.Rows[0]["model"],
                dt.Rows[0]["barCode"],
                dt.Rows[0]["unit"],
                dt.Rows[0]["materialNumber"].ToString() == "" ? 0.0M : dt.Rows[0]["materialNumber"],
                0.0M,
                dt.Rows[0]["materialPrice"].ToString() == "" ? 0.0M : dt.Rows[0]["materialPrice"],
                dt.Rows[0]["discountRate"].ToString() == "" ? 0.0M : dt.Rows[0]["discountRate"],
                dt.Rows[0]["VATRate"].ToString() == "" ? 0.0M : dt.Rows[0]["VATRate"],
                dt.Rows[0]["discountMoney"].ToString() == "" ? 0.0M : dt.Rows[0]["discountMoney"],
                dt.Rows[0]["materialMoney"].ToString() == "" ? 0.0M : dt.Rows[0]["materialMoney"],
                dt.Rows[0]["tax"],
                dt.Rows[0]["taxTotal"],
                dt.Rows[0]["inPrice"],
                dt.Rows[0]["inMoney"].ToString() == "" ? 0.0M : dt.Rows[0]["inMoney"],
                dt.Rows[0]["productionDate"],
                dt.Rows[0]["effectiveDate"],
                dt.Rows[0]["qualityDate"],
                dt.Rows[0]["remark"],
                _salesOrderMainCode,
                dt.Rows[0]["materialCode"]
                ));
            try
            {
                GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[ClickRowIndex];
                //逐行统计数据总数
                decimal tempAllNumber = 0;
                decimal tempAllMoney = 0;
                decimal tempAllTaxMoney = 0;
                decimal tempAllPriceAndTax = 0;
                decimal tempAllChengBenJine = 0;
                for (int i = 0; i < superGridControlShangPing.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControlShangPing.PrimaryGrid.Rows[i] as GridRow;
                    tempAllNumber += Convert.ToDecimal(tempGR["gridColumndinggoushu"].FormattedValue);
                    tempAllMoney += Convert.ToDecimal(tempGR["gridColumnmoney"].FormattedValue);
                    tempAllTaxMoney += Convert.ToDecimal(tempGR["gridColumnshuie"].FormattedValue);
                    tempAllPriceAndTax += Convert.ToDecimal(tempGR["gridColumnjiashuiheji"].FormattedValue);
                    tempAllChengBenJine += Convert.ToDecimal(tempGR["gridColumnchengbenjine"].FormattedValue);
                }
                _Materialnumber = tempAllNumber;
                _Money = tempAllMoney;
                _TaxMoney = tempAllTaxMoney;
                _PriceAndTaxMoney = tempAllPriceAndTax;
                _chengBenJinE = tempAllChengBenJine;
                gr = (GridRow)superGridControlShangPing.PrimaryGrid.LastSelectableRow;
                gr["gridColumndinggoushu"].Value = _Materialnumber.ToString();
                gr["gridColumnmoney"].Value = _Money.ToString();
                gr["gridColumnshuie"].Value = _TaxMoney.ToString();
                gr["gridColumnjiashuiheji"].Value = _PriceAndTaxMoney.ToString();
                gr["gridColumnchengbenjine"].Value = _chengBenJinE.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-统计数量出错！请检查：" + ex.Message);
            }
        }
        /// <summary>
        /// 审核按钮的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripBtnShengHe_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 保存按钮的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripBtnSave_Click(object sender, EventArgs e)
        {
            //非空验证
            if (isNUllValidate() == false)
            {
                return;
            }
            //获得界面上的数据,准备传给base层新增数据

            //WarehouseInInterface warehouseInterface = new WarehouseInInterface();
            //入库单
            SalesMain salesMain = new SalesMain();
            //入库商品列表
            List<SalesDetail> salesdetialList = new List<SalesDetail>();
            try
            {
                salesMain.accountCode = XYEEncoding.strCodeHex(txtBank.Text);//结算账户
                salesMain.checkMan = XYEEncoding.strCodeHex(ltxtbMakeMan.Text);//审核人
                salesMain.checkState = 0;//审核状态
                salesMain.clientAddress = "";//客户地址
                salesMain.clientCode = XYEEncoding.strCodeHex(_clientCode);//客户地址
                salesMain.clientName = XYEEncoding.strCodeHex(labtextboxTop2.Text);//客户姓名
                salesMain.clientPhone = XYEEncoding.strCodeHex(labtextboxTop3.Text);//客户电话
                salesMain.code = XYEEncoding.strCodeHex(textBoxOddNumbers.Text);//编号
                salesMain.collectMoney = Convert.ToDecimal(labtextboxTop7.Text);//本次收款
                salesMain.date = dateTimePicker1.Value;//订单日期
                salesMain.disInvoiceMoney = Convert.ToDecimal(txtWeiKaiPiao.Text);//未开票金额
                salesMain.expireDate = null;//最晚到底时间
                salesMain.invoiceMoney = Convert.ToDecimal(txtYiKaiPiao.Text);//已开票金额
                salesMain.invoiceNumber = XYEEncoding.strCodeHex(labtextboxTop5.Text);//发票号码
                salesMain.invoiceType = XYEEncoding.strCodeHex(comboBoxfapiaotype.Text);//发票类型
                salesMain.isClear = 1;//是否删除
                salesMain.lastMoney = 0.0M;//剩余尾款
                salesMain.linkMan = XYEEncoding.strCodeHex(labtextboxTop8.Text);//联系人
                salesMain.oddAllMoney = Convert.ToDecimal(labtextboxTop9.Text);//本单总额
                salesMain.operationMan = XYEEncoding.strCodeHex(ltxtbMakeMan.Text);//操作人
                salesMain.payMathod = XYEEncoding.strCodeHex(cboJiesuanMethod.Text);//付款方式
                salesMain.payState = 0;//是否付款
                salesMain.Preferentialsubjects = XYEEncoding.strCodeHex(txtYouHuiKeMu.Text);//优惠科目
                salesMain.receiptDate = dateTimePickershoukuan.Value;//收款日期
                salesMain.remark = XYEEncoding.strCodeHex(txtZhaiYao.Text);//备注
                salesMain.reserved1 = "";
                salesMain.reserved2 = "";
                salesMain.salesMan = XYEEncoding.strCodeHex(ltxtbSalsMan.Text);//销售员
                salesMain.salesOrderState = 0;//发货状态
                salesMain.type = XYEEncoding.strCodeHex(comboBoxType.Text);//单据状态
                salesMain.updateDate = DateTime.Now;//更改时间
                salesMain.urgentState = 0;//加急状态

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:;尝试创建销售单商品数据出错,请检查输入" + ex.Message, "销售单温馨提示");
                return;
            }
            try
            {
                //获得商品列表数据,准备传给base层新增数据
                GridRow g = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[ClickRowIndex];
                GridItemsCollection grs = superGridControlShangPing.PrimaryGrid.Rows;
                int i = 0;
                foreach (GridRow gr in grs)
                {
                    if (gr["gridColumnname"].Value != null)
                    {

                        i++;
                        SalesDetail salesDetail = new SalesDetail();
                        salesDetail.actualNumber = 0.0M;
                        salesDetail.barCode = gr["gridColumntiaoxingma"].Value == null ? "" : XYEEncoding.strCodeHex(gr["gridColumntiaoxingma"].Value.ToString());//条形码
                        salesDetail.code = XYEEncoding.strCodeHex(textBoxOddNumbers.Text + i.ToString());//销售详细ccode
                        salesDetail.costMoney = gr["gridColumnchengbenjine"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnchengbenjine"].Value);//成本金额
                        salesDetail.costPrice = gr["gridColumnchengbeidanjia"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnchengbeidanjia"].Value);//成本单价
                        salesDetail.discount = gr["gridColumnzhekoul"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnzhekoul"].Value);//折扣
                        salesDetail.discountAfterPrice = gr["gridColumnprice"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnprice"].Value);//折扣前单价

                        salesDetail.discountBeforePrice = 0.0M;//折扣后单价

                        salesDetail.discountMoney = gr["gridColumnzhekoue"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnzhekoue"].Value);//折扣额
                        salesDetail.effectiveDate = Convert.ToDateTime(gr["gridColumnyouxiaoqi"].Value == null ? null : gr["gridColumnyouxiaoqi"].Value);//有效期至
                        salesDetail.isClear = 1;//是否删除
                        salesDetail.leviedMoney = gr["gridColumnjiashuiheji"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnjiashuiheji"].Value);//价税合计

                        salesDetail.lostNumber = gr["gridColumnfahuoshu"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnfahuoshu"].Value);//发货数量

                        salesDetail.materialCode = gr["gridColumncode"].Value == null ? "" : XYEEncoding.strCodeHex(gr["gridColumncode"].Value.ToString());//物料编号
                        salesDetail.materialDaima = gr["material"].Value == null ? "" : XYEEncoding.strCodeHex(gr["material"].Value.ToString());//物料代码
                        salesDetail.materialName = gr["gridColumnmaterialname"].Value == null ? "" : XYEEncoding.strCodeHex(gr["gridColumnmaterialname"].Value.ToString());//物料名称
                        salesDetail.materiaModel = gr["gridColumnmode"].Value == null ? "" : XYEEncoding.strCodeHex(gr["gridColumnmode"].Value.ToString());//物料规格型号
                        salesDetail.money = gr["gridColumnmoney"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnmoney"].Value);//金额
                        salesDetail.needNumber = gr["gridColumndinggoushu"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumndinggoushu"].Value);//需求数量

                        salesDetail.productionDate = null;//生产采购日期

                        salesDetail.qualityDate = gr["gridColumnbaozhiqi"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnbaozhiqi"].Value);//保质期

                        salesDetail.remark = gr["gridColumnremark"].Value == null ? "" : XYEEncoding.strCodeHex(gr["gridColumnremark"].Value.ToString());//备注
                        salesDetail.reserved1 = "";//保留字段
                        salesDetail.reserved2 = "";//保留字段
                        salesDetail.ReturnsNumber = 0.0M;//退货数量
                        salesDetail.mainCode = XYEEncoding.strCodeHex(textBoxOddNumbers.Text);//主表code
                        salesDetail.sourceCode = XYEEncoding.strCodeHex(_salesOrderMainCode);//销售订单code
                        salesDetail.storageCode = XYEEncoding.strCodeHex( _storgeCode);//仓库code
                        salesDetail.storageName =gr["gridColumnStock"].Value==null?"":XYEEncoding.strCodeHex(gr["gridColumnStock"].Value) ;//仓库名称
                        salesDetail.tax = gr["gridColumnshuie"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnshuie"].Value);//税额
                        salesDetail.unit = gr["gridColumndanwei"].Value == null ? "" : XYEEncoding.strCodeHex(gr["gridColumndanwei"].Value.ToString());//单位
                        salesDetail.updateDate = DateTime.Now;//更改时间
                        salesDetail.VATRate = gr["gridColumnzengzhishui"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnzengzhishui"].Value);//增值税税率
                        salesDetail.zhujima = gr["material"].Value == null ? "" : XYEEncoding.strCodeHex(gr["material"].Value.ToString());//助记码
                        GridRow dr = superGridControlShangPing.PrimaryGrid.Rows[0] as GridRow;
                        salesdetialList.Add(salesDetail);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-尝试创建销售单详商品数据出错,请检查输入" + ex.Message, "销售单温馨提示");
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

        private void dataGridViewFuJia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //客户
                if (_Click == 1 || _Click == 5)
                {
                    _clientCode = dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString();//客户code
                    string name = dataGridViewFuJia.Rows[e.RowIndex].Cells["name"].Value.ToString();//客户名称
                    string linkman = dataGridViewFuJia.Rows[e.RowIndex].Cells["linkMan"].Value.ToString();//联系人
                    string phone = dataGridViewFuJia.Rows[e.RowIndex].Cells["mobilePhone"].Value.ToString();//电话
                    string fax = dataGridViewFuJia.Rows[e.RowIndex].Cells["fax"].Value.ToString();//传真
                    labtextboxTop2.Text = name;
                    labtextboxTop8.Text = linkman;
                    labtextboxTop3.Text = phone;
                    labtextboxTop6.Text = fax;
                    resizablePanel1.Visible = false;
                }
                //销售员
                if (_Click == 2 || _Click == 6)
                {
                    _employeeCode = dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString();//销售员code
                    string name = dataGridViewFuJia.Rows[e.RowIndex].Cells["name"].Value.ToString();//销售员
                    ltxtbSalsMan.Text = name;
                    resizablePanel1.Visible = false;
                }
                //仓库
                if (_Click == 3 || _Click == 7)
                {
                    GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[ClickRowIndex];
                    string code = dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString();
                    string Name = dataGridViewFuJia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                    gr.Cells["gridColumnStock"].Value = Name;
                    gr.Cells["gridColumncode"].Value = code;
                    _ClickStorageList = new KeyValuePair<string, string>(code, Name);
                    _storgeCode = code;
                    resizablePanel1.Visible = false;
                }
                //结算账户
                if (_Click == 4 || _Click == 8)
                {
                    GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[ClickRowIndex];
                    string code = dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString();
                    string Name = dataGridViewFuJia.Rows[e.RowIndex].Cells["openBank"].Value.ToString();
                    txtBank.Text = Name;                   
                    _bankCode = code;
                    resizablePanel1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("双击绑定数据错误！请检查：" + ex.Message);
            }
        }

        /// <summary>
        /// 双击绑定商品的
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewShangPing_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /// <summary>
        /// 客户的下拉箭头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxClient_Click(object sender, EventArgs e)
        {
            if (_Click != 1)
            {
                InitClient();
            }
            _Click = 5;
        }
        /// <summary>
        /// 销售员的下拉箭头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxEmployee_Click(object sender, EventArgs e)
        {
            if (_Click != 2)
            {
                InitEmployee();
            }
            _Click = 6;
        }
        /// <summary>
        /// 结算账户下拉箭头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxBank_Click(object sender, EventArgs e)
        {
            if (_Click != 4)
            {
                InitBank();
            }
            _Click = 8;
        }


        /// <summary>
        /// 第一行第一列选择仓库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControlShangPing_BeginEdit(object sender, GridEditEventArgs e)
        {
            try
            {
                if (e.GridCell.GridColumn.Name == "gridColumnStock")
                {
                    if (_Click != 3)
                    {
                        InitStorage();
                    }
                    _Click = 7;
                    return;
                }
                if (e.GridCell.GridColumn.Name == "material")
                {
                    //if (_StorageCode != "")
                    //{
                    //    //查询商品列表
                    //    _AllMaterial = waremain.GetWMainAndMaterialByWMCode(999, "", XYEEncoding.strCodeHex(_StorageCode));
                    //    InitMaterialDataGridView();
                    //}
                    //else
                    //{
                    //    resizablePanelData.Visible = false;
                    //    MessageBox.Show("请先选择仓库：");
                    //}
                }
            }
            catch (Exception)
            {
                //if (_StorageCode != "")
                //{
                //    //查询商品列表
                //    _AllMaterial = waremain.GetWMainAndMaterialByWMCode(999, "", XYEEncoding.strCodeHex(_StorageCode));
                //    InitMaterialDataGridView();
                //}
                //else
                //{
                //    this.resizablePanelData.Visible = false;
                //    MessageBox.Show("请先选择仓库！");
                //}
            }

        }

        /// <summary>
        /// 客户的模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labtextboxTopClient_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.labtxtDanJuType.Text.Trim() == "")
                {
                    InitClient();
                    _Click = 5;
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
        /// 销售员的模糊查询
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
        /// 结算账户模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBank_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtBank.Text.Trim() == "")
                {
                    InitBank();
                    _Click = 8;
                    return;
                }

                dataGridViewFuJia.DataSource = null;
                dataGridViewFuJia.Columns.Clear();

                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "code";
                dgvc.HeaderText = "账户编号";
                dgvc.DataPropertyName = "code";
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "openBank";
                dgvc.HeaderText = "开户行";
                dgvc.DataPropertyName = "openBank";
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "bankCard";
                dgvc.HeaderText = "银行账户";
                dgvc.DataPropertyName = "bankCard";
                dgvc.Visible = false;
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "cardHolder";
                dgvc.HeaderText = "持卡人";
                dgvc.DataPropertyName = "cardHolder";
                dgvc.Visible = false;
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "remark";
                dgvc.HeaderText = "备注";
                dgvc.DataPropertyName = "remark";
                dgvc.Visible = false;
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "availableBalance";
                dgvc.HeaderText = "可用额度";
                dgvc.DataPropertyName = "availableBalance";
                dgvc.Visible = false;
                dataGridViewFuJia.Columns.Add(dgvc);

                resizablePanel1.Location = new Point(470, 230);
                string name = XYEEncoding.strCodeHex(this.txtBank.Text.Trim());
                dataGridViewFuJia.DataSource = ch.DataTableReCoding(bank.GetList(1, name, false, false));
                resizablePanel1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：模糊查询结算账户数据错误" + ex.Message, "销售单温馨提示");
            }
        }

        /// <summary>
        /// 按ESC关闭副表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SalesTicketForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.resizablePanel1.Visible = false;
                this.resizablePanelData.Visible = false;
            }
        }

        /// <summary>
        /// 单据类型改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (comboBoxType.Text.Trim())
                {
                    case "销售发货":
                        superGridControlShangPing.PrimaryGrid.Columns["gridColumndinggoushu"].Visible = true;
                        superGridControlShangPing.DefaultVisualStyles.CellStyles.Default.TextColor = Color.Black;
                        break;
                    case "销售退货":
                        superGridControlShangPing.PrimaryGrid.Columns["gridColumndinggoushu"].Visible = false;
                        superGridControlShangPing.DefaultVisualStyles.CellStyles.Default.TextColor = Color.Red;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-选择调拨类型数据错误" + ex.Message);
            }
        }

        /// <summary>
        /// 修改边框 的颜色
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

        private void SalesTicketForm_Activated(object sender, EventArgs e)
        {
            labtextboxTop2.Focus();//焦点在客户上
        }

        /// <summary>
        /// 发票类型的选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxfapiaotype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (comboBoxfapiaotype.Text)
                {
                    case "增值税发票":
                        superGridControlShangPing.PrimaryGrid.Columns["gridColumnzengzhishui"].Visible = true;
                        superGridControlShangPing.PrimaryGrid.Columns["gridColumnshuie"].Visible = true;
                        superGridControlShangPing.PrimaryGrid.Columns["gridColumnjiashuiheji"].Visible = true;
                        break;
                    case "普通发票":
                        superGridControlShangPing.PrimaryGrid.Columns["gridColumnzengzhishui"].Visible = true;
                        superGridControlShangPing.PrimaryGrid.Columns["gridColumnshuie"].Visible = true;
                        superGridControlShangPing.PrimaryGrid.Columns["gridColumnjiashuiheji"].Visible = true;
                        break;
                    case "收据":
                        superGridControlShangPing.PrimaryGrid.Columns["gridColumnzengzhishui"].Visible = false;
                        superGridControlShangPing.PrimaryGrid.Columns["gridColumnshuie"].Visible = false;
                        superGridControlShangPing.PrimaryGrid.Columns["gridColumnjiashuiheji"].Visible = false;
                        break;
                    case "其他":
                        superGridControlShangPing.PrimaryGrid.Columns["gridColumnzengzhishui"].Visible = false;
                        superGridControlShangPing.PrimaryGrid.Columns["gridColumnshuie"].Visible = false;
                        superGridControlShangPing.PrimaryGrid.Columns["gridColumnjiashuiheji"].Visible = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-选择发票类型数据绑定错误!" + ex.Message);
            }
        }

        /// <summary>
        /// 快捷方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SalesTicketForm_KeyDown(object sender, KeyEventArgs e)
        {
            //新增
            if (e.KeyCode == Keys.N && e.Modifiers == Keys.Control)
            {
                MessageBox.Show("新增");
                return;
            }
            //保存
            if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
            {
                ToolStripBtnSave_Click(sender, e);
                return;
            }
            //审核
            if (e.KeyCode == Keys.F4)
            {
                ToolStripBtnShengHe_Click(sender, e);
                return;
            }
            //打印
            if (e.KeyCode == Keys.P && e.Modifiers == Keys.Control)
            {
                MessageBox.Show("打印");
                return;
            }
            //导出Excel
            if (e.KeyCode == Keys.T && e.Modifiers == Keys.Control)
            {
                MessageBox.Show("导出Excel");
                return;
            }
            //关闭
            if (e.KeyCode == Keys.X && e.Modifiers == Keys.Control)
            {
                this.Close();
                this.Dispose();
            }
        }
        /// <summary>
        /// 验证计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControlShangPing_CellValidated(object sender, GridCellValidatedEventArgs e)
        {
            try
            {
                //最后一行做统计行
                GridRow gr = e.GridPanel.Rows[e.GridCell.RowIndex] as GridRow;
                //计算金额
                decimal number = Convert.ToDecimal(gr.Cells["gridColumnfahuoshu"].FormattedValue);//发货数量
                decimal price = Convert.ToDecimal(gr.Cells["gridColumnprice"].FormattedValue);//单价               
                decimal money = number * price;//金额
                gr.Cells["gridColumnmoney"].Value = money;
                decimal discountRate = Convert.ToDecimal(gr.Cells["gridColumnzhekoul"].FormattedValue);//折扣率
                decimal discountAfter = money * (discountRate / 100);
                decimal discountMoney = money - discountAfter;//折扣额
                gr.Cells["gridColumnzhekoue"].Value = discountMoney;
                decimal taxRate = Convert.ToDecimal(gr.Cells["gridColumnzengzhishui"].FormattedValue);//增值税税率
                decimal rateMoney = money * (taxRate / 100);//税额
                gr.Cells["gridColumnshuie"].Value = rateMoney;
                decimal priceAndtax = money + rateMoney;//价税合计
                gr.Cells["gridColumnjiashuiheji"].Value = priceAndtax;

                //逐行统计数据总数
                decimal tempAllDingGouNumber = 0;
                decimal tempAllNumber = 0;
                decimal tempAllMoney = 0;
                decimal tempAllTaxMoney = 0;
                decimal tempAllPriceAndTax = 0;
                for (int i = 0; i < superGridControlShangPing.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControlShangPing.PrimaryGrid.Rows[i] as GridRow;
                    tempAllDingGouNumber += Convert.ToDecimal(tempGR["gridColumndinggoushu"].FormattedValue);
                    tempAllNumber += Convert.ToDecimal(tempGR["gridColumnfahuoshu"].FormattedValue);
                    tempAllMoney += Convert.ToDecimal(tempGR["gridColumnmoney"].FormattedValue);
                    tempAllTaxMoney += Convert.ToDecimal(tempGR["gridColumnshuie"].FormattedValue);
                    tempAllPriceAndTax += Convert.ToDecimal(tempGR["gridColumnjiashuiheji"].FormattedValue);
                }

                _Materialnumber = tempAllDingGouNumber;
                _FaHuoShuLiang = tempAllNumber;
                _Money = tempAllMoney;
                _TaxMoney = tempAllTaxMoney;
                _PriceAndTaxMoney = tempAllPriceAndTax;
                gr = (GridRow)superGridControlShangPing.PrimaryGrid.LastSelectableRow;
                gr["gridColumndinggoushu"].Value = _Materialnumber.ToString();
                gr["gridColumnfahuoshu"].Value = _FaHuoShuLiang.ToString();
                gr["gridColumnmoney"].Value = _Money.ToString();
                gr["gridColumnshuie"].Value = _TaxMoney.ToString();
                gr["gridColumnjiashuiheji"].Value = _PriceAndTaxMoney.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-统计数量出错！请检查：" + ex.Message);
            }
        }
    }
}
