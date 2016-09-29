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
        /// 所有业务员
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
        /// 初始化业务员
        /// </summary>
        private void InitEmployee()
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
        #endregion

        private void WareHouseDisassemblyForm_Load(object sender, EventArgs e)
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
            ///pictureBox10.Parent = pictureBoxtitle;
        }

        /// <summary>
        /// 审核按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonshen_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 保存的按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonsave_Click(object sender, EventArgs e)
        {
            ////非空验证
            //isNUllValidate();
            ////获得界面上的数据,准备传给base层新增数据
            //WarehouseAllotInterface warehouseallterface = new WarehouseAllotInterface();
            ////调拨单
            //WarehouseAllot warehouseallot = new WarehouseAllot();
            ////调拨商品列表
            //List<WarehouseAllotDetail> wareHouseallList = new List<WarehouseAllotDetail>();
            try
            {
                //warehouseallot.allotGap = labtextboxTop7.Text == "" ? 0.0M : Convert.ToDecimal(labtextboxTop7.Text);
                //warehouseallot.allotType = XYEEncoding.strCodeHex(cbotype.Text);
                //warehouseallot.cause = labtextboxTop6.Text == "" ? "" : XYEEncoding.strCodeHex(labtextboxTop6.Text);
                //warehouseallot.checkMan = ltxtbShengHeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbShengHeMan.Text);
                //warehouseallot.checkState = 1;
                //warehouseallot.code = XYEEncoding.strCodeHex(textBoxOddNumbers.Text);
                //warehouseallot.date = dateTimePicker1.Value;
                //warehouseallot.isClear = 1;
                //warehouseallot.makeMan = ltxtbSalsMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbSalsMan.Text);
                //warehouseallot.operationMan = ltxtbMakeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbMakeMan.Text);
                //warehouseallot.remark = "";
                //warehouseallot.reserved1 = "";
                //warehouseallot.reserved2 = "";
                //warehouseallot.updateDate = DateTime.Now;

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:2104;尝试创建调拨单商品数据出错,请检查输入" + ex.Message, "调拨单温馨提示");
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
                        //WarehouseAllotDetail warehousealld = new WarehouseAllotDetail();
                        //warehousealld.allotInPrice = gr["gridColumnpricein"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnpricein"].Value.ToString());
                        //warehousealld.allotInSummoney = gr["gridColumnmoneyin"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnmoneyin"].Value.ToString());
                        //warehousealld.allotOutPrice = gr["gridColumnpriceout"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnpriceout"].Value.ToString());
                        //warehousealld.allotOutSummoney = gr["gridColumnmoneyout"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnmoneyout"].Value.ToString());
                        //warehousealld.barcode = gr["gridColumntiaoxingma"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumntiaoxingma"].Value.ToString());
                        //warehousealld.code = XYEEncoding.strCodeHex(textBoxOddNumbers.Text) + i.ToString();
                        //warehousealld.curNumber = gr["gridColumnnumber"].Value == null ? 0.0M : Convert.ToDecimal(gr["gridColumnnumber"].Value.ToString());
                        //warehousealld.effectiveDate = gr["gridColumnyouxiao"].Value == DBNull.Value ? Convert.ToDateTime("1990-01-01") : Convert.ToDateTime(gr["gridColumnyouxiao"].Value);
                        //warehousealld.isClear = 1;
                        //warehousealld.mainCode = XYEEncoding.strCodeHex(textBoxOddNumbers.Text);
                        //warehousealld.materialCode = gr["gridColumnMaterialcode"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnMaterialcode"].Value.ToString());
                        //warehousealld.materialDaima = gr["material"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["material"].Value.ToString());
                        //warehousealld.materiaModel = gr["gridColumnmodel"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnmodel"].Value.ToString());
                        //warehousealld.materiaName = gr["gridColumnname"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnname"].Value.ToString());
                        //warehousealld.materiaUnit = gr["gridColumnunit"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnunit"].Value.ToString());
                        //warehousealld.productionDate = gr["gridColumnshengchandate"].Value == DBNull.Value ? Convert.ToDateTime("1990-01-01") : Convert.ToDateTime(gr["gridColumnshengchandate"].Value);
                        //warehousealld.qualityDate = gr["gridColumnbaozhi"].Value.ToString() == "" ? 0.0M : Convert.ToDecimal(gr["gridColumnbaozhi"].Value.ToString());
                        //warehousealld.remark = gr["gridColumnremark"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnremark"].Value.ToString());
                        //warehousealld.reserved1 = "";
                        //warehousealld.reserved2 = "";
                        //warehousealld.stoIncode = _InStorage == "" ? "" : XYEEncoding.strCodeHex(_InStorage);
                        //warehousealld.stoInName = gr["gridColumnStockIn"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnStockIn"].Value.ToString());
                        //warehousealld.stoOutcode = _OutStorage == "" ? "" : XYEEncoding.strCodeHex(_OutStorage);
                        //warehousealld.stoOutName = gr["gridColumnStock"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["gridColumnStock"].Value.ToString());
                        //warehousealld.updateDate = DateTime.Now;
                        //GridRow dr = superGridControl1.PrimaryGrid.Rows[0] as GridRow;
                        //wareHouseallList.Add(warehousealld);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2105-尝试创建调拨单详商品数据出错,请检查输入" + ex.Message, "调拨单温馨提示");
                return;
            }
            //增加一条入库单和入库单详细数据
            //object warehouseInResult = warehouseallterface.AddAndModify(warehouseallot, wareHouseallList);
            //// this.textBoxid.Text = warehouseInResult.ToString();
            //if (warehouseInResult != null)
            //{
            //    MessageBox.Show("新增调拨数据成功", "调拨单温馨提示");
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
        private  void panel2_Paint(object sender, PaintEventArgs e)
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
        /// 出库员模糊查询
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
                MessageBox.Show("错误代码：模糊查询出库员数据错误" + ex.Message, "拆卸单温馨提示");
            }


        }
    }
}
