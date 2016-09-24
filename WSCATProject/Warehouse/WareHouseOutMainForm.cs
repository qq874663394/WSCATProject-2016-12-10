using DevComponents.DotNetBar.SuperGrid;
using HelperUtility;
using HelperUtility.Encrypt;
using InterfaceLayer.Base;
using InterfaceLayer.Warehouse;
using InterfaceLayer.Sales;
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
    public partial class WareHouseOutMainForm : TestForm
    {
        public WareHouseOutMainForm()
        {
            InitializeComponent();
        }

        #region 调用接口以及加密解密方法
        CodingHelper ch = new CodingHelper();
        InterfaceLayer.Warehouse.WarehouseOutDetailInterface waredetaout = new WarehouseOutDetailInterface();
        EmpolyeeInterface employee = new EmpolyeeInterface();
        ClientInterface client = new ClientInterface();//客户
        InterfaceLayer.Sales.SalesMainInterface sales = new InterfaceLayer.Sales.SalesMainInterface();
        SalesDetailInterface salesdinterface = new SalesDetailInterface();
        #endregion

        #region 数据字段

        /// <summary>
        /// 所有客户
        /// </summary>
        private DataTable _AllClient = null;
        /// <summary>
        /// 所有业务员
        /// </summary>
        private DataTable _AllEmployee = null;
        /// <summary>
        /// 点击的项,1供应商  2为业务员
        /// </summary>
        private int _Click = 0;
        /// <summary>
        /// 定义显示类型 0,待入库的 1、部分入库 2、已入库的
        /// </summary>
        private int _state;
        /// <summary>
        /// 保存仓库商品明细
        /// </summary>
       // private GridRow _wareHouseModel;
        // private decimal _MaterialMoney;
        private decimal _MaterialNumber;
        private int _rukushu;//入库数量

        /// <summary>
        /// 所有商品列表
        /// </summary>
        private DataTable _AllMaterial = null;
        /// <summary>
        /// 统计数量
        /// </summary>
        private decimal _Materialnumber;
        /// <summary>
        /// 保存客户Code
        /// </summary>
        private string _clientcode;
        /// <summary>
        /// 出库code
        /// </summary>
        private string _warehouseoutcode;
        #endregion

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WareHouseOutMainForm_Load(object sender, EventArgs e)
        {
            try
            {
                //客户
                _AllClient = client.GetClientByBool(false);
                //业务员
                _AllEmployee = employee.SelSupplierTable(false);

                //数量
                GridDoubleInputEditControl gdiecNumber = superGridControl1.PrimaryGrid.Columns["gridColumnnumber"].EditControl as GridDoubleInputEditControl;
                gdiecNumber.MinValue = 0;
                gdiecNumber.MaxValue = 999999999;

                //禁用自动创建列
                dataGridView1.AutoGenerateColumns = false;
                dataGridViewFujia.AutoGenerateColumns = false;
                superGridControl1.HScrollBarVisible = true;
                //绑定事件 双击事填充内容并隐藏列表
                dataGridViewFujia.CellDoubleClick += DataGridViewFujia_CellDoubleClick;
                dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;

                InitDataGridView();
                toolStripButtonhou.Click += ToolStripButtonhou_Click;//前单
                toolStripButtonqian.Click += ToolStripButtonqian_Click;//后单
                toolStripButtonsave.Click += ToolStripButtonsave_Click;//保存
                toolStripButtonshen.Click += ToolStripButtonshen_Click;//审核
                //生成出库code 和显示条形码
                _warehouseoutcode = BuildCode.ModuleCode("WHO");
                textBoxOddNumbers.Text = _warehouseoutcode;
                barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
                _Code.ValueFont = new Font("微软雅黑", 20);
                System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
                pictureBox9.Image = imgTemp;
                pictureBox10.Parent = pictureBoxtitle;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2201-初始化数据错误" + ex.Message, "出库单温馨提示");
            }
        }
        /// <summary>
        /// 审核按钮的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonshen_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 保存按钮的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonsave_Click(object sender, EventArgs e)
        {
            //非空验证
            // isNUllValidate();
            //获得界面上的数据,准备传给base层新增数据
            //string warehouseIncode = "";
            WarehouseOutInterface warehouseoutinterface = new WarehouseOutInterface();
            //入库单
            WarehouseOut warehouseout = new WarehouseOut();
            //入库商品列表
            List<WarehouseOutDetail> wareHouseOutList = new List<WarehouseOutDetail>();
            try
            {
                warehouseout.checkState = 0;
                warehouseout.clientCode = XYEEncoding.strCodeHex(_clientcode);//客户code
                warehouseout.code = XYEEncoding.strCodeHex(_warehouseoutcode);//单号
                warehouseout.date = this.dateTimePicker1.Value;//开单日期
                warehouseout.defaultType = XYEEncoding.strCodeHex("出库开单");//默认单据类型
                warehouseout.delivery = XYEEncoding.strCodeHex(comboBoxExsonghuo.Text);//配送方式
                warehouseout.examine = XYEEncoding.strCodeHex(labtextboxBotton4.Text);//审核人
                warehouseout.MakeMan = XYEEncoding.strCodeHex(labtextboxBotton3.Text);//制单人
                warehouseout.SalesPhone = XYEEncoding.strCodeHex(labtextboxTop9.Text);//销售电话
                warehouseout.ClientName = XYEEncoding.strCodeHex(labtextboxTop2.Text);//客户名称
                warehouseout.expressMan = "";
                warehouseout.expressOdd = "";
                warehouseout.expressPhone = "";
                warehouseout.isClear = 1;
                warehouseout.operation = XYEEncoding.strCodeHex(labtextboxBotton1.Text);
                warehouseout.remark = XYEEncoding.strCodeHex(labtextboxBotton2.Text);
                warehouseout.reserved1 = "";
                warehouseout.reserved2 = "";
                warehouseout.salesCode = XYEEncoding.strCodeHex(comboBoxExxiaos.Text);
                warehouseout.state = 0;
                warehouseout.stock = "";
                warehouseout.type = XYEEncoding.strCodeHex(comboBoxEx1.Text);
                warehouseout.updateDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:2104;尝试创建入库单商品数据出错,请检查输入" + ex.Message, "入库单温馨提示");
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
                        //WarehouseInDetail WarehouseIndetail = new WarehouseInDetail();
                        //WarehouseIndetail.mainCode = XYEEncoding.strCodeHex(this.textBoxOddNumbers.Text);//单据入库单号
                        //WarehouseIndetail.barcode = XYEEncoding.strCodeHex(gr["gridColumntiaoxingma"].Value.ToString());//条形码
                        //WarehouseIndetail.code = XYEEncoding.strCodeHex(_WareHouseInCode + i.ToString());//入库明细的商品入库单
                        //WarehouseIndetail.date = this.dateTimePicker1.Value;
                        //WarehouseIndetail.isClear = 1;
                        //WarehouseIndetail.materialDaima = XYEEncoding.strCodeHex(gr["material"].Value.ToString());//商品代码
                        //WarehouseIndetail.materiaModel = XYEEncoding.strCodeHex(gr["gridColumnmodel"].Value.ToString());//规格型号
                        //WarehouseIndetail.materiaName = XYEEncoding.strCodeHex(gr["gridColumnname"].Value.ToString());//商品名称
                        //WarehouseIndetail.materiaUnit = XYEEncoding.strCodeHex(gr["gridColumnunit"].Value.ToString());//单位
                        //WarehouseIndetail.money = Convert.ToDecimal(gr["gridColumnmoney"].Value);//金额
                        //WarehouseIndetail.number = Convert.ToDecimal(gr["gridColumnnumber"].Value);//数量
                        //WarehouseIndetail.price = Convert.ToDecimal(gr["gridColumnprice"].Value);//价格
                        //WarehouseIndetail.remark = XYEEncoding.strCodeHex(gr["gridColumnremark"].Value == null ?
                        //     "" : gr["gridColumnremark"].Value.ToString());//备注
                        //WarehouseIndetail.warehouseCode = XYEEncoding.strCodeHex(StorageCode);//仓库code；
                        //WarehouseIndetail.warehouseName = XYEEncoding.strCodeHex(Storage);//仓库名称
                        //WarehouseIndetail.storageRackCode = XYEEncoding.strCodeHex(StorageRackCode);//货架code
                        //WarehouseIndetail.storageRackName = StorageRackCode == "" ? XYEEncoding.strCodeHex(Storage + "/" + StorageQuyu) : XYEEncoding.strCodeHex(Storage + "/" + StorageQuyu + "/" + StorageRackCode + "/" + StoragePai + "/" + StorageGe);  //货架名称、排、格
                        //WarehouseIndetail.productionDate = Convert.ToDateTime(gr["gridColumndate"].Value.ToString());//采购或者生产日期
                        //WarehouseIndetail.qualityDate = Convert.ToDouble(gr["gridColumnbaozhe"].Value.ToString());//保质期
                        //WarehouseIndetail.effectiveDate = Convert.ToDateTime(gr["gridColumnyouxiao"].Value.ToString());//有效期至
                        //WarehouseIndetail.reserved2 = "";
                        //WarehouseIndetail.reserved1 = "";
                        //WarehouseIndetail.rfid = "";
                        //WarehouseIndetail.updateDate = nowDataTime;
                        //WarehouseIndetail.state = 1;
                        //WarehouseIndetail.isArrive = 1;
                        //WarehouseIndetail.zhujima = "";//暂时为空
                        //GridRow dr = superGridControl1.PrimaryGrid.Rows[0] as GridRow;
                        //wareHouseInList.Add(WarehouseIndetail);

                        WarehouseOutDetail warehouseoutd = new WarehouseOutDetail();
                        warehouseoutd.barcode = XYEEncoding.strCodeHex(gr[""].Value.ToString());
                        warehouseoutd.code = XYEEncoding.strCodeHex(_warehouseoutcode + i.ToString());
                        warehouseoutd.date = dateTimePicker1.Value;
                        warehouseoutd.effectiveDate = Convert.ToDateTime(gr[""].Value.ToString());
                        warehouseoutd.IsArrive = 1;
                        warehouseoutd.isClear = 1;
                        warehouseoutd.MainCode = XYEEncoding.strCodeHex(_warehouseoutcode);
                        warehouseoutd.materialCode = XYEEncoding.strCodeHex(gr[""].Value.ToString());
                        warehouseoutd.materialDaima = XYEEncoding.strCodeHex(gr[""].Value.ToString());
                        warehouseoutd.materiaModel = XYEEncoding.strCodeHex(gr[""].Value.ToString());
                        warehouseoutd.materiaName = XYEEncoding.strCodeHex(gr[""].Value.ToString());
                        warehouseoutd.materiaUnit = XYEEncoding.strCodeHex(gr[""].Value.ToString());
                        warehouseoutd.money = Convert.ToDecimal(gr[""].Value.ToString());
                        warehouseoutd.number = Convert.ToDecimal(gr[""].Value.ToString());
                        warehouseoutd.price = Convert.ToDecimal(gr[""].Value.ToString());
                        warehouseoutd.productionDate = Convert.ToDateTime(gr[""].Value.ToString());
                        warehouseoutd.qualityDate = Convert.ToDecimal(gr[""].Value.ToString());
                        warehouseoutd.remark = XYEEncoding.strCodeHex(gr["gridColumnremark"].Value == null ?
                        "" : gr["gridColumnremark"].Value.ToString());//备注;
                        warehouseoutd.reserved1 = "";
                        warehouseoutd.reserved2 = "";
                        warehouseoutd.rfid = "";
                        warehouseoutd.state = 0;
                        warehouseoutd.StorageRackCode = "";
                        warehouseoutd.StorageRackName = gr[""].Value.ToString();
                        warehouseoutd.updateDate = DateTime.Now;
                        warehouseoutd.WarehouseCode = "";
                        warehouseoutd.WarehouseName = "";
                        GridRow dr = superGridControl1.PrimaryGrid.Rows[0] as GridRow;
                        wareHouseOutList.Add(warehouseoutd);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2105-尝试创建入库单详商品数据出错,请检查输入" + ex.Message, "入库单温馨提示");
                return;
            }

            //增加一条入库单和入库单详细数据
            // object warehouseInResult = warehouseInterface.AddWarehouseOrToDetail(warehouseIn, wareHouseInList);
            //this.textBoxid.Text = warehouseInResult.ToString();
            //if (warehouseInResult != null)
            //{
            //    MessageBox.Show("新增入库数据成功", "入库单温馨提示");
            //}
        }
        /// <summary>
        /// 前单的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonqian_Click(object sender, EventArgs e)
        {
            WarehouseOutInterface warehouseoutterface = new WarehouseOutInterface();
            try
            {
                if (textBoid.Text == "")
                {
                    textBoid.Text = "0";
                }

                WarehouseOut warehouseout = warehouseoutterface.GetPreAndNext(Convert.ToInt32(textBoid.Text), 1);
                this.comboBoxEx1.Text = XYEEncoding.strHexDecode(warehouseout.type);
                comboBoxExxiaos.Text = XYEEncoding.strHexDecode(warehouseout.salesCode);//销售单号
                labtextboxTop2.Text = XYEEncoding.strHexDecode(warehouseout.ClientName);//客户
                labtextboxTop9.Text = XYEEncoding.strHexDecode(warehouseout.SalesPhone);//销售电话
                comboBoxExsonghuo.Text = XYEEncoding.strHexDecode(warehouseout.delivery);//送货方式
                labtextboxBotton2.Text = XYEEncoding.strHexDecode(warehouseout.remark);//摘要
                this.labtextboxBotton1.Text = XYEEncoding.strHexDecode(warehouseout.operation);
                this.labtextboxBotton3.Text = XYEEncoding.strHexDecode(warehouseout.MakeMan);
                this.labtextboxBotton4.Text = XYEEncoding.strHexDecode(warehouseout.examine);
                if (warehouseout.id != 0)
                {
                    textBoid.Text = warehouseout.id.ToString();
                    WarehouseOutDetailInterface widiout = new WarehouseOutDetailInterface();
                    superGridControl1.PrimaryGrid.DataSource = null;
                    superGridControl1.PrimaryGrid.Columns.Clear();
                    superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
                    //DataTable dt = ch.DataTableReCoding(widiout.getListByMainCode(XYEEncoding.strCodeHex(textBoxOddNumbers.Text)).Tables[0]);
                    DataTable dt = new DataTable();
                    superGridControl1.PrimaryGrid.DataSource = dt;
                    InitWarehouseDetail();
                    //InitForm();
                }
                barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
                _Code.ValueFont = new Font("微软雅黑", 20);
                System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
                pictureBox9.Image = imgTemp;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-尝试点击后单数据错误" + ex.Message, "出库单温馨提示");
            }
        }
        /// <summary>
        /// 后单的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonhou_Click(object sender, EventArgs e)
        {
            WarehouseOutInterface warehouseoutterface = new WarehouseOutInterface();
            try
            {
                if (textBoid.Text == "")
                {
                    textBoid.Text = "0";
                }

                WarehouseOut warehouseout = warehouseoutterface.GetPreAndNext(Convert.ToInt32(textBoid.Text), 0);
                this.comboBoxEx1.Text = XYEEncoding.strHexDecode(warehouseout.type);
                comboBoxExxiaos.Text = XYEEncoding.strHexDecode(warehouseout.salesCode);//销售单号
                labtextboxTop2.Text = XYEEncoding.strHexDecode(warehouseout.ClientName);//客户
                labtextboxTop9.Text = XYEEncoding.strHexDecode(warehouseout.SalesPhone);//销售电话
                comboBoxExsonghuo.Text = XYEEncoding.strHexDecode(warehouseout.delivery);//送货方式
                labtextboxBotton2.Text = XYEEncoding.strHexDecode(warehouseout.remark);//摘要
                this.labtextboxBotton1.Text = XYEEncoding.strHexDecode(warehouseout.operation);
                this.labtextboxBotton3.Text = XYEEncoding.strHexDecode(warehouseout.MakeMan);
                this.labtextboxBotton4.Text = XYEEncoding.strHexDecode(warehouseout.examine);
                if (warehouseout.id != 0)
                {
                    textBoid.Text = warehouseout.id.ToString();
                    WarehouseOutDetailInterface widiout = new WarehouseOutDetailInterface();
                    superGridControl1.PrimaryGrid.DataSource = null;
                    superGridControl1.PrimaryGrid.Columns.Clear();
                    superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
                    //DataTable dt = ch.DataTableReCoding(widiout.getListByMainCode(XYEEncoding.strCodeHex(textBoxOddNumbers.Text)).Tables[0]);
                    DataTable dt = new DataTable();
                    superGridControl1.PrimaryGrid.DataSource = dt;
                    InitWarehouseDetail();
                    //InitForm();
                }
                barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
                _Code.ValueFont = new Font("微软雅黑", 20);
                System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
                pictureBox9.Image = imgTemp;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-尝试点击后单数据错误" + ex.Message, "出库单温馨提示");
            }
        }

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
            gr.Cells["gridColumnnumber"].Value = 0;
            gr.Cells["gridColumnnumber"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnnumber"].CellStyles.Default.Background.Color1 = Color.Orange;
        }

        /// <summary>
        /// 初始化商品下拉别表的数据
        /// </summary>
        private void InitMaterialDataGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "id";
            dgvc.Visible = false;
            dgvc.HeaderText = "maid";
            dgvc.DataPropertyName = "id";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "code";
            dgvc.Visible = false;
            dgvc.HeaderText = "code";
            dgvc.DataPropertyName = "code";
            dataGridView1.Columns.Add(dgvc);


            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "materialCode";
            dgvc.Visible = false;
            dgvc.HeaderText = "materialCode";
            dgvc.DataPropertyName = "materialCode";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "unit";
            dgvc.Visible = false;
            dgvc.HeaderText = "unit";
            dgvc.DataPropertyName = "unit";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "needNumber";
            dgvc.Visible = false;
            dgvc.HeaderText = "needNumber";
            dgvc.DataPropertyName = "needNumber";
            dataGridView1.Columns.Add(dgvc);


            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "discountAfterPrice";
            dgvc.Visible = false;
            dgvc.HeaderText = "discountAfterPrice";
            dgvc.DataPropertyName = "discountAfterPrice";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "money";
            dgvc.Visible = false;
            dgvc.HeaderText = "money";
            dgvc.DataPropertyName = "money";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "remark";
            dgvc.Visible = false;
            dgvc.HeaderText = "remark";
            dgvc.DataPropertyName = "remark";
            dataGridView1.Columns.Add(dgvc);
            //货架路径
            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "storageRackLocation";
            dgvc.Visible = false;
            dgvc.HeaderText = "storageRackLocation";
            dgvc.DataPropertyName = "storageRackLocation";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "zhujima";
            dgvc.Visible = true;
            dgvc.HeaderText = "助记码";
            dgvc.DataPropertyName = "zhujima";
            dataGridView1.Columns.Add(dgvc);


            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "materialName";
            dgvc.Visible = true;
            dgvc.HeaderText = "商品名称";
            dgvc.DataPropertyName = "materialName";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "materiaModel";
            dgvc.Visible = true;
            dgvc.HeaderText = "规格型号";
            dgvc.DataPropertyName = "materiaModel";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "barCode";
            dgvc.Visible = true;
            dgvc.HeaderText = "条形码";
            dgvc.DataPropertyName = "barCode";
            dataGridView1.Columns.Add(dgvc);
        }

        /// <summary>
        /// 初始化业务员
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

                resizablePanel1.Location = new Point(234, 440);
                dataGridViewFujia.DataSource = ch.DataTableReCoding(_AllEmployee);
                resizablePanel1.Visible = true;
                if (this.WindowState == FormWindowState.Maximized)
                {
                    resizablePanel1.Location = new Point(220, 670);
                    return;
                }
                if (this.WindowState == FormWindowState.Normal)
                {
                    resizablePanel1.Location = new Point(234, 440);
                    return;
                }
            }
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
                dgvc.HeaderText = "销售电话";
                dgvc.DataPropertyName = "mobilePhone";
                dgvc.Visible = false;
                dataGridViewFujia.Columns.Add(dgvc);

                resizablePanel1.Location = new Point(470, 160);
                dataGridViewFujia.DataSource = ch.DataTableReCoding(_AllClient);
                resizablePanel1.Visible = true;
            }
        }

        /// <summary>
        /// 初始化入库明细表格
        /// </summary>
        private void InitWarehouseDetail()
        {
            GridColumn gc = null;
            gc = new GridColumn();
            gc.DataPropertyName = "materialDaima";
            gc.Name = "materialDaima";
            gc.HeaderText = "商品代码";
            gc.Width = 120;
            gc.AutoSizeMode = ColumnAutoSizeMode.Fill;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "materiaName";
            gc.Name = "materiaName";
            gc.HeaderText = "商品名称";
            gc.Width = 140;
            gc.AutoSizeMode = ColumnAutoSizeMode.Fill;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "materiaModel";
            gc.Name = "materiaModel";
            gc.HeaderText = "规格型号";
            gc.Width = 130;
            gc.AutoSizeMode = ColumnAutoSizeMode.Fill;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "barcode";
            gc.Name = "barcode";
            gc.HeaderText = "条形码";
            gc.Width = 150;
            gc.AutoSizeMode = ColumnAutoSizeMode.Fill;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "materiaUnit";
            gc.Name = "materiaUnit";
            gc.HeaderText = "单位";
            gc.Width = 70;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "number";
            gc.Name = "number";
            gc.HeaderText = "数量";
            gc.Width = 80;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "warehouseName";
            gc.Name = "warehouseName";
            gc.HeaderText = "仓库";
            gc.Width = 80;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "storageRackName";
            gc.Name = "storageRackName";
            gc.HeaderText = "区域/排/行/列";
            gc.Width = 80;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "productionDate";
            gc.Name = "productionDate";
            gc.HeaderText = "采购/生产日期";
            gc.Width = 70;
            gc.HeaderStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "qualityDate";
            gc.Name = "qualityDate";
            gc.HeaderText = "保质期(天)";
            gc.Width = 50;
            gc.HeaderStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "effectiveDate";
            gc.Name = "effectiveDate";
            gc.HeaderText = "有效期至";
            gc.Width = 80;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "remark";
            gc.Name = "remark";
            gc.HeaderText = "备注";
            gc.Width = 110;
            superGridControl1.PrimaryGrid.Columns.Add(gc);
        }

        #endregion

        /// <summary>
        /// 第一列编辑的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
        {
            if (this.labtextboxTop2.Text.Trim() == "")
            {
                resizablePanelData.Visible = false;
                MessageBox.Show("请先选择客户，显示销售单号!");
                return;
            }
            if (e.GridCell.GridColumn.Name == "material")
            {
                SelectedElementCollection ge = superGridControl1.PrimaryGrid.GetSelectedCells();
                GridCell gc = ge[0] as GridCell;
                if (gc.GridRow.Cells[material].Value != null && (gc.GridRow.Cells[material].Value).ToString() != "")
                {
                    //模糊查询商品列表
                    _AllMaterial = salesdinterface.GetDetailByMainCode(XYEEncoding.strCodeHex(this.comboBoxExxiaos.Text.Trim()), 2, XYEEncoding.strCodeHex(gc.GridRow.Cells[material].Value.ToString()));
                    InitMaterialDataGridView();
                }
                else
                {
                    //绑定商品列表
                    _AllMaterial = salesdinterface.GetDetailByMainCode(XYEEncoding.strCodeHex(this.comboBoxExxiaos.Text.Trim()), 4, "");
                    InitMaterialDataGridView();
                }

                dataGridView1.DataSource = ch.DataTableReCoding(_AllMaterial);
            }
        }
        /// <summary>
        /// 统计和验证数据
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
                decimal number = Convert.ToDecimal(gr.Cells["gridColumnnumber"].FormattedValue);
                decimal price = Convert.ToDecimal(gr.Cells["gridColumnprice"].FormattedValue);
                decimal allPrice = number * price;
                gr.Cells["gridColumnmoney"].Value = allPrice;
                //逐行统计数据总数
                decimal tempAllNumber = 0;
                for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                    tempAllNumber += Convert.ToDecimal(tempGR["gridColumnnumber"].FormattedValue);
                }
                _MaterialNumber = tempAllNumber;
                gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                gr["gridColumnnumber"].Value = _MaterialNumber.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("统计数量出错！请检查：" + ex.Message);
            }
        }

        private void superGridControl1_EditorValueChanged(object sender, GridEditEventArgs e)
        {
            string SS = "";
            GridRow gr = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
            string materialDaima = XYEEncoding.strCodeHex(e.EditControl.EditorValue.ToString());
            if (SS == "")
            {
                //模糊查询商品列表
                _AllMaterial = salesdinterface.GetDetailByMainCode(XYEEncoding.strCodeHex(this.comboBoxExxiaos.Text.Trim()), 2, materialDaima);
                InitMaterialDataGridView();
                dataGridView1.DataSource = ch.DataTableReCoding(_AllMaterial);
            }
        }

        #region 小箭头和两个附表的点击事件
        /// <summary>
        /// 出库员箭头点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (_Click != 2)
            {
                InitEmployee();
            }
            _Click = 4;
        }
        /// <summary>
        /// 客户箭头的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (_Click != 1)
            {
                //调用客户的绑定列
                InitClient();
            }
            _Click = 3;
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //是否要新增一行的标记
            bool newAdd = false;
            GridRow gr = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
            //id字段为空 说明是没有数据的行 不是修改而是新增
            if (gr.Cells["gridColumnid"].Value == null)
            {
                newAdd = true;
            }
            try
            {
                gr.Cells["material"].Value = dataGridView1.Rows[e.RowIndex].Cells["zhujima"].Value;//助记码
                gr.Cells["gridColumnname"].Value = dataGridView1.Rows[e.RowIndex].Cells["materialName"].Value;//商品名称
                gr.Cells["gridColumnmodel"].Value = dataGridView1.Rows[e.RowIndex].Cells["materialModel"].Value;//规格型号
                gr.Cells["gridColumnunit"].Value = dataGridView1.Rows[e.RowIndex].Cells["unit"].Value;//单位
                gr.Cells["gridColumntiaoxingma"].Value = dataGridView1.Rows[e.RowIndex].Cells["barCode"].Value;//条码
                gr.Cells["gridColumnprice"].Value = dataGridView1.Rows[e.RowIndex].Cells["discountBeforePrice"].Value;//单价
                gr.Cells["griCoulumcangku"].Value = "";//仓库
                gr.Cells["griCoulumhuojia"].Value = "";//货架
                decimal number = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["number"].Value);
                decimal price = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["discountBeforePrice"].Value);
                gr.Cells["gridColumnmoney"].Value = number * price;//金额
                resizablePanelData.Visible = false;
                //新增一行 
                if (newAdd)
                {
                    superGridControl1.PrimaryGrid.NewRow(superGridControl1.PrimaryGrid.Rows.Count);
                    //递增数量和金额 默认为1和单价 
                    gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                    _Materialnumber += 0;
                    gr.Cells["gridColumnnumber"].Value = _MaterialNumber;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("点击物料绑定数据错误！" + ex.Message);
            }
            SendKeys.Send("^{End}{Home}");
        }

        private void DataGridViewFujia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //客户
            if (_Click == 1 || _Click == 3)
            {
                string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                _clientcode = XYEEncoding.strCodeHex(dataGridViewFujia.Rows[e.RowIndex].Cells["code"].Value.ToString());
                string phone = dataGridViewFujia.Rows[e.RowIndex].Cells["mobilePhone"].Value.ToString();
                labtextboxTop2.Text = name;
                labtextboxTop9.Text = phone;
                resizablePanel1.Visible = false;
                //根据搜索的客户来绑定下拉列表
                DataTable dt = ch.DataTableReCoding(sales.GetTableByClientCode(_clientcode));
                this.comboBoxExxiaos.DataSource = dt;
                comboBoxExxiaos.ValueMember = "code";
                comboBoxExxiaos.DisplayMember = "name";

            }
            //业务员
            if (_Click == 2 || _Click == 4)
            {
                string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                labtextboxBotton1.Text = name;
                resizablePanel1.Visible = false;
            }
        }

        #endregion

        /// <summary>
        /// 光标默认在哪个控件上面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WareHouseOutMainForm_Activated(object sender, EventArgs e)
        {
            labtextboxTop2.Focus();
        }
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
        /// <summary>
        /// 出库员模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labtextboxBotton1_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (labtextboxBotton1.Text.Trim() == "")
                {
                    InitEmployee();
                    _Click = 4;
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
                dataGridViewFujia.DataSource = ch.DataTableReCoding(employee.GetList(0, "" + XYEEncoding.strCodeHex(labtextboxBotton1.Text.Trim()) + ""));
                resizablePanel1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：模糊查询出库员数据错误" + ex.Message, "出库单温馨提示");
            }


        }
        /// <summary>
        /// 客户的模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labtextboxTop2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.labtextboxTop2.Text.Trim() == "")
                {
                    InitClient();
                    _Click = 3;
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
                dgvc.HeaderText = "销售电话";
                dgvc.DataPropertyName = "mobilePhone";
                dgvc.Visible = false;
                dataGridViewFujia.Columns.Add(dgvc);

                resizablePanel1.Location = new Point(550, 160);
                string name = XYEEncoding.strCodeHex(this.labtextboxTop2.Text.Trim());
                dataGridViewFujia.DataSource = ch.DataTableReCoding(client.GetList(0, name));
                resizablePanel1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：模糊查询客户数据错误" + ex.Message, "出库单温馨提示");
            }
        }
        /// <summary>
        /// 按ESC关闭副表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WareHouseOutMainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.resizablePanel1.Visible = false;
                this.resizablePanelData.Visible = false;
            }
        }
        /// <summary>
        /// 产品检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxchanpin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.comboBoxExxiaos.Text.Trim() == "")
                {
                    resizablePanelData.Visible = false;
                    MessageBox.Show("请先选择客户，显示销售单号!");
                    return;
                }
                resizablePanelData.Location = new Point(91, 193);
                resizablePanelData.Visible = true;
                SalesDetailInterface salinf = new SalesDetailInterface();
                if (string.IsNullOrWhiteSpace(textBoxchanpin.Text.Trim()))
                {
                    //模糊查询商品列表
                    _AllMaterial = salinf.GetList(0, XYEEncoding.strCodeHex(comboBoxExxiaos.Text));
                    InitMaterialDataGridView();
                    dataGridView1.DataSource = ch.DataTableReCoding(_AllMaterial);
                    return;
                }
                //模糊查询商品列表
                _AllMaterial = salinf.GetWhereList(XYEEncoding.strCodeHex(this.textBoxchanpin.Text), XYEEncoding.strCodeHex(comboBoxExxiaos.Text));
                InitMaterialDataGridView();
                dataGridView1.DataSource = ch.DataTableReCoding(_AllMaterial);
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2114-产品检索模糊查询数据失败" + ex.Message, "入库单温馨提示");
            }
        }

        private void textBoxchanpin_Enter(object sender, EventArgs e)
        {
            textBoxchanpin_TextChanged(sender, e);
        }

        private void textBoxchanpin_Leave(object sender, EventArgs e)
        {
            if (ActiveControl.Name == "dataGridView1")//如果当前活动控件是dataGridView1
            {
                resizablePanelData.Visible = true;
                return;
            }
            resizablePanelData.Visible = false;
        }
    }
}
