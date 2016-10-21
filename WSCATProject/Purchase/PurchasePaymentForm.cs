﻿using DevComponents.DotNetBar.SuperGrid;
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
    public partial class PurchasePaymentForm : TestForm
    {
        public PurchasePaymentForm()
        {
            InitializeComponent();
        }

        #region 调用接口以及加密解密方法
        CodingHelper ch = new CodingHelper();
        SupplierInterface supplier = new SupplierInterface();//供应商
        BankAccountInterface bank = new BankAccountInterface();//结算账户
        EmpolyeeInterface employee = new EmpolyeeInterface();//员工
        #endregion

        #region 数据字段

        /// <summary>
        /// 所有供应商
        /// </summary>
        private DataTable _AllSupply = null;
        /// <summary>
        /// 所有账户
        /// </summary>
        private DataTable _AllBank = null;
        /// <summary>
        /// 所有付款员
        /// </summary>
        private DataTable _AllEmployee = null;
        /// <summary>
        /// 点击的项,1供应商  2为结算账户  3为付款员
        /// </summary>
        private int _Click = 0;
        /// <summary>
        /// 保存供应商Code
        /// </summary>
        private string _supplyCode;
        /// <summary>
        /// 保存银行账户code
        /// </summary>
        private string _bankCode;
        /// <summary>
        /// 保存收款员Code
        /// </summary>
        private string _employeeCode;
        /// <summary>
        /// 所有商品列表
        /// </summary>
        private DataTable _AllMaterial = null;
        /// <summary>
        /// 商品code
        /// </summary>
        private string _materialCode;
        /// <summary>
        /// 付款单单code
        /// </summary>
        private string _PurchasePaymentCode;
        /// <summary>
        /// 统计单据金额
        /// </summary>
        private decimal _danJuMoney;
        /// <summary>
        /// 统计已核销金额
        /// </summary>
        private decimal _yiHeXiaoMoney;
        /// <summary>
        /// 统计未核销金额
        /// </summary>
        private decimal _weiHeXiaoMoney;
        /// <summary>
        /// 统计本次核销金额
        /// </summary>
        private decimal _benCiHeXiaoMoney;
        /// <summary>
        /// 统计剩余尾款
        /// </summary>
        private decimal _shengYuMoney;
        List<string> _purchaseMainList = new List<string>();
        /// <summary>
        /// 采购单code
        /// </summary>
        private string _purchaseMainCode;
        public string PurchaseMainCode
        {
            get { return _purchaseMainCode; }
            set { _purchaseMainCode = value; }
        }

        public List<string> PurchaseMainList
        {
            get { return _purchaseMainList; }
            set { _purchaseMainList = value; }
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
            gr.Cells["BillDate"].Value = "合计";
            gr.Cells["BillDate"].CellStyles.Default.Alignment =
                DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["BillMoney"].Value = 0;
            gr.Cells["BillMoney"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["BillMoney"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["yiHeXiao"].Value = 0;
            gr.Cells["yiHeXiao"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["yiHeXiao"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["weiHeXiao"].Value = 0;
            gr.Cells["weiHeXiao"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["weiHeXiao"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["benciHeXiao"].Value = 0;
            gr.Cells["benciHeXiao"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["benciHeXiao"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["benciHeXiao"].AllowSelection = false;
            gr.Cells["shengyuMoney"].Value = 0;
            gr.Cells["shengyuMoney"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["shengyuMoney"].CellStyles.Default.Background.Color1 = Color.Orange;
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
            if (labtextboxTop4.Text.Trim() == null || labtextboxTop4.Text == "")
            {
                MessageBox.Show("结算账户不能为空！");
                labtextboxTop4.Focus();
                return false;
            }
            GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[1];
            double benci = Convert.ToDouble(gr.Cells["benciHeXiao"].Value);
            if (gr.Cells["BillCode"].Value == null || gr.Cells["BillCode"].Value.ToString() == "")
            {
                MessageBox.Show("表格里源单编号不能为空！");
                superGridControlShangPing.Focus();
                return false;
            }
            if (benci == 0.00)
            {
                MessageBox.Show("本次核销不应该是等于零的数据！请检查：");
                superGridControlShangPing.Focus();
                return false;
            }
            if (ltxtbSalsMan.Text.Trim() == null || ltxtbSalsMan.Text == "")
            {
                MessageBox.Show("付款员不能为空！");
                ltxtbSalsMan.Focus();
                return false;
            }
            return true;
        }

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
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvc.Visible = false;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "linkMan";
                    dgvc.HeaderText = "联系人";
                    dgvc.DataPropertyName = "联系人";
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvc.Visible = false;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "fax";
                    dgvc.HeaderText = "传真";
                    dgvc.DataPropertyName = "传真";
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvc.Visible = false;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    resizablePanel1.Location = new Point(550, 160);
                    dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllSupply);
                    resizablePanel1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：3106-尝试点击供应商数据出错或者无数据！请检查：" + ex.Message, "采购订单温馨提示！");
            }
        }

        /// <summary>
        /// 初始化结算账户
        /// </summary>
        private void InitBank()
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
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "cardHolder";
                    dgvc.HeaderText = "持卡人";
                    dgvc.DataPropertyName = "cardHolder";
                    dgvc.Visible = false;
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "remark";
                    dgvc.HeaderText = "备注";
                    dgvc.DataPropertyName = "remark";
                    dgvc.Visible = false;
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "availableBalance";
                    dgvc.HeaderText = "可用额度";
                    dgvc.DataPropertyName = "availableBalance";
                    dgvc.Visible = false;
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    resizablePanel1.Location = new Point(550, 200);
                    dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllBank);
                    resizablePanel1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：1507-尝试点击结算账户，数据显示失败或者无数据！请检查：" + ex.Message, "收款单温馨提示！");
            }
        }

        /// <summary>
        /// 初始化收款员
        /// </summary>
        private void InitEmployee()
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
                MessageBox.Show("错误代码：1508-尝试点击收款员，数据显示失败或者无数据！请检查：" + ex.Message, "收款单温馨提示！");
            }
        }

        #endregion

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PurchasePaymentForm_Load(object sender, EventArgs e)
        {
            try
            {
                #region 初始化窗体
                cboType.SelectedIndex = 0;
                cboMethod.SelectedIndex = 0;
                toolStripButtonXuanYuanDan.Visible = true;
                labtextboxTop7.Text = "100.00";
                labtextboxTop3.Text = "0.00";
                labtextboxTop6.Text = "0.00";
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

                //供应商
                _AllSupply = supplier.SelSupplierTable();
                //结算账户
                _AllBank = bank.GetList(999, "", false, false);
                //收款员
                _AllEmployee = employee.SelSupplierTable(false);

                //生成销售订单code和显示条形码
                _PurchasePaymentCode = BuildCode.ModuleCode("PPM");
                textBoxOddNumbers.Text = _PurchasePaymentCode;
                barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
                _Code.ValueFont = new Font("微软雅黑", 20);
                System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
                picBarCode.Image = imgTemp;

                //绑定事件 双击事填充内容并隐藏列表
                dataGridViewFuJia.CellDoubleClick += dataGridViewFuJia_CellDoubleClick;
                toolStripBtnSave.Click += toolStripBtnSave_Click;//保存按钮
                toolStripBtnShengHe.Click += toolStripBtnShengHe_Click;//审核按钮 
                toolStripButtonXuanYuanDan.Click += ToolStripButtonXuanYuanDan_Click;//选源单的点击事件
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-窗体加载时，初始化数据失败！" + ex.Message, "付款单温馨提示！");
                this.Close();
                return;
            }
        }

        /// <summary>
        /// 选源单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonXuanYuanDan_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 下拉框选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboType.Text.Trim())
            {
                case "采购付款":
                    labTop6.ForeColor = Color.Black;
                    labtextboxTop6.ReadOnly = false;
                    labtextboxTop6.ForeColor = Color.Black;
                    labtextboxTop6.Border.BorderBottomColor = Color.Black;
                    labTop7.ForeColor = Color.Black;
                    labtextboxTop7.ReadOnly = false;
                    labtextboxTop7.ForeColor = Color.Black;
                    labtextboxTop7.Border.BorderBottomColor = Color.Black;
                    break;
                case "预付款":
                    labTop6.ForeColor = Color.Gray;
                    labtextboxTop6.ReadOnly = true;
                    labtextboxTop6.BackColor = Color.White;
                    labtextboxTop6.ForeColor = Color.Gray;
                    labtextboxTop6.Border.BorderBottomColor = Color.Gray;
                    labTop7.ForeColor = Color.Gray;
                    labtextboxTop7.ReadOnly = true;
                    labtextboxTop7.BackColor = Color.White;
                    labtextboxTop7.ForeColor = Color.Gray;
                    labtextboxTop7.Border.BorderBottomColor = Color.Gray;
                    break;
                case "预付退款":
                    labTop6.ForeColor = Color.Gray;
                    labtextboxTop6.ReadOnly = true;
                    labtextboxTop6.BackColor = Color.White;
                    labtextboxTop6.ForeColor = Color.Gray;
                    labtextboxTop6.Border.BorderBottomColor = Color.Gray;
                    labTop7.ForeColor = Color.Gray;
                    labtextboxTop7.ReadOnly = true;
                    labtextboxTop7.BackColor = Color.White;
                    labtextboxTop7.ForeColor = Color.Gray;
                    labtextboxTop7.Border.BorderBottomColor = Color.Gray;
                    break;
            }
        }

        #region 小箭头点击事件和表格的双击事件

        /// <summary>
        /// 供应商
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxSupply_Click(object sender, EventArgs e)
        {
            if (_Click != 1)
            {
                InitSupplier();
            }
            _Click = 4;
        }

        /// <summary>
        /// 结算账户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxBank_Click(object sender, EventArgs e)
        {
            if (_Click != 2)
            {
                InitBank();
            }
            _Click = 5;
        }

        /// <summary>
        /// 付款员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxEmployee_Click(object sender, EventArgs e)
        {
            if (_Click != 3)
            {
                InitEmployee();
            }
            _Click = 6;
        }

        /// <summary>
        /// 双击绑定供应商、结算账户、收款员数据
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
                if (_Click == 1 || _Click == 4)
                {
                    _supplyCode = dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString();//供应商code
                    string name = dataGridViewFuJia.Rows[e.RowIndex].Cells["name"].Value.ToString();//供应商名称
                    labtextboxTop2.Text = name;
                    resizablePanel1.Visible = false;
                }
                //结算账户
                if (_Click == 2 || _Click == 5)
                {
                    _bankCode = dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString();//银行账户code
                    string name = dataGridViewFuJia.Rows[e.RowIndex].Cells["openBank"].Value.ToString();//账户名称
                    labtextboxTop4.Text = name;
                    resizablePanel1.Visible = false;
                }
                //收款员
                if (_Click == 3 || _Click == 6)
                {
                    _employeeCode = dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString();//收款员code
                    string name = dataGridViewFuJia.Rows[e.RowIndex].Cells["name"].Value.ToString();//收款员
                    ltxtbSalsMan.Text = name;
                    resizablePanel1.Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：1509-双击绑定客户、结算账户、收款员数据错误！请检查：" + ex.Message, "收款单温馨提示！");
            }
        }

        #endregion

        #region 模糊查询
        /// <summary>
        /// 供应商模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSupply_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.labtextboxTop2.Text.Trim() == "")
                {
                    InitSupplier();
                    _Click = 4;
                    return;
                }
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
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvc.Visible = false;
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "linkMan";
                dgvc.HeaderText = "联系人";
                dgvc.DataPropertyName = "联系人";
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvc.Visible = false;
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "fax";
                dgvc.HeaderText = "传真";
                dgvc.DataPropertyName = "传真";
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvc.Visible = false;
                dataGridViewFuJia.Columns.Add(dgvc);

                resizablePanel1.Location = new Point(550, 160);
                string name = XYEEncoding.strCodeHex(this.labtextboxTop2.Text.Trim());
                dataGridViewFuJia.DataSource = ch.DataTableReCoding(supplier.GetList(0, name));
                resizablePanel1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-模糊查询供应商数据错误" + ex.Message, "付款单温馨提示");
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
                if (this.labtextboxTop4.Text.Trim() == "")
                {
                    InitBank();
                    _Click = 5;
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

                resizablePanel1.Location = new Point(550, 200);
                string name = XYEEncoding.strCodeHex(this.labtextboxTop4.Text.Trim());
                dataGridViewFuJia.DataSource = ch.DataTableReCoding(bank.GetList(1, name, false, false));
                resizablePanel1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-模糊查询结算账户数据错误" + ex.Message, "付款单温馨提示");
            }
        }

        /// <summary>
        ///收款员模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ltxtbSalsMan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ltxtbSalsMan.Text.Trim() == "")
                {
                    _Click = 6;
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
                MessageBox.Show("错误代码：1512-模糊查询付款员数据失败！" + ex.Message, "付款单温馨提示！");
            }
        }
        #endregion

        private void PurchasePaymentForm_Activated(object sender, EventArgs e)
        {
            labtextboxTop2.Focus();
        }

        /// <summary>
        /// 按ESC关闭副表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PurchasePaymentForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.resizablePanel1.Visible = false;
                this.resizablePanelData.Visible = false;
            }
        }

        /// <summary>
        /// 快捷方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PurchasePaymentForm_KeyDown(object sender, KeyEventArgs e)
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
                toolStripBtnSave_Click(sender, e);
                return;
            }
            //审核
            if (e.KeyCode == Keys.F4)
            {
                toolStripBtnShengHe_Click(sender, e);
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
        /// 设置边框
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
        /// <summary>
        /// 表格回车键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControlShangPing_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                labtextboxTop2.Focus();
            }
        }

        /// <summary>
        /// 整单折扣
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDiscount_Validated(object sender, EventArgs e)
        {
            try
            {
                decimal zhekou = Convert.ToDecimal(labtextboxTop7.Text);
                decimal hexiao = Convert.ToDecimal(labtextboxTop3.Text);
                if (zhekou > 100 || zhekou < 0)
                {
                    MessageBox.Show("折扣率不能大于100或者不能小于0！");
                    labtextboxTop7.FindForm();
                    labtextboxTop7.Text = "100.00";
                }
                decimal bencishoukuan = hexiao * (Convert.ToDecimal(labtextboxTop7.Text) / 100);
                labtextboxTop6.Text = bencishoukuan.ToString("0.00");
                labtextboxTop7.Text = zhekou.ToString("0.00");
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-验证整单折扣金额出错！请检查:" + ex.Message, "付款单温馨提示！");
            }
        }
        /// <summary>
        /// 验证整单折扣
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //小数点的处理。
                if ((int)e.KeyChar == 46)//小数点
                {
                    if (labtextboxTop7.Text.Length <= 0)
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
                MessageBox.Show("错误代码：-整单折扣输入的值为非法字符，请重新输入:" + ex.Message, "付款单温馨提示！");
            }
        }

        private void Save()
        {
            if (isNUllValidate() == false)
            {
                return;
            }
            ////获得界面上的数据,准备传给base层新增数据
            //PurchaseOrderInterface purchaseOrderinterface = new PurchaseOrderInterface();
            ///付款单
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripBtnSave_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 审核按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripBtnShengHe_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 验证、统计数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControlShangPing_CellValidated(object sender, GridCellValidatedEventArgs e)
        {
            try
            {
                //最后一行做统计行
               
                GridRow gr = e.GridPanel.Rows[e.GridCell.RowIndex] as GridRow;
                if (gr.Cells["BillCode"].FormattedValue == null || gr.Cells["BillCode"].FormattedValue == "")
                {
                    MessageBox.Show("请先选择源单：");
                    gr.Cells["benciHeXiao"].Value = 0.00;
                    return;
                }
                ////计算金额
                decimal benciHeXiaoMoney = Convert.ToDecimal(gr.Cells["benciHeXiao"].FormattedValue);//本次核销金额
                decimal weiHeXiaoMoney = Convert.ToDecimal(gr.Cells["weiHeXiao"].FormattedValue);//未核销金额
                if (benciHeXiaoMoney > weiHeXiaoMoney)
                {
                    MessageBox.Show("本次核销金额不能大于未核销金额！");
                    return;
                }

                decimal weifuMoney = weiHeXiaoMoney - benciHeXiaoMoney;//未付金额
                gr.Cells["shengyuMoney"].Value = weifuMoney;

                //逐行统计数据总数
                decimal tempDanJuMoney = 0;
                decimal tempYiHeXiaoMoney = 0;
                decimal tempWeiHeXiaoMoney = 0;
                decimal tempBenCiHeXiao = 0;
                decimal tempShengYuMoney = 0;
                for (int i = 0; i < superGridControlShangPing.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControlShangPing.PrimaryGrid.Rows[i] as GridRow;
                    tempDanJuMoney += Convert.ToDecimal(tempGR["BillMoney"].FormattedValue);
                    tempYiHeXiaoMoney += Convert.ToDecimal(tempGR["yiHeXiao"].FormattedValue);
                    tempWeiHeXiaoMoney += Convert.ToDecimal(tempGR["weiHeXiao"].FormattedValue);
                    tempBenCiHeXiao += Convert.ToDecimal(tempGR["benciHeXiao"].FormattedValue);
                    tempShengYuMoney += Convert.ToDecimal(tempGR["shengyuMoney"].FormattedValue);
                }
                _danJuMoney = tempDanJuMoney;
                _yiHeXiaoMoney = tempYiHeXiaoMoney;
                _weiHeXiaoMoney = tempWeiHeXiaoMoney;
                _benCiHeXiaoMoney = tempBenCiHeXiao;
                _shengYuMoney = tempShengYuMoney;
                gr = (GridRow)superGridControlShangPing.PrimaryGrid.LastSelectableRow;
                gr["BillMoney"].Value = _danJuMoney.ToString();
                gr["yiHeXiao"].Value = _yiHeXiaoMoney.ToString();
                gr["weiHeXiao"].Value = _weiHeXiaoMoney.ToString();
                gr["benciHeXiao"].Value = _benCiHeXiaoMoney.ToString();
                gr["shengyuMoney"].Value = tempShengYuMoney.ToString();

                labtextboxTop3.Text = _benCiHeXiaoMoney.ToString("0.00");
                labtextboxTop6.Text = _benCiHeXiaoMoney.ToString("0.00");
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：1513-验证表格里的金额以及统计数量出错！请检查：" + ex.Message, "收款单温馨提示！");
            }
        }
    }
}