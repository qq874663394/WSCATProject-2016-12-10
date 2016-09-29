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

namespace WSCATProject.Warehouse
{
    public partial class WareHouseDisassemblyForm : TestForm
    {
        public WareHouseDisassemblyForm()
        {
            InitializeComponent();
        }
        #region 数据字段
        /// <summary>
        /// 所有拆卸员
        /// </summary>
        private DataTable _AllEmployee = null;
        /// <summary>
        /// 拆卸单code
        /// </summary>
        private string _WareHouseDisassemblyCode;
        #endregion

        #region 实例化接口层 以及解密方法

        CodingHelper ch = new CodingHelper();
        EmpolyeeInterface employee = new EmpolyeeInterface();

        #endregion

        #region 初始化数据
        /// <summary>
        /// 初始化拆卸员
        /// </summary>
        private void InitEmployee()
        {
            try
            {
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

                resizablePanel1.Location = new Point(234, 440);
                dataGridViewFujia.DataSource = ch.DataTableReCoding(_AllEmployee);
                resizablePanel1.Visible = true;

                if (this.WindowState == FormWindowState.Maximized)
                {
                    resizablePanel1.Location = new Point(230, 670);
                    return;
                }
                if (this.WindowState == FormWindowState.Normal)
                {
                    resizablePanel1.Location = new Point(230, 450);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化拆卸员错误！请检查：" + ex.Message, "拆卸单温馨提示");
            }
        }

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
            gr.Cells["gridColumn1"].Value = "合计";
            gr.Cells["gridColumn1"].CellStyles.Default.Alignment =
                DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnnumber"].Value = 0;
            gr.Cells["gridColumnnumber"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnnumber"].CellStyles.Default.Background.Color1 = Color.Orange;
        }

        /// <summary>
        /// 非空验证
        /// </summary>
        private bool isNUllValidate()
        {
            if (cbotype.Text.Trim() == null)
            {
                MessageBox.Show("费用类型不能为空！");
                return false;
            }

            GridRow gr = (GridRow)superGridControl2.PrimaryGrid.Rows[0];
            if (gr.Cells[""].Value == null || gr.Cells[""].Value.ToString() == "")
            {
                MessageBox.Show("仓库1不能为空！");
                return false;
            }
            if (gr.Cells[""].Value == null || gr.Cells[""].Value.ToString() == "")
            {
                MessageBox.Show("商品代码1不能为空！");
                return false;
            }

            GridRow grs = (GridRow)superGridControl1.PrimaryGrid.Rows[0];
            if (grs.Cells[""].Value == null || grs.Cells[""].Value.ToString() == "")
            {
                MessageBox.Show("仓库2不能为空！");
                return false;
            }
            if (grs.Cells[""].Value == null || grs.Cells[""].Value.ToString() == "")
            {
                MessageBox.Show("商品代码2不能为空！");
                return false;
            }

            if (ltxtbSalsMan.Text.Trim() == null || ltxtbSalsMan.Text == "")
            {
                MessageBox.Show("拆卸员不能为空！");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 标识那个控件不可用
        /// </summary>
        private void InitForm()
        {
            this.cbotype.Enabled = false;
            cbotype.BackColor = Color.FromArgb(240, 240, 240);
            this.labtextboxTop6.ReadOnly = true;
            labtextboxTop6.BackColor = Color.FromArgb(240, 240, 240);
            this.labtextboxTop9.ReadOnly = true;
            labtextboxTop9.BackColor = Color.FromArgb(240, 240, 240);
            this.textBoxOddNumbers.ReadOnly = true;
            this.superGridControl1.PrimaryGrid.ReadOnly = true;
            this.superGridControl1.BackColor = Color.FromArgb(240, 240, 240);
            this.superGridControl2.PrimaryGrid.ReadOnly = true;
            this.superGridControl2.BackColor = Color.FromArgb(240, 240, 240);
            this.toolStripButtonsave.Enabled = false;
            this.panel2.BackColor = Color.FromArgb(240, 240, 240);
            this.panel5.BackColor = Color.FromArgb(240, 240, 240);
            this.ltxtbSalsMan.ReadOnly = true;
            this.ltxtbMakeMan.ReadOnly = true;
            this.ltxtbShengHeMan.ReadOnly = true;
            this.resizablePanel1.Visible = false;
            this.dateTimePicker1.Enabled = false;
        }

        #endregion

        private void WareHouseDisassemblyForm_Load(object sender, EventArgs e)
        {
            try
            {
                //业务员
                _AllEmployee = employee.SelSupplierTable(false);

                //禁用自动创建列
                dataGridView1.AutoGenerateColumns = false;
                dataGridViewFujia.AutoGenerateColumns = false;
                this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
                this.superGridControl2.PrimaryGrid.AutoGenerateColumns = false;
                superGridControl1.HScrollBarVisible = true;
                // 内容居中显示
                dataGridViewFujia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //显示行号
                superGridControl1.PrimaryGrid.ShowRowGridIndex = true;

                //绑定事件 双击事填充内容并隐藏列表
                dataGridViewFujia.CellDoubleClick += DataGridViewFujia_CellDoubleClick;
                dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
                //调用表格初始化
                superGridControl1.PrimaryGrid.EnsureVisible();
                InitDataGridView();
                toolStripButtonsave.Click += ToolStripButtonsave_Click;//保存
                toolStripButtonshen.Click += ToolStripButtonshen_Click;//审核

                //生成code 和显示条形码
                _WareHouseDisassemblyCode = BuildCode.ModuleCode("WHD");
                textBoxOddNumbers.Text = _WareHouseDisassemblyCode;
                barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
                _Code.ValueFont = new Font("微软雅黑", 20);
                System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
                pictureBox9.Image = imgTemp;
            }
            catch (Exception ex)
            {
                MessageBox.Show("窗体初始化数据错误！请检查：" + ex.Message, "拆卸单温馨提示");
            }
        }

        /// <summary>
        /// 审核按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonshen_Click(object sender, EventArgs e)
        {
            //非空验证
            if (isNUllValidate() == false)
            {
                return;
            }
            //获得界面上的数据,准备传给base层新增数据
            //WarehouseAdjustPriceInterface warehouseAdjpriceinterface = new WarehouseAdjustPriceInterface();
            //拆卸单
            WarehouseDisassembly warehousedisassembly = new WarehouseDisassembly();
            //拆卸单商品列表
            List<WarehouseDisassemblyDetail> warehousedisassemblydetailList = new List<WarehouseDisassemblyDetail>();
            try
            {
                warehousedisassembly.code = textBoxOddNumbers.Text == "" ? "" : XYEEncoding.strCodeHex(textBoxOddNumbers.Text);//单据code
                warehousedisassembly.date = dateTimePicker1.Value;//单据日期
                warehousedisassembly.type = cbotype.Text == "" ? "" : XYEEncoding.strCodeHex(cbotype.Text);//费用类型
                warehousedisassembly.disAssemblyCost = Convert.ToDecimal(labtextboxTop6.Text == "" ? "" : labtextboxTop1.Text);//拆卸费用
                warehousedisassembly.Abstract = labtextboxTop2.Text == "" ? "" : XYEEncoding.strCodeHex(labtextboxTop9.Text);//摘要
                warehousedisassembly.operation = ltxtbSalsMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbSalsMan.Text);//拆卸员
                warehousedisassembly.makeMan = ltxtbMakeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbMakeMan.Text);//制单人
                warehousedisassembly.examine = ltxtbShengHeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbShengHeMan.Text);//审核人
                warehousedisassembly.checkState = 1;//审核状态
                warehousedisassembly.isClear = 1;
                warehousedisassembly.updatetime = DateTime.Now;
                warehousedisassembly.reserved1 = "";
                warehousedisassembly.reserved2 = "";
                GridRow gr = (GridRow)superGridControl2.PrimaryGrid.Rows[0];
                warehousedisassembly.materialCode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品code
                warehousedisassembly.materialDaima = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品代码
                warehousedisassembly.barCode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//条形码
                warehousedisassembly.materialName = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品名称
                warehousedisassembly.materialMode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//规格型号
                warehousedisassembly.materialUnit = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//单位
                warehousedisassembly.stockCode = "";//仓库code
                warehousedisassembly.stockName = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//仓库名称
                warehousedisassembly.number = Convert.ToDecimal(gr.Cells[""].Value == null ? 0 : gr.Cells[""].Value);//数量
                warehousedisassembly.materialremark = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//备注

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:；尝试创建拆卸单商品数据出错,请检查输入" + ex.Message, "拆卸单温馨提示");
                return;
            }

            try
            {
                //获得商品列表数据,准备传给base层新增数据
                GridRow g = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
                GridItemsCollection grs = superGridControl1.PrimaryGrid.Rows;
                int i = 0;
                DateTime nowDataTime = DateTime.Now;
                foreach (GridRow gr in grs)
                {
                    if (gr["gridColumnname"].Value != null)
                    {
                        i++;
                        WarehouseDisassemblyDetail warehosuedisassemblydetail = new WarehouseDisassemblyDetail();
                        warehosuedisassemblydetail.mainCode = XYEEncoding.strCodeHex(textBoxOddNumbers.Text);
                        warehosuedisassemblydetail.materialCode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品code
                        warehosuedisassemblydetail.materialDaima = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品代码
                        warehosuedisassemblydetail.barCode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//条形码
                        warehosuedisassemblydetail.materialName = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品名称
                        warehosuedisassemblydetail.materialMode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//规格型号
                        warehosuedisassemblydetail.materialUnit = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//单位
                        warehosuedisassemblydetail.stockCode = "";//仓库code
                        warehosuedisassemblydetail.stockName = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//仓库名称
                        warehosuedisassemblydetail.number = Convert.ToDecimal(gr.Cells[""].Value == null ? 0 : gr.Cells[""].Value);//数量
                        warehosuedisassemblydetail.price = Convert.ToDecimal(gr.Cells[""].Value == null ? 0 : gr.Cells[""].Value);//单价
                        warehosuedisassemblydetail.money = Convert.ToDecimal(gr.Cells[""].Value == null ? 0 : gr.Cells[""].Value);//金额
                        warehosuedisassemblydetail.remark = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//备注
                        warehosuedisassemblydetail.isClear = 1;
                        warehosuedisassemblydetail.updatetime = DateTime.Now;
                        warehosuedisassemblydetail.reserved1 = "";
                        warehosuedisassemblydetail.reserved2 = "";
                        GridRow dr = superGridControl1.PrimaryGrid.Rows[0] as GridRow;
                        warehousedisassemblydetailList.Add(warehosuedisassemblydetail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2105-尝试创建拆卸单详情商品数据出错,请检查输入" + ex.Message, "拆卸单温馨提示");
                return;
            }

            //增加一条拆卸单和拆卸详细数据
            //object warehouseAdjResult = warehouseAdjpriceinterface.AddAndModify(warehouseADJprice, warehouseADJpriceList);
            ////this.textBoxid.Text = warehouseProfitResult.ToString();
            //if (warehouseAdjResult != null)
            //{
            //    MessageBox.Show("审核拆卸单数据成功", "拆卸单温馨提示");
               // pictureBoxshenghe.Image = Properties.Resources.审核;
               // InitForm();
            //}
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonsave_Click(object sender, EventArgs e)
        {
            //非空验证
            if (isNUllValidate() == false)
            {
                return;
            }
            //获得界面上的数据,准备传给base层新增数据
            //WarehouseAdjustPriceInterface warehouseAdjpriceinterface = new WarehouseAdjustPriceInterface();
            //拆卸单
            WarehouseDisassembly warehousedisassembly = new WarehouseDisassembly();
            //拆卸单商品列表
            List<WarehouseDisassemblyDetail> warehousedisassemblydetailList = new List<WarehouseDisassemblyDetail>();
            try
            {
                warehousedisassembly.code = textBoxOddNumbers.Text == "" ? "" : XYEEncoding.strCodeHex(textBoxOddNumbers.Text);//单据code
                warehousedisassembly.date = dateTimePicker1.Value;//单据日期
                warehousedisassembly.type = cbotype.Text == "" ? "" : XYEEncoding.strCodeHex(cbotype.Text);//费用类型
                warehousedisassembly.disAssemblyCost = Convert.ToDecimal(labtextboxTop6.Text == "" ? "" : labtextboxTop1.Text);//拆卸费用
                warehousedisassembly.Abstract = labtextboxTop2.Text == "" ? "" : XYEEncoding.strCodeHex(labtextboxTop9.Text);//摘要
                warehousedisassembly.operation = ltxtbSalsMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbSalsMan.Text);//拆卸员
                warehousedisassembly.makeMan = ltxtbMakeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbMakeMan.Text);//制单人
                warehousedisassembly.examine = ltxtbShengHeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbShengHeMan.Text);//审核人
                warehousedisassembly.checkState = 0;//审核状态
                warehousedisassembly.isClear = 1;
                warehousedisassembly.updatetime = DateTime.Now;
                warehousedisassembly.reserved1 = "";
                warehousedisassembly.reserved2 = "";
                GridRow gr = (GridRow)superGridControl2.PrimaryGrid.Rows[0];
                warehousedisassembly.materialCode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品code
                warehousedisassembly.materialDaima = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品代码
                warehousedisassembly.barCode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//条形码
                warehousedisassembly.materialName = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品名称
                warehousedisassembly.materialMode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//规格型号
                warehousedisassembly.materialUnit = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//单位
                warehousedisassembly.stockCode = "";//仓库code
                warehousedisassembly.stockName = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//仓库名称
                warehousedisassembly.number = Convert.ToDecimal(gr.Cells[""].Value == null ? 0 : gr.Cells[""].Value);//数量
                warehousedisassembly.materialremark = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//备注

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:；尝试创建拆卸单商品数据出错,请检查输入" + ex.Message, "拆卸单温馨提示");
                return;
            }

            try
            {
                //获得商品列表数据,准备传给base层新增数据
                GridRow g = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
                GridItemsCollection grs = superGridControl1.PrimaryGrid.Rows;
                int i = 0;
                DateTime nowDataTime = DateTime.Now;
                foreach (GridRow gr in grs)
                {
                    if (gr["gridColumnname"].Value != null)
                    {
                        i++;
                        WarehouseDisassemblyDetail warehosuedisassemblydetail = new WarehouseDisassemblyDetail();
                        warehosuedisassemblydetail.mainCode = XYEEncoding.strCodeHex(textBoxOddNumbers.Text);
                        warehosuedisassemblydetail.materialCode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品code
                        warehosuedisassemblydetail.materialDaima = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品代码
                        warehosuedisassemblydetail.barCode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//条形码
                        warehosuedisassemblydetail.materialName = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品名称
                        warehosuedisassemblydetail.materialMode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//规格型号
                        warehosuedisassemblydetail.materialUnit = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//单位
                        warehosuedisassemblydetail.stockCode = "";//仓库code
                        warehosuedisassemblydetail.stockName = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//仓库名称
                        warehosuedisassemblydetail.number = Convert.ToDecimal(gr.Cells[""].Value == null ? 0 : gr.Cells[""].Value);//数量
                        warehosuedisassemblydetail.price = Convert.ToDecimal(gr.Cells[""].Value == null ? 0 : gr.Cells[""].Value);//单价
                        warehosuedisassemblydetail.money = Convert.ToDecimal(gr.Cells[""].Value == null ? 0 : gr.Cells[""].Value);//金额
                        warehosuedisassemblydetail.remark = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//备注
                        warehosuedisassemblydetail.isClear = 1;
                        warehosuedisassemblydetail.updatetime = DateTime.Now;
                        warehosuedisassemblydetail.reserved1 = "";
                        warehosuedisassemblydetail.reserved2 = "";
                        GridRow dr = superGridControl1.PrimaryGrid.Rows[0] as GridRow;
                        warehousedisassemblydetailList.Add(warehosuedisassemblydetail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2105-尝试创建拆卸单详情商品数据出错,请检查输入" + ex.Message, "拆卸单温馨提示");
                return;
            }

            //增加一条拆卸单和拆卸详细数据
            //object warehouseAdjResult = warehouseAdjpriceinterface.AddAndModify(warehouseADJprice, warehouseADJpriceList);
            ////this.textBoxid.Text = warehouseProfitResult.ToString();
            //if (warehouseAdjResult != null)
            //{
            //    MessageBox.Show("新增拆卸单数据成功", "拆卸单温馨提示");
            //}
        }

        #region 两个副框的双击事件和下拉箭头的点击事件

        private void DataGridViewFujia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
            ltxtbSalsMan.Text = name;
            resizablePanel1.Visible = false;
        }
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            InitEmployee();
        }

        #endregion

        private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
        {
            //if (this.comboBoxEx1.Text.Trim() == "")
            //{
            //    resizablePanelData.Visible = false;
            //    MessageBox.Show("请先选择供应商，显示采购单号!");
            //    return;
            //}
            //if (e.GridCell.GridColumn.Name == "material")
            //{
            //    SelectedElementCollection ge = superGridControl1.PrimaryGrid.GetSelectedCells();
            //    GridCell gc = ge[0] as GridCell;
            //    if (gc.GridRow.Cells[material].Value != null && (gc.GridRow.Cells[material].Value).ToString() != "")
            //    {
            //        //模糊查询商品列表
            //        _AllMaterial = pdi.GetList("" + XYEEncoding.strCodeHex(this.comboBoxEx1.Text.Trim() + ""), "" + XYEEncoding.strCodeHex(gc.GridRow.Cells[material].Value.ToString()) + "");
            //        InitMaterialDataGridView();
            //    }
            //    else
            //    {
            //        //绑定商品列表
            //        _AllMaterial = pdi.GetList("" + XYEEncoding.strCodeHex(this.comboBoxEx1.Text.Trim() + ""), "");
            //        InitMaterialDataGridView();
            //    }
            //    dataGridView1.DataSource = ch.DataTableReCoding(_AllMaterial);
            //}
        }

        private void superGridControl2_BeginEdit(object sender, GridEditEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "material")
            {
                ClickRowIndex = e.GridCell.RowIndex;
                resizablePanelData.Visible = true;
                resizablePanelData.Location = new Point(e.GridCell.UnMergedBounds.X,
                    e.GridCell.UnMergedBounds.Bottom + panel3.Location.Y);
            }
        }

        /// <summary>
        /// 验证只能输入数字和小数点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labtextboxTop6_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)//小数点
            {
                if (labtextboxTop6.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(labtextboxTop6.Text, out oldf);
                    b2 = float.TryParse(labtextboxTop6.Text + e.KeyChar.ToString(), out f);
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

        /// <summary>
        /// 按下ESC关闭子窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WareHouseDisassemblyForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.resizablePanel1.Visible = false;
                this.resizablePanelData.Visible = false;
            }
        }

        #region 修改Panel的边框颜色
        /// <summary>
        /// 修改Panel的边框颜色
        /// </summary>
        /// <param name = "sender" ></ param >
        /// < param name="e"></param>
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

        /// <summary>
        /// 拆卸员模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labtextboxBotton1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ltxtbSalsMan.Text.Trim() == "")
                {
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

                resizablePanel1.Location = new Point(234, 440);
                dataGridViewFujia.DataSource = ch.DataTableReCoding(employee.GetList(0, "" + XYEEncoding.strCodeHex(ltxtbSalsMan.Text.Trim()) + ""));
                resizablePanel1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：模糊查询拆卸员数据错误" + ex.Message, "拆卸单温馨提示");
            }


        }
    }
}
