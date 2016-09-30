using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.SuperGrid;
using HelperUtility;
using HelperUtility.Encrypt;
using InterfaceLayer.Base;
using InterfaceLayer.Warehouse;
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
        /// <summary>
        /// 点击的项,1业务员
        /// </summary>
        private int _Click = 0;
        /// <summary>
        /// 所有仓库
        /// </summary>
        private DataTable _AllStorage = null;
        /// <summary>
        /// 保存仓库的Code
        /// </summary>
        private string _StorageCode = "";
        /// <summary>
        /// 仓库列表
        /// </summary>
        private KeyValuePair<string, string> _ClickStorageList;
        /// <summary>
        /// 所有商品列表
        /// </summary>
        private DataTable _AllMaterial = null;
        /// <summary>
        /// 保存仓库
        /// </summary>
        private int _WareHouse;
        /// <summary>
        ///保存商品
        /// </summary>
        private int _Material;
        /// <summary>
        /// 统计数量
        /// </summary>
        private decimal _MaterialNumber;
        private decimal _number;
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
        StorageInterface storage = new StorageInterface();
        WarehouseMainInterface waremain = new WarehouseMainInterface();
        

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
                    resizablePanel1.Location = new Point(230, 500);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化组装员错误！请检查：" + ex.Message, "组装单温馨提示");
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
            gr.Cells["number"].Value = 0;
            gr.Cells["number"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["number"].CellStyles.Default.Background.Color1 = Color.Orange;
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
            if (gr.Cells["gridColumnStock"].Value == null || gr.Cells["gridColumnStock"].Value.ToString() == "")
            {
                MessageBox.Show("仓库1不能为空！");
                return false;
            }
            if (gr.Cells["material"].Value == null || gr.Cells["material"].Value.ToString() == "")
            {
                MessageBox.Show("商品代码1不能为空！");
                return false;
            }

            GridRow grs = (GridRow)superGridControl1.PrimaryGrid.Rows[0];
            if (grs.Cells["gridColumnStockIn"].Value == null || grs.Cells["gridColumnStockIn"].Value.ToString() == "")
            {
                MessageBox.Show("仓库2不能为空！");
                return false;
            }
            if (grs.Cells["sup1material"].Value == null || grs.Cells["sup1material"].Value.ToString() == "")
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

        /// <summary>
        /// 初始化仓库列和数据
        /// </summary>
        private void InitStorageList()
        {
            try
            {
                if (_Click != 2)
                {
                    _Click = 2;
                    dataGridViewFujia.DataSource = null;
                    dataGridViewFujia.Columns.Clear();

                    DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                    dgvc.Name = "code";
                    dgvc.Visible = false;
                    dgvc.HeaderText = "code";
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
                    dgvc.Visible = true;
                    dgvc.HeaderText = "仓库地址";
                    dgvc.DataPropertyName = "address";
                    dataGridViewFujia.Columns.Add(dgvc);

                    //查询仓库的方法
                    dataGridViewFujia.DataSource = ch.DataTableReCoding(_AllStorage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化仓库出错！请检查：" + ex.Message);
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
            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "remark";
            dgvc.Visible = false;
            dgvc.HeaderText = "备注";
            dgvc.DataPropertyName = "remark";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "price";
            dgvc.Visible = false;
            dgvc.HeaderText = "单价";
            dgvc.DataPropertyName = "price";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "currentNumber";
            dgvc.Visible = false;
            dgvc.HeaderText = "数量";
            dgvc.DataPropertyName = "currentNumber";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "unit";
            dgvc.Visible = false;
            dgvc.HeaderText = "单位";
            dgvc.DataPropertyName = "unit";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "code";
            dgvc.Visible = false;
            dgvc.HeaderText = "商品code";
            dgvc.DataPropertyName = "code";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "storageCode";
            dgvc.Visible = false;
            dgvc.HeaderText = "仓库code";
            dgvc.DataPropertyName = "storageCode";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "storageName";
            dgvc.Visible = false;
            dgvc.HeaderText = "仓库名称";
            dgvc.DataPropertyName = "storageName";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "materialDaima";
            dgvc.Visible = true;
            dgvc.HeaderText = "商品代码";
            dgvc.DataPropertyName = "materialDaima";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "name";
            dgvc.Visible = true;
            dgvc.HeaderText = "商品名称";
            dgvc.DataPropertyName = "name";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "model";
            dgvc.Visible = true;
            dgvc.HeaderText = "规格型号";
            dgvc.DataPropertyName = "model";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "barCode";
            dgvc.Visible = true;
            dgvc.HeaderText = "条码";
            dgvc.DataPropertyName = "barCode";
            dataGridView1.Columns.Add(dgvc);

            dataGridView1.DataSource = ch.DataTableReCoding(_AllMaterial);

        }

        #endregion

        private void WareHouseAssembyForm_Load(object sender, EventArgs e)
        {
            try
            {
                //业务员
                _AllEmployee = employee.SelSupplierTable(false);
                //仓库
                _AllStorage = storage.GetList(00, "");

                cbotype.SelectedIndex = 0;
                // 将dataGridView中的内容居中显示
                dataGridViewFujia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //显示行号
                superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
                //内容居中
                superGridControl1.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                superGridControl2.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;

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
                MessageBox.Show("窗体初始化数据错误！请检查：" + ex.Message, "组装单温馨提示");
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
            WarehouseAssemblyInterface whAssemblyI = new WarehouseAssemblyInterface();
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
                warehouseassembly.materialCode = gr.Cells["gridColumncode"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["gridColumncode"].Value.ToString());//商品code
                warehouseassembly.materialDaima = gr.Cells["material"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["material"].Value.ToString());//商品代码
                warehouseassembly.barCode = gr.Cells["gridColumnbarcode"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["gridColumnbarcode"].Value.ToString());//条形码
                warehouseassembly.materialName = gr.Cells["gridColumnname"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["gridColumnname"].Value.ToString());//商品名称
                warehouseassembly.materialMode = gr.Cells["gridColumnmodel"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["gridColumnmodel"].Value.ToString());//规格型号
                warehouseassembly.materialUnit = gr.Cells["gridColumnunit"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["gridColumnunit"].Value.ToString());//单位
                warehouseassembly.stockCode = gr.Cells["gridColumnstockCode"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["gridColumnstockCode"].Value.ToString()); ;//仓库code
                warehouseassembly.stockName = gr.Cells["gridColumnStock"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["gridColumnStock"].Value.ToString());//仓库名称
                warehouseassembly.number = Convert.ToDecimal(gr.Cells["gridColumnnumber"].Value == null ? 0 : gr.Cells["gridColumnnumber"].Value);//数量
                warehouseassembly.materialremark = gr.Cells["gridColumnremark"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["gridColumnremark"].Value.ToString());//备注

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
                    if (gr["name"].Value != null)
                    {
                        i++;
                        WarehouseAssemblyDetail warehouseassemblydetail = new WarehouseAssemblyDetail();
                        warehouseassemblydetail.mainCode = XYEEncoding.strCodeHex(textBoxOddNumbers.Text);
                        warehouseassemblydetail.materialCode = gr.Cells["code"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["code"].Value.ToString());//商品code
                        warehouseassemblydetail.materialDaima = gr.Cells["sup1material"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["sup1material"].Value.ToString());//商品代码
                        warehouseassemblydetail.barCode = gr.Cells["barcode"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["barcode"].Value.ToString());//条形码
                        warehouseassemblydetail.materialName = gr.Cells["name"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["name"].Value.ToString());//商品名称
                        warehouseassemblydetail.materialMode = gr.Cells["model"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["model"].Value.ToString());//规格型号
                        warehouseassemblydetail.materialUnit = gr.Cells["unit"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["unit"].Value.ToString());//单位
                        warehouseassemblydetail.stockCode = gr.Cells["stockInCode"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["stockInCode"].Value.ToString());//仓库code
                        warehouseassemblydetail.stockName = gr.Cells["gridColumnStockIn"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["gridColumnStockIn"].Value.ToString());//仓库名称
                        warehouseassemblydetail.number = Convert.ToDecimal(gr.Cells["number"].Value == null ? 0 : gr.Cells["number"].Value);//数量
                        warehouseassemblydetail.price = Convert.ToDecimal(gr.Cells["price"].Value == null ? 0 : gr.Cells["price"].Value);//单价
                        warehouseassemblydetail.money = Convert.ToDecimal(gr.Cells["money"].Value == null ? 0 : gr.Cells["money"].Value);//金额
                        warehouseassemblydetail.remark = gr.Cells["remark"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["remark"].Value.ToString());//备注
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

            //增加一条组装单和组装详细数据
            object warehouseAssemblyResult = whAssemblyI.AddAndModify(warehouseassembly, warehouseassemblydetailList);
            if (warehouseAssemblyResult != null)
            {
                MessageBox.Show("新增并审核组装单数据成功", "组装单温馨提示");
                InitForm();
            }
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
            WarehouseAssemblyInterface whAssemblyI = new WarehouseAssemblyInterface();
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
                warehouseassembly.materialCode = gr.Cells["gridColumncode"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["gridColumncode"].Value.ToString());//商品code
                warehouseassembly.materialDaima = gr.Cells["material"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["material"].Value.ToString());//商品代码
                warehouseassembly.barCode = gr.Cells["gridColumnbarcode"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["gridColumnbarcode"].Value.ToString());//条形码
                warehouseassembly.materialName = gr.Cells["gridColumnname"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["gridColumnname"].Value.ToString());//商品名称
                warehouseassembly.materialMode = gr.Cells["gridColumnmodel"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["gridColumnmodel"].Value.ToString());//规格型号
                warehouseassembly.materialUnit = gr.Cells["gridColumnunit"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["gridColumnunit"].Value.ToString());//单位
                warehouseassembly.stockCode = gr.Cells["gridColumnstockCode"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["gridColumnstockCode"].Value.ToString()); ;//仓库code
                warehouseassembly.stockName = gr.Cells["gridColumnStock"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["gridColumnStock"].Value.ToString());//仓库名称
                warehouseassembly.number = Convert.ToDecimal(gr.Cells["gridColumnnumber"].Value == null ? 0 : gr.Cells["gridColumnnumber"].Value);//数量
                warehouseassembly.materialremark = gr.Cells["gridColumnremark"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["gridColumnremark"].Value.ToString());//备注

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
                    if (gr["name"].Value != null)
                    {
                        i++;
                        WarehouseAssemblyDetail warehouseassemblydetail = new WarehouseAssemblyDetail();
                        warehouseassemblydetail.mainCode = XYEEncoding.strCodeHex(textBoxOddNumbers.Text);
                        warehouseassemblydetail.materialCode = gr.Cells["code"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["code"].Value.ToString());//商品code
                        warehouseassemblydetail.materialDaima = gr.Cells["sup1material"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["sup1material"].Value.ToString());//商品代码
                        warehouseassemblydetail.barCode = gr.Cells["barcode"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["barcode"].Value.ToString());//条形码
                        warehouseassemblydetail.materialName = gr.Cells["name"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["name"].Value.ToString());//商品名称
                        warehouseassemblydetail.materialMode = gr.Cells["model"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["model"].Value.ToString());//规格型号
                        warehouseassemblydetail.materialUnit = gr.Cells["unit"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["unit"].Value.ToString());//单位
                        warehouseassemblydetail.stockCode = gr.Cells["stockInCode"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["stockInCode"].Value.ToString());//仓库code
                        warehouseassemblydetail.stockName = gr.Cells["gridColumnStockIn"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["gridColumnStockIn"].Value.ToString());//仓库名称
                        warehouseassemblydetail.number = Convert.ToDecimal(gr.Cells["number"].Value == null ? 0 : gr.Cells["number"].Value);//数量
                        warehouseassemblydetail.price = Convert.ToDecimal(gr.Cells["price"].Value == null ? 0 : gr.Cells["price"].Value);//单价
                        warehouseassemblydetail.money = Convert.ToDecimal(gr.Cells["money"].Value == null ? 0 : gr.Cells["price"].Value);//金额
                        warehouseassemblydetail.remark = gr.Cells["remark"].Value == null ? "" : XYEEncoding.strCodeHex(gr.Cells["money"].Value.ToString());//备注
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

            //增加一条组装单和组装详细数据
            object warehouseAssemblyResult = whAssemblyI.AddAndModify(warehouseassembly, warehouseassemblydetailList);
            if (warehouseAssemblyResult != null)
            {
                MessageBox.Show("新增组装单数据成功", "组装单温馨提示");
            }
        }

        #region 两个副框的双击事件和下拉箭头的点击事件

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (_Material == 2)
                {
                    GridRow grs = (GridRow)superGridControl2.PrimaryGrid.Rows[ClickRowIndex];
                    grs.Cells["gridColumncode"].Value = dataGridView1.Rows[e.RowIndex].Cells["code"].Value;//商品code
                    grs.Cells["material"].Value = dataGridView1.Rows[e.RowIndex].Cells["materialDaima"].Value;//商品代码
                    grs.Cells["gridColumnname"].Value = dataGridView1.Rows[e.RowIndex].Cells["name"].Value;//商品名称
                    grs.Cells["gridColumnmodel"].Value = dataGridView1.Rows[e.RowIndex].Cells["model"].Value;//规格型号
                    grs.Cells["gridColumnbarcode"].Value = dataGridView1.Rows[e.RowIndex].Cells["barCode"].Value;//条码
                    grs.Cells["gridColumnunit"].Value = dataGridView1.Rows[e.RowIndex].Cells["unit"].Value;//单位
                    grs.Cells["gridColumnnumber"].Value = dataGridView1.Rows[e.RowIndex].Cells["currentNumber"].Value;//商品数量
                    grs.Cells["gridColumnremark"].Value = dataGridView1.Rows[e.RowIndex].Cells["remark"].Value;//备注  
                    resizablePanelData.Visible = false; 
                }
                if (_Material == 1)
                {
                    ////是否要新增一行的标记
                    bool newAdd = false;
                    GridRow gr = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
                    //id字段为空 说明是没有数据的行 不是修改而是新增
                    if (gr.Cells["gridColumnid"].Value == null)
                    {
                        newAdd = true;
                    }
                    gr.Cells["code"].Value = dataGridView1.Rows[e.RowIndex].Cells["code"].Value;//商品code
                    gr.Cells["sup1material"].Value = dataGridView1.Rows[e.RowIndex].Cells["materialDaima"].Value;//商品代码
                    gr.Cells["name"].Value = dataGridView1.Rows[e.RowIndex].Cells["name"].Value;//商品名称
                    gr.Cells["model"].Value = dataGridView1.Rows[e.RowIndex].Cells["model"].Value;//规格型号
                    gr.Cells["barcode"].Value = dataGridView1.Rows[e.RowIndex].Cells["barCode"].Value;//条码
                    gr.Cells["unit"].Value = dataGridView1.Rows[e.RowIndex].Cells["unit"].Value;//单位
                    gr.Cells["number"].Value = dataGridView1.Rows[e.RowIndex].Cells["currentNumber"].Value;//商品数量
                    gr.Cells["price"].Value = dataGridView1.Rows[e.RowIndex].Cells["price"].Value;//单价
                    decimal number= Convert.ToDecimal( dataGridView1.Rows[e.RowIndex].Cells["currentNumber"].Value);
                    decimal price = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["price"].Value);
                    decimal money = number * price;
                    gr.Cells["money"].Value = money;
                    gr.Cells["remark"].Value = dataGridView1.Rows[e.RowIndex].Cells["remark"].Value;//备注       

                    _number = Convert.ToDecimal( dataGridView1.Rows[e.RowIndex].Cells["currentNumber"].Value);
                    //当上一次有选择仓库时 默认本次也为上次选择仓库
                    if (!string.IsNullOrEmpty(_ClickStorageList.Value) && !string.IsNullOrEmpty(_ClickStorageList.Key))
                    {
                        gr.Cells["stockInCode"].Value = _ClickStorageList.Key;
                        gr.Cells["gridColumnStockIn"].Value = _ClickStorageList.Value;
                    }
                    //新增一行 
                    if (newAdd)
                    {
                        superGridControl1.PrimaryGrid.NewRow(superGridControl1.PrimaryGrid.Rows.Count);
                        //递增数量 默认为1 
                        gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                        _MaterialNumber += _number;
                        gr.Cells["number"].Value = _MaterialNumber;
                    }
                    superGridControl1.Focus();
                    SendKeys.Send("^{End}{Home}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("双击绑定物料信息数据错误！请检查：" + ex.Message);
            }
        }

        private void DataGridViewFujia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //调价员
                if (_Click == 1 || _Click == 3)
                {
                    string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                    ltxtbSalsMan.Text = name;
                    resizablePanel1.Visible = false;
                }
                //仓库信息
                if (_Click == 2)
                {
                    if (_WareHouse == 2)
                    {
                    GridRow gr = (GridRow)superGridControl2.PrimaryGrid.Rows[ClickRowIndex];
                    string code = dataGridViewFujia.Rows[e.RowIndex].Cells["code"].Value.ToString();
                    string Name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                    gr.Cells["gridColumnstockCode"].Value = code;
                    gr.Cells["gridColumnStock"].Value = Name;
                     _StorageCode = code;
                    _ClickStorageList = new KeyValuePair<string, string>(code, Name);
                    //_StorageCode = code;
                    resizablePanel1.Visible = false;
                    }
                    if (_WareHouse == 1)
                    {
                        GridRow gr = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
                        string code = dataGridViewFujia.Rows[e.RowIndex].Cells["code"].Value.ToString();
                        string Name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                        gr.Cells["stockInCode"].Value = code;
                        gr.Cells["gridColumnStockIn"].Value = Name;
                        _ClickStorageList = new KeyValuePair<string, string>(code, Name);
                        _StorageCode = code;
                        resizablePanel1.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("双击绑定组装员或仓库信息数据错误！请检查：" + ex.Message);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            InitEmployee();
            _Click = 3;
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
            try
            {
                if (e.GridCell.GridColumn.Name == "gridColumnStock")
                {
                    ClickRowIndex = e.GridCell.RowIndex;
                    ClickStorage = true;
                    resizablePanel1.Visible = true;
                    resizablePanel1.Size = new Size(190, 120);
                    resizablePanel1.Location = new Point(e.GridCell.UnMergedBounds.X,
                        e.GridCell.UnMergedBounds.Bottom + panel3.Location.Y);
                    InitStorageList();
                    _WareHouse = 2;
                    return;
                }
                if (e.GridCell.GridColumn.Name == "material")
                {
                    if (_StorageCode != "")
                    {
                        ClickRowIndex = e.GridCell.RowIndex;
                        resizablePanelData.Visible = true;
                        resizablePanelData.Location = new Point(e.GridCell.UnMergedBounds.X,
                            e.GridCell.UnMergedBounds.Bottom + panel3.Location.Y);
                        //查询商品列表
                        _AllMaterial = waremain.GetWMainAndMaterialByWMCode(999, "", XYEEncoding.strCodeHex(_StorageCode));
                        InitMaterialDataGridView();
                        _Material = 2;
                    }
                    else
                    {
                        resizablePanelData.Visible = false;
                        MessageBox.Show("请先选择仓库：");
                    }
                }
            }
            catch (Exception)
            {
                if (_StorageCode != "")
                {
                    //查询商品列表
                    _AllMaterial = waremain.GetWMainAndMaterialByWMCode(999, "", XYEEncoding.strCodeHex(_StorageCode));
                    InitMaterialDataGridView();
                }
                else
                {
                    this.resizablePanelData.Visible = false;
                    MessageBox.Show("请先选择仓库！");
                }
            }
        }

        private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
        {
            try
            {
                if (e.GridCell.GridColumn.Name == "gridColumnStockIn")
                {
                    ClickRowIndex = e.GridCell.RowIndex;
                    ClickStorage = true;
                    resizablePanel1.Visible = true;
                    resizablePanel1.Size = new Size(190, 120);
                    resizablePanel1.Location = new Point(38,350);
                    InitStorageList();
                    _WareHouse = 1;
                    return;
                }
                if (e.GridCell.GridColumn.Name == "sup1material")
                {
                    if (_StorageCode != "")
                    {
                        ClickRowIndex = e.GridCell.RowIndex;
                        resizablePanelData.Visible = true;
                        resizablePanelData.Location = new Point(156,350);
                        //查询商品列表
                        _AllMaterial = waremain.GetWMainAndMaterialByWMCode(999, "", XYEEncoding.strCodeHex(_StorageCode));
                        InitMaterialDataGridView();
                        _Material = 1;
                    }
                    else
                    {
                        resizablePanelData.Visible = false;
                        MessageBox.Show("请先选择仓库：");
                    }
                }
            }
            catch (Exception)
            {
                if (_StorageCode != "")
                {
                    //查询商品列表
                    _AllMaterial = waremain.GetWMainAndMaterialByWMCode(999, "", XYEEncoding.strCodeHex(_StorageCode));
                    InitMaterialDataGridView();
                }
                else
                {
                    this.resizablePanelData.Visible = false;
                    MessageBox.Show("请先选择仓库！");
                }
            }
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

        #region 表格模糊查询

        private void superGridControl1_EditorValueChanged(object sender, GridEditEventArgs e)
        {
            try
            {
                string SS = "";
                GridRow gr = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
                string materialDaima = XYEEncoding.strCodeHex(e.EditControl.EditorValue.ToString());
                if (SS == "")
                {
                    //模糊查询商品列表
                    _AllMaterial = waremain.GetWMainAndMaterialByWMCode(0, "" + materialDaima + "", XYEEncoding.strCodeHex(_StorageCode));
                    InitMaterialDataGridView();
                    dataGridView1.DataSource = ch.DataTableReCoding(_AllMaterial);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:2111-表格商品模糊查询错误，查询数据错误" + ex.Message, "组装单温馨提示");
            }
        }

        private void superGridControl2_EditorValueChanged(object sender, GridEditEventArgs e)
        {
            try
            {
                string SS = "";
                GridRow gr = (GridRow)superGridControl2.PrimaryGrid.Rows[ClickRowIndex];
                string materialDaima = XYEEncoding.strCodeHex(e.EditControl.EditorValue.ToString());
                if (SS == "")
                {
                    //模糊查询商品列表
                    _AllMaterial = waremain.GetWMainAndMaterialByWMCode(0, "" + materialDaima + "", XYEEncoding.strCodeHex(_StorageCode));
                    InitMaterialDataGridView();
                    dataGridView1.DataSource = ch.DataTableReCoding(_AllMaterial);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:2111-表格商品模糊查询错误，查询数据错误" + ex.Message, "组装单温馨提示");
            }
        }

        #endregion

        /// <summary>
        /// 验证完成后，统计单元格数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_CellValidated(object sender, GridCellValidatedEventArgs e)
        {
            GridRow gr = e.GridPanel.Rows[e.GridCell.RowIndex] as GridRow;
            //计算金额及统计数量
            try
            {
                decimal tempAllnumber = 0;
                //数量
                decimal number = Convert.ToDecimal(gr.Cells["number"].FormattedValue);
                //单价
                decimal Price = Convert.ToDecimal(gr.Cells["price"].FormattedValue);
                //金额
                decimal money = number * Price;
                gr.Cells["money"].Value = money;

                //逐行统计数据总数
                for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                    tempAllnumber += Convert.ToDecimal(tempGR["number"].FormattedValue);
                }
                _MaterialNumber = tempAllnumber;
                gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                gr["number"].Value = _MaterialNumber.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("统计数量错误！请检查：" + ex.Message);
            }
        }
    }
}
