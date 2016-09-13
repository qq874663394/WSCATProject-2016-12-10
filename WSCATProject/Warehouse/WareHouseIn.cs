using DevComponents.DotNetBar.SuperGrid;
using HelperUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InterfaceLayer.Warehouse;
using Model;
using HelperUtility.Encrypt;
using BaseLayer;
using InterfaceLayer;
using WSCATProject.Warehouse;
using InterfaceLayer.Base;

namespace WSCATProject.WareHouse
{
    public partial class WareHouseIn : TestForm
    {
        public WareHouseIn()
        {
            InitializeComponent();
        }
        #region 仓库、区域、货架、排、格所需的参数
        /// <summary>
        /// 仓库code
        /// </summary>
        private string _cangkucode;
        public string StorageCode
        {
            get { return _cangkucode; }
            set { _cangkucode = value; }
        }
        /// <summary>
        /// 仓库名称
        /// </summary>
        private string _cangku;
        public string Storage
        {
            get { return _cangku; }
            set { _cangku = value; }
        }
        /// <summary>
        /// 货架code
        /// </summary>
        private string _huojiacode;
        public string StorageRackCode
        {
            get { return _huojiacode; }
            set { _huojiacode = value; }
        }
        //货架名称
        private string _huojia;
        public string StorageRack
        {
            get { return _huojia; }
            set { _huojia = value; }
        }
        /// <summary>
        /// 排code
        /// </summary>
        private string _paicode;
        public string StoragePaiCode
        {
            get { return _paicode; }
            set { _paicode = value; }
        }
        /// <summary>
        /// 排名称
        /// </summary>
        private string _pai;
        public string StoragePai
        {
            get { return _pai; }
            set { _pai = value; }
        }
        /// <summary>
        /// 格子code
        /// </summary>
        private string _gecode;
        public string StorageGeCode
        {
            get { return _gecode; }
            set { _gecode = value; }
        }
        /// <summary>
        /// 格子名称
        /// </summary>
        private string _ge;
        public string StorageGe
        {
            get { return _ge; }
            set { _ge = value; }
        }
        /// <summary>
        /// 区域code
        /// </summary>
        private string _quyuCode;
        public string StorageQuyuCode
        {
            get { return _quyuCode; }
            set { _quyuCode = value; }
        }
        /// <summary>
        /// 区域名称
        /// </summary>
        private string _quyu;
        public string StorageQuyu
        {
            get { return _quyu; }
            set { _quyu = value; }
        }
        /// <summary>
        /// 定义显示类型 0,待入库的 1、部分入库 2、已入库的
        /// </summary>
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
        /// <summary>
        /// 保存仓库商品明细
        /// </summary>
        public GridRow WareHouseModel
        {
            get { return _wareHouseModel; }
            set { _wareHouseModel = value; }
        }
        #endregion

        CodingHelper ch = new CodingHelper();
        InterfaceLayer.Warehouse.WarehouseInDetailInterface waredeta = new WarehouseInDetailInterface();

        //SupplierInterface supply = new SupplierInterface();
        EmpolyeeInterface employee = new EmpolyeeInterface();
        InterfaceLayer.Purchase.PurchaseDetailInterface pdi = new InterfaceLayer.Purchase.PurchaseDetailInterface();
        #region 数据字段

        /// <summary>
        /// 所有供应商
        /// </summary>
        private DataTable _AllSupply = null;
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
        private GridRow _wareHouseModel;
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

        #endregion

