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
    public partial class WareHouseInMain : TestForm
    {
        public WareHouseInMain()
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

        #region 调用接口以及加密解密方法
        CodingHelper ch = new CodingHelper();
        InterfaceLayer.Warehouse.WarehouseInDetailInterface waredeta = new WarehouseInDetailInterface();
        SupplierInterface supply = new SupplierInterface();
        EmpolyeeInterface employee = new EmpolyeeInterface();
        InterfaceLayer.Purchase.PurchaseDetailInterface pdi = new InterfaceLayer.Purchase.PurchaseDetailInterface();

        #endregion

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
        /// <summary>
        /// 保存供应商Code
        /// </summary>
        private string _suppliercode;
        #endregion

        private void WareHouseInMain_Load(object sender, EventArgs e)
        {
            //供应商
            _AllSupply = supply.SelSupplierTable();

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

            toolStripButtonqian.Click += ToolStripButtonqian_Click;//前单的事件
            toolStripButtonhou.Click += ToolStripButtonhou_Click;//后单的事件
            toolStripButtonsave.Click += ToolStripButtonsave_Click;//保存的事件
            toolStripButtonshen.Click += ToolStripButtonshen_Click;//审核的事件
            //禁用自动创建列
            dataGridView1.AutoGenerateColumns = false;
            dataGridViewFujia.AutoGenerateColumns = false;
            superGridControl1.HScrollBarVisible = true;
            //绑定事件 双击事填充内容并隐藏列表
            dataGridViewFujia.CellDoubleClick += DataGridViewFujia_CellDoubleClick;
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
            // 将dataGridView中的内容居中显示
            dataGridViewFujia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //调用合计行数据
            InitDataGridView();
            comboBoxEx2.SelectedIndex = 0;
        }

        /// <summary>
        /// 审核按钮的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonshen_Click(object sender, EventArgs e)
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
                warehouseIn.code = warehouseIncode;//入库单号
                warehouseIn.isClear = 1;
                warehouseIn.purchaseCode = XYEEncoding.strCodeHex(this.comboBoxEx1.Text);//采购单号
                warehouseIn.date = this.dateTimePicker1.Value;//开单日期
                warehouseIn.checkState = 0;
                warehouseIn.supplierCode = _suppliercode;//供应商code
                warehouseIn.supplierName = XYEEncoding.strCodeHex(labtextboxTop6.Text);//供应商名称
                warehouseIn.supplierPhone = XYEEncoding.strCodeHex(labtextboxTop9.Text);//采购电话
                warehouseIn.examine = XYEEncoding.strCodeHex(labtextboxBotton4.Text);//审核人
                warehouseIn.operation = XYEEncoding.strCodeHex(labtextboxBotton1.Text);//入库员
                warehouseIn.makeMan = XYEEncoding.strCodeHex(labtextboxBotton3.Text);//制单人
                warehouseIn.remark = XYEEncoding.strCodeHex(labtextboxBotton2.Text);//摘要
                warehouseIn.state = 0;
                warehouseIn.reserved1 = "";
                warehouseIn.reserved2 = "";
                warehouseIn.type = XYEEncoding.strCodeHex(comboBoxEx2.Text);//入货类别
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
                string _wareHouesDetailCode = "123";
                DateTime nowDataTime = DateTime.Now;
                foreach (GridRow gr in grs)
                {
                    if (gr["material"].Value.ToString() != "")
                    {
                        i++;
                        WarehouseInDetail WarehouseIndetail = new WarehouseInDetail();
                        WarehouseIndetail.MainCode = XYEEncoding.strCodeHex(this.textBoxOddNumbers.Text);//单据入库单号
                        WarehouseIndetail.barcode = XYEEncoding.strCodeHex(gr["gridColumntiaoxingma"].Value.ToString());//条形码
                        WarehouseIndetail.code = XYEEncoding.strCodeHex(gr["gridColumncode"].Value.ToString() + _wareHouesDetailCode);//商品单号
                        WarehouseIndetail.date = this.dateTimePicker1.Value;
                        WarehouseIndetail.isClear = 1;
                        WarehouseIndetail.materialDaima = XYEEncoding.strCodeHex(gr["material"].Value.ToString());//商品代码
                        WarehouseIndetail.materiaModel = XYEEncoding.strCodeHex(gr["gridColumnmodel"].Value.ToString());//规格型号
                        WarehouseIndetail.materiaName = XYEEncoding.strCodeHex(gr["gridColumnname"].Value.ToString());//商品名称
                        WarehouseIndetail.materiaUnit = XYEEncoding.strCodeHex(gr["gridColumnunit"].Value.ToString());//单位
                        WarehouseIndetail.money = Convert.ToDecimal(gr["gridColumnmoney"].Value);//金额
                        WarehouseIndetail.number = Convert.ToDecimal(gr["gridColumnnumber"].Value);//数量
                        WarehouseIndetail.price = Convert.ToDecimal(gr["gridColumnprice"].Value);//价格
                        WarehouseIndetail.remark = XYEEncoding.strCodeHex(gr["gridColumnremark"].Value.ToString()); ;//备注
                        WarehouseIndetail.WarehouseCode = XYEEncoding.strCodeHex(StorageCode);//仓库code；
                        WarehouseIndetail.WarehouseName = XYEEncoding.strCodeHex(Storage);//仓库名称
                        WarehouseIndetail.StorageRackCode = XYEEncoding.strCodeHex(StorageRackCode);//货架code
                        WarehouseIndetail.StorageRackName = StorageRackCode == "" ? XYEEncoding.strCodeHex(Storage + "/" + StorageQuyu) : XYEEncoding.strCodeHex(Storage + "/" + StorageQuyu + "/" + StorageRackCode + "/" + StoragePai + "/" + StorageGe);  //货架名称、排、格
                        WarehouseIndetail.productionDate = Convert.ToDateTime(gr["gridColumndate"].Value);//采购或者生产日期
                        WarehouseIndetail.qualityDate = Convert.ToDateTime(gr["gridColumnbaozhe"].Value);//保质期
                        WarehouseIndetail.effectiveDate = Convert.ToDateTime(gr["gridColumnyouxiao"].Value);//有效期至
                        WarehouseIndetail.reserved2 = "";
                        WarehouseIndetail.rfid = "";
                        WarehouseIndetail.rfid = "";
                        WarehouseIndetail.updateDate = nowDataTime;
                        WarehouseIndetail.state = 1;
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
            //int warehouseInResult = warehouseInterface.addWarehouseIn(warehouseIn, wareHouseInList);
            //switch (warehouseInResult)
            //{
            //    case -1:
            //        MessageBox.Show("错误代码:4001;拼接连接字符串时出现异常,请尝试重新插入数据.");
            //        break;
            //    case -2:
            //        MessageBox.Show("错误代码:4002;建立查询字符串参数时出现异常");
            //        break;
            //    case -3:
            //        MessageBox.Show("错误代码:4003;对参数赋值时出现异常,请检查输入");
            //        break;
            //    case -4:
            //        MessageBox.Show("错误代码:4004;尝试打开数据库连接时出错,请检查服务器连接");
            //        break;
            //    case -5:
            //        MessageBox.Show("错误代码:4005;对数据库新增数据时未能增加任何数据");
            //        break;
            //    case -6:
            //        MessageBox.Show("错误代码:4006;对数据库新增数据的方法失效,未能增加任何行");
            //        break;
            //    case -7:
            //        MessageBox.Show("错误代码:4007;检查到传入的参数为空,无法进行新增操作");
            //        break;
            //}
            //if (warehouseInResult > 0)
            //{
            //    MessageBox.Show("新增入库数据成功");
            //}
        }

        /// <summary>
        /// 保存按钮的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonsave_Click(object sender, EventArgs e)
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
                warehouseIn.code = warehouseIncode;//入库单号
                warehouseIn.isClear = 1;
                warehouseIn.purchaseCode = XYEEncoding.strCodeHex(this.comboBoxEx1.Text);//采购单号
                warehouseIn.date = this.dateTimePicker1.Value;//开单日期
                warehouseIn.checkState = 0;
                warehouseIn.supplierCode = _suppliercode;//供应商code
                warehouseIn.supplierName = XYEEncoding.strCodeHex(labtextboxTop6.Text);//供应商名称
                warehouseIn.supplierPhone = XYEEncoding.strCodeHex(labtextboxTop9.Text);//采购电话
                warehouseIn.examine = XYEEncoding.strCodeHex(labtextboxBotton4.Text);//审核人
                warehouseIn.operation = XYEEncoding.strCodeHex(labtextboxBotton1.Text);//入库员
                warehouseIn.makeMan = XYEEncoding.strCodeHex(labtextboxBotton3.Text);//制单人
                warehouseIn.remark = XYEEncoding.strCodeHex(labtextboxBotton2.Text);//摘要
                warehouseIn.state = 0;
                warehouseIn.reserved1 = "";
                warehouseIn.reserved2 = "";
                warehouseIn.type = XYEEncoding.strCodeHex(comboBoxEx2.Text);//入货类别
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
                string _wareHouesDetailCode = "123";
                DateTime nowDataTime = DateTime.Now;
                foreach (GridRow gr in grs)
                {
                    if (gr["material"].Value.ToString() != "")
                    {
                        i++;
                        WarehouseInDetail WarehouseIndetail = new WarehouseInDetail();
                        WarehouseIndetail.MainCode = XYEEncoding.strCodeHex(this.textBoxOddNumbers.Text);//单据入库单号
                        WarehouseIndetail.barcode = XYEEncoding.strCodeHex(gr["gridColumntiaoxingma"].Value.ToString());//条形码
                        WarehouseIndetail.code = XYEEncoding.strCodeHex(gr["gridColumncode"].Value.ToString() + _wareHouesDetailCode);//商品单号
                        WarehouseIndetail.date = this.dateTimePicker1.Value;
                        WarehouseIndetail.isClear = 1;
                        WarehouseIndetail.materialDaima = XYEEncoding.strCodeHex(gr["material"].Value.ToString());//商品代码
                        WarehouseIndetail.materiaModel = XYEEncoding.strCodeHex(gr["gridColumnmodel"].Value.ToString());//规格型号
                        WarehouseIndetail.materiaName = XYEEncoding.strCodeHex(gr["gridColumnname"].Value.ToString());//商品名称
                        WarehouseIndetail.materiaUnit = XYEEncoding.strCodeHex(gr["gridColumnunit"].Value.ToString());//单位
                        WarehouseIndetail.money = Convert.ToDecimal(gr["gridColumnmoney"].Value);//金额
                        WarehouseIndetail.number = Convert.ToDecimal(gr["gridColumnnumber"].Value);//数量
                        WarehouseIndetail.price = Convert.ToDecimal(gr["gridColumnprice"].Value);//价格
                        WarehouseIndetail.remark = XYEEncoding.strCodeHex(gr["gridColumnremark"].Value.ToString()); ;//备注
                        WarehouseIndetail.WarehouseCode = XYEEncoding.strCodeHex(StorageCode);//仓库code；
                        WarehouseIndetail.WarehouseName = XYEEncoding.strCodeHex(Storage);//仓库名称
                        WarehouseIndetail.StorageRackCode = XYEEncoding.strCodeHex(StorageRackCode);//货架code
                        WarehouseIndetail.StorageRackName = StorageRackCode == "" ? XYEEncoding.strCodeHex(Storage + "/" + StorageQuyu) : XYEEncoding.strCodeHex(Storage + "/" + StorageQuyu + "/" + StorageRackCode + "/" + StoragePai + "/" + StorageGe);  //货架名称、排、格
                        WarehouseIndetail.productionDate = Convert.ToDateTime(gr["gridColumndate"].Value);//采购或者生产日期
                        WarehouseIndetail.qualityDate = Convert.ToDateTime(gr["gridColumnbaozhe"].Value);//保质期
                        WarehouseIndetail.effectiveDate = Convert.ToDateTime(gr["gridColumnyouxiao"].Value);//有效期至
                        WarehouseIndetail.reserved2 = "";
                        WarehouseIndetail.rfid = "";
                        WarehouseIndetail.rfid = "";
                        WarehouseIndetail.updateDate = nowDataTime;
                        WarehouseIndetail.state = 1;
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
            //int warehouseInResult = warehouseInterface.addWarehouseIn(warehouseIn, wareHouseInList);

        }

        /// <summary>
        /// 后单的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonhou_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 前单的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonqian_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #region 小箭头图标和仓库的选择以及两个表格的点击事件

        //供应商
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (_Click != 1)
            {
                InitSupply();
            }
            _Click = 3;
        }
        //入库员
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (_Click != 2)
            {
                InitEmployee();
            }
            _Click = 4;
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
                gr.Cells["gridColumncode"].Value = dataGridView1.Rows[e.RowIndex].Cells["materialCode"].Value;//商品单号
                gr.Cells["gridColumnname"].Value = dataGridView1.Rows[e.RowIndex].Cells["materialName"].Value;//商品名称
                gr.Cells["gridColumnmodel"].Value = dataGridView1.Rows[e.RowIndex].Cells["materialModel"].Value;//规格型号
                gr.Cells["gridColumnunit"].Value = dataGridView1.Rows[e.RowIndex].Cells["unit"].Value;//单位
                gr.Cells["gridColumntiaoxingma"].Value = dataGridView1.Rows[e.RowIndex].Cells["barCode"].Value;//条码
                gr.Cells["gridColumnprice"].Value = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["discountBeforePrice"].Value);//单价
                decimal number = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["number"].Value);
                decimal price = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["discountBeforePrice"].Value);

                gr.Cells["gridColumnmoney"].Value = number * price;//金额
                gr.Cells["gridColumnundate"].Value = dataGridView1.Rows[e.RowIndex].Cells["productionDate"].Value;//生产日期
                gr.Cells["gridColumnunbaozhe"].Value = dataGridView1.Rows[e.RowIndex].Cells["qualityDate"].Value;//保质期
                gr.Cells["gridColumnunyouxiao"].Value = dataGridView1.Rows[e.RowIndex].Cells["effectiveDate"].Value;//有效期至
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
            try
            {
                //供应商
                if (_Click == 1 || _Click == 3)
                {
                    string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                    _suppliercode = XYEEncoding.strCodeHex(dataGridViewFujia.Rows[e.RowIndex].Cells["code"].Value.ToString());
                    labtextboxTop6.Text = name;
                    resizablePanel1.Visible = false;
                    DataTable dt = ch.DataTableReCoding(supply.GetPurchaseList(_suppliercode));
                    this.comboBoxEx1.DataSource = dt;
                    comboBoxEx1.ValueMember = "code";
                    comboBoxEx1.DisplayMember = "name";
                    string phone = dataGridViewFujia.Rows[e.RowIndex].Cells["mobilePhone"].Value.ToString();
                    labtextboxTop9.Text = phone;//联系电话
                }
                //入库员
                if (_Click == 2 || _Click == 4)
                {
                    string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                    labtextboxBotton1.Text = name;
                    resizablePanel1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("双击绑定供应商或者入库员数据错误！请检查："+ex.Message);
            }
        
        }

        /// <summary>
        /// 选择仓库和货架的按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gdiec_ButtonCustomClick(object sender, EventArgs e)
        {
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

        #endregion

        /// <summary>
        /// 第一列编辑的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
        {
            if (this.comboBoxEx1.Text.Trim() == "")
            {
                MessageBox.Show("请先选择供应商，显示采购单号!");
                return;
            }
            if (e.GridCell.GridColumn.Name == "material")
            {
                SelectedElementCollection ge = superGridControl1.PrimaryGrid.GetSelectedCells();
                GridCell gc = ge[0] as GridCell;
                if (gc.GridRow.Cells[material].Value != null && (gc.GridRow.Cells[material].Value).ToString() != "")
                {
                    //模糊查询商品列表
                    _AllMaterial = pdi.GetList("" + XYEEncoding.strCodeHex(this.comboBoxEx1.Text.Trim() + ""), "" + XYEEncoding.strCodeHex(gc.GridRow.Cells[material].Value.ToString()) + "");
                    InitMaterialDataGridView();
                }
                else
                {
                    //绑定商品列表
                    _AllMaterial = pdi.GetList("" + XYEEncoding.strCodeHex(this.comboBoxEx1.Text.Trim() + ""), "");
                    InitMaterialDataGridView();
                }
                dataGridView1.DataSource = ch.DataTableReCoding(_AllMaterial);
            }
        }

        #region  初始化数据

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
            dgvc.Name = "productionDate";
            dgvc.Visible = false;
            dgvc.HeaderText = "productionDate";
            dgvc.DataPropertyName = "productionDate";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "qualityDate";
            dgvc.Visible = false;
            dgvc.HeaderText = "qualityDate";
            dgvc.DataPropertyName = "qualityDate";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "effectiveDate";
            dgvc.Visible = false;
            dgvc.HeaderText = "effectiveDate";
            dgvc.DataPropertyName = "effectiveDate";
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
        /// 非空验证
        /// </summary>
        private void isNUllValidate()
        {
            if (comboBoxEx2.Text.Trim() == null)
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

            }
        }

        /// <summary>
        /// 初始化供应商
        /// </summary>
        private void InitSupply()
        {
            if (_Click != 1)
            {
                _Click = 1;
                dataGridViewFujia.DataSource = null;
                dataGridViewFujia.Columns.Clear();

                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "code";
                dgvc.HeaderText = "编码";
                dgvc.DataPropertyName = "编码";
                dataGridViewFujia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "name";
                dgvc.HeaderText = "单位名称";
                dgvc.DataPropertyName = "单位名称";
                dataGridViewFujia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();

                dgvc.Name = "mobilePhone";
                dgvc.Visible = false;
                dgvc.HeaderText = "联系手机";
                dgvc.DataPropertyName = "联系手机";
                dataGridViewFujia.Columns.Add(dgvc);

                resizablePanel1.Location = new Point(550, 160);
                dataGridViewFujia.DataSource = ch.DataTableReCoding(_AllSupply);
            }
        }

        #endregion

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

        /// <summary>
        /// 实时摸查询的
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_EditorValueChanged(object sender, GridEditEventArgs e)
        {
            string SS = "";
            GridRow gr = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
            string materialDaima = XYEEncoding.strCodeHex(e.EditControl.EditorValue.ToString());
            if (SS == "")
            {
                //模糊查询商品列表
                _AllMaterial = pdi.GetList("" + XYEEncoding.strCodeHex(this.comboBoxEx1.Text.Trim() + ""), "" + materialDaima + "");
                InitMaterialDataGridView();
                dataGridView1.DataSource = ch.DataTableReCoding(_AllMaterial);
            }
        }

        /// <summary>
        /// 按下ESC关闭子窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WareHouseInMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.resizablePanel1.Visible = false;
            }
        }

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

        /// <summary>
        /// 供应商模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labtextboxTop6_TextChanged(object sender, EventArgs e)
        {

            if (labtextboxTop6.Text.Trim() == "")
            {
                InitSupply();
                return;
            }
            dataGridViewFujia.DataSource = null;
            dataGridViewFujia.Columns.Clear();

            DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "code";
            dgvc.HeaderText = "编码";
            dgvc.DataPropertyName = "code";
            dataGridViewFujia.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "name";
            dgvc.HeaderText = "单位名称";
            dgvc.DataPropertyName = "name";
            dataGridViewFujia.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "mobilePhone";
            dgvc.Visible = false;
            dgvc.HeaderText = "联系手机";
            dgvc.DataPropertyName = "mobilePhone";
            dataGridViewFujia.Columns.Add(dgvc);

            resizablePanel1.Location = new Point(550, 160);
            dataGridViewFujia.DataSource = ch.DataTableReCoding(supply.GetList(0, "" + XYEEncoding.strCodeHex(labtextboxTop6.Text.Trim()) + ""));
            resizablePanel1.Visible = true;
            _Click = 3;
        }

        /// <summary>
        /// 入库员模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labtextboxBotton1_TextChanged(object sender, EventArgs e)
        {
            if (labtextboxBotton1.Text.Trim() == "")
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
            dataGridViewFujia.DataSource = ch.DataTableReCoding(employee.GetList(0, "" + XYEEncoding.strCodeHex(labtextboxBotton1.Text.Trim()) + ""));
            resizablePanel1.Visible = true;
        }
    }
}
