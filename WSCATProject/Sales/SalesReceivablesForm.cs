using DevComponents.DotNetBar.SuperGrid;
using HelperUtility;
using HelperUtility.Encrypt;
using InterfaceLayer.Base;
using Model;
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
    public partial class SalesReceivablesForm : TestForm
    {
        public SalesReceivablesForm()
        {
            InitializeComponent();
        }
        #region 调用接口以及加密解密方法
        CodingHelper ch = new CodingHelper();
        ClientInterface client = new ClientInterface();//客户
        EmpolyeeInterface employee = new EmpolyeeInterface();//员工
        #endregion

        #region 数据字段

        /// <summary>
        /// 所有客户
        /// </summary>
        private DataTable _AllClient = null;
        /// <summary>
        /// 所有收款员
        /// </summary>
        private DataTable _AllEmployee = null;
        /// <summary>
        /// 点击的项,1客户  2为结算账户  3为收款员
        /// </summary>
        private int _Click = 0;
        /// <summary>
        /// 保存客户Code
        /// </summary>
        private string _clientCode;
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
        /// 收款单单code
        /// </summary>
        private string _SaleReceivablesCode;
        #endregion

        #region 初始化数据
        /// <summary>
        /// 统计行数据
        /// </summary>
        private void InitDataGridView()
        {
            //新增一行 用于给客户操作
            //superGridControlShangPing.PrimaryGrid.NewRow(true);
            ////最后一行做统计行
            //GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.
            //    Rows[superGridControlShangPing.PrimaryGrid.Rows.Count - 1];
            //gr.ReadOnly = true;
            //gr.CellStyles.Default.Background.Color1 = Color.SkyBlue;
            //gr.Cells["material"].Value = "合计";
            //gr.Cells["material"].CellStyles.Default.Alignment =
            //    DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            //gr.Cells["dinggouNumber"].Value = 0;
            //gr.Cells["dinggouNumber"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            //gr.Cells["dinggouNumber"].CellStyles.Default.Background.Color1 = Color.Orange;
            //gr.Cells["money"].Value = 0;
            //gr.Cells["money"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            //gr.Cells["money"].CellStyles.Default.Background.Color1 = Color.Orange;
            //gr.Cells["TaxMoney"].Value = 0;
            //gr.Cells["TaxMoney"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            //gr.Cells["TaxMoney"].CellStyles.Default.Background.Color1 = Color.Orange;
            //gr.Cells["priceANDtax"].Value = 0;
            //gr.Cells["priceANDtax"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            //gr.Cells["priceANDtax"].CellStyles.Default.Background.Color1 = Color.Orange;
        }

        /// <summary>
        /// 非空验证
        /// </summary>
        private bool isNUllValidate()
        {
            if (labtxtDanJuType.Text.Trim() == null || labtxtDanJuType.Text == "")
            {
                MessageBox.Show("客户不能为空！");
                return false;
            }
            if (txtBank.Text.Trim() == null || txtBank.Text == "")
            {
                MessageBox.Show("结算账户不能为空！");
                return false;
            }
            //GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[0];
            //if (gr.Cells["material"].Value == null || gr.Cells["material"].Value.ToString() == "")
            //{
            //    MessageBox.Show("商品代码不能为空！");
            //    return false;
            //}
            if (ltxtbSalsMan.Text.Trim() == null || ltxtbSalsMan.Text == "")
            {
                MessageBox.Show("收款员不能为空！");
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

                    resizablePanel1.Location = new Point(500, 160);
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
                if (_Click != 3)
                {
                    _Click = 3;
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
                MessageBox.Show("初始化收款员数据失败！请检查：" + ex.Message);
            }
        }

        #endregion

        private void SalesReceivablesForm_Load(object sender, EventArgs e)
        {
            #region 初始化窗体
            cboDanJuType.SelectedIndex = 0;
            cboJieSuanMethod.SelectedIndex = 0;
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

            //客户
            _AllClient = client.GetClientByBool(false);
            //收款员
            _AllEmployee = employee.SelSupplierTable(false);

            //生成销售订单code和显示条形码
            _SaleReceivablesCode = BuildCode.ModuleCode("SRS");
            textBoxOddNumbers.Text = _SaleReceivablesCode;
            barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
            _Code.ValueFont = new Font("微软雅黑", 20);
            System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
            pictureBoxBarCode.Image = imgTemp;

            //绑定事件 双击事填充内容并隐藏列表
            dataGridViewFuJia.CellDoubleClick += dataGridViewFuJia_CellDoubleClick;
            //dataGridViewShangPing.CellDoubleClick += dataGridViewShangPing_CellDoubleClick;
            toolStripBtnSave.Click += toolStripBtnSave_Click;//保存按钮
            toolStripBtnShengHe.Click += toolStripBtnShengHe_Click;//审核按钮
        }

        /// <summary>
        /// 下拉框选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboDanJuType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboDanJuType.Text.Trim())
            {
                case "销售收款":
                    labTop6.ForeColor = Color.Black;
                    txtBenCiShouKuan.ReadOnly = false;
                    txtBenCiShouKuan.Border.BorderBottomColor = Color.Black;
                    labTop7.ForeColor = Color.Black;
                    txtDiscount.ReadOnly = false;
                    txtDiscount.Border.BorderBottomColor = Color.Black;
                    break;
                case "预收款":
                    labTop6.ForeColor = Color.Gray;
                    txtBenCiShouKuan.ReadOnly = true;
                    txtBenCiShouKuan.BackColor = Color.White;
                    txtBenCiShouKuan.Border.BorderBottomColor = Color.Gray;
                    labTop7.ForeColor = Color.Gray;
                    txtDiscount.ReadOnly = true;
                    txtDiscount.BackColor = Color.White;
                    txtDiscount.Border.BorderBottomColor = Color.Gray;
                    break;
                case "预收退款":
                    labTop6.ForeColor = Color.Gray;
                    txtBenCiShouKuan.ReadOnly = true;
                    txtBenCiShouKuan.BackColor = Color.White;
                    txtBenCiShouKuan.Border.BorderBottomColor = Color.Gray;
                    labTop7.ForeColor = Color.Gray;
                    txtDiscount.ReadOnly = true;
                    txtDiscount.BackColor = Color.White;
                    txtDiscount.Border.BorderBottomColor = Color.Gray;
                    break;
            }
        }

        #region 小箭头点击事件和表格的双击事件

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
            if (_Click != 3)
            {
                InitEmployee();
            }
            _Click = 6;
        }

        /// <summary>
        /// 双击绑定客户、结算账户、收款员数据
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
                    txtClient.Text = name;
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
                MessageBox.Show("双击绑定数据错误！请检查：" + ex.Message);
            }
        }

        #endregion

        #region 模糊查询
        /// <summary>
        /// 客户模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtClient_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtClient.Text.Trim() == "")
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

                resizablePanel1.Location = new Point(500, 160);
                string name = XYEEncoding.strCodeHex(this.txtClient.Text.Trim());
                dataGridViewFuJia.DataSource = ch.DataTableReCoding(client.GetList(0, name));
                resizablePanel1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：模糊查询客户数据错误" + ex.Message, "收款单温馨提示");
            }
        }

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
                MessageBox.Show("错误代码：-收款员模糊查询数据失败！" + ex.Message);
            }
        }
        #endregion

        private void SalesReceivablesForm_Activated(object sender, EventArgs e)
        {
            txtClient.Focus();//窗体活动时焦点在客户
        }

        /// <summary>
        /// 按ESC关闭副表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SalesReceivablesForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.resizablePanel1.Visible = false;
                this.resizablePanelData.Visible = false;
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
            //SalesOrderInterface saleorderinterface = new SalesOrderInterface();
            //收款单
            FinanceCollection financecollection = new FinanceCollection();
            //销售订单商品列表
            //List<SalesOrderDetail> salesorderList = new List<SalesOrderDetail>();
            try
            {
                financecollection.code = XYEEncoding.strCodeHex(_SaleReceivablesCode);//收款单单号
                financecollection.date = this.dateTimePicker1.Value;//开单日期
                financecollection.type = XYEEncoding.strCodeHex(cboDanJuType.Text.Trim());//单据类型
                financecollection.clientCode = XYEEncoding.strCodeHex(_clientCode);//客户编号
                financecollection.clientName = XYEEncoding.strCodeHex(txtClient.Text.Trim());//客户名称
                financecollection.accountCode = XYEEncoding.strCodeHex("");//账户code
                financecollection.accountName = XYEEncoding.strCodeHex(txtBank.Text.Trim());//账户名称
                financecollection.settlementMethod = XYEEncoding.strCodeHex(cboJieSuanMethod.Text.Trim());
                financecollection.discount = Convert.ToDecimal(txtDiscount.Text.Trim());//整单折扣率
                financecollection.totalCollection = Convert.ToDecimal(txtBenCiShouKuan.Text.Trim());//整单收款   
                financecollection.remark = XYEEncoding.strCodeHex(txtRemark.Text.Trim());//摘要 
                financecollection.salesCode = XYEEncoding.strCodeHex(_employeeCode);//收款员code
                financecollection.salesMan = XYEEncoding.strCodeHex(ltxtbSalsMan.Text.Trim());//收款员
                financecollection.operationMan = XYEEncoding.strCodeHex(ltxtbMakeMan.Text.Trim());//制单人
                financecollection.checkMan = XYEEncoding.strCodeHex(ltxtbShengHeMan.Text.Trim());//审核人   
                financecollection.isClear = 1;
                financecollection.updatedate = DateTime.Now;
                financecollection.checkState = 0;//审核状态
                financecollection.financeCollectionState = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:尝试创建收款单数据出错,请检查输入" + ex.Message, "收款单温馨提示");
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
                    if (gr["name"].Value != null)
                    {

                        i++;
                        FinanceCollectionDetail financecollectionDetail = new FinanceCollectionDetail();
                        financecollectionDetail.mainCode = XYEEncoding.strCodeHex(this.textBoxOddNumbers.Text);//主表code（销售单code）

                        //SalesOrderDetail salesorderDetail = new SalesOrderDetail();
                        //salesorderDetail.mainCode = XYEEncoding.strCodeHex(this.textBoxOddNumbers.Text);//销售订单单据Code
                        //salesorderDetail.code = XYEEncoding.strCodeHex(_SalesOrderCode + i.ToString());//销售订单明细Code
                        //salesorderDetail.materialCode = XYEEncoding.strCodeHex(_materialCode);//物料code
                        //salesorderDetail.materialNumber = Convert.ToDecimal(gr["dinggouNumber"].Value);//数量
                        //salesorderDetail.materialPrice = Convert.ToDecimal(gr["price"].Value);//单价
                        //salesorderDetail.discountRate = Convert.ToDecimal(gr["DiscountRate"].Value);//折扣率
                        //salesorderDetail.discountMoney = Convert.ToDecimal(gr["DiscountMoney"].Value);//折扣额
                        //salesorderDetail.VATRate = Convert.ToDecimal(gr["TaxRate"].Value);//增值税税率
                        //salesorderDetail.tax = Convert.ToDecimal(gr["TaxMoney"].Value);//税额
                        //salesorderDetail.taxTotal = Convert.ToDecimal(gr["priceANDtax"].Value);//价税合计
                        //salesorderDetail.remark = XYEEncoding.strCodeHex(gr["remark"].Value == null ? "" : gr["remark"].Value.ToString());//备注
                        //salesorderDetail.deliveryNumber = Convert.ToDecimal(gr["FaHuoNumber"].Value);//发货数量    

                        //GridRow dr = superGridControlShangPing.PrimaryGrid.Rows[0] as GridRow;
                        //salesorderList.Add(salesorderDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-尝试创建销售订单详情商品数据出错,请检查输入" + ex.Message, "销售订单温馨提示");
                return;
            }

            ////增加一条销售订单和销售订单详细数据
            //object saleOrderResult = saleorderinterface.AddOrUpdate(salesorder, salesorderList);
            //if (saleOrderResult != null)
            //{
            //    MessageBox.Show("新增销售订单数据成功", "销售订单温馨提示");
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
            ////获得界面上的数据,准备传给base层新增数据
            //SalesOrderInterface saleorderinterface = new SalesOrderInterface();
            ////销售订单
            //SalesOrder salesorder = new SalesOrder();
            ////销售订单商品列表
            //List<SalesOrderDetail> salesorderList = new List<SalesOrderDetail>();
            try
            {
            //    salesorder.code = XYEEncoding.strCodeHex(_SalesOrderCode);//销售订单Code
            //    salesorder.date = this.dateTimePicker1.Value;//开单日期
            //    salesorder.clientCode = XYEEncoding.strCodeHex(_clientCode);//客户code
            //    switch (cboMethod.Text.Trim())//交货方式
            //    {
            //        case "提货":
            //            salesorder.deliversMethod = 0;
            //            break;
            //        case "送货":
            //            salesorder.deliversMethod = 1;
            //            break;
            //        case "发货":
            //            salesorder.deliversMethod = 2;
            //            break;
            //    }
            //    salesorder.deliversLocation = XYEEncoding.strCodeHex(labtextboxTop5.Text);//交货地点
            //    salesorder.deliversDate = dateTimePicker2.Value;//交货日期
            //    salesorder.remark = XYEEncoding.strCodeHex(labtextboxTop9.Text);//摘要
            //    salesorder.operation = XYEEncoding.strCodeHex(ltxtbSalsMan.Text);//销售员
            //    salesorder.makeMan = XYEEncoding.strCodeHex(ltxtbMakeMan.Text);//制单人
            //    salesorder.examine = XYEEncoding.strCodeHex(ltxtbShengHeMan.Text);//审核人
            //    salesorder.checkState = 1;//审核状态
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:尝试创建销售订单数据出错,请检查输入" + ex.Message, "销售订单温馨提示");
                return;
            }

            try
            {
                ////获得商品列表数据,准备传给base层新增数据
                //GridRow g = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[ClickRowIndex];
                //GridItemsCollection grs = superGridControlShangPing.PrimaryGrid.Rows;
                //int i = 0;
                //DateTime nowDataTime = DateTime.Now;
                //foreach (GridRow gr in grs)
                //{
                //    if (gr["name"].Value != null)
                //    {

                //        i++;
                //        SalesOrderDetail salesorderDetail = new SalesOrderDetail();
                //        salesorderDetail.mainCode = XYEEncoding.strCodeHex(this.textBoxOddNumbers.Text);//销售订单单据Code
                //        salesorderDetail.code = XYEEncoding.strCodeHex(_SalesOrderCode + i.ToString());//销售订单明细Code
                //        salesorderDetail.materialCode = XYEEncoding.strCodeHex(_materialCode);//物料code
                //        salesorderDetail.materialNumber = Convert.ToDecimal(gr["dinggouNumber"].Value);//数量
                //        salesorderDetail.materialPrice = Convert.ToDecimal(gr["price"].Value);//单价
                //        salesorderDetail.discountRate = Convert.ToDecimal(gr["DiscountRate"].Value);//折扣率
                //        salesorderDetail.discountMoney = Convert.ToDecimal(gr["DiscountMoney"].Value);//折扣额
                //        salesorderDetail.VATRate = Convert.ToDecimal(gr["TaxRate"].Value);//增值税税率
                //        salesorderDetail.tax = Convert.ToDecimal(gr["TaxMoney"].Value);//税额
                //        salesorderDetail.taxTotal = Convert.ToDecimal(gr["priceANDtax"].Value);//价税合计
                //        salesorderDetail.remark = XYEEncoding.strCodeHex(gr["remark"].Value == null ? "" : gr["remark"].Value.ToString());//备注
                //        salesorderDetail.deliveryNumber = Convert.ToDecimal(gr["FaHuoNumber"].Value);//发货数量    

                //        GridRow dr = superGridControlShangPing.PrimaryGrid.Rows[0] as GridRow;
                //        salesorderList.Add(salesorderDetail);
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-尝试创建销售订单详情商品数据出错,请检查输入" + ex.Message, "销售订单温馨提示");
                return;
            }

            ////增加一条销售订单和销售订单详细数据
            //object saleOrderResult = saleorderinterface.AddOrUpdate(salesorder, salesorderList);
            //if (saleOrderResult != null)
            //{
            //    MessageBox.Show("新增并审核销售订单数据成功", "销售订单温馨提示");
            //    InitForm();
            //    this.picShengHe.Image = Properties.Resources.审核;
            //}
        }
    }
}
