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
    public partial class FinanceVerificationForm : TestForm
    {
        public FinanceVerificationForm()
        {
            InitializeComponent();
        }

        #region 调用接口以及加密解密方法
        CodingHelper ch = new CodingHelper();
        ClientInterface client = new ClientInterface();//客户
        SupplierInterface supplier = new SupplierInterface();//供应商
        EmpolyeeInterface employee = new EmpolyeeInterface();//员工
        #endregion

        #region 数据字段

        /// <summary>
        /// 所有客户
        /// </summary>
        private DataTable _AllClient = null;
        /// <summary>
        /// 所有供应商
        /// </summary>
        private DataTable _AllSupplier = null;
        /// <summary>
        /// 所有核销员
        /// </summary>
        private DataTable _AllEmployee = null;
        /// <summary>
        /// 点击的项,1客户  2为供应商  3为核销员
        /// </summary>
        private int _Click = 0;
        /// <summary>
        /// 保存客户Code
        /// </summary>
        private string _clientCode;
        /// <summary>
        /// 保存供应商code
        /// </summary>
        private string _supplierCode;
        /// <summary>
        /// 保存核销员Code
        /// </summary>
        private string _employeeCode;
        /// <summary>
        /// 核销单code
        /// </summary>
        private string _financeVerificationCode;
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


        #endregion

        #region 初始化数据
        /// <summary>
        /// 统计行数据
        /// </summary>
        private void InitDataGridView()
        {
            #region 上表格统计行
            //新增一行 用于给客户操作
            superGridControlTop.PrimaryGrid.NewRow(true);
            //最后一行做统计行
            GridRow gr = (GridRow)superGridControlTop.PrimaryGrid.
                Rows[superGridControlTop.PrimaryGrid.Rows.Count - 1];
            gr.ReadOnly = true;
            gr.CellStyles.Default.Background.Color1 = Color.SkyBlue;
            gr.Cells["gridColumnSelect"].Value = "合计";
            gr.Cells["gridColumnSelect"].CellStyles.Default.Alignment =
                DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnSelect"].EditorType = typeof(GridTextBoxXEditControl);
            gr.Cells["gridColumnDanJuMoney"].Value = 0;
            gr.Cells["gridColumnDanJuMoney"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnDanJuMoney"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumnYiHeXiao"].Value = 0;
            gr.Cells["gridColumnYiHeXiao"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnYiHeXiao"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumnWeiHeXiao"].Value = 0;
            gr.Cells["gridColumnWeiHeXiao"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnWeiHeXiao"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumnBenCi"].Value = 0;
            gr.Cells["gridColumnBenCi"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnBenCi"].CellStyles.Default.Background.Color1 = Color.Orange;

            #region 表格最后一行不能点击
            gr.Cells["gridColumnSelect"].AllowSelection = false;
            gr.Cells["gridColumnYuanDanCode"].AllowSelection = false;
            gr.Cells["gridColumnYuanDanDate"].AllowSelection = false;
            gr.Cells["gridColumnYuanDanType"].AllowSelection = false;
            gr.Cells["gridColumnDanJuMoney"].AllowSelection = false;
            gr.Cells["gridColumnYiHeXiao"].AllowSelection = false;
            gr.Cells["gridColumnWeiHeXiao"].AllowSelection = false;
            gr.Cells["gridColumnBenCi"].AllowSelection = false;
            gr.Cells["gridColumnRemark"].AllowSelection = false;
            #endregion

            #endregion

            #region 下表格统计行
            //新增一行 用于给客户操作
            superGridControlShangPing.PrimaryGrid.NewRow(true);
            //最后一行做统计行
            GridRow grs = (GridRow)superGridControlShangPing.PrimaryGrid.
                Rows[superGridControlShangPing.PrimaryGrid.Rows.Count - 1];
            grs.ReadOnly = true;
            grs.CellStyles.Default.Background.Color1 = Color.SkyBlue;
            grs.Cells["select"].Value = "合计";
            grs.Cells["select"].CellStyles.Default.Alignment =
                DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            grs.Cells["select"].EditorType = typeof(GridTextBoxXEditControl);
            grs.Cells["danJuMoney"].Value = 0;
            grs.Cells["danJuMoney"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            grs.Cells["danJuMoney"].CellStyles.Default.Background.Color1 = Color.Orange;
            grs.Cells["yiHeXiao"].Value = 0;
            grs.Cells["yiHeXiao"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            grs.Cells["yiHeXiao"].CellStyles.Default.Background.Color1 = Color.Orange;
            grs.Cells["weiHeXiao"].Value = 0;
            grs.Cells["weiHeXiao"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            grs.Cells["weiHeXiao"].CellStyles.Default.Background.Color1 = Color.Orange;
            grs.Cells["benCiHeXiao"].Value = 0;
            grs.Cells["benCiHeXiao"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            grs.Cells["benCiHeXiao"].CellStyles.Default.Background.Color1 = Color.Orange;

            #region 表格最后一行不能点击
            grs.Cells["select"].AllowSelection = false;
            grs.Cells["yuanDanCode"].AllowSelection = false;
            grs.Cells["danJuDate"].AllowSelection = false;
            grs.Cells["yuanDanType"].AllowSelection = false;
            grs.Cells["danJuMoney"].AllowSelection = false;
            grs.Cells["yiHeXiao"].AllowSelection = false;
            grs.Cells["weiHeXiao"].AllowSelection = false;
            grs.Cells["benCiHeXiao"].AllowSelection = false;
            grs.Cells["remark"].AllowSelection = false;
            #endregion

            #endregion
        }

        /// <summary>
        /// 非空验证
        /// </summary>
        private bool isNUllValidate()
        {
            //if (labtextboxTop2.Text.Trim() == null || labtextboxTop2.Text == "")
            //{
            //    MessageBox.Show("客户不能为空！");
            //    labtextboxTop2.Focus();
            //    return false;
            //}
            //if (labtextboxTop4.Text.Trim() == null || labtextboxTop4.Text == "")
            //{
            //    MessageBox.Show("结算账户不能为空！");
            //    labtextboxTop4.Focus();
            //    return false;
            //}
            //GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[1];
            //double BenCiMoney = Convert.ToDouble(gr.Cells["benCiHeXiao"].Value);
            //if (gr.Cells["select"].Value == null || gr.Cells["material"].Value.ToString() == "合计")
            //{
            //    MessageBox.Show("表格里源单不能为空！请选择");
            //    superGridControlShangPing.Focus();
            //    return false;
            //}
            //if (BenCiMoney == 0.00)
            //{
            //    MessageBox.Show("金额不能为0，请检查：");
            //    superGridControlShangPing.Focus();
            //    return false;
            //}
            //if (ltxtbSalsMan.Text.Trim() == null || ltxtbSalsMan.Text == "")
            //{
            //    MessageBox.Show("经手人不能为空！");
            //    ltxtbSalsMan.Focus();
            //    return false;
            //}
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
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "name";
                    dgvc.HeaderText = "客户姓名";
                    dgvc.DataPropertyName = "name";
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    resizablePanel1.Location = new Point(550, 160);
                    dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllClient);
                    resizablePanel1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-尝试点击客户数据显示失败或者无数据！请检查：" + ex.Message, "核销单单温馨提示！");
            }
        }

        /// <summary>
        /// 初始化供应商
        /// </summary>
        private void InitSupplier()
        {
            try
            {
                if (_Click != 2)
                {
                    _Click =2;
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

                    resizablePanel1.Location = new Point(230, 160);
                    dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllSupplier);
                    resizablePanel1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-尝试点击供应商数据出错或者无数据！请检查：" + ex.Message, "核销单温馨提示！");
            }
        }

        /// <summary>
        /// 初始化核销员
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
                MessageBox.Show("错误代码：5103-尝试点击经手人，数据显示失败或者无数据！请检查：" + ex.Message, "其它收款单温馨提示！");
            }
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
            superGridControlTop.PrimaryGrid.ReadOnly = true;
            superGridControlShangPing.PrimaryGrid.ReadOnly = true;
            pictureBoxShengHe.Visible = true;
            toolStripBtnSave.Enabled = false;
            toolStripBtnShengHe.Enabled = false;
        }

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
                                  Color.FromArgb(85, 177, 238),
                                 //Color.White,
                                 1,
                                 ButtonBorderStyle.Solid);
        }
        #endregion

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinanceVerificationForm_Load(object sender, EventArgs e)
        {
            try
            {
                #region 初始化窗体
                pictureBoxShengHe.Parent = pictureBoxtitle;
                cboHeXiaoType.SelectedIndex = 0;
                toolStripButtonXuanYuanDan.Visible = true;
                //禁用自动创建列
                dataGridViewShangPing.AutoGenerateColumns = false;
                dataGridViewFuJia.AutoGenerateColumns = false;
                //是否显示滚动条
                superGridControlShangPing.HScrollBarVisible = true;
                superGridControlTop.HScrollBarVisible = true;
                // 将dataGridView中的内容居中显示
                dataGridViewFuJia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //显示行号
                superGridControlShangPing.PrimaryGrid.ShowRowGridIndex = true;
                superGridControlTop.PrimaryGrid.ShowRowGridIndex = true;
                //内容居中
                superGridControlShangPing.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                superGridControlTop.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                //排序方式范围
                superGridControlShangPing.PrimaryGrid.SortCycle = SortCycle.AscDesc;
                superGridControlTop.PrimaryGrid.SortCycle = SortCycle.AscDesc;
                //设置排序列和排序方式
                superGridControlShangPing.PrimaryGrid.AddSort(superGridControlShangPing.PrimaryGrid.Columns[0], SortDirection.Ascending);
                superGridControlTop.PrimaryGrid.AddSort(superGridControlTop.PrimaryGrid.Columns[0], SortDirection.Ascending);
                //调用合计行数据
                InitDataGridView();

                //上表格本次核销金额金额
                GridDoubleInputEditControl gdiecMoneyTop = superGridControlTop.PrimaryGrid.Columns["gridColumnBenCi"].EditControl as GridDoubleInputEditControl;
                gdiecMoneyTop.MinValue = 1;
                gdiecMoneyTop.MaxValue = 999999999;
                //下表格本次核销金额金额
                GridDoubleInputEditControl gdiecMoneyBottom = superGridControlShangPing.PrimaryGrid.Columns["benCiHeXiao"].EditControl as GridDoubleInputEditControl;
                gdiecMoneyBottom.MinValue = 1;
                gdiecMoneyBottom.MaxValue = 999999999;
                #endregion

                //客户
                _AllClient = client.GetClientByBool(false);
                //供应商
                _AllSupplier = supplier.SelSupplierTable();
                //核销员
                _AllEmployee = employee.SelSupplierTable(false);

                //生成其它收款单code和显示条形码
                _financeVerificationCode = BuildCode.ModuleCode("FVC");
                textBoxOddNumbers.Text = _financeVerificationCode;
                barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
                _Code.ValueFont = new Font("微软雅黑", 20);
                System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
                pictureBoxBarCode.Image = imgTemp;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-窗体加载时，初始化数据错误！请检查：" + ex.Message, "核销单温馨提示！");
                return;
            }
        }

        /// <summary>
        /// 窗体加载时，焦点在核销类型上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinanceVerificationForm_Activated(object sender, EventArgs e)
        {
            cboHeXiaoType.Focus();
        }

        /// <summary>
        /// 按Esc关闭表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinanceVerificationForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.resizablePanel1.Visible = false;
                this.resizablePanelData.Visible = false;
            }
        }

        /// <summary>
        /// 快捷方式设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinanceVerificationForm_KeyDown(object sender, KeyEventArgs e)
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
                //Save();
                return;
            }
            //审核
            if (e.KeyCode == Keys.F4)
            {
                //DialogResult result = MessageBox.Show("是否一键审核？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                //if (result == DialogResult.OK)
                //{
                //    Review();
                //}
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
    }
}
