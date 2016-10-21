using DevComponents.DotNetBar.SuperGrid;
using HelperUtility;
using HelperUtility.Encrypt;
using InterfaceLayer.Base;
using Model;
using InterfaceLayer.Purchase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSCATProject.Purchase
{
    public partial class PurchaseTicketForm : TestForm
    {
        public PurchaseTicketForm()
        {
            InitializeComponent();
        }

        #region 调用接口以及加密解密方法
        CodingHelper ch = new CodingHelper();
        SupplierInterface supplier = new SupplierInterface();//供应商
        EmpolyeeInterface employee = new EmpolyeeInterface();//员工  
        StorageInterface storage = new StorageInterface();//仓库
        BankAccountInterface bank = new BankAccountInterface();//结算账户
        #endregion

        #region 数据字段

        /// <summary>
        /// 所有供应商
        /// </summary>
        private DataTable _AllSupplier = null;
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
        /// 点击的项,1供应商  2为销售员  3为仓库   
        /// </summary>
        private int _Click = 0;
        /// <summary>
        /// 保存供应商Code
        /// </summary>
        private string _supplierCode;
        /// <summary>
        /// 保存销售员Code
        /// </summary>
        private string _employeeCode;
        /// <summary>
        /// 保存仓库Code
        /// </summary>
        private string _storgeCode;
        /// <summary>
        /// 仓库名称
        /// </summary>
        private string _storgeName;
        /// <summary>
        /// 保存账户code
        /// </summary>
        private string _bankCode;
        /// <summary>
        /// 保存仓库的字典集合
        /// </summary>
        private KeyValuePair<string, string> _ClickStorageList;
        /// <summary>
        /// 统计数量
        /// </summary>
        private decimal _Materialnumber;
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
        /// 统计采购费用合计
        /// </summary>
        private decimal _purchaseCost;
        /// <summary>
        /// 采购单code
        /// </summary>
        private decimal _benCiShouKuan;
        /// <summary>
        /// 总金额
        /// </summary>
        private decimal _zongJinE;
        /// <summary>
        /// 未付款金额
        /// </summary>
        private decimal _weiFuKuan;
        /// <summary>
        /// 缺少数量
        /// </summary>
        private decimal _lostNumber;
        /// <summary>
        /// 发货数量
        /// </summary>
        private decimal _faHuoShuLiang;
        /// <summary>
        /// 订购数量
        /// </summary>
        private decimal _dingGouShuLiang;
        private string _purchaseMainCode;
        private string _shangPinCode;
        /// <summary>
        /// 商品code
        /// </summary>
        public string ShangPinCode
        {
            get { return _shangPinCode; }
            set { _shangPinCode = value; }
        }

        private decimal _MaterialNumber;
        /// <summary>
        /// 采购主表code
        /// </summary>
        private string _purchaseMainCodel;
        public string PurchaseMainCodel
        {
            get { return _purchaseMainCodel; }
            set { _purchaseMainCodel = value; }
        }

        /// <summary>
        /// 采购明细的code
        /// </summary>
        private string _purchaseCode;
        public string PurchaseCode
        {
            get { return _purchaseCode; }
            set { _purchaseCode = value; }
        }
        /// <summary>
        /// 交货方式
        /// </summary>
        public string JiaoHuoFangShi
        {
            get { return _jiaoHuoFangShi; }
            set { _jiaoHuoFangShi = value; }
        }
        private string _jiaoHuoFangShi;
        private decimal _caiGoiJinE;
        #endregion

        #region 初始化数据
        /// <summary>
        /// 初始化供应商
        /// </summary>
        private void InitSupplier()
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
                    dgvc.HeaderText = "编码";
                    dgvc.DataPropertyName = "编码";
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "name";
                    dgvc.HeaderText = "单位名称";
                    dgvc.DataPropertyName = "单位名称";
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "mobilePhone";
                    dgvc.HeaderText = "联系手机";
                    dgvc.DataPropertyName = "联系手机";
                    dgvc.Visible = false;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "linkMan";
                    dgvc.HeaderText = "联系人";
                    dgvc.DataPropertyName = "联系人";
                    dgvc.Visible = false;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "fax";
                    dgvc.HeaderText = "传真";
                    dgvc.DataPropertyName = "传真";
                    dgvc.Visible = false;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    resizablePanel1.Location = new Point(439, 150);
                    dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllSupplier);
                    resizablePanel1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-尝试点击供应商数据出错或者无数据！请检查：" + ex.Message, "购货温馨提示！");
            }
        }

        /// <summary>
        /// 初始化采购员
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
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "name";
                    dgvc.HeaderText = "姓名";
                    dgvc.DataPropertyName = "姓名";
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
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
                MessageBox.Show("错误代码：1107-尝试点击采购员数据出错或者无数据！请检查：" + ex.Message, "购货单温馨提示！");
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
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "name";
                    dgvc.Visible = true;
                    dgvc.HeaderText = "仓库名称";
                    dgvc.DataPropertyName = "name";
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
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
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：1108-尝试点击仓库数据出错或者无数据！请检查：" + ex.Message, "购货单温馨提示！");
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
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "openBank";
                    dgvc.HeaderText = "开户行";
                    dgvc.DataPropertyName = "openBank";
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
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

                    resizablePanel1.Location = new Point(439, 190);
                    dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllBank);
                    resizablePanel1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：1307-尝试点击结算账户，数据显示失败或者无数据！" + ex.Message, "购货单温馨提示！");
            }
        }

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
            gr.Cells["gridColumnNumber"].Value = 0;
            gr.Cells["gridColumnNumber"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnNumber"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumnMoney"].Value = 0;
            gr.Cells["gridColumnMoney"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnMoney"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumnshuie"].Value = 0;
            gr.Cells["gridColumnshuie"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnshuie"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumncaihouMoney"].Value = 0;
            gr.Cells["gridColumncaihouMoney"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumncaihouMoney"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumnjiashuiheji"].Value = 0;
            gr.Cells["gridColumnjiashuiheji"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnjiashuiheji"].CellStyles.Default.Background.Color1 = Color.Orange;
        }

        /// <summary>
        /// 非空验证
        /// </summary>
        private bool isNUllValidate()
        {
            if (labtextboxTop2.Text.Trim() == null || labtextboxTop2.Text == "")
            {
                MessageBox.Show("供应商不能为空！");
                labtextboxTop2.Focus();
                return false;
            }
            GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[1];
            if (gr.Cells["gridColumnStock"].Value == null || gr.Cells["gridColumnStock"].Value.ToString() == "")
            {
                MessageBox.Show("仓库不能为空！");
                superGridControlShangPing.Focus();
                return false;
            }
            if (gr.Cells["gridColumnname"].Value == null || gr.Cells["gridColumnname"].Value.ToString() == "")
            {
                MessageBox.Show("商品代码不能为空！");
                superGridControlShangPing.Focus();
                return false;
            }
            if (ltxtbSalsMan.Text.Trim() == null || ltxtbSalsMan.Text == "")
            {
                MessageBox.Show("采购员不能为空！");
                ltxtbSalsMan.Focus();
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 窗体加载事件
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PurchaseTicketForm_Load(object sender, EventArgs e)
        {
            try
            {
                toolStripButtonXuanYuanDan.Visible = true;
                //供应商
                _AllSupplier = supplier.SelSupplierTable();
                //采购员
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

                comboBoxEWuLiuType.SelectedIndex = 0;
                comboBoxExJieSuaType.SelectedIndex = 0;
                comboBoxExType.SelectedIndex = 0;
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

                //生成采购单code和显示条形码
                _purchaseMainCode = BuildCode.ModuleCode("PCT");
                textBoxOddNumbers.Text = _purchaseMainCode;
                barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
                _Code.ValueFont = new Font("微软雅黑", 20);
                System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
                pictureBoxTiaoXiangMa.Image = imgTemp;
                //审核图标
                pictureBoxShengHe.Parent = pictureBoxtitle;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：3301-窗体加载时，初始化数据失败！请检查：" + ex.Message, "购货单温馨提示！");
                this.Close();
                return;
            }
        }
        /// <summary>
        /// 选源单按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonXuanYuanDan_Click(object sender, EventArgs e)
        {
            XuanYuanDan();
        }
        /// <summary>
        /// 审核按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripBtnShengHe_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripBtnSave_Click(object sender, EventArgs e)
        {

        }

        #region 小箭头和两个附表的点击事件
        /// <summary>
        /// 商品附表的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewShangPing_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        /// <summary>
        /// 小箭头的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewFuJia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    return;
                }

                //供应商
                if (_Click == 1 || _Click == 7)
                {
                    _supplierCode = dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString();//供应商code
                    string name = dataGridViewFuJia.Rows[e.RowIndex].Cells["name"].Value.ToString();//供应商名称
                    string linkman = dataGridViewFuJia.Rows[e.RowIndex].Cells["linkMan"].Value.ToString();//联系人
                    string phone = dataGridViewFuJia.Rows[e.RowIndex].Cells["mobilePhone"].Value.ToString();//电话
                    labtextboxTop2.Text = name;
                    labtextboxTop8.Text = linkman;
                    labtextboxTop9.Text = phone;
                    resizablePanel1.Visible = false;
                }
                //仓库
                if (_Click == 3 || _Click == 8)
                {
                    GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[ClickRowIndex];
                    string code = dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString();
                    string Name = dataGridViewFuJia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                    gr.Cells["gridColumnStock"].Value = Name;
                    gr.Cells["gridColumncode"].Value = code;
                    _ClickStorageList = new KeyValuePair<string, string>(code, Name);
                    _storgeCode = code;
                    _storgeName = Name;
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
                //结算账户
                if (_Click == 4 || _Click == 6)
                {

                    _bankCode = dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString();
                    string Name = dataGridViewFuJia.Rows[e.RowIndex].Cells["openBank"].Value.ToString();
                    labtextboxTop4.Text = Name;
                    resizablePanel1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：1311-双击绑定供应商、采购员、结算账户数据错误！请检查：" + ex.Message, "购货单温馨提示！");
            }
        }

        /// <summary>
        /// 光标定位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PurchaseTicketForm_Activated(object sender, EventArgs e)
        {
            labtextboxTop2.Focus();
        }

        /// <summary>
        /// 供应商点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxSupplier_Click(object sender, EventArgs e)
        {
            if (_Click != 1)
            {
                InitSupplier();
            }
            _Click = 7;
        }

        /// <summary>
        /// 结算账户点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxBank_Click(object sender, EventArgs e)
        {
            if (_Click != 4)
            {
                InitBank();
            }
            _Click = 6;
        }

        /// <summary>
        /// 采购员的点击事件
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
        /// 第一列的第一格的点击事件
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
                    _Click = 8;
                    return;
                }
                if (e.GridCell.GridColumn.Name == "material")
                {
                    this.resizablePanelData.Visible = false;
                    toolStripButtonXuanYuanDan.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：330-选择表格第一个数据错误！" + ex.Message);
                return;
            }

        }

        #endregion

        #region 模糊查询
        /// <summary>
        /// 供应商模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labtextboxTopSupplier_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.labtextboxTop2.Text.Trim() == "")
                {
                    InitSupplier();
                    _Click = 7;
                    return;
                }
                dataGridViewFuJia.DataSource = null;
                dataGridViewFuJia.Columns.Clear();

                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "code";
                dgvc.HeaderText = "编码";
                dgvc.DataPropertyName = "code";
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "name";
                dgvc.HeaderText = "单位名称";
                dgvc.DataPropertyName = "name";
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "mobilePhone";
                dgvc.HeaderText = "联系手机";
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

                resizablePanel1.Location = new Point(439, 150);
                string name = XYEEncoding.strCodeHex(this.labtextboxTop2.Text.Trim());
                dataGridViewFuJia.DataSource = ch.DataTableReCoding(supplier.GetList(0, name));
                resizablePanel1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：1114-模糊查询供应商数据错误" + ex.Message, "购货单温馨提示");
            }
        }
        /// <summary>
        /// 账户的模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labtextboxTopBank_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (this.labtextboxTop4.Text.Trim() == "")
                {
                    InitBank();
                    _Click = 6;
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

                resizablePanel1.Location = new Point(439, 190);
                string name = XYEEncoding.strCodeHex(this.labtextboxTop4.Text.Trim());
                dataGridViewFuJia.DataSource = ch.DataTableReCoding(bank.GetList(1, name, false, false));
                resizablePanel1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：1313-模糊查询结算账户数据错误！请检查：" + ex.Message, "购货单温馨提示");
            }
        }
        /// <summary>
        /// 采购员的模糊查询
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
                MessageBox.Show("错误代码：1115-模糊查询采购员数据错误！" + ex.Message, "购货单单温馨提示！");
            }
        }


        #endregion

        /// <summary>
        /// 表格按回车的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControlShangPing_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int jishu = 0;
                //GridRow gr=superGridControlShangPing.PrimaryGrid.Rows[superGridControlShangPing.PrimaryGrid.Rows.Coun]
                for (int i = 0; i < superGridControlShangPing.PrimaryGrid.Rows.Count; i++)
                {
                    GridRow gr = superGridControlShangPing.PrimaryGrid.Rows[i] as GridRow;
                    if (gr["material"].Value == null || gr["material"].Value.ToString() == "")
                    {
                        jishu++;
                        if (jishu >= 2)
                        {
                            superGridControlShangPing.PrimaryGrid.Rows.RemoveAt(i);
                        }
                    }
                }

                labtextboxTop2.Focus();
            }
        }

        /// <summary>
        /// 选源单的封装函数
        /// </summary>
        private void XuanYuanDan()
        {
            if (_supplierCode == "" || labtextboxTop2.Text == "")
            {
                MessageBox.Show("请先选择供应商！");
                return;
            }
            //显示窗体
            PurchaseOrderReportForm purchaseOrder = new PurchaseOrderReportForm();
            purchaseOrder.SupplierCode = _supplierCode;
            purchaseOrder.ShowDialog(this);

            labTop1.ForeColor = Color.Gray;
            comboBoxExType.Enabled = false;
            GridItemsCollection grs = superGridControlShangPing.PrimaryGrid.Rows;
            GridRow grid = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[ClickRowIndex];

            PurchaseOrderInterface purchaseOrderinter = new PurchaseOrderInterface();
            DataTable dt = ch.DataTableReCoding(purchaseOrderinter.GetJoinSearch(XYEEncoding.strCodeHex(_purchaseMainCodel), XYEEncoding.strCodeHex(_purchaseCode)));

            foreach (GridRow g in grs)
            {
                if (g.Cells["material"].Value == null || g.Cells["material"].Value.ToString() == "合计")
                {
                    continue;
                }
                if (g.Cells["gridColumnwuliaocode"].Value.Equals(dt.Rows[0]["materialCode"]))
                {
                    decimal dingdanshu = Convert.ToDecimal(g.Cells["gridColumndinggoushu"].Value);
                    decimal shuliang = Convert.ToDecimal(g.Cells["gridColumnNumber"].Value);
                    decimal gridColumnmoney = Convert.ToDecimal(g.Cells["gridColumnMoney"].Value);
                    decimal gridColumnshuie = Convert.ToDecimal(g.Cells["gridColumnshuie"].Value);
                    decimal gridColumnjiashuiheji = Convert.ToDecimal(g.Cells["gridColumnjiashuiheji"].Value);
                    decimal gridColumncaihouMoney = Convert.ToDecimal(g.Cells["gridColumncaihouMoney"].Value);
                    dingdanshu += dingdanshu;
                    shuliang += 1;
                    gridColumnmoney += gridColumnmoney;
                    gridColumnshuie += gridColumnshuie;
                    gridColumnjiashuiheji += gridColumnjiashuiheji;
                    gridColumncaihouMoney += gridColumncaihouMoney;

                    g.Cells["gridColumndinggoushu"].Value = dingdanshu;
                    g.Cells["gridColumnNumber"].Value = shuliang;
                    g.Cells["gridColumnMoney"].Value = gridColumnmoney;
                    g.Cells["gridColumnshuie"].Value = gridColumnshuie;
                    g.Cells["gridColumnjiashuiheji"].Value = gridColumnjiashuiheji;
                    g.Cells["gridColumncaihouMoney"].Value = gridColumncaihouMoney;
                    //统计数据
                    TongJi();
                    labtextboxTop5.Text = _PriceAndTaxMoney.ToString();
                    labtextboxTop3.Text = _PriceAndTaxMoney.ToString();
                    resizablePanelData.Visible = false;
                    return;
                }
                continue;
            }

            superGridControlShangPing.PrimaryGrid.Rows.Add(new GridRow(_storgeName, dt.Rows[0]["materialDaima"],
                dt.Rows[0]["name"],
                dt.Rows[0]["model"],
                dt.Rows[0]["barCode"],
                dt.Rows[0]["unit"],
                dt.Rows[0]["deliveryNumber"].ToString() == "" ? 1.0M : dt.Rows[0]["deliveryNumber"],
                1.00,
                dt.Rows[0]["materialPrice"].ToString() == "" ? 0.0M : dt.Rows[0]["materialPrice"],
                dt.Rows[0]["discountRate"].ToString() == "" ? 0.0M : dt.Rows[0]["discountRate"],
                dt.Rows[0]["VATRate"].ToString() == "" ? 0.0M : dt.Rows[0]["VATRate"],
                dt.Rows[0]["discountMoney"].ToString() == "" ? 0.0M : dt.Rows[0]["discountMoney"],
                dt.Rows[0]["materialMoney"].ToString() == "" ? 0.0M : dt.Rows[0]["materialMoney"],
                dt.Rows[0]["tax"],
                dt.Rows[0]["purchaseAmount"].ToString() == "" ? 0.0M : dt.Rows[0]["purchaseAmount"],
                dt.Rows[0]["taxTotal"],
                dt.Rows[0]["createDate"].ToString(),
                dt.Rows[0]["qualityDate"],
                dt.Rows[0]["effectiveDate"],
                dt.Rows[0]["mainCode"],
                dt.Rows[0]["code"],
                dt.Rows[0]["remark"],
                _storgeCode,
                _shangPinCode
                ));
            if (dt.Rows[0]["isDouble"].ToString() == "0")
            {
                GridRow gridrow = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[superGridControlShangPing.PrimaryGrid.Rows.Count - 2];
                gridrow.Cells["gridColumnNumber"].EditorType = typeof(GridDoubleInputEditControl);
            }
            TongJi();
        }

        /// <summary>
        /// 统计数据
        /// </summary>
        private void TongJi()
        {
            GridRow grid = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[ClickRowIndex];
            //逐行统计数据总数
            decimal tempAllDingDanShu = 0;
            decimal tempAllMaterialNumber = 0;
            decimal tempAllMoney = 0;
            decimal tempAllTaxMoney = 0;
            decimal tempAllPriceAndTax = 0;
            decimal tempAllCaiGouJine = 0;

            for (int i = 0; i < superGridControlShangPing.PrimaryGrid.Rows.Count - 1; i++)
            {
                GridRow tempGR = superGridControlShangPing.PrimaryGrid.Rows[i] as GridRow;
                tempAllDingDanShu += Convert.ToDecimal(tempGR["gridColumndinggoushu"].FormattedValue);
                tempAllMaterialNumber += Convert.ToDecimal(tempGR["gridColumnNumber"].FormattedValue);
                tempAllMoney += Convert.ToDecimal(tempGR["gridColumnMoney"].FormattedValue);
                tempAllTaxMoney += Convert.ToDecimal(tempGR["gridColumnshuie"].FormattedValue);
                tempAllPriceAndTax += Convert.ToDecimal(tempGR["gridColumnjiashuiheji"].FormattedValue);
                tempAllCaiGouJine += Convert.ToDecimal(tempGR["gridColumncaihouMoney"].FormattedValue);
            }
            _dingGouShuLiang = tempAllDingDanShu;
            _MaterialNumber = tempAllMaterialNumber;
            _Money = tempAllMoney;
            _TaxMoney = tempAllTaxMoney;
            _PriceAndTaxMoney = tempAllPriceAndTax;
            _caiGoiJinE = tempAllCaiGouJine;
            grid = (GridRow)superGridControlShangPing.PrimaryGrid.LastSelectableRow;
            grid["gridColumndinggoushu"].Value = _dingGouShuLiang;
            grid["gridColumnNumber"].Value = _MaterialNumber;
            grid["gridColumnMoney"].Value = _Money.ToString();
            grid["gridColumnshuie"].Value = _TaxMoney.ToString();
            grid["gridColumnjiashuiheji"].Value = _PriceAndTaxMoney.ToString();
            grid["gridColumncaihouMoney"].Value = _caiGoiJinE.ToString();
        }

        /// <summary>
        /// 本次收款
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ltxtBenCiShouKuan_Validated(object sender, EventArgs e)
        {
            try
            {
                decimal BenCiFuKuan = Convert.ToDecimal(labtextboxTop3.Text);
                decimal BenCiHeXiao = Convert.ToDecimal(labtextboxTop5.Text);
                if (BenCiFuKuan > BenCiHeXiao)
                {
                    MessageBox.Show("本次付款不能大于所付金额！");
                    labtextboxTop3.Focus();
                    labtextboxTop3.Text = BenCiHeXiao.ToString("0.00");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-验证本次收款金额出错！请检查:" + ex.Message, "购货单温馨提示！");
            }
        }
        /// <summary>
        /// 验证本次收款
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ltxtBenCiShouKuan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //小数点的处理。
                if ((int)e.KeyChar == 46)//小数点
                {
                    if (labtextboxTop3.Text.Length <= 0)
                        e.Handled = true;   //小数点不能在第一位
                    else
                    {
                        float f;
                        float oldf;
                        bool b1 = false, b2 = false;
                        b1 = float.TryParse(labtextboxTop3.Text, out oldf);
                        b2 = float.TryParse(labtextboxTop3.Text + e.KeyChar.ToString(), out f);
                        if (b2 == false)
                        {
                            if (b1 == true)
                                e.Handled = true;
                            else
                                e.Handled = false;
                        }
                    }

                }
                if (!(((e.KeyChar >= '0') && (e.KeyChar <= '9')) || e.KeyChar <= 31))
                {
                    if (e.KeyChar == '.')
                    {
                        if (((TextBox)sender).Text.Trim().IndexOf('.') > -1)
                            e.Handled = true;
                    }
                    else
                        e.Handled = true;
                }
                else
                {
                    if (e.KeyChar <= 31)
                    {
                        e.Handled = false;
                    }
                    else if (((TextBox)sender).Text.Trim().IndexOf('.') > -1)
                    {
                        if (((TextBox)sender).Text.Trim().Substring(((TextBox)sender).Text.Trim().IndexOf('.') + 1).Length >= 2)
                            e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-本次付款输入的值为非法字符，请重新输入:" + ex.Message, "购货单单温馨提示！");
            }
        }
        /// <summary>
        /// 验证表格数据及统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControlShangPing_CellValidated(object sender, GridCellValidatedEventArgs e)
        {
            try
            {
                GridRow g = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[1];
                if (g.Cells["gridColumnname"].Value == null || g.Cells["gridColumnname"].Value.ToString() == "")
                {
                    MessageBox.Show("请选择商品代码：");
                    g.Cells["gridColumnNumber"].Value = 0.00;
                    g.Cells["gridColumndanjia"].Value = 0.00;
                    g.Cells["gridColumnzhekoul"].Value = 100.00;
                    return;
                }
                //最后一行做统计行
                GridRow gr = e.GridPanel.Rows[e.GridCell.RowIndex] as GridRow;
                ////计算金额
                decimal number = Convert.ToDecimal(gr.Cells["gridColumnNumber"].FormattedValue);//数量
                decimal price = Convert.ToDecimal(gr.Cells["gridColumndanjia"].FormattedValue);//单价               
                decimal money = number * price;//金额
                gr.Cells["gridColumnMoney"].Value = money;
                decimal discountRate = Convert.ToDecimal(gr.Cells["gridColumnzhekoul"].FormattedValue);//折扣率
                decimal discountAfter = money * (discountRate / 100);
                decimal discountMoney = money - discountAfter;//折扣额
                gr.Cells["gridColumnzhekoue"].Value = discountMoney;
                decimal taxRate = Convert.ToDecimal(gr.Cells["gridColumnzengzhishiu"].FormattedValue);//增值税税率
                decimal rateMoney = money * (taxRate / 100);//税额
                gr.Cells["gridColumnshuie"].Value = rateMoney;
                decimal priceAndtax = money + rateMoney;//价税合计
                gr.Cells["gridColumnjiashuiheji"].Value = priceAndtax;

                TongJi();
                labtextboxTop7.Text = _purchaseCost.ToString("0.00");
                labtextboxTop5.Text = _PriceAndTaxMoney.ToString("0.00");
                labtextboxTop3.Text = _PriceAndTaxMoney.ToString("0.00");
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-验证表格里的金额以及统计数量出错！请检查：" + ex.Message, "采购单温馨提示！");
            }
        }

        /// <summary>
        /// 保存按钮的封装函数
        /// </summary>
        private void Save()
        {
            if (isNUllValidate() == false)
            {
                return;
            }
            ////获得界面上的数据,准备传给base层新增数据
            // PurchaseOrderInterface purchaseOrderinterface = new PurchaseOrderInterface();
            ////采购单
            PurchaseMain purchasemain = new PurchaseMain();
            ////采购单商品列表
            List<PurchaseDetail> purchasedetailList = new List<PurchaseDetail>();
            try
            {
                purchasemain.code = XYEEncoding.strCodeHex(_purchaseMainCode);//采购单code
                purchasemain.data = this.dateTimePicker1.Value;//开单日期
                purchasemain.type = XYEEncoding.strCodeHex(comboBoxExType.Text);//单据类别
                purchasemain.supplierCode = XYEEncoding.strCodeHex(_supplierCode);//供应商code
                //供应商
                if (labtextboxTop2.Text != null || labtextboxTop2.Text != "")
                {
                    purchasemain.supplierName = XYEEncoding.strCodeHex(labtextboxTop2.Text.Trim());
                }
                else
                {
                    MessageBox.Show("供应商不能为空！");
                    labtextboxTop2.Focus();
                    return;
                }
                //purchaseorder.code = XYEEncoding.strCodeHex(_PurchaseOrderCode);//采购订单code
                //purchaseorder.date = this.dateTimePicker1.Value;//开单日期
                //purchaseorder.supplierCode = XYEEncoding.strCodeHex(_supplierCode);//供应商code
                //switch (cboMethod.Text.Trim())//交货方式
                //{
                //    case "提货":
                //        purchaseorder.deliversMethod = 0;
                //        break;
                //    case "送货":
                //        purchaseorder.deliversMethod = 1;
                //        break;
                //    case "发货":
                //        purchaseorder.deliversMethod = 2;
                //        break;
                //}
                //if (txtAddress.Text != null || txtAddress.Text != "")
                //{
                //    purchaseorder.deliversLocation = XYEEncoding.strCodeHex(txtAddress.Text.Trim());//交货地点
                //}
                //else
                //{
                //    MessageBox.Show("交货地点员不能为空！");
                //    txtAddress.FindForm();
                //    return;
                //}

                //purchaseorder.deliversDate = this.dateTimePicker2.Value;//交货日期
                //purchaseorder.depositReceived = Convert.ToDecimal(txtYiFuDingJin.Text.Trim() == "" ? 0.0M : Convert.ToDecimal(txtYiFuDingJin.Text));//已付订金
                //purchaseorder.remark = XYEEncoding.strCodeHex(txtRemark.Text == null ? "" : txtRemark.Text.Trim());//摘要
                //if (ltxtbSalsMan.Text != null || ltxtbSalsMan.Text != "")
                //{
                //    purchaseorder.operation = XYEEncoding.strCodeHex(ltxtbSalsMan.Text.Trim());
                //}
                //else
                //{
                //    MessageBox.Show("采购员不能为空！");
                //    ltxtbSalsMan.FindForm();
                //    return;
                //}
                //purchaseorder.makeMan = XYEEncoding.strCodeHex(ltxtbMakeMan.Text == null ? "" : ltxtbMakeMan.Text.Trim());//制单人
                //purchaseorder.examine = XYEEncoding.strCodeHex(ltxtbShengHeMan.Text == null ? "" : ltxtbShengHeMan.Text.Trim());//审核人
                //purchaseorder.checkState = 0;//审核状态
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:3102-尝试创建采购订单数据出错！请检查：" + ex.Message, "采购订单温馨提示");
                return;
            }

        }

    }
}

