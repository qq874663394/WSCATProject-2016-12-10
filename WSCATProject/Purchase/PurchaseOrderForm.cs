﻿using DevComponents.DotNetBar.SuperGrid;
using HelperUtility;
using HelperUtility.Encrypt;
using InterfaceLayer.Base;
using InterfaceLayer.Purchase;
using Model.Purchase;
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
    public partial class PurchaseOrderForm : TestForm
    {
        public PurchaseOrderForm()
        {
            InitializeComponent();
        }


        #region 调用接口以及加密解密方法
        CodingHelper ch = new CodingHelper();
        SupplierInterface supplier = new SupplierInterface();//供应商
        EmpolyeeInterface employee = new EmpolyeeInterface();//员工  
        StorageInterface storage = new StorageInterface();//交货地点
        MaterialInterface materialInterface = new MaterialInterface();
        #endregion

        #region 数据字段

        /// <summary>
        /// 所有供应商
        /// </summary>
        private DataTable _AllSupplier = null;
        /// <summary>
        /// 所有采购员
        /// </summary>
        private DataTable _AllEmployee = null;
        /// <summary>
        /// 所有仓库
        /// </summary>
        private DataTable _AllStorage = null;
        /// <summary>
        /// 点击的项,1供应商  2为采购员  3为仓库   
        /// </summary>
        private int _Click = 0;
        /// <summary>
        /// 保存供应商Code
        /// </summary>
        private string _supplierCode;
        /// <summary>
        /// 保存采购员Code
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
        /// 采购订单code
        /// </summary>
        private string _PurchaseOrderCode;
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
            gr.Cells["CaiGouNumber"].Value = 0;
            gr.Cells["CaiGouNumber"].EditorType = typeof(GridDoubleInputEditControl);
            gr.Cells["CaiGouNumber"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["CaiGouNumber"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["Money"].Value = 0;
            gr.Cells["Money"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["Money"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["faxMoney"].Value = 0;
            gr.Cells["faxMoney"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["faxMoney"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["priceANDrate"].Value = 0;
            gr.Cells["priceANDrate"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["priceANDrate"].CellStyles.Default.Background.Color1 = Color.Orange;
            #region 合计行不能点击
            gr.Cells["material"].AllowSelection = false;
            gr.Cells["name"].AllowSelection = false;
            gr.Cells["model"].AllowSelection = false;
            gr.Cells["barCode"].AllowSelection = false;
            gr.Cells["unit"].AllowSelection = false;
            gr.Cells["CaiGouNumber"].AllowSelection = false;
            gr.Cells["price"].AllowSelection = false;
            gr.Cells["Money"].AllowSelection = false;
            gr.Cells["discountRate"].AllowSelection = false;
            gr.Cells["discountMoney"].AllowSelection = false;
            gr.Cells["faxRate"].AllowSelection = false;
            gr.Cells["faxMoney"].AllowSelection = false;
            gr.Cells["priceANDrate"].AllowSelection = false;
            gr.Cells["remark"].AllowSelection = false;
            #endregion
        }

        /// <summary>
        /// 非空验证
        /// </summary>
        private bool isNUllValidate()
        {
            if (txtSupply.Text.Trim() == null || txtSupply.Text == "")
            {
                MessageBox.Show("供应商不能为空！");
                txtSupply.Focus();
                return false;
            }
            //if (txtLinkMan.Text.Trim() == null || txtLinkMan.Text == "")
            //{
            //    MessageBox.Show("联系人不能为空！");
            //    txtLinkMan.Focus();
            //    return false;
            //}
            //if (txtPhone.Text.Trim() == null || txtPhone.Text == "")
            //{
            //    MessageBox.Show("电话不能为空！");
            //    txtPhone.Focus();
            //    return false;
            //}
            if (cboMethod.Text.Trim() == null || cboMethod.Text == "")
            {
                MessageBox.Show("交货方式不能为空！");
                cboMethod.Focus();
                return false;
            }
            if (txtAddress.Text.Trim() == null || txtAddress.Text == "")
            {
                MessageBox.Show("交货地点不能为空！");
                txtAddress.Focus();
                return false;
            }
            GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[1];
            if (gr.Cells["name"].Value == null || gr.Cells["name"].Value.ToString() == "")
            {
                MessageBox.Show("商品代码不能为空！");
                superGridControlShangPing.Focus();
                return false;
            }
            if (ltxtbSalsMan.Text.Trim() == null || ltxtbSalsMan.Text == "")
            {
                MessageBox.Show("销售员不能为空！");
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

                    resizablePanel1.Location = new Point(230, 160);
                    dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllSupplier);
                    resizablePanel1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：3106-尝试点击供应商数据出错或者无数据！请检查：" + ex.Message, "采购订单温馨提示！");
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
                MessageBox.Show("错误代码：3107-尝试点击采购员数据出错或者无数据！请检查：" + ex.Message, "采购订单温馨提示！");
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
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridViewFuJia.Columns.Add(dgvc);

                    resizablePanel1.Location = new Point(560, 190);
                    //查询仓库的方法
                    dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllStorage);
                    resizablePanel1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：3108-尝试点击交货地点数据出错或者无数据！请检查：" + ex.Message, "采购订单温馨提示！");
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
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewShangPing.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "materialDaima";
                dgvc.HeaderText = "商品代码";
                dgvc.DataPropertyName = "materialDaima";
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewShangPing.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "name";
                dgvc.HeaderText = "商品名称";
                dgvc.DataPropertyName = "name";
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewShangPing.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "model";
                dgvc.Visible = false;
                dgvc.HeaderText = "规格型号";
                dgvc.DataPropertyName = "model";
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewShangPing.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "barCode";
                dgvc.Visible = false;
                dgvc.HeaderText = "条形码";
                dgvc.DataPropertyName = "barCode";
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewShangPing.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "unit";
                dgvc.Visible = false;
                dgvc.HeaderText = "单位";
                dgvc.DataPropertyName = "unit";
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewShangPing.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "price";
                dgvc.Visible = false;
                dgvc.HeaderText = "单价";
                dgvc.DataPropertyName = "price";
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewShangPing.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "remark";
                dgvc.Visible = false;
                dgvc.HeaderText = "备注";
                dgvc.DataPropertyName = "remark";
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewShangPing.Columns.Add(dgvc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化商品列表失败，请检查：" + ex.Message);
            }
        }

        /// <summary>
        /// 标示那个控件不可用
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
        }

        #endregion

        /// <summary>
        /// 保存按钮函数
        /// </summary>
        private void Save()
        {
            if (isNUllValidate() == false)
            {
                return;
            }
            ////获得界面上的数据,准备传给base层新增数据
            PurchaseOrderInterface purchaseOrderinterface = new PurchaseOrderInterface();
            ////采购订单
            PurchaseOrder purchaseorder = new PurchaseOrder();
            ////采购订单商品列表
            List<PurchaseOrderDetail> purchaseorderList = new List<PurchaseOrderDetail>();
            try
            {
                purchaseorder.code = XYEEncoding.strCodeHex(_PurchaseOrderCode);//采购订单code
                purchaseorder.date = this.dateTimePicker1.Value;//开单日期
                purchaseorder.supplierCode = XYEEncoding.strCodeHex(_supplierCode);//供应商code
                switch (cboMethod.Text.Trim())//交货方式
                {
                    case "提货":
                        purchaseorder.deliversMethod = 0;
                        break;
                    case "送货":
                        purchaseorder.deliversMethod = 1;
                        break;
                    case "发货":
                        purchaseorder.deliversMethod = 2;
                        break;
                }
                if (txtAddress.Text != null || txtAddress.Text != "")
                {
                    purchaseorder.deliversLocation = XYEEncoding.strCodeHex(txtAddress.Text.Trim());//交货地点
                }
                else
                {
                    MessageBox.Show("交货地点员不能为空！");
                    txtAddress.Focus();
                    return;
                }

                purchaseorder.deliversDate = this.dateTimePicker2.Value;//交货日期
                purchaseorder.depositReceived = Convert.ToDecimal(txtYiFuDingJin.Text.Trim() == "" ? 0.0M : Convert.ToDecimal(txtYiFuDingJin.Text));//已付订金
                purchaseorder.remark = XYEEncoding.strCodeHex(txtRemark.Text == null ? "" : txtRemark.Text.Trim());//摘要
                if (ltxtbSalsMan.Text != null || ltxtbSalsMan.Text != "")
                {
                    purchaseorder.operation = XYEEncoding.strCodeHex(ltxtbSalsMan.Text.Trim());
                }
                else
                {
                    MessageBox.Show("采购员不能为空！");
                    ltxtbSalsMan.Focus();
                    return;
                }
                purchaseorder.makeMan = XYEEncoding.strCodeHex(ltxtbMakeMan.Text == null ? "" : ltxtbMakeMan.Text.Trim());//制单人
                purchaseorder.examine = XYEEncoding.strCodeHex(ltxtbShengHeMan.Text == null ? "" : ltxtbShengHeMan.Text.Trim());//审核人
                purchaseorder.checkState = 0;//审核状态
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:3102-尝试创建采购订单数据出错！请检查：" + ex.Message, "采购订单温馨提示");
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
                        PurchaseOrderDetail purchaseorderDetail = new PurchaseOrderDetail();
                        purchaseorderDetail.mainCode = XYEEncoding.strCodeHex(this.textBoxOddNumbers.Text);//采购订单单据code
                        purchaseorderDetail.code = XYEEncoding.strCodeHex(_PurchaseOrderCode + i.ToString());//采购订单明细code
                        purchaseorderDetail.materialCode = XYEEncoding.strCodeHex(_materialCode);//商品code
                        purchaseorderDetail.materialNumber = Convert.ToDecimal(gr["CaiGouNumber"].Value.ToString() == "" ? 0.0M : Convert.ToDecimal(gr["CaiGouNumber"].Value));//数量
                        purchaseorderDetail.materialPrice = Convert.ToDecimal(gr["price"].Value.ToString() == "" ? 0.0M : Convert.ToDecimal(gr["price"].Value));//单价    
                        //判断表格里的金额与实际值是否相符     
                        decimal money = Convert.ToDecimal(gr["CaiGouNumber"].Value) * Convert.ToDecimal(gr["price"].Value);
                        if (money == Convert.ToDecimal(gr["Money"].Value))
                        {
                            purchaseorderDetail.materialMoney = Convert.ToDecimal(gr["Money"].Value);//金额
                        }
                        else
                        {
                            MessageBox.Show("表格里金额的值错误！");
                            return;
                        }
                        purchaseorderDetail.discountRate = Convert.ToDecimal(gr["discountRate"].Value.ToString() == "" ? 0.0M : Convert.ToDecimal(gr["discountRate"].Value));//折扣率
                        decimal discountAfter = money * (Convert.ToDecimal(gr["discountRate"].Value) / 100);
                        decimal discountMoney = money - discountAfter;
                        //判断表格里的折扣额与实际值是否相符
                        if (discountMoney == Convert.ToDecimal(gr["discountMoney"].Value))
                        {
                            purchaseorderDetail.discountMoney = Convert.ToDecimal(gr["discountMoney"].Value);//折扣额
                        }
                        else
                        {
                            MessageBox.Show("表格里折扣额的值错误！");
                            return;
                        }
                        purchaseorderDetail.VATRate = Convert.ToDecimal(gr["faxRate"].Value.ToString() == "" ? 0.0M : Convert.ToDecimal(gr["faxRate"].Value));//增值税税率
                        //判断表格里的税额与实际值是否相符
                        decimal rateMoney = money * (Convert.ToDecimal(gr["faxRate"].Value) / 100);
                        if (rateMoney == Convert.ToDecimal(gr["faxMoney"].Value))
                        {
                            purchaseorderDetail.tax = Convert.ToDecimal(gr["faxMoney"].Value);//税额
                        }
                        else
                        {
                            MessageBox.Show("表格里税额的值错误！");
                            return;
                        }
                        //判断表格里的价税合计与实际值是否相符
                        decimal jiashui = money + rateMoney;
                        if (jiashui == Convert.ToDecimal(gr["priceANDrate"].Value))
                        {
                            purchaseorderDetail.taxTotal = Convert.ToDecimal(gr["priceANDrate"].Value);//价税合计
                        }
                        else
                        {
                            MessageBox.Show("表格里价税合计的值错误！");
                            return;
                        }
                        purchaseorderDetail.remark = XYEEncoding.strCodeHex(gr["remark"].Value == null ? "" : gr["remark"].Value.ToString());//备注
                        if (gr["shouHuoNumber"].Value == null || gr["shouHuoNumber"].Value.ToString() == "")
                        {
                            purchaseorderDetail.deliveryNumber = Convert.ToDecimal(0.00);
                        }
                        else
                        {
                            purchaseorderDetail.deliveryNumber = Convert.ToDecimal(gr["shouHuoNumber"].Value.ToString() == "" ? 0.0M : Convert.ToDecimal(gr["shouHuoNumber"].Value));//收货数量    
                        }



                        GridRow dr = superGridControlShangPing.PrimaryGrid.Rows[0] as GridRow;
                        purchaseorderList.Add(purchaseorderDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：3103-尝试创建采购订单商品详细数据出错!请检查:" + ex.Message, "采购订单温馨提示");
                return;
            }

            ////增加一条销售订单和销售订单详细数据
            object purchaseOrderResult = purchaseOrderinterface.AddOrUpdateToMainOrDetail(purchaseorder, purchaseorderList);
            if (purchaseOrderResult != null)
            {
                MessageBox.Show("新增采购订单数据成功", "采购订单温馨提示");
            }
        }

        /// <summary>
        /// 审核按钮函数
        /// </summary>
        private void Review()
        {
            if (isNUllValidate() == false)
            {
                return;
            }
            ////获得界面上的数据,准备传给base层新增数据
            PurchaseOrderInterface purchaseOrderinterface = new PurchaseOrderInterface();
            ////采购订单
            PurchaseOrder purchaseorder = new PurchaseOrder();
            ////采购订单商品列表
            List<PurchaseOrderDetail> purchaseorderList = new List<PurchaseOrderDetail>();
            try
            {
                purchaseorder.code = XYEEncoding.strCodeHex(_PurchaseOrderCode);//采购订单code
                purchaseorder.date = this.dateTimePicker1.Value;//开单日期
                purchaseorder.supplierCode = XYEEncoding.strCodeHex(_supplierCode);//供应商code
                switch (cboMethod.Text.Trim())//交货方式
                {
                    case "提货":
                        purchaseorder.deliversMethod = 0;
                        break;
                    case "送货":
                        purchaseorder.deliversMethod = 1;
                        break;
                    case "发货":
                        purchaseorder.deliversMethod = 2;
                        break;
                }
                if (txtAddress.Text != null || txtAddress.Text != "")
                {
                    purchaseorder.deliversLocation = XYEEncoding.strCodeHex(txtAddress.Text.Trim());//交货地点
                }
                else
                {
                    MessageBox.Show("交货地点员不能为空！");
                    txtAddress.FindForm();
                    return;
                }

                purchaseorder.deliversDate = this.dateTimePicker2.Value;//交货日期
                purchaseorder.depositReceived = Convert.ToDecimal(txtYiFuDingJin.Text.Trim() == "" ? 0.0M : Convert.ToDecimal(txtYiFuDingJin.Text));//已付订金
                purchaseorder.remark = XYEEncoding.strCodeHex(txtRemark.Text == null ? "" : txtRemark.Text.Trim());//摘要
                if (ltxtbSalsMan.Text != null || ltxtbSalsMan.Text != "")
                {
                    purchaseorder.operation = XYEEncoding.strCodeHex(ltxtbSalsMan.Text.Trim());
                }
                else
                {
                    MessageBox.Show("采购员不能为空！");
                    ltxtbSalsMan.FindForm();
                    return;
                }
                purchaseorder.makeMan = XYEEncoding.strCodeHex(ltxtbMakeMan.Text == null ? "" : ltxtbMakeMan.Text.Trim());//制单人
                purchaseorder.examine = XYEEncoding.strCodeHex(ltxtbShengHeMan.Text == null ? "" : ltxtbShengHeMan.Text.Trim());//审核人
                purchaseorder.checkState = 0;//审核状态
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:3104-尝试创建并审核采购订单数据出错！请检查：" + ex.Message, "采购订单温馨提示");
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
                        PurchaseOrderDetail purchaseorderDetail = new PurchaseOrderDetail();
                        purchaseorderDetail.mainCode = XYEEncoding.strCodeHex(this.textBoxOddNumbers.Text);//采购订单单据code
                        purchaseorderDetail.code = XYEEncoding.strCodeHex(_PurchaseOrderCode + i.ToString());//采购订单明细code
                        purchaseorderDetail.materialCode = XYEEncoding.strCodeHex(_materialCode);//商品code
                        purchaseorderDetail.materialNumber = Convert.ToDecimal(gr["CaiGouNumber"].Value.ToString() == "" ? 0.0M : Convert.ToDecimal(gr["CaiGouNumber"].Value));//数量
                        purchaseorderDetail.materialPrice = Convert.ToDecimal(gr["price"].Value.ToString() == "" ? 0.0M : Convert.ToDecimal(gr["price"].Value));//单价    
                        //判断表格里的金额与实际值是否相符     
                        decimal money = Convert.ToDecimal(gr["CaiGouNumber"].Value) * Convert.ToDecimal(gr["price"].Value);
                        if (money == Convert.ToDecimal(gr["Money"].Value))
                        {
                            purchaseorderDetail.materialMoney = Convert.ToDecimal(gr["Money"].Value);//金额
                        }
                        else
                        {
                            MessageBox.Show("表格里金额的值错误！");
                            return;
                        }
                        purchaseorderDetail.discountRate = Convert.ToDecimal(gr["discountRate"].Value.ToString() == "" ? 0.0M : Convert.ToDecimal(gr["discountRate"].Value));//折扣率
                        decimal discountAfter = money * (Convert.ToDecimal(gr["discountRate"].Value) / 100);
                        decimal discountMoney = money - discountAfter;
                        //判断表格里的折扣额与实际值是否相符
                        if (discountMoney == Convert.ToDecimal(gr["discountMoney"].Value))
                        {
                            purchaseorderDetail.discountMoney = Convert.ToDecimal(gr["discountMoney"].Value);//折扣额
                        }
                        else
                        {
                            MessageBox.Show("表格里折扣额的值错误！");
                            return;
                        }
                        purchaseorderDetail.VATRate = Convert.ToDecimal(gr["faxRate"].Value.ToString() == "" ? 0.0M : Convert.ToDecimal(gr["faxRate"].Value));//增值税税率
                        //判断表格里的税额与实际值是否相符
                        decimal rateMoney = money * (Convert.ToDecimal(gr["faxRate"].Value) / 100);
                        if (rateMoney == Convert.ToDecimal(gr["faxMoney"].Value))
                        {
                            purchaseorderDetail.tax = Convert.ToDecimal(gr["faxMoney"].Value);//税额
                        }
                        else
                        {
                            MessageBox.Show("表格里税额的值错误！");
                            return;
                        }
                        //判断表格里的价税合计与实际值是否相符
                        decimal jiashui = money + rateMoney;
                        if (jiashui == Convert.ToDecimal(gr["priceANDrate"].Value))
                        {
                            purchaseorderDetail.taxTotal = Convert.ToDecimal(gr["priceANDrate"].Value);//价税合计
                        }
                        else
                        {
                            MessageBox.Show("表格里价税合计的值错误！");
                            return;
                        }
                        purchaseorderDetail.remark = XYEEncoding.strCodeHex(gr["remark"].Value == null ? "" : gr["remark"].Value.ToString());//备注
                        if (gr["shouHuoNumber"].Value == null || gr["shouHuoNumber"].Value.ToString() == "")
                        {
                            purchaseorderDetail.deliveryNumber = Convert.ToDecimal(0.00);
                        }
                        else
                        {
                            purchaseorderDetail.deliveryNumber = Convert.ToDecimal(gr["shouHuoNumber"].Value.ToString() == "" ? 0.0M : Convert.ToDecimal(gr["shouHuoNumber"].Value));//收货数量    
                        }



                        GridRow dr = superGridControlShangPing.PrimaryGrid.Rows[0] as GridRow;
                        purchaseorderList.Add(purchaseorderDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：3105-尝试创建并审核采购订单商品详细数据出错!请检查:" + ex.Message, "采购订单温馨提示");
                return;
            }

            ////增加一条销售订单和销售订单详细数据
            object purchaseOrderResult = purchaseOrderinterface.AddOrUpdateToMainOrDetail(purchaseorder, purchaseorderList);
            if (purchaseOrderResult != null)
            {
                picShengHe.Visible = true;
                InitForm();
                MessageBox.Show("新增并审核采购订单数据成功", "采购订单温馨提示");
            }
        }

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PurchaseOrderForm_Load(object sender, EventArgs e)
        {
            try
            {
                //供应商
                _AllSupplier = supplier.SelSupplierTable();
                //采购员
                _AllEmployee = employee.SelSupplierTable(false);
                //仓库
                _AllStorage = storage.GetList(00, "");
                superGridControlShangPing.PrimaryGrid.SortCycle = SortCycle.AscDesc;    //排序方式范围
                superGridControlShangPing.PrimaryGrid.AddSort(superGridControlShangPing.PrimaryGrid.Columns[0], SortDirection.Ascending);//设置排序列和排序方式
                superGridControlShangPing.PrimaryGrid.ShowRowGridIndex = true;//显示行号

                #region 初始化窗体
                picShengHe.Parent = pictureBoxtitle;
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

                //采购数量
                GridIntegerInputEditControl gdiecNumber = superGridControlShangPing.PrimaryGrid.Columns["CaiGouNumber"].EditControl as GridIntegerInputEditControl;
                gdiecNumber.MinValue = 1;
                gdiecNumber.MaxValue = 999999999;
                //单价
                GridDoubleInputEditControl gdiecPrice = superGridControlShangPing.PrimaryGrid.Columns["price"].EditControl as GridDoubleInputEditControl;
                gdiecNumber.MinValue = 1;
                gdiecNumber.MaxValue = 999999999;
                //折扣率
                GridDoubleInputEditControl gdiecDiscountRate = superGridControlShangPing.PrimaryGrid.Columns["discountRate"].EditControl as GridDoubleInputEditControl;
                gdiecNumber.MinValue = 1;
                gdiecNumber.MaxValue = 100;

                //生成采购订单code和显示条形码
                _PurchaseOrderCode = BuildCode.ModuleCode("POR");
                textBoxOddNumbers.Text = _PurchaseOrderCode;
                barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
                _Code.ValueFont = new Font("微软雅黑", 20);
                System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
                picBarCode.Image = imgTemp;

                //绑定事件 双击事填充内容并隐藏列表
                dataGridViewFuJia.CellDoubleClick += dataGridViewFuJia_CellDoubleClick;
                dataGridViewShangPing.CellDoubleClick += dataGridViewShangPing_CellDoubleClick;

                toolStripBtnSave.Click += toolStripBtnSave_Click;//保存按钮
                toolStripBtnShengHe.Click += toolStripBtnShengHe_Click;//审核按钮

            }
            catch (Exception ex)
            {

                MessageBox.Show("错误代码：3101-窗体加载时，初始化数据错误！请检查：" + ex.Message,"采购订单温馨提示！");
                return;
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripBtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// 审核按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripBtnShengHe_Click(object sender, EventArgs e)
        {
            Review();
        }

        #region 小箭头和表格点击事件以及两个表格双击绑定数据

        /// <summary>
        /// 供应商
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxDanJuType_Click(object sender, EventArgs e)
        {
            if (_Click != 1)
            {
                InitSupplier();
            }
            _Click = 4;
        }

        /// <summary>
        /// 采购员
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
        /// 表格里查询商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void  superGridControlShangPing_BeginEdit(object sender, GridEditEventArgs e)
        {
            try
            {
                if (txtSupply.Text.Trim() == null || txtSupply.Text == "")
                {
                    resizablePanelData.Visible = false;
                    MessageBox.Show("请先选择供应商:");
                    txtSupply.Focus();
                    return;
                }
                else if (e.GridCell.GridColumn.Name == "material")
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
                MessageBox.Show("错误代码：3109-尝试点击商品代码出错或者无数据！请检查：" + ex.Message, "采购订单温馨提示！");
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

                if (e.RowIndex == -1)
                {
                    return;
                }
                //供应商
                if (_Click == 1 || _Click == 4)
                {
                    _supplierCode = dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString();//供应商code
                    string name = dataGridViewFuJia.Rows[e.RowIndex].Cells["name"].Value.ToString();//供应商名称
                    string linkman = dataGridViewFuJia.Rows[e.RowIndex].Cells["linkMan"].Value.ToString();//联系人
                    string phone = dataGridViewFuJia.Rows[e.RowIndex].Cells["mobilePhone"].Value.ToString();//电话
                    string fax = dataGridViewFuJia.Rows[e.RowIndex].Cells["fax"].Value.ToString();//传真
                    txtSupply.Text = name;
                    txtLinkMan.Text = linkman;
                    txtPhone.Text = phone;
                    txtFax.Text = fax;
                    resizablePanel1.Visible = false;
                }
                //采购员
                if (_Click == 2 || _Click == 5)
                {
                    _employeeCode = dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString();//采购员code
                    string name = dataGridViewFuJia.Rows[e.RowIndex].Cells["name"].Value.ToString();//采购员
                    ltxtbSalsMan.Text = name;
                    resizablePanel1.Visible = false;
                }
                //仓库
                if (_Click == 3 || _Click == 6)
                {
                    _storgeCode = dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString();//仓库code
                    string name = dataGridViewFuJia.Rows[e.RowIndex].Cells["name"].Value.ToString();//仓库
                    txtAddress.Text = name;
                    resizablePanel1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：3110-双击绑定供应商或者采购员或者交货地点数据错误！请检查：" + ex.Message, "采购订单温馨提示！");
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
                if (e.RowIndex == -1)
                {
                    return;
                }
                //是否要新增一行的标记
                bool newAdd = false;
                GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[ClickRowIndex];
                GridItemsCollection grs = superGridControlShangPing.PrimaryGrid.Rows;
                //id字段为空 说明是没有数据的行 不是修改而是新增
                if (gr.Cells["gridColumnid"].Value == null)
                {
                    newAdd = true;
                }
                try
                {
                    foreach (GridRow g in grs)
                    {
                        if (g.Cells["materialCode"].Value == null)
                        {
                            newAdd = true;
                            continue;
                        }
                        if (g.Cells["materialCode"].Value.Equals(dataGridViewShangPing.Rows[e.RowIndex].Cells["code"].Value))
                        {
                            decimal shuliang = Convert.ToDecimal(g.Cells["CaiGouNumber"].Value);
                            shuliang += 1;
                            g.Cells["CaiGouNumber"].Value = shuliang;

                            //计算金额
                            decimal dingguoshu = Convert.ToDecimal(g.Cells["CaiGouNumber"].FormattedValue);//采购数量
                            decimal danJa = Convert.ToDecimal(g.Cells["price"].FormattedValue);//单价               
                            decimal Jine = dingguoshu * danJa;//金额
                            g.Cells["Money"].Value = Jine;
                            decimal zheKou = Convert.ToDecimal(g.Cells["discountRate"].FormattedValue);//折扣率
                            decimal zheKouJine = Jine * (zheKou / 100);
                            decimal zheKouE = Jine - zheKouJine;//折扣额
                            g.Cells["discountMoney"].Value = zheKouE;
                            decimal taxrate = Convert.ToDecimal(g.Cells["faxRate"].FormattedValue);//增值税税率
                            decimal ratemoney = Jine * (taxrate / 100);//税额
                            g.Cells["faxMoney"].Value = ratemoney;
                            decimal priceandtax = Jine + ratemoney;//价税合计
                            g.Cells["priceANDrate"].Value = priceandtax;
                            resizablePanelData.Visible = false;

                            //逐行统计数据总数
                            decimal TempAllNumber = 0;
                            decimal tempallMoney = 0;
                            decimal tempallTaxMoney = 0;
                            decimal tempallPriceAndTax = 0;
                            for (int i = 0; i < superGridControlShangPing.PrimaryGrid.Rows.Count - 1; i++)
                            {
                                GridRow tempGR = superGridControlShangPing.PrimaryGrid.Rows[i] as GridRow;
                                TempAllNumber += Convert.ToDecimal(tempGR["CaiGouNumber"].FormattedValue);
                                tempallMoney += Convert.ToDecimal(tempGR["Money"].FormattedValue);
                                tempallTaxMoney += Convert.ToDecimal(tempGR["faxMoney"].FormattedValue);
                                tempallPriceAndTax += Convert.ToDecimal(tempGR["priceANDrate"].FormattedValue);
                            }
                            _MaterialNumber = TempAllNumber;
                            _Money = tempallMoney;
                            _TaxMoney = tempallTaxMoney;
                            _PriceAndTaxMoney = tempallPriceAndTax;
                            gr = (GridRow)superGridControlShangPing.PrimaryGrid.LastSelectableRow;
                            gr["CaiGouNumber"].Value = _MaterialNumber.ToString();
                            gr["Money"].Value = _Money.ToString();
                            gr["faxMoney"].Value = _TaxMoney.ToString();
                            gr["priceANDrate"].Value = _PriceAndTaxMoney.ToString();
                            resizablePanelData.Visible = false;
                            return;
                        }
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误代码：3111-重复添加商品并且计算数据错误" + ex.Message, "采购订单温馨提示！");
                }

                _materialCode = dataGridViewShangPing.Rows[e.RowIndex].Cells["code"].Value.ToString();//商品code 
                gr.Cells["materialCode"].Value = _materialCode;//商品code 
                gr.Cells["material"].Value = dataGridViewShangPing.Rows[e.RowIndex].Cells["materialDaima"].Value;//商品代码
                gr.Cells["name"].Value = dataGridViewShangPing.Rows[e.RowIndex].Cells["name"].Value;//商品名称
                gr.Cells["model"].Value = dataGridViewShangPing.Rows[e.RowIndex].Cells["model"].Value;//规格型号
                gr.Cells["barCode"].Value = dataGridViewShangPing.Rows[e.RowIndex].Cells["barCode"].Value;//条形码
                gr.Cells["unit"].Value = dataGridViewShangPing.Rows[e.RowIndex].Cells["unit"].Value;//单位
                if (dataGridViewShangPing.Rows[e.RowIndex].Cells["unit"].Value.ToString() == "斤")
                {
                    gr.Cells["CaiGouNumber"].EditorType = typeof(GridDoubleInputEditControl);
                }

                gr.Cells["CaiGouNumber"].Value = 1.00;//数量
                gr.Cells["price"].Value = dataGridViewShangPing.Rows[e.RowIndex].Cells["price"].Value;//单价
                double discount = 100.00;
                gr.Cells["discountRate"].Value = discount;//折扣率
                gr.Cells["faxRate"].Value = 17.00;//增值税税率
                gr.Cells["remark"].Value = dataGridViewShangPing.Rows[e.RowIndex].Cells["remark"].Value;//备注

                ////计算金额
                decimal number = Convert.ToDecimal(gr.Cells["CaiGouNumber"].FormattedValue);//订购数量
                decimal price = Convert.ToDecimal(gr.Cells["price"].FormattedValue);//单价               
                decimal money = number * price;//金额
                gr.Cells["Money"].Value = money;
                decimal discountRate = Convert.ToDecimal(gr.Cells["discountRate"].FormattedValue);//折扣率
                decimal discountAfter = money * (discountRate / 100);
                decimal discountMoney = money - discountAfter;//折扣额
                gr.Cells["discountMoney"].Value = discountMoney;
                decimal taxRate = Convert.ToDecimal(gr.Cells["faxRate"].FormattedValue);//增值税税率
                decimal rateMoney = money * (taxRate / 100);//税额
                gr.Cells["faxMoney"].Value = rateMoney;
                decimal priceAndtax = money + rateMoney;//价税合计
                gr.Cells["priceANDrate"].Value = priceAndtax;
                resizablePanelData.Visible = false;

                //逐行统计数据总数
                decimal tempAllNumber = 0;
                decimal tempAllMoney = 0;
                decimal tempAllTaxMoney = 0;
                decimal tempAllPriceAndTax = 0;
                for (int i = 0; i < superGridControlShangPing.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControlShangPing.PrimaryGrid.Rows[i] as GridRow;
                    tempAllNumber += Convert.ToDecimal(tempGR["CaiGouNumber"].FormattedValue);
                    tempAllMoney += Convert.ToDecimal(tempGR["Money"].FormattedValue);
                    tempAllTaxMoney += Convert.ToDecimal(tempGR["faxMoney"].FormattedValue);
                    tempAllPriceAndTax += Convert.ToDecimal(tempGR["priceANDrate"].FormattedValue);
                }
                _Materialnumber = tempAllNumber;
                _Money = tempAllMoney;
                _TaxMoney = tempAllTaxMoney;
                _PriceAndTaxMoney = tempAllPriceAndTax;
                gr = (GridRow)superGridControlShangPing.PrimaryGrid.LastSelectableRow;
                gr["CaiGouNumber"].Value = _Materialnumber.ToString();
                gr["Money"].Value = _Money.ToString();
                gr["faxMoney"].Value = _TaxMoney.ToString();
                gr["priceANDrate"].Value = _PriceAndTaxMoney.ToString();

                //新增一行
                if (newAdd)
                {
                    superGridControlShangPing.PrimaryGrid.NewRow(superGridControlShangPing.PrimaryGrid.Rows.Count);
                    //递增数量和金额 默认为1和单价 
                    gr = (GridRow)superGridControlShangPing.PrimaryGrid.LastSelectableRow;
                    _MaterialNumber += 1;
                    gr.Cells["CaiGouNumber"].Value = _Materialnumber;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：3112-双击绑定物料数据错误" + ex.Message, "采购订单温馨提示！");
            }

            superGridControlShangPing.Focus();
            SendKeys.Send("^{End}{Home}");
        }

        #endregion

        #region 模糊查询

        /// <summary>
        /// 采购员模糊查询
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
                MessageBox.Show("错误代码：3114-模糊查询采购员数据错误！" + ex.Message, "采购订单温馨提示！");
            }
        }

        /// <summary>
        /// 供应商模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSupply_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtSupply.Text.Trim() == "")
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

                resizablePanel1.Location = new Point(230, 160);
                string name = XYEEncoding.strCodeHex(this.txtSupply.Text.Trim());
                dataGridViewFuJia.DataSource = ch.DataTableReCoding(supplier.GetList(0, name));
                resizablePanel1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：3113-模糊查询供应商数据错误" + ex.Message, "采购订单温馨提示");
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
                MessageBox.Show("错误代码:3115-模糊查询表格商品数据错误！请检查：" + ex.Message, "采购订单温馨提示");
            }
        }

        #endregion

        private void PurchaseOrderForm_Activated(object sender, EventArgs e)
        {
            txtSupply.Focus();//焦点在供应商
        }

        /// <summary>
        /// 按ESC关闭副表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PurchaseOrderForm_KeyPress(object sender, KeyPressEventArgs e)
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
                GridRow g = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[1];
                if (g.Cells["name"].Value == null || g.Cells["name"].Value.ToString() == "")
                {
                    MessageBox.Show("请选择商品代码：");
                    g.Cells["CaiGouNumber"].Value = 0.00;
                    g.Cells["price"].Value = 0.00;
                    g.Cells["discountRate"].Value = 100.00;
                    return;
                }
                //最后一行做统计行
                GridRow gr = e.GridPanel.Rows[e.GridCell.RowIndex] as GridRow;
                ////计算金额
                decimal number = Convert.ToDecimal(gr.Cells["CaiGouNumber"].FormattedValue);//订购数量
                decimal price = Convert.ToDecimal(gr.Cells["price"].FormattedValue);//单价               
                decimal money = number * price;//金额
                gr.Cells["Money"].Value = money;
                decimal discountRate = Convert.ToDecimal(gr.Cells["discountRate"].FormattedValue);//折扣率
                decimal discountAfter = money * (discountRate / 100);
                decimal discountMoney = money - discountAfter;//折扣额
                gr.Cells["discountMoney"].Value = discountMoney;
                decimal taxRate = Convert.ToDecimal(gr.Cells["faxRate"].FormattedValue);//增值税税率
                decimal rateMoney = money * (taxRate / 100);//税额
                gr.Cells["faxMoney"].Value = rateMoney;
                decimal priceAndtax = money + rateMoney;//价税合计
                gr.Cells["priceANDrate"].Value = priceAndtax;

                //逐行统计数据总数
                decimal tempAllNumber = 0;
                decimal tempAllMoney = 0;
                decimal tempAllTaxMoney = 0;
                decimal tempAllPriceAndTax = 0;
                for (int i = 0; i < superGridControlShangPing.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControlShangPing.PrimaryGrid.Rows[i] as GridRow;
                    tempAllNumber += Convert.ToDecimal(tempGR["CaiGouNumber"].FormattedValue);
                    tempAllMoney += Convert.ToDecimal(tempGR["Money"].FormattedValue);
                    tempAllTaxMoney += Convert.ToDecimal(tempGR["faxMoney"].FormattedValue);
                    tempAllPriceAndTax += Convert.ToDecimal(tempGR["priceANDrate"].FormattedValue);
                }
                _Materialnumber = tempAllNumber;
                _Money = tempAllMoney;
                _TaxMoney = tempAllTaxMoney;
                _PriceAndTaxMoney = tempAllPriceAndTax;
                gr = (GridRow)superGridControlShangPing.PrimaryGrid.LastSelectableRow;
                gr["CaiGouNumber"].Value = _Materialnumber.ToString();
                gr["Money"].Value = _Money.ToString();
                gr["faxMoney"].Value = _TaxMoney.ToString();
                gr["priceANDrate"].Value = _PriceAndTaxMoney.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：3116-验证表格里的金额以及统计数量出错！请检查：" + ex.Message, "采购订单温馨提示！");
            }
        }

        #region 验证只能输入数字和小数点
        /// <summary>
        /// 验证只能输入数字和小数点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtYiFuDingJin_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //小数点的处理。
                if ((int)e.KeyChar == 46)//小数点
                {
                    if (txtYiFuDingJin.Text.Length <= 0)
                        e.Handled = true;   //小数点不能在第一位
                    else
                    {
                        float f;
                        float oldf;
                        bool b1 = false, b2 = false;
                        b1 = float.TryParse(txtYiFuDingJin.Text, out oldf);
                        b2 = float.TryParse(txtYiFuDingJin.Text + e.KeyChar.ToString(), out f);
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
                        if (((TextBox)sender).Text.Trim().IndexOf('.') > 0)
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
                MessageBox.Show("错误代码：3117-已付定金输入的值为非法字符，请输入数字：" + ex.Message, "采购订单温馨提示！");
            }
        }

        private void txtYiFuDingJin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtYiFuDingJin.MaxLength > 12)
                {
                    MessageBox.Show("输入的值超出了范围！");
                    txtYiFuDingJin.Text = "0.00";
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：3117-已付定金输入的值为非法字符，请输入数字：" + ex.Message, "采购订单温馨提示！");
            }
        }
        #endregion

        /// <summary>
        /// 表格按回车键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControlShangPing_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSupply.Focus();
            }
        }

        /// <summary>
        /// 快捷方式设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PurchaseOrderForm_KeyDown(object sender, KeyEventArgs e)
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
                Save();
                return;
            }
            //审核
            if (e.KeyCode == Keys.F4)
            {
                Review();
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