        private void StockIn_Load(object sender, EventArgs e)
        {
            //供应商
            //_AllSupply = supply.SelSupplierTable();

            //业务员
            _AllEmployee = employee.SelSupplierTable(false);

            //仓库
            GridTextBoxXEditControl gdieccangku = superGridControl1.PrimaryGrid.Columns["griCoulumcangku"].EditControl as GridTextBoxXEditControl;
            gdieccangku.ButtonCustom.Visible = true;
            gdieccangku.ButtonCustomClick += Gdiec_ButtonCustomClick;

            //数量
            GridDoubleInputEditControl gdiecNumber = superGridControl1.PrimaryGrid.Columns["gridColumnnumber"].EditControl as GridDoubleInputEditControl;
            gdiecNumber.MinValue = 0;
            gdiecNumber.MaxValue = 999999999;
            //货架
            GridTextBoxXEditControl gdiehuojia = superGridControl1.PrimaryGrid.Columns["griCoulumhuojia"].EditControl as GridTextBoxXEditControl;
            gdiehuojia.ButtonCustom.Visible = true;
            gdiehuojia.ButtonCustomClick += Gdiec_ButtonCustomClick;

            //禁用自动创建列
            dataGridView1.AutoGenerateColumns = false;
            dataGridViewFujia.AutoGenerateColumns = false;


            //绑定事件 双击事填充内容并隐藏列表
            dataGridViewFujia.CellDoubleClick += DataGridViewFujia_CellDoubleClick;
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
            // 将dataGridView中的内容居中显示
            dataGridViewFujia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            textBoxOddNumbers.Text = _wareHouseModel["code"].Value.ToString();
         
            this.labtextboxTop5.Text = _wareHouseModel["purchaseCode"].Value.ToString();
            comboBoxEx1.SelectedIndex = 0;
            superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            //待入库进行查看的时候
            if (_state == 0)
            {
                if (_wareHouseModel != null)
                {
                    try
                    {
                        //superGridControl1.PrimaryGrid.DataSource = ch.DataSetReCoding(waredeta.getListByMainCode(XYEEncoding.strCodeHex(_wareHouseModel["code"].Value.ToString())));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("错误" + ex.Message);
                    }
                }

            }
            //部分入库查看
            if (_state == 1)
            {
                if (_wareHouseModel != null)
                {
                    try
                    {
                        //superGridControl1.PrimaryGrid.DataSource = ch.DataSetReCoding(waredeta.getListByMainCode(XYEEncoding.strCodeHex(_wareHouseModel["code"].Value.ToString())));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("错误" + ex.Message);
                    }
                }
            }
            //以入库的状态查看
            if (_state == 2)
            {
                if (_wareHouseModel != null)
                {
                    try
                    {
                        // superGridControl1.PrimaryGrid.DataSource = ch.DataSetReCoding(waredeta.getListByMainCode(XYEEncoding.strCodeHex(_wareHouseModel["code"].Value.ToString())));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("错误" + ex.Message);
                    }
                }
            }
            superGridControl1.PrimaryGrid.EnsureVisible();
            InitDataGridView();
            //OrderTypeInterface oti = new OrderTypeInterface();
            //comboBoxEx1.ValueMember = "code";
            //comboBoxEx1.DisplayMember = "name";
            //comboBoxEx1.DataSource = oti.GetList("");
        }

        /// <summary>
        /// 点击panel隐藏扩展panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void panel6_Click(object sender, EventArgs e)
        {
            resizablePanel1.Visible = false;
        }

