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
        /// <summary>
        /// 本次收款
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
        /// <summary>
        /// 商品code
        /// </summary>
        public string ShangPinCode
        {
            get { return _shangPinCode; }
            set { _shangPinCode = value; }
        }
        private decimal _MaterialNumber;
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

                //生成销售单code和显示条形码
                _SalesOrderCode = BuildCode.ModuleCode("PCT");
                textBoxOddNumbers.Text = _SalesOrderCode;
                barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
                _Code.ValueFont = new Font("微软雅黑", 20);
                System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
                pictureBoxTiaoXiangMa.Image = imgTemp;
                //审核图标
                //pictureBoxShengHe.Parent = pictureBoxtitle;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：3301-窗体加载时，初始化数据失败！请检查：" + ex.Message, "购货单温馨提示！");
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

        private void superGridControlShangPing_BeginEdit(object sender, GridEditEventArgs e)
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
        }
    }
}
