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

        #region 初始化数据
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
        /// 初始化客户
        /// </summary>
        private void InitClient()
        {
            if (_Click != 1)
            {
                _Click = 1;
                dataGridViewFujia.DataSource = null;
                dataGridViewFujia.Columns.Clear();

                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "code";
                dgvc.HeaderText = "客户编号";
                dgvc.DataPropertyName = "code";
                dataGridViewFujia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "name";
                dgvc.HeaderText = "客户姓名";
                dgvc.DataPropertyName = "name";
                dataGridViewFujia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "mobilePhone";
                dgvc.HeaderText = "电话";
                dgvc.DataPropertyName = "mobilePhone";
                dgvc.Visible = false;
                dataGridViewFujia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "linkMan";
                dgvc.HeaderText = "联系人";
                dgvc.DataPropertyName = "linkMan";
                dgvc.Visible = false;
                dataGridViewFujia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "fax";
                dgvc.HeaderText = "传真";
                dgvc.DataPropertyName = "fax";
                dgvc.Visible = false;
                dataGridViewFujia.Columns.Add(dgvc);

                resizablePanel1.Location = new Point(230, 160);
                dataGridViewFujia.DataSource = ch.DataTableReCoding(_AllClient);
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

                dataGridViewFujia.DataSource = ch.DataTableReCoding(_AllEmployee);
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

        /// <summary>
        /// 初始化仓库
        /// </summary>
        private void InitStorage()
        {
            if (_Click != 3)
            {
                _Click = 3;
                dataGridViewFujia.DataSource = null;
                dataGridViewFujia.Columns.Clear();

                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "code";
                dgvc.Visible = true;
                dgvc.HeaderText = "仓库编号";
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
                dgvc.Visible = false;
                dgvc.HeaderText = "仓库地址";
                dgvc.DataPropertyName = "address";
                dataGridViewFujia.Columns.Add(dgvc);

                resizablePanel1.Location = new Point(560, 190);
                //查询仓库的方法
                dataGridViewFujia.DataSource = ch.DataTableReCoding(_AllStorage);
                resizablePanel1.Visible = true;
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
            dgvc.Name = "code";
            dgvc.Visible = false;
            dgvc.HeaderText = "code";
            dgvc.DataPropertyName = "code";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "materialDaima";
            dgvc.HeaderText = "商品代码";
            dgvc.DataPropertyName = "materialDaima";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "name";
            dgvc.HeaderText = "商品名称";
            dgvc.DataPropertyName = "name";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "model";
            dgvc.Visible = false;
            dgvc.HeaderText = "规格型号";
            dgvc.DataPropertyName = "model";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "barCode";
            dgvc.Visible = false;
            dgvc.HeaderText = "条形码";
            dgvc.DataPropertyName = "barCode";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "unit";
            dgvc.Visible = false;
            dgvc.HeaderText = "单位";
            dgvc.DataPropertyName = "unit";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "remark";
            dgvc.Visible = false;
            dgvc.HeaderText = "备注";
            dgvc.DataPropertyName = "remark";
            dataGridView1.Columns.Add(dgvc);

        }

        #endregion

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SalesOrderForm_Load(object sender, EventArgs e)
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
            dataGridView1.AutoGenerateColumns = false;
            dataGridViewFujia.AutoGenerateColumns = false;
            superGridControl1.HScrollBarVisible = true;
            // 将dataGridView中的内容居中显示
            dataGridViewFujia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //显示行号
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            //内容居中
            superGridControl1.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            //调用合计行数据
            InitDataGridView();

            #endregion

            //绑定事件 双击事填充内容并隐藏列表
            dataGridViewFujia.CellDoubleClick += DataGridViewFujia_CellDoubleClick;
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;

            //订购数量
            GridDoubleInputEditControl gdiecNumber = superGridControl1.PrimaryGrid.Columns["dinggouNumber"].EditControl as GridDoubleInputEditControl;
            gdiecNumber.MinValue = 0;
            gdiecNumber.MaxValue = 999999999;

            //生成销售订单code和显示条形码
            _SalesOrderCode = BuildCode.ModuleCode("SO");
            textBoxOddNumbers.Text = _SalesOrderCode;
            barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
            _Code.ValueFont = new Font("微软雅黑", 20);
            System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
            pictureBoxBarCode.Image = imgTemp;
        }

        #region 小箭头和表格点击事件以及两个表格双击绑定数据

        /// <summary>
        /// 客户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
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
        private void pictureBox5_Click(object sender, EventArgs e)
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
        private void pictureBox2_Click(object sender, EventArgs e)
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
        private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "material")
            {
                SelectedElementCollection ge = superGridControl1.PrimaryGrid.GetSelectedCells();
                GridCell gc = ge[0] as GridCell;
                if (gc.GridRow.Cells[material].Value != null && (gc.GridRow.Cells[material].Value).ToString() != "")
                {
                    //模糊查询商品列表
                    //_AllMaterial = pdi.GetList("" + XYEEncoding.strCodeHex(this.cboPurchaseCode.Text.Trim() + ""), "" + XYEEncoding.strCodeHex(gc.GridRow.Cells[material].Value.ToString()) + "");
                    InitMaterialDataGridView();
                }
                else
                {
                    //绑定商品列表
                    //_AllMaterial = pdi.GetList("" + XYEEncoding.strCodeHex(this.cboPurchaseCode.Text.Trim() + ""), "");
                    InitMaterialDataGridView();
                }
                dataGridView1.DataSource = ch.DataTableReCoding(_AllMaterial);
            }
        }

        /// <summary>
        /// 双击绑定客户、销售员、仓库数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewFujia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //客户
                if (_Click == 1 || _Click == 4)
                {
                    _clientCode = dataGridViewFujia.Rows[e.RowIndex].Cells["code"].Value.ToString();//客户code
                    string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();//客户名称
                    string linkman = dataGridViewFujia.Rows[e.RowIndex].Cells["linkMan"].Value.ToString();//联系人
                    string phone = dataGridViewFujia.Rows[e.RowIndex].Cells["mobilePhone"].Value.ToString();//电话
                    string fax = dataGridViewFujia.Rows[e.RowIndex].Cells["fax"].Value.ToString();//传真
                    labtextboxTop1.Text = name;
                    labtextboxTop2.Text = linkman;
                    labtextboxTop3.Text = phone;
                    labtextboxTop8.Text = fax;
                    resizablePanel1.Visible = false;
                }
                //销售员
                if (_Click == 2 || _Click == 5)
                {
                    _employeeCode = dataGridViewFujia.Rows[e.RowIndex].Cells["code"].Value.ToString();//销售员code
                    string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();//销售员
                    ltxtbSalsMan.Text = name;
                    resizablePanel1.Visible = false;
                }
                //仓库
                if (_Click == 3 || _Click == 6)
                {
                    _storgeCode = dataGridViewFujia.Rows[e.RowIndex].Cells["code"].Value.ToString();//仓库code
                    string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();//仓库
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
                gr.Cells["materialCode"].Value = dataGridView1.Rows[e.RowIndex].Cells["code"].Value;//商品code 
                gr.Cells["material"].Value = dataGridView1.Rows[e.RowIndex].Cells["materialDaima"].Value;//商品代码
                gr.Cells["name"].Value = dataGridView1.Rows[e.RowIndex].Cells["name"].Value;//商品名称
                gr.Cells["model"].Value = dataGridView1.Rows[e.RowIndex].Cells["model"].Value;//规格型号
                gr.Cells["barcode"].Value = dataGridView1.Rows[e.RowIndex].Cells["barCode"].Value;//条形码
                gr.Cells["unit"].Value = dataGridView1.Rows[e.RowIndex].Cells["unit"].Value;//单位
                gr.Cells["dinggouNumber"].Value = 1.00;//数量
                gr.Cells["price"].Value = 0.00;//单价
                gr.Cells["discountRate"].Value = 100.00;//折扣率
                gr.Cells["taxRate"].Value = 17.00;//增值税税率
                gr.Cells["remark"].Value = dataGridView1.Rows[e.RowIndex].Cells["remark"].Value;//备注

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
                for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                    tempAllNumber += Convert.ToDecimal(tempGR["dinggouNumber"].FormattedValue);
                    tempAllMoney += Convert.ToDecimal(tempGR["money"].FormattedValue);
                    tempAllTaxMoney += Convert.ToDecimal(tempGR["TaxMoney"].FormattedValue);
                    tempAllPriceAndTax += Convert.ToDecimal(tempGR["priceANDtax"].FormattedValue);
                }
                _Materialnumber = tempAllNumber;
                _Money = tempAllMoney;
                _TaxMoney = tempAllTaxMoney;
                _PriceAndTaxMoney = tempAllPriceAndTax;
                gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                gr["dinggouNumber"].Value = _Materialnumber.ToString();
                gr["money"].Value = _Money.ToString();
                gr["TaxMoney"].Value = _TaxMoney.ToString();
                gr["priceANDtax"].Value = _PriceAndTaxMoney.ToString();

                //新增一行
                if (newAdd)
                {
                    superGridControl1.PrimaryGrid.NewRow(superGridControl1.PrimaryGrid.Rows.Count);
                    //递增数量和金额 默认为1和单价 
                    gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                    _MaterialNumber += 1;
                    gr.Cells["dinggouNumber"].Value = _Materialnumber;
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

                dataGridViewFujia.DataSource = ch.DataTableReCoding(employee.GetList(0, "" + XYEEncoding.strCodeHex(ltxtbSalsMan.Text.Trim()) + ""));
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
        private void labtextboxTop1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.labtextboxTop1.Text.Trim() == "")
                {
                    InitClient();
                    _Click = 4;
                    return;
                }
                dataGridViewFujia.DataSource = null;
                dataGridViewFujia.Columns.Clear();

                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "code";
                dgvc.HeaderText = "客户编号";
                dgvc.DataPropertyName = "code";
                dataGridViewFujia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "name";
                dgvc.HeaderText = "客户姓名";
                dgvc.DataPropertyName = "name";
                dataGridViewFujia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "mobilePhone";
                dgvc.HeaderText = "电话";
                dgvc.DataPropertyName = "mobilePhone";
                dgvc.Visible = false;
                dataGridViewFujia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "linkMan";
                dgvc.HeaderText = "联系人";
                dgvc.DataPropertyName = "linkMan";
                dgvc.Visible = false;
                dataGridViewFujia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "fax";
                dgvc.HeaderText = "传真";
                dgvc.DataPropertyName = "fax";
                dgvc.Visible = false;
                dataGridViewFujia.Columns.Add(dgvc);

                resizablePanel1.Location = new Point(230, 160);
                string name = XYEEncoding.strCodeHex(this.labtextboxTop1.Text.Trim());
                dataGridViewFujia.DataSource = ch.DataTableReCoding(client.GetList(0, name));
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
        private void superGridControl1_EditorValueChanged(object sender, GridEditEventArgs e)
        {

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
        private void superGridControl1_CellValidated(object sender, GridCellValidatedEventArgs e)
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
                for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                    tempAllNumber += Convert.ToDecimal(tempGR["dinggouNumber"].FormattedValue);
                    tempAllMoney += Convert.ToDecimal(tempGR["money"].FormattedValue);
                    tempAllTaxMoney += Convert.ToDecimal(tempGR["TaxMoney"].FormattedValue);
                    tempAllPriceAndTax += Convert.ToDecimal(tempGR["priceANDtax"].FormattedValue);
                }
                _Materialnumber = tempAllNumber;
                _Money = tempAllMoney;
                _TaxMoney = tempAllTaxMoney;
                _PriceAndTaxMoney = tempAllPriceAndTax;
                gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
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

    }
}