        #region 初始化数据
        /// <summary>
        /// 初始化供应商
        /// </summary>
        private void InitSupply()
        {
            //if (_Click != 1)
            //{
            //    _Click = 1;
            //    dataGridViewFujia.DataSource = null;
            //    dataGridViewFujia.Columns.Clear();

            //    DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
            //    dgvc.Name = "code";
            //    dgvc.HeaderText = "编码";
            //    dgvc.DataPropertyName = "编码";
            //    dataGridViewFujia.Columns.Add(dgvc);

            //    dgvc = new DataGridViewTextBoxColumn();
            //    dgvc.Name = "name";
            //    dgvc.HeaderText = "单位名称";
            //    dgvc.DataPropertyName = "单位名称";
            //    dataGridViewFujia.Columns.Add(dgvc);
            //    resizablePanel1.Location = new Point(640, 110);
            //    dataGridViewFujia.DataSource = _AllSupply;
            //}
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

                resizablePanel1.Location = new Point(204, 310);
                dataGridViewFujia.DataSource = _AllEmployee;
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
            dgvc.Name = "id";
            dgvc.Visible = false;
            dgvc.HeaderText = "maid";
            dgvc.DataPropertyName = "id";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "isClear";
            dgvc.Visible = false;
            dgvc.HeaderText = "clear";
            dgvc.DataPropertyName = "isClear";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "code";
            dgvc.Visible = false;
            dgvc.HeaderText = "code";
            dgvc.DataPropertyName = "code";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "PurchaseCode";
            dgvc.Visible = false;
            dgvc.HeaderText = "PurchaseCode";
            dgvc.DataPropertyName = "PurchaseCode";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "storageCode";
            dgvc.Visible = false;
            dgvc.HeaderText = "storageCode";
            dgvc.DataPropertyName = "storageCode";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "storageName";
            dgvc.Visible = false;
            dgvc.HeaderText = "storageName";
            dgvc.DataPropertyName = "storageName";
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
            dgvc.Name = "number";
            dgvc.Visible = false;
            dgvc.HeaderText = "number";
            dgvc.DataPropertyName = "number";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "discountBeforePrice";
            dgvc.Visible = false;
            dgvc.HeaderText = "discountBeforePrice";
            dgvc.DataPropertyName = "discountBeforePrice";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "discount";
            dgvc.Visible = false;
            dgvc.HeaderText = "discount";
            dgvc.DataPropertyName = "discount";
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

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "updateDate";
            dgvc.Visible = false;
            dgvc.HeaderText = "updateDate";
            dgvc.DataPropertyName = "updateDate";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "reserved1";
            dgvc.Visible = false;
            dgvc.HeaderText = "reserved1";
            dgvc.DataPropertyName = "reserved1";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "reserved2";
            dgvc.Visible = false;
            dgvc.HeaderText = "reserved2";
            dgvc.DataPropertyName = "reserved2";
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
            dgvc.Name = "materialModel";
            dgvc.Visible = true;
            dgvc.HeaderText = "规格型号";
            dgvc.DataPropertyName = "materialModel";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "barCode";
            dgvc.Visible = true;
            dgvc.HeaderText = "条形码";
            dgvc.DataPropertyName = "barCode";
            dataGridView1.Columns.Add(dgvc);
        }
        #endregion

        #region picture  图标点击事件

