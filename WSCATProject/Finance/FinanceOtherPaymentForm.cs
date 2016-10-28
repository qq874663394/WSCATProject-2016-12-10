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

namespace WSCATProject.Finance
{
    public partial class FinanceOtherPaymentForm : TestForm
    {
        public FinanceOtherPaymentForm()
        {
            InitializeComponent();
        }

        #region 调用接口以及加密解密方法
        CodingHelper ch = new CodingHelper();
        SupplierInterface supplier = new SupplierInterface();//供应商
        BankAccountInterface bank = new BankAccountInterface();//结算账户
        EmpolyeeInterface employee = new EmpolyeeInterface();//员工  
        ProjectCostInterface projectCostInterface = new ProjectCostInterface();//其他付款项目
        #endregion

        #region 数据字段
        /// <summary>
        /// 所有供应商
        /// </summary>
        private DataTable _AllSupplier = null; 

        /// <summary>
        /// 所以经手人
        /// </summary>
        private DataTable _AllEmployee = null;

        /// <summary>
        /// 所有账户
        /// </summary>
        private DataTable _AllBank = null;

        /// <summary>
        /// 点击的项,1供应商  2为采购员  3为仓库   
        /// </summary>
        private int _Click = 0;

        /// <summary>
        /// 保存供应商Code
        /// </summary>
        private string _supplierCode;

        /// <summary>
        /// 保存经手人Code
        /// </summary>
        private string _employeeCode;

        /// <summary>
        /// 账号code
        /// </summary>
        private string _bankCode;

        /// <summary>
        /// 其他付款单的code
        /// </summary>
        private string _financeOtherPaymentCode;

        /// <summary>
        /// 所以其他付款项目
        /// </summary>
        private DataTable _AllProjectInCost;

        /// <summary>
        /// 统计单据金额
        /// </summary>
        private decimal _Money;
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

