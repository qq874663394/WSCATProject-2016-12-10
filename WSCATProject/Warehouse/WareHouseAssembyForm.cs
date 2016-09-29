using DevComponents.DotNetBar.Controls;
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
    public partial class WareHouseAssembyForm : TestForm
    {
        public WareHouseAssembyForm()
        {
            InitializeComponent();
        }

        #region 数据字段
        /// <summary>
        /// 所有组装员
        /// </summary>
        private DataTable _AllEmployee = null;
        /// <summary>
        /// 组装单单号
        /// </summary>
        private string _WareHouseAssembyCode;
        #endregion

        #region 修改Panel的边框颜色
        /// <summary>
        /// 修改Panel的边框颜色
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

        #region 实例化接口层 以及解密方法

        CodingHelper ch = new CodingHelper();
        EmpolyeeInterface employee = new EmpolyeeInterface();

        #endregion

        #region 初始化数据
        /// <summary>
        /// 初始化组装员
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
                MessageBox.Show("初始化组装员错误！请检查：" + ex.Message,"组装单温馨提示");
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
            gr.Cells["sup1material"].Value = "合计";
            gr.Cells["sup1material"].CellStyles.Default.Alignment =
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
                MessageBox.Show("组装员不能为空！");
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
            this.labtextboxTop1.ReadOnly = true;
            labtextboxTop1.BackColor = Color.FromArgb(240, 240, 240);
            this.labtextboxTop2.ReadOnly = true;
            labtextboxTop2.BackColor = Color.FromArgb(240, 240, 240);
            this.textBoxOddNumbers.ReadOnly = true;    
            this.superGridControl1.PrimaryGrid.ReadOnly = true;
            this.superGridControl1.BackColor = Color.FromArgb(240, 240, 240);
            this.superGridControl2.PrimaryGrid.ReadOnly = true;
            this.superGridControl2.BackColor = Color.FromArgb(240, 240, 240);
            this.toolStripButtonsave.Enabled = false;
            this.panel2.BackColor = Color.FromArgb(240, 240, 240);
            this.panel5.BackColor = Color.FromArgb(240, 240, 240);
            this.ltxtbSalsMan.ReadOnly = true;
            this.ltxtbSalsMan.BackColor = Color.FromArgb(240, 240, 240);
            this.ltxtbMakeMan.ReadOnly = true;
            this.ltxtbMakeMan.BackColor = Color.FromArgb(240, 240, 240);
            this.ltxtbShengHeMan.ReadOnly = true;
            this.ltxtbShengHeMan.BackColor = Color.FromArgb(240, 240, 240);
            this.resizablePanel1.Visible = false;
            this.dateTimePicker1.Enabled = false;
            pictureBoxshenghe.Parent = pictureBoxtitle;
            pictureBoxshenghe.Image = Properties.Resources.审核;
            pictureBoxshenghe.Visible = true;

        }
        #endregion

        private void WareHouseAssembyForm_Load(object sender, EventArgs e)
        {
            try
            {
                //业务员
                _AllEmployee = employee.SelSupplierTable(false);

                //禁用自动创建列
                dataGridView1.AutoGenerateColumns = false;
                dataGridViewFujia.AutoGenerateColumns = false;
                superGridControl1.HScrollBarVisible = true;

                //绑定事件 双击事填充内容并隐藏列表
                dataGridViewFujia.CellDoubleClick += DataGridViewFujia_CellDoubleClick;
                dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;

                InitDataGridView();
                //生成code 和显示条形码
                _WareHouseAssembyCode = BuildCode.ModuleCode("WAP");
                textBoxOddNumbers.Text = _WareHouseAssembyCode;
                barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
                _Code.ValueFont = new Font("微软雅黑", 20);
                System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
                pictureBox9.Image = imgTemp;

                toolStripButtonsave.Click += ToolStripButtonsave_Click;//保存按钮
                toolStripButtonshen.Click += ToolStripButtonshen_Click;//审核按钮

            }
            catch (Exception ex)
            {
                MessageBox.Show("窗体初始化数据错误！请检查："+ex.Message,"组装单温馨提示");
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
            //组装单
            WarehouseAssembly warehouseassembly = new WarehouseAssembly();
            //组装单商品列表
            List<WarehouseAssemblyDetail> warehouseassemblydetailList = new List<WarehouseAssemblyDetail>();
            try
            {
                warehouseassembly.code = textBoxOddNumbers.Text == "" ? "" : XYEEncoding.strCodeHex(textBoxOddNumbers.Text);//单据code
                warehouseassembly.date = dateTimePicker1.Value;//单据日期
                warehouseassembly.type = cbotype.Text == "" ? "" : XYEEncoding.strCodeHex(cbotype.Text);//费用类型
                warehouseassembly.assemblyCost = Convert.ToDecimal(labtextboxTop1.Text == "" ? "" : labtextboxTop1.Text);//组装费用
                warehouseassembly.Abstract = labtextboxTop2.Text == "" ? "" : XYEEncoding.strCodeHex(labtextboxTop2.Text);//摘要
                warehouseassembly.operation = ltxtbSalsMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbSalsMan.Text);//组装员
                warehouseassembly.makeMan = ltxtbMakeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbMakeMan.Text);//制单人
                warehouseassembly.examine = ltxtbShengHeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbShengHeMan.Text);//审核人
                warehouseassembly.checkState = 1;//审核状态
                warehouseassembly.isClear = 1;
                warehouseassembly.updatetime = DateTime.Now;
                warehouseassembly.reserved1 = "";
                warehouseassembly.reserved2 = "";
                GridRow gr = (GridRow)superGridControl2.PrimaryGrid.Rows[0];
                warehouseassembly.materialCode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品code
                warehouseassembly.materialDaima = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品代码
                warehouseassembly.barCode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//条形码
                warehouseassembly.materialName = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品名称
                warehouseassembly.materialMode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//规格型号
                warehouseassembly.materialUnit = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//单位
                warehouseassembly.stockCode = "";//仓库code
                warehouseassembly.stockName = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//仓库名称
                warehouseassembly.number = Convert.ToDecimal(gr.Cells[""].Value == null ? 0 : gr.Cells[""].Value);//数量
                warehouseassembly.materialremark = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//备注

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:；尝试创建组装单商品数据出错,请检查输入" + ex.Message, "组装单温馨提示");
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
                        WarehouseAssemblyDetail warehouseassemblydetail = new WarehouseAssemblyDetail();
                        warehouseassemblydetail.mainCode = XYEEncoding.strCodeHex(textBoxOddNumbers.Text);
                        warehouseassemblydetail.materialCode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品code
                        warehouseassemblydetail.materialDaima = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品代码
                        warehouseassemblydetail.barCode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//条形码
                        warehouseassemblydetail.materialName = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品名称
                        warehouseassemblydetail.materialMode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//规格型号
                        warehouseassemblydetail.materialUnit = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//单位
                        warehouseassemblydetail.stockCode = "";//仓库code
                        warehouseassemblydetail.stockName = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//仓库名称
                        warehouseassemblydetail.number = Convert.ToDecimal(gr.Cells[""].Value == null ? 0 : gr.Cells[""].Value);//数量
                        warehouseassemblydetail.price = Convert.ToDecimal(gr.Cells[""].Value == null ? 0 : gr.Cells[""].Value);//单价
                        warehouseassemblydetail.money = Convert.ToDecimal(gr.Cells[""].Value == null ? 0 : gr.Cells[""].Value);//金额
                        warehouseassemblydetail.remark = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//备注
                        warehouseassemblydetail.isClear = 1;
                        warehouseassemblydetail.updatetime = DateTime.Now;
                        warehouseassemblydetail.reserved1 = "";
                        warehouseassemblydetail.reserved2 = "";
                        GridRow dr = superGridControl1.PrimaryGrid.Rows[0] as GridRow;
                        warehouseassemblydetailList.Add(warehouseassemblydetail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2105-尝试创建组装单详情商品数据出错,请检查输入" + ex.Message, "组装单温馨提示");
                return;
            }

            //增加一条调价单和调价详细数据
            //object warehouseAdjResult = warehouseAdjpriceinterface.AddAndModify(warehouseADJprice, warehouseADJpriceList);
            ////this.textBoxid.Text = warehouseProfitResult.ToString();
            //if (warehouseAdjResult != null)
            //{
            //    MessageBox.Show("新增调价单数据成功", "调价单温馨提示");
                //pictureBoxshenghe.Image = Properties.Resources.审核;
                //InitForm();
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
            //组装单
            WarehouseAssembly warehouseassembly = new WarehouseAssembly();
            //组装单商品列表
            List<WarehouseAssemblyDetail> warehouseassemblydetailList = new List<WarehouseAssemblyDetail>();
            try
            {
                warehouseassembly.code = textBoxOddNumbers.Text == "" ? "" : XYEEncoding.strCodeHex(textBoxOddNumbers.Text);//单据code
                warehouseassembly.date = dateTimePicker1.Value;//单据日期
                warehouseassembly.type = cbotype.Text == "" ? "" : XYEEncoding.strCodeHex(cbotype.Text);//费用类型
                warehouseassembly.assemblyCost = Convert.ToDecimal(labtextboxTop1.Text == "" ? "" : labtextboxTop1.Text);//组装费用
                warehouseassembly.Abstract = labtextboxTop2.Text == "" ? "" : XYEEncoding.strCodeHex(labtextboxTop2.Text);//摘要
                warehouseassembly.operation = ltxtbSalsMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbSalsMan.Text);//组装员
                warehouseassembly.makeMan = ltxtbMakeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbMakeMan.Text);//制单人
                warehouseassembly.examine = ltxtbShengHeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbShengHeMan.Text);//审核人
                warehouseassembly.checkState = 0;//审核状态
                warehouseassembly.isClear = 1;
                warehouseassembly.updatetime = DateTime.Now;
                warehouseassembly.reserved1 = "";
                warehouseassembly.reserved2 = "";
                GridRow gr = (GridRow)superGridControl2.PrimaryGrid.Rows[0];
                warehouseassembly.materialCode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品code
                warehouseassembly.materialDaima = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品代码
                warehouseassembly.barCode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//条形码
                warehouseassembly.materialName = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品名称
                warehouseassembly.materialMode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//规格型号
                warehouseassembly.materialUnit = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//单位
                warehouseassembly.stockCode = "";//仓库code
                warehouseassembly.stockName = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//仓库名称
                warehouseassembly.number = Convert.ToDecimal(gr.Cells[""].Value == null ? 0 : gr.Cells[""].Value);//数量
                warehouseassembly.materialremark = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//备注

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:；尝试创建组装单商品数据出错,请检查输入" + ex.Message, "组装单温馨提示");
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
                        WarehouseAssemblyDetail warehouseassemblydetail = new WarehouseAssemblyDetail();
                        warehouseassemblydetail.mainCode = XYEEncoding.strCodeHex(textBoxOddNumbers.Text);
                        warehouseassemblydetail.materialCode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品code
                        warehouseassemblydetail.materialDaima = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品代码
                        warehouseassemblydetail.barCode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//条形码
                        warehouseassemblydetail.materialName = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//商品名称
                        warehouseassemblydetail.materialMode = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//规格型号
                        warehouseassemblydetail.materialUnit = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//单位
                        warehouseassemblydetail.stockCode = "";//仓库code
                        warehouseassemblydetail.stockName = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//仓库名称
                        warehouseassemblydetail.number = Convert.ToDecimal(gr.Cells[""].Value == null ? 0 : gr.Cells[""].Value);//数量
                        warehouseassemblydetail.price = Convert.ToDecimal(gr.Cells[""].Value == null ? 0 : gr.Cells[""].Value);//单价
                        warehouseassemblydetail.money = Convert.ToDecimal(gr.Cells[""].Value == null ? 0 : gr.Cells[""].Value);//金额
                        warehouseassemblydetail.remark = gr.Cells[""].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells[""].Value.ToString());//备注
                        warehouseassemblydetail.isClear = 1;
                        warehouseassemblydetail.updatetime = DateTime.Now;
                        warehouseassemblydetail.reserved1 = "";
                        warehouseassemblydetail.reserved2 = "";
                        GridRow dr = superGridControl1.PrimaryGrid.Rows[0] as GridRow;
                        warehouseassemblydetailList.Add(warehouseassemblydetail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2105-尝试创建组装单详情商品数据出错,请检查输入" + ex.Message, "组装单温馨提示");
                return;
            }

            //增加一条调价单和调价详细数据
            //object warehouseAdjResult = warehouseAdjpriceinterface.AddAndModify(warehouseADJprice, warehouseADJpriceList);
            ////this.textBoxid.Text = warehouseProfitResult.ToString();
            //if (warehouseAdjResult != null)
            //{
            //    MessageBox.Show("新增调价单数据成功", "调价单温馨提示");
            //}
        }

        #region 两个副框的双击事件和下拉箭头的点击事件

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridViewFujia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
            ltxtbSalsMan.Text = name;
            resizablePanel1.Visible = false;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            InitEmployee();
        }

        #endregion

        /// <summary>
        /// 验证只能输入数字和小数点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labtextboxTop1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)//小数点
            {
                if (labtextboxTop1.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(labtextboxTop1.Text, out oldf);
                    b2 = float.TryParse(labtextboxTop1.Text + e.KeyChar.ToString(), out f);
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

        /// <summary>
        /// 组装员模糊查询
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
                MessageBox.Show("错误代码：模糊查询组装员数据错误" + ex.Message, "组装单温馨提示");
            }


        }
    }
}