        //供应商图标的点击事件
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //if (_Click != 1)
            //{
            //    InitSupply();
            //}
        }
        //业务员图标点击事件
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (_Click != 2)
            {
                InitEmployee();
            }
        }
        #endregion

        #region  绑定pictureBox表格的数据
        /// <summary>
        /// 绑定pictureBox表格的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewFujia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //供应商
            //if (_Click == 1)
            //{
            //    string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
            //    labtextboxTop3.Text = name;
            //    resizablePanel1.Visible = false;
            //}
            //业务员
            if (_Click == 2)
            {
                string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                labtextboxBotton1.Text = name;
                resizablePanel1.Visible = false;
            }
        }
        #endregion

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            //非空验证
            isNUllValidate();
            //获得界面上的数据,准备传给base层新增数据
            string warehouseIncode = "";
            WarehouseInInterface warehouseInterface = new WarehouseInInterface();
            //入库单
            WarehouseIn warehouseIn = new WarehouseIn();
            //入库商品列表
            List<WarehouseInDetail> wareHouseInList = new List<WarehouseInDetail>();
            try
            {
                warehouseIncode = XYEEncoding.strCodeHex(this.textBoxOddNumbers.Text);
                warehouseIn.code = warehouseIncode;
                warehouseIn.isClear = 1;
                warehouseIn.purchaseCode = XYEEncoding.strCodeHex(this.labtextboxTop5.Text);
                warehouseIn.date = this.dateTimePicker1.Value;
                warehouseIn.checkState = 0;
                warehouseIn.examine = XYEEncoding.strCodeHex(labtextboxBotton4.Text);
                warehouseIn.operation = XYEEncoding.strCodeHex(labtextboxBotton3.Text);
                warehouseIn.remark = XYEEncoding.strCodeHex(labtextboxBotton2.Text);
                warehouseIn.state = 0;
                warehouseIn.reserved1 = "";
                warehouseIn.reserved2 = "";
                warehouseIn.type = XYEEncoding.strCodeHex(comboBoxEx1.Text);//入货类别
                warehouseIn.updateDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:4101;尝试创建入库单商品详情数据出错,请检查输入" + ex.Message);
                return;
            }

            try
            {
                //获得商品列表数据,准备传给base层新增数据
                GridRow g = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
                GridItemsCollection grs = superGridControl1.PrimaryGrid.Rows;
                int i = 0;
                string _wareHouesDetailCode = XYEEncoding.strCodeHex(BuildCode.ModuleCode("WD"));
                DateTime nowDataTime = DateTime.Now;
                foreach (GridRow gr in grs)
                {
                    if (gr["gridColumn2"].Value.ToString() != "")
                    {
                        i++;
                        WarehouseInDetail WarehouseIndetail = new WarehouseInDetail();
                        WarehouseIndetail.barcode = XYEEncoding.strCodeHex(this.textBoxOddNumbers.Text);
                        WarehouseIndetail.code = XYEEncoding.strCodeHex(gr["gridColumn1"].Value.ToString());
                        WarehouseIndetail.date = this.dateTimePicker1.Value;
                        WarehouseIndetail.isClear = 1;
                        WarehouseIndetail.materiaModel = XYEEncoding.strCodeHex(gr["gridColumn3"].Value.ToString());
                        WarehouseIndetail.materiaName = XYEEncoding.strCodeHex(gr["gridColumn2"].Value.ToString());
                        WarehouseIndetail.materiaUnit = XYEEncoding.strCodeHex(gr["gridColumn5"].Value.ToString());
                        WarehouseIndetail.money = Convert.ToDecimal(gr["gridColumn8"].Value);
                        WarehouseIndetail.number = Convert.ToDecimal(gr["gridColumn6"].Value);
                        WarehouseIndetail.price = Convert.ToDecimal(gr["gridColumn7"].Value);
                        WarehouseIndetail.remark = "";
                        WarehouseIndetail.WarehouseName = XYEEncoding.strCodeHex(Storage);//仓库名称
                        WarehouseIndetail.StorageRackCode = XYEEncoding.strCodeHex(StorageRackCode);//货架code
                        WarehouseIndetail.StorageRackName = StorageRackCode == "" ? XYEEncoding.strCodeHex(Storage + "/" + StorageQuyu) : XYEEncoding.strCodeHex(Storage + "/" + StorageQuyu + "/" + StorageRackCode + "/" + StoragePai + "/" + StorageGe);  //货架名称、排、格
                        WarehouseIndetail.reserved2 = "";
                        WarehouseIndetail.rfid = "";
                        WarehouseIndetail.rfid = "";
                        WarehouseIndetail.updateDate = nowDataTime;
                        WarehouseIndetail.state = 1;
                        int isarrive = Convert.ToBoolean((superGridControl1.PrimaryGrid.Rows[i] as GridRow).Cells["gridColumn11"].Value) == true ? 1 : 0;
                        if (isarrive == 1)
                        {
                            _rukushu++;
                        }
                        WarehouseIndetail.IsArrive = isarrive;
                        GridRow dr = superGridControl1.PrimaryGrid.Rows[0] as GridRow;
                        wareHouseInList.Add(WarehouseIndetail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：4102-尝试创建入库单数据出错,请检查输入" + ex.Message);
                return;
            }

            //增加一条入库单和入库单详细数据
            int warehouseInResult = warehouseInterface.addWarehouseIn(warehouseIn, wareHouseInList);

            switch (warehouseInResult)
            {
                case -1:
                    MessageBox.Show("错误代码:4001;拼接连接字符串时出现异常,请尝试重新插入数据.");
                    break;
                case -2:
                    MessageBox.Show("错误代码:4002;建立查询字符串参数时出现异常");
                    break;
                case -3:
                    MessageBox.Show("错误代码:4003;对参数赋值时出现异常,请检查输入");
                    break;
                case -4:
                    MessageBox.Show("错误代码:4004;尝试打开数据库连接时出错,请检查服务器连接");
                    break;
                case -5:
                    MessageBox.Show("错误代码:4005;对数据库新增数据时未能增加任何数据");
                    break;
                case -6:
                    MessageBox.Show("错误代码:4006;对数据库新增数据的方法失效,未能增加任何行");
                    break;
                case -7:
                    MessageBox.Show("错误代码:4007;检查到传入的参数为空,无法进行新增操作");
                    break;
            }
            if (warehouseInResult > 0)
            {
                MessageBox.Show("新增入库数据成功");
            }
        }

        /// <summary>
        /// 按下ESC按钮关闭子窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WareHouseIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.resizablePanel1.Visible = false;
            }
        }

        /// <summary>
        /// 点击仓库和货架的扩展按钮弹出窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Gdiec_ButtonCustomClick(object sender, EventArgs e)
        {
            //if (labtextboxTop3.Text == null)
            //{
            //    MessageBox.Show("请先选择供应商:");
            //    return;
            //}
            SelectedElementCollection ge = superGridControl1.PrimaryGrid.GetSelectedCells();
            if (ge.Count > 0)
            {
                GridCell gc = ge[0] as GridCell;
                WareHuseStorageRack whsr = new WareHuseStorageRack();
                whsr.ShowDialog(this);

                gc.GridRow.Cells[8].Value = Storage;
                //gc.GridRow.Cells[9].Value = StorageRackCode == "" ? (Storage + "/" + StorageQuyu) : (Storage + "/" + StorageQuyu + "/" + StorageRackCode + "/" + StoragePai + "/" + StorageGe);  //区域、货架名称、排、格;
                gc.GridRow.Cells[9].Value = StorageQuyu + "/" + StorageRack + "/" + StoragePai + "/" + StorageGe;
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
            gr.Cells["material"].Value = "合计";
            gr.Cells["material"].CellStyles.Default.Alignment =
                DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnnumber"].Value = 0;
            gr.Cells["gridColumnnumber"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnnumber"].CellStyles.Default.Background.Color1 = Color.Orange;
        }

        /// <summary>
        /// 审核按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExamine_Click(object sender, EventArgs e)
        {
            //非空验证
            isNUllValidate();
            int checkResult = 0;
            string wherecode = XYEEncoding.strCodeHex(textBoxOddNumbers.Text.Trim());
            WarehouseInInterface warehouseInterface = new WarehouseInInterface();
            int result = warehouseInterface.getWarehouseInList("code='" + wherecode + "' and state=0").Tables[0].Rows.Count;
            if (result > 0)
            {
                if (MessageBox.Show("还有商品未入库，是否继续审核？", "审核确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    try
                    {
                        checkResult = warehouseInterface.updateByCode(XYEEncoding.strCodeHex(textBoxOddNumbers.Text.Trim()));
                        if (checkResult > 0)
                        {
                            MessageBox.Show("审核成功");
                        }
                        else
                        {
                            MessageBox.Show("审核失败");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("错误代码：4103 - 尝试审核入库单出错, 请检查数据是否正常" + ex.Message);
                    }
                }
            }
            else
            {
                try
                {
                    checkResult = warehouseInterface.updateByCode(XYEEncoding.strCodeHex(textBoxOddNumbers.Text.Trim()));
                    if (checkResult > 0)
                    {
                        MessageBox.Show("审核成功");
                    }
                    else
                    {
                        MessageBox.Show("审核失败");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误代码：4103 - 尝试审核入库单出错, 请检查数据是否正常" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 非空验证
        /// </summary>
        private void isNUllValidate()
        {
            if (comboBoxEx1.Text.Trim() == null)
            {
                MessageBox.Show("入货类型不能为空！");
            }
            if (labtextboxTop3.Text.Trim() == null)
            {
                MessageBox.Show("供应商不能为空！");
            }
            if (labtextboxBotton1.Text.Trim() == null)
            {
                MessageBox.Show("业务员不能为空！");
            }
            GridRow gr = (GridRow)superGridControl1.PrimaryGrid.
               Rows[superGridControl1.PrimaryGrid.Rows.Count];
            if (gr.Cells["griCoulumcangku"].Value == null && gr.Cells["griCoulumhuojia"].Value == null)
            {
                MessageBox.Show("仓库和货架不能为空！");
            }

        }
        /// <summary>
        /// 模糊查询显示表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labtextboxTop3_TextAlignChanged(object sender, EventArgs e)
        {
            //string name = this.labtextboxTop3.Text.Trim();
            //dataGridViewFujia.DataSource = null;
            //dataGridViewFujia.Columns.Clear();

            //DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
            //dgvc.Name = "code";
            //dgvc.HeaderText = "编码";
            //dgvc.DataPropertyName = "编码";
            //dataGridViewFujia.Columns.Add(dgvc);

            //dgvc = new DataGridViewTextBoxColumn();
            //dgvc.Name = "name";
            //dgvc.HeaderText = "单位名称";
            //dgvc.DataPropertyName = "单位名称";
            //dataGridViewFujia.Columns.Add(dgvc);
            //resizablePanel1.Location = new Point(640, 110);
            //dataGridViewFujia.DataSource = supply.SelSupplierTable();

        }

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
                throw;
            }
        }

        private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "material")
            {
                SelectedElementCollection ge = superGridControl1.PrimaryGrid.GetSelectedCells();
                GridCell gc = ge[0] as GridCell;
                if (gc.GridRow.Cells[material].Value != null && (gc.GridRow.Cells[material].Value).ToString() != "")
                {
                    //模糊查询商品列表
                    _AllMaterial = pdi.GetList("" + XYEEncoding.strCodeHex(_wareHouseModel["purchaseCode"].Value.ToString() + ""), "" + XYEEncoding.strCodeHex(gc.GridRow.Cells[material].Value.ToString()) + "");
                    InitMaterialDataGridView();
                }
                else
                {
                    //绑定商品列表
                    _AllMaterial = pdi.GetList("" + XYEEncoding.strCodeHex(_wareHouseModel["purchaseCode"].Value.ToString() + ""), "");
                    InitMaterialDataGridView();
                }
                dataGridView1.DataSource = ch.DataTableReCoding(_AllMaterial);
            }
        }

        /// <summary>
        /// 双击物料信息填写在表格里面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            gr.Cells["material"].Value = dataGridView1.Rows[e.RowIndex].Cells["zhujima"].Value;//助记码
            gr.Cells["gridColumnname"].Value = dataGridView1.Rows[e.RowIndex].Cells["materialName"].Value;//商品名称
            gr.Cells["gridColumnmodel"].Value = dataGridView1.Rows[e.RowIndex].Cells["materialModel"].Value;//规格型号
            gr.Cells["gridColumnunit"].Value = dataGridView1.Rows[e.RowIndex].Cells["unit"].Value;//单位
            gr.Cells["gridColumntiaoxingma"].Value = dataGridView1.Rows[e.RowIndex].Cells["barCode"].Value;//条码
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
                _Materialnumber += 1;
                gr.Cells["gridColumnnumber"].Value = _Materialnumber;
            }
            SendKeys.Send("^{End}{Home}");
        }

        private void superGridControl1_TextChanged(object sender, EventArgs e)
        {
            string SS = "";
            GridRow gr = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
            string zujima = XYEEncoding.strCodeHex(gr.Cells["material"].Value.ToString());
            if (SS == "")
            {
                //模糊查询商品列表
                _AllMaterial = pdi.GetList("" + XYEEncoding.strCodeHex(_wareHouseModel["purchaseCode"].Value.ToString() + ""), "" + zujima + "");
                InitMaterialDataGridView();
                dataGridView1.DataSource = ch.DataTableReCoding(_AllMaterial);
            }
        }

        /// <summary>
        /// 输入值模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_EditorValueChanged(object sender, GridEditEventArgs e)
        {
            string SS = "";
            GridRow gr = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
            string zujima = XYEEncoding.strCodeHex(e.EditControl.EditorValue.ToString());
            if (SS == "")
            {
                //模糊查询商品列表
                _AllMaterial = pdi.GetList("" + XYEEncoding.strCodeHex(_wareHouseModel["purchaseCode"].Value.ToString() + ""), "" + zujima + "");
                InitMaterialDataGridView();
                dataGridView1.DataSource = ch.DataTableReCoding(_AllMaterial);
            }
        }
    }
}