                    resizablePanel1.Location = new Point(520, 160);
                    dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllSupplier);
                    resizablePanel1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：3106-尝试点击供应商数据出错或者无数据！请检查：" + ex.Message, "其他付款单温馨提示！");
            }
        }

        /// <summary>
        /// 初始化经手人
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
                MessageBox.Show("错误代码：3107-尝试点击经手人数据出错或者无数据！请检查：" + ex.Message, "其他付款单温馨提示！");
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

                    resizablePanel1.Location = new Point(530, 190);
                    dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllBank);
                    resizablePanel1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：3503-尝试点击结算账户，数据显示失败或者无数据！请检查：" + ex.Message, "其他付款单温馨提示！");
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
            gr.Cells["gridColumnMoney"].Value = 0;
            gr.Cells["gridColumnMoney"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnMoney"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["material"].AllowSelection = false;
            gr.Cells["gridColumnMoney"].AllowSelection = false;
            gr.Cells["gridColumnbeizhu"].AllowSelection = false;
        }

        /// <summary>
        /// 标记那个控件不可用
        /// </summary>
        private void InitForm()
        {
            foreach (Control c in panel2.Controls)
            {
                switch (c.GetType().Name)
                {
                    case "Label":
                        c.Enabled = false;
                        c.ForeColor = Color.Gray;
                        break;
                    case "TextBoxX":
                        c.Enabled = false;
                        c.BackColor = Color.White;
                        break;
                    case "ComboBoxEx":
                        c.Enabled = false;
                        c.BackColor = Color.White;
                        break;
                    case "PictureBox":
                        c.Enabled = false;
                        break;
                }
            }
            foreach (Control c in panel5.Controls)
            {
                switch (c.GetType().Name)
                {
                    case "Label":
                        c.Enabled = false;
                        c.ForeColor = Color.Gray;
                        break;
                    case "TextBoxX":
                        c.Enabled = false;
                        c.BackColor = Color.White;
                        break;
                    case "PictureBox":
                        c.Enabled = false;
                        break;
                }
            }
            superGridControlShangPing.PrimaryGrid.ReadOnly = true;
            pictureBoxshenghe.Visible = true;
        }

        /// <summary>
        /// 初始化收入类别
        /// </summary>
        private void InitProjectCost()
        {
            try
            {
                dataGridViewShangPing.DataSource = null;
                dataGridViewShangPing.Columns.Clear();
                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "code";
                dgvc.Visible = false;
                dgvc.HeaderText = "编号";
                dgvc.DataPropertyName = "code";
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewShangPing.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "projectCode";
                dgvc.HeaderText = "收入类别代码";
                dgvc.DataPropertyName = "projectCode";
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewShangPing.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "name";
                dgvc.HeaderText = "收入类别名称";
                dgvc.DataPropertyName = "name";
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewShangPing.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "parentId";
                dgvc.Visible = false;
                dgvc.HeaderText = "parentId";
                dgvc.DataPropertyName = "parentId";
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewShangPing.Columns.Add(dgvc);
                resizablePanelData.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化收入类别失败，请检查：" + ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 关标定位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinanceOtherPaymentForm_Activated(object sender, EventArgs e)
        {
            labtextboxTop2.Focus();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinanceOtherPaymentForm_Load(object sender, EventArgs e)
        {

            //供应商
            _AllSupplier = supplier.SelSupplierTable();
            //经手人
            _AllEmployee = employee.SelSupplierTable(false);
            //结算账户
            _AllBank = bank.GetList(999, "", false, false);

            //生成采购订单code和显示条形码
            _financeOtherPaymentCode = BuildCode.ModuleCode("FOP");
            textBoxOddNumbers.Text = _financeOtherPaymentCode;
            barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
            _Code.ValueFont = new Font("微软雅黑", 20);
            System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
            pictureBoxtiaoxingma.Image = imgTemp;

            #region 初始化数据
            comboBoxType.SelectedIndex = 0;
            InitDataGridView();
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
            superGridControlShangPing.PrimaryGrid.SortCycle = SortCycle.AscDesc;    //排序方式范围
            superGridControlShangPing.PrimaryGrid.AddSort(superGridControlShangPing.PrimaryGrid.Columns[0], SortDirection.Ascending);//设置排序列和排序方式
                                                                                                                                     //金额
            GridDoubleInputEditControl gdiecMoney = superGridControlShangPing.PrimaryGrid.Columns["gridColumnMoney"].EditControl as GridDoubleInputEditControl;
            gdiecMoney.MinValue = 1;
            gdiecMoney.MaxValue = 999999999;
            #endregion

            //绑定事件 双击事填充内容并隐藏列表
            dataGridViewFuJia.CellDoubleClick += dataGridViewFuJia_CellDoubleClick;
            dataGridViewShangPing.CellDoubleClick += DataGridViewShangPing_CellDoubleClick;
            toolStripBtnSave.Click += ToolStripBtnSave_Click;//保存
            toolStripBtnShengHe.Click += ToolStripBtnShengHe_Click;//审核按钮
            dataGridViewFuJia.KeyDown += DataGridViewFuJia_KeyDown;
        }
        
        /// <summary>
        /// 小表格的回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewFuJia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                dataGridViewFuJiTableClick();
            }
        }

        /// <summary>
        /// 审核按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripBtnShengHe_Click(object sender, EventArgs e)
        {
            //审核
            DialogResult result = MessageBox.Show("是否一键审核？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                Review();
            }
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripBtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        #region 小箭头和表格点击绑定事件

        /// <summary>
        /// 小表格的双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewFuJia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            dataGridViewFuJiTableClick();
        }

        /// <summary>
        /// 小表格的项目点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewShangPing_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            dataGridViewShangPingTableClick();
        }

        /// <summary>
        /// dataGridViewShangPing表格双击事件函数
        /// </summary>
        private void dataGridViewShangPingTableClick()
        {
            try
            {
                //是否要新增一行的标记
                bool newAdd = false;
                GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[ClickRowIndex];
                GridItemsCollection grs = superGridControlShangPing.PrimaryGrid.Rows;
                //id字段为空 说明是没有数据的行 不是修改而是新增
                if (gr.Cells["gridColumnid"].Value == null)
                {
                    newAdd = true;
                }
                gr.Cells["material"].Value = dataGridViewShangPing.Rows[dataGridViewShangPing.CurrentRow.Index].Cells["name"].Value;
                resizablePanelData.Visible = false;
                //新增一行
                if (newAdd)
                {
                    superGridControlShangPing.PrimaryGrid.NewRow(superGridControlShangPing.PrimaryGrid.Rows.Count);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-双击绑定收入类别失败！" + ex.Message, "其它收款单温馨提示！");
            }
            superGridControlShangPing.Focus();
            SendKeys.Send("^{End}{Home}");
        }

        /// <summary>
        /// 供应商小箭头的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxSupplier_Click(object sender, EventArgs e)
        {
            if (_Click != 1)
            {
                InitSupplier();
                this.dataGridViewFuJia.Focus();
            }
            _Click = 4;
        }

        /// <summary>
        /// 结算账户小箭头点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxBank_Click(object sender, EventArgs e)
        {
            if (_Click != 2)
            {
                InitBank();
                dataGridViewFuJia.Focus();
            }
            _Click = 5;
        }

        /// <summary>
        /// 经手人的小箭头点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxEmployee_Click(object sender, EventArgs e)
        {
            if (_Click != 3)
            {
                InitEmployee();
                dataGridViewFuJia.Focus();
            }
            _Click = 6;
        }

        private void dataGridViewFuJiTableClick()
        {
            try
            {
                //供应商
                if (_Click == 1 || _Click == 4)
                {
                    _supplierCode = dataGridViewFuJia.Rows[dataGridViewFuJia.CurrentRow.Index].Cells["code"].Value.ToString();//供应商code
                    string name = dataGridViewFuJia.Rows[dataGridViewFuJia.CurrentRow.Index].Cells["name"].Value.ToString();//供应商名称
                    labtextboxTop2.Text = name;
                    resizablePanel1.Visible = false;
                }
                //结算账户
                if (_Click == 2 || _Click == 5)
                {
                    _bankCode = dataGridViewFuJia.Rows[dataGridViewFuJia.CurrentRow.Index].Cells["code"].Value.ToString();//银行账户code
                    string name = dataGridViewFuJia.Rows[dataGridViewFuJia.CurrentRow.Index].Cells["openBank"].Value.ToString();//账户名称
                    labtextboxTop4.Text = name;
                    resizablePanel1.Visible = false;
                }
                //收款员
                if (_Click == 3 || _Click == 6)
                {
                    _employeeCode = dataGridViewFuJia.Rows[dataGridViewFuJia.CurrentRow.Index].Cells["code"].Value.ToString();//收款员code
                    string name = dataGridViewFuJia.Rows[dataGridViewFuJia.CurrentRow.Index].Cells["name"].Value.ToString();//收款员
                    ltxtbSalsMan.Text = name;
                    resizablePanel1.Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：3506-双击绑定客户、结算账户、收款员数据错误！请检查：" + ex.Message, "其他付款单温馨提示！");
            }
        }

        #endregion

        /// <summary>
        /// 单据类别的选中改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = comboBoxType.Text;
            switch (type)
            {
                case "费用结算":
                    labTop4.ForeColor = Color.Black;
                    labtextboxTop4.ReadOnly = false;
                    labtextboxTop4.ForeColor = Color.Black;
                    labtextboxTop4.Text = "";
                    pictureBox3.Enabled = true;
                    labTop8.ForeColor = Color.Black;
                    labtextboxTop8.ReadOnly = false;
                    labtextboxTop8.ForeColor = Color.Black;
                    labTop6.ForeColor = Color.Black;
                    labtextboxTop6.ReadOnly = false;
                    labtextboxTop6.BackColor = Color.White;
                    labtextboxTop6.ForeColor = Color.Black;
                    break;
                case "其他应付":
                    labTop4.ForeColor = Color.Gray;
                    labtextboxTop4.ReadOnly = true;
                    labtextboxTop4.BackColor = Color.White;
                    labtextboxTop4.ForeColor = Color.Gray;
                    labtextboxTop4.Text = "应付账款账号";
                    pictureBox3.Enabled = false;
                    labTop8.ForeColor = Color.Gray;
                    labtextboxTop8.ReadOnly = true;
                    labtextboxTop8.BackColor = Color.White;
                    labtextboxTop8.ForeColor = Color.Gray;
                    labTop6.ForeColor = Color.Gray;
                    labtextboxTop6.ReadOnly = true;
                    labtextboxTop6.BackColor = Color.White;
                    labtextboxTop6.ForeColor = Color.Gray;
                    break;
            }
        }

        /// <summary>
        /// 设置快捷键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinanceOtherPaymentForm_KeyDown(object sender, KeyEventArgs e)
        {
            //前单
            if (e.KeyCode == Keys.B && e.Modifiers == Keys.Control)
            {
                MessageBox.Show("前单");
                return;
            }
            //后单
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                MessageBox.Show("后单");
                return;
            }
            //新增
            if (e.KeyCode == Keys.N && e.Modifiers == Keys.Control)
            {
                MessageBox.Show("新增");
                return;
            }
            //保存
            if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
            {
                Save();
                return;
            }
            //审核
            if (e.KeyCode == Keys.F4)
            {
                DialogResult result = MessageBox.Show("是否一键审核？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    Review();
                }
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
        /// 保存按钮的函数
        /// </summary>
        private void Save()
        {

        }

        /// <summary>
        /// 审核按钮的函数
        /// </summary>
        private void Review()
        {

        }

        /// <summary>
        /// 表格的第一个格子的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControlShangPing_BeginEdit(object sender, GridEditEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "material")
            {
                SelectedElementCollection ge = superGridControlShangPing.PrimaryGrid.GetSelectedCells();
                GridCell gc = ge[0] as GridCell;
                string typeDaima = XYEEncoding.strCodeHex(e.EditControl.EditorValue.ToString());
                if (gc.GridRow.Cells[material].Value != null && (gc.GridRow.Cells[material].Value).ToString() != "")
                {
                    //模糊查询收入类别列表
                    _AllProjectInCost = projectCostInterface.GetList(0, "" + typeDaima + "");
                    InitProjectCost();
                }
                else
                {
                    //查询收入类别列表
                    _AllProjectInCost = projectCostInterface.GetList(999, "");
                    InitProjectCost();
                }

                dataGridViewShangPing.DataSource = ch.DataTableReCoding(_AllProjectInCost);

            }
        }

        /// <summary>
        /// 表格输入验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControlShangPing_CellValidated(object sender, GridCellValidatedEventArgs e)
        {
            try
            {
                TongJi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-验证表格里的金额以及统计金额出错！请检查：" + ex.Message, "其它收款单温馨提示！");
            }
        }

        /// <summary>
        /// 统计表格里的金额
        /// </summary>
        private void TongJi()
        {
            GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[ClickRowIndex];
            //逐行统计数据总数
            decimal tempAllMoney = 0;
            for (int i = 0; i < superGridControlShangPing.PrimaryGrid.Rows.Count - 1; i++)
            {
                GridRow tempGR = superGridControlShangPing.PrimaryGrid.Rows[i] as GridRow;
                tempAllMoney += Convert.ToDecimal(tempGR["gridColumnMoney"].FormattedValue);
            }
            _Money = tempAllMoney;
            gr = (GridRow)superGridControlShangPing.PrimaryGrid.LastSelectableRow;
            gr["gridColumnMoney"].Value = _Money.ToString();
            labtextboxTop3.Text = _Money.ToString("0.00");
            labtextboxTop5.Text = _Money.ToString("0.00");
        }

        /// <summary>
        /// 实时模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControlShangPing_EditorValueChanged(object sender, GridEditEventArgs e)
        {
            try
            {
                string SS = "";
                GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[ClickRowIndex];
                string materialDaima = XYEEncoding.strCodeHex(e.EditControl.EditorValue.ToString());
                if (SS == "")
                {
                    //模糊查询商品列表
                    _AllProjectInCost = projectCostInterface.GetList( 0,""+ materialDaima + "");
                    InitProjectCost();
                    dataGridViewShangPing.DataSource = ch.DataTableReCoding(_AllProjectInCost);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:-表格模糊查询错误，查询数据错误" + ex.Message, "入库单温馨提示");
            }
        }
    }
}
