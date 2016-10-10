using DevComponents.DotNetBar.SuperGrid;
using HelperUtility;
using HelperUtility.Encrypt;
using InterfaceLayer.Base;
using InterfaceLayer.Purchase;
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
        private string _StorgeCode;
        public string StorageCode
        {
            get { return _StorgeCode; }
            set { _StorgeCode = value; }
        }
        /// <summary>
        /// 仓库名称
        /// </summary>
        private string _Storage;
        public string Storage
        {
            get { return _Storage; }
            set { _Storage = value; }
        }
        /// <summary>
        /// 货架code
        /// </summary>
        private string _StorageRackCode;
        public string StorageRackCode
        {
            get { return _StorageRackCode; }
            set { _StorageRackCode = value; }
        }
        //货架名称
        private string _StorageRack;
        public string StorageRack
        {
            get { return _StorageRack; }
            set { _StorageRack = value; }
        }
        /// <summary>
        /// 排code
        /// </summary>
        private string _StoragePaiCode;
        public string StoragePaiCode
        {
            get { return _StoragePaiCode; }
            set { _StoragePaiCode = value; }
        }
        /// <summary>
        /// 排名称
        /// </summary>
        private string _StoragePai;
        public string StoragePai
        {
            get { return _StoragePai; }
            set { _StoragePai = value; }
        }
        /// <summary>
        /// 格子code
        /// </summary>
        private string _StorageGeCode;
        public string StorageGeCode
        {
            get { return _StorageGeCode; }
            set { _StorageGeCode = value; }
        }
        /// <summary>
        /// 格子名称
        /// </summary>
        private string _StorageGe;
        public string StorageGe
        {
            get { return _StorageGe; }
            set { _StorageGe = value; }
        }
        /// <summary>
        /// 区域code
        /// </summary>
        private string _StorageQuyuCode;
        public string StorageQuyuCode
        {
            get { return _StorageQuyuCode; }
            set { _StorageQuyuCode = value; }
        }
        /// <summary>
        /// 区域名称
        /// </summary>
        private string _StorageQuyu;
        public string StorageQuyu
        {
            get { return _StorageQuyu; }
            set { _StorageQuyu = value; }
        }
        /// <summary>
        /// 定义显示类型 0,待入库的 1、部分入库 2、已入库的
        /// </summary>
        public int State
        {
            get { return _State; }
            set { _State = value; }
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
        private int _State;
        /// <summary>
        /// 保存仓库商品明细
        /// </summary>
        private GridRow _wareHouseModel;
        private decimal _MaterialNumber;
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
        private string _SupplierCode;
        /// <summary>
        /// 入库code
        /// </summary>
        private string _WareHouseInCode;
        /// <summary>
        /// 计算数量
        /// </summary>
        private decimal _BeforeNumber;
        #endregion

        private void WareHouseInMain_Load(object sender, EventArgs e)
        {
            try
            {
                //供应商
                _AllSupply = supply.GetList(99, "");

                //业务员
                _AllEmployee = employee.SelSupplierTable(false);

                //仓库
                GridTextBoxXEditControl gdieccangku = superGridControlShangPing.PrimaryGrid.Columns["griCoulumcangku"].EditControl as GridTextBoxXEditControl;
                gdieccangku.ButtonCustom.Visible = true;
                gdieccangku.ButtonCustomClick += Gdiec_ButtonCustomClick;

                //数量
                GridDoubleInputEditControl gdiecNumber = superGridControlShangPing.PrimaryGrid.Columns["gridColumnnumber"].EditControl as GridDoubleInputEditControl;
                gdiecNumber.MinValue = 0;
                gdiecNumber.MaxValue = 999999999;
                //货架
                GridTextBoxXEditControl gdiehuojia = superGridControlShangPing.PrimaryGrid.Columns["griCoulumhuojia"].EditControl as GridTextBoxXEditControl;
                gdiehuojia.ButtonCustom.Visible = true;
                gdiehuojia.ButtonCustomClick += Gdiec_ButtonCustomClick;
                ///前单
                toolStripBtnQianDan.Click += toolStripBtnQianDan_Click;
                //后单
                toolStripBtnHouDan.Click += toolStripBtnHouDan_Click;
                //保存
                toolStripBtnSave.Click += toolStripBtnSave_Click;
                //审核
                toolStripBtnShengHe.Click += toolStripBtnShengHe_Click;
                //禁用自动创建列
                dataGridViewShangPing.AutoGenerateColumns = false;
                dataGridViewFuJia.AutoGenerateColumns = false;
                superGridControlShangPing.HScrollBarVisible = true;
                //绑定事件 双击事填充内容并隐藏列表
                dataGridViewFuJia.CellDoubleClick += dataGridViewFuJia_CellDoubleClick;
                dataGridViewShangPing.CellDoubleClick += dataGridViewShangPing_CellDoubleClick;
                // 将dataGridView中的内容居中显示
                dataGridViewFuJia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //调用合计行数据
                InitDataGridView();
                cboInType.SelectedIndex = 0;
                //生成code 和显示条形码

                _WareHouseInCode = BuildCode.ModuleCode("WHI");
                textBoxOddNumbers.Text = _WareHouseInCode;
                barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
                _Code.ValueFont = new Font("微软雅黑", 20);
                System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
                pictureBoxBarCode.Image = imgTemp;
                picBoxShengHeIn.Parent = pictureBoxtitle;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2101-初始化数据失败" + ex.Message, "入库单温馨提示");
            }

        }

        /// <summary>
        /// 审核按钮的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripBtnShengHe_Click(object sender, EventArgs e)
        {
            //非空验证
            if (isNUllValidate() == false)
            {
                return;
            }
            //获得界面上的数据,准备传给base层新增数据
            WarehouseInInterface warehouseInterface = new WarehouseInInterface();
            //入库单
            WarehouseIn warehouseIn = new WarehouseIn();
            //入库商品列表
            List<WarehouseInDetail> wareHouseInList = new List<WarehouseInDetail>();
            try
            {
                warehouseIn.code = XYEEncoding.strCodeHex(_WareHouseInCode);//入库单号
                warehouseIn.isClear = 1;
                warehouseIn.purchaseCode = XYEEncoding.strCodeHex(this.cboPurchaseCode.Text);//采购单号
                warehouseIn.date = this.dateTimePicker1.Value;//开单日期
                warehouseIn.checkState = 1;
                warehouseIn.supplierCode = _SupplierCode;//供应商code
                warehouseIn.supplierName = XYEEncoding.strCodeHex(labtextboxTop6.Text);//供应商名称
                warehouseIn.supplierPhone = XYEEncoding.strCodeHex(labtextboxTop9.Text);//采购电话
                warehouseIn.examine = XYEEncoding.strCodeHex(ltxtbShengHeMan.Text);//审核人
                warehouseIn.operation = XYEEncoding.strCodeHex(ltxtbSalsMan.Text);//入库员
                warehouseIn.makeMan = XYEEncoding.strCodeHex(ltxtbMakeMan.Text);//制单人
                warehouseIn.remark = XYEEncoding.strCodeHex(labtextboxBotton2.Text);//摘要
                warehouseIn.state = 0;
                warehouseIn.reserved1 = "";
                warehouseIn.reserved2 = "";
                warehouseIn.type = XYEEncoding.strCodeHex(cboInType.Text);//入货类别
                warehouseIn.updateDate = DateTime.Now;
                warehouseIn.defaultType = XYEEncoding.strCodeHex("入库开单");
                warehouseIn.goodsCode = "";
                warehouseIn.stock = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:2102;尝试创建入库单数据出错,请检查输入" + ex.Message, "入库单温馨提示");
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
                    if (gr["gridColumnname"].Value != null)
                    {
                        i++;
                        WarehouseInDetail WarehouseIndetail = new WarehouseInDetail();
                        WarehouseIndetail.mainCode = XYEEncoding.strCodeHex(this.textBoxOddNumbers.Text);//单据入库单号
                        WarehouseIndetail.barcode = XYEEncoding.strCodeHex(gr["gridColumntiaoxingma"].Value.ToString());//条形码
                        WarehouseIndetail.code = XYEEncoding.strCodeHex(_WareHouseInCode + i.ToString());//入库明细的商品入库单
                        WarehouseIndetail.date = this.dateTimePicker1.Value;
                        WarehouseIndetail.isClear = 1;
                        WarehouseIndetail.materialDaima = XYEEncoding.strCodeHex(gr["material"].Value.ToString());//商品代码
                        WarehouseIndetail.materiaModel = XYEEncoding.strCodeHex(gr["gridColumnmodel"].Value.ToString());//规格型号
                        WarehouseIndetail.materiaName = XYEEncoding.strCodeHex(gr["gridColumnname"].Value.ToString());//商品名称
                        WarehouseIndetail.materiaUnit = XYEEncoding.strCodeHex(gr["gridColumnunit"].Value.ToString());//单位
                        WarehouseIndetail.money = Convert.ToDecimal(gr["gridColumnmoney"].Value);//金额
                        WarehouseIndetail.number = Convert.ToDecimal(gr["gridColumnnumber"].Value);//数量
                        WarehouseIndetail.price = Convert.ToDecimal(gr["gridColumnprice"].Value);//价格
                        WarehouseIndetail.remark = XYEEncoding.strCodeHex(gr["gridColumnremark"].Value == null ?
                             "" : gr["gridColumnremark"].Value.ToString());//备注
                        WarehouseIndetail.warehouseCode = XYEEncoding.strCodeHex(StorageCode);//仓库code；
                        WarehouseIndetail.warehouseName = XYEEncoding.strCodeHex(Storage);//仓库名称
                        WarehouseIndetail.storageRackCode = XYEEncoding.strCodeHex(StorageRackCode);//货架code
                        WarehouseIndetail.storageRackName = StorageRackCode == "" ? XYEEncoding.strCodeHex(Storage + "/" + StorageQuyu) : XYEEncoding.strCodeHex(Storage + "/" + StorageQuyu + "/" + StorageRackCode + "/" + StoragePai + "/" + StorageGe);  //货架名称、排、格
                        WarehouseIndetail.productionDate = Convert.ToDateTime(gr["gridColumndate"].Value.ToString());//采购或者生产日期
                        WarehouseIndetail.qualityDate = Convert.ToDouble(gr["gridColumnbaozhe"].Value.ToString());//保质期
                        WarehouseIndetail.effectiveDate = Convert.ToDateTime(gr["gridColumnyouxiao"].Value.ToString());//有效期至
                        WarehouseIndetail.reserved2 = "";
                        WarehouseIndetail.reserved1 = "";
                        WarehouseIndetail.rfid = "";
                        WarehouseIndetail.updateDate = nowDataTime;
                        WarehouseIndetail.state = 0;
                        WarehouseIndetail.isArrive = 1;
                        WarehouseIndetail.zhujima = "";//暂时为空
                        GridRow dr = superGridControlShangPing.PrimaryGrid.Rows[0] as GridRow;
                        wareHouseInList.Add(WarehouseIndetail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2103-尝试创建入库单详细商品数据出错,请检查输入" + ex.Message, "入库单温馨提示");
                return;
            }

            //增加一条入库单和入库单详细数据
            int warehouseInResult = warehouseInterface.updateByCode(warehouseIn, wareHouseInList);
            this.textBoxid.Text = warehouseInResult.ToString();
            if (warehouseInResult > 0)
            {
                MessageBox.Show("保存和审核入库数据成功");
                InitForm();
                this.picBoxShengHeIn.Image = Properties.Resources.审核;
            }
        }

        /// <summary>
        /// 保存按钮的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripBtnSave_Click(object sender, EventArgs e)
        {
            //非空验证
            if (isNUllValidate() == false)
            {
                return;
            }
            //获得界面上的数据,准备传给base层新增数据
            //string warehouseIncode = "";
            WarehouseInInterface warehouseInterface = new WarehouseInInterface();
            //入库单
            WarehouseIn warehouseIn = new WarehouseIn();
            //入库商品列表
            List<WarehouseInDetail> wareHouseInList = new List<WarehouseInDetail>();
            try
            {
                warehouseIn.code = XYEEncoding.strCodeHex(_WareHouseInCode);//入库单号
                warehouseIn.isClear = 1;
                warehouseIn.purchaseCode = XYEEncoding.strCodeHex(this.cboPurchaseCode.Text);//采购单号
                warehouseIn.date = this.dateTimePicker1.Value;//开单日期
                warehouseIn.checkState = 0;
                warehouseIn.supplierCode = _SupplierCode;//供应商code
                warehouseIn.supplierName = XYEEncoding.strCodeHex(labtextboxTop6.Text);//供应商名称
                warehouseIn.supplierPhone = XYEEncoding.strCodeHex(labtextboxTop9.Text);//采购电话
                warehouseIn.examine = XYEEncoding.strCodeHex(ltxtbShengHeMan.Text);//审核人
                warehouseIn.operation = XYEEncoding.strCodeHex(ltxtbSalsMan.Text);//入库员
                warehouseIn.makeMan = XYEEncoding.strCodeHex(ltxtbMakeMan.Text);//制单人
                warehouseIn.remark = XYEEncoding.strCodeHex(labtextboxBotton2.Text);//摘要
                warehouseIn.state = 0;
                warehouseIn.reserved1 = "";
                warehouseIn.reserved2 = "";
                warehouseIn.type = XYEEncoding.strCodeHex(cboInType.Text);//入货类别
                warehouseIn.updateDate = DateTime.Now;
                warehouseIn.defaultType = XYEEncoding.strCodeHex("入库开单");
                warehouseIn.goodsCode = "";
                warehouseIn.stock = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:2104;尝试创建入库单商品数据出错,请检查输入" + ex.Message, "入库单温馨提示");
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
                    if (gr["gridColumnname"].Value != null)
                    {

                        i++;
                        WarehouseInDetail WarehouseIndetail = new WarehouseInDetail();
                        WarehouseIndetail.mainCode = XYEEncoding.strCodeHex(this.textBoxOddNumbers.Text);//单据入库单号
                        WarehouseIndetail.barcode = XYEEncoding.strCodeHex(gr["gridColumntiaoxingma"].Value.ToString());//条形码
                        WarehouseIndetail.code = XYEEncoding.strCodeHex(_WareHouseInCode + i.ToString());//入库明细的商品入库单
                        WarehouseIndetail.date = this.dateTimePicker1.Value;
                        WarehouseIndetail.isClear = 1;
                        WarehouseIndetail.materialDaima = XYEEncoding.strCodeHex(gr["material"].Value.ToString());//商品代码
                        WarehouseIndetail.materiaModel = XYEEncoding.strCodeHex(gr["gridColumnmodel"].Value.ToString());//规格型号
                        WarehouseIndetail.materiaName = XYEEncoding.strCodeHex(gr["gridColumnname"].Value.ToString());//商品名称
                        WarehouseIndetail.materiaUnit = XYEEncoding.strCodeHex(gr["gridColumnunit"].Value.ToString());//单位
                        WarehouseIndetail.money = Convert.ToDecimal(gr["gridColumnmoney"].Value);//金额
                        WarehouseIndetail.number = Convert.ToDecimal(gr["gridColumnnumber"].Value);//数量
                        WarehouseIndetail.price = Convert.ToDecimal(gr["gridColumnprice"].Value);//价格
                        WarehouseIndetail.remark = XYEEncoding.strCodeHex(gr["gridColumnremark"].Value == null ?
                             "" : gr["gridColumnremark"].Value.ToString());//备注
                        WarehouseIndetail.warehouseCode = XYEEncoding.strCodeHex(StorageCode);//仓库code；
                        WarehouseIndetail.warehouseName = XYEEncoding.strCodeHex(Storage);//仓库名称
                        WarehouseIndetail.storageRackCode = XYEEncoding.strCodeHex(StorageRackCode);//货架code
                        WarehouseIndetail.storageRackName = StorageRackCode == "" ? XYEEncoding.strCodeHex(Storage + "/" + StorageQuyu) : XYEEncoding.strCodeHex(Storage + "/" + StorageQuyu + "/" + StorageRackCode + "/" + StoragePai + "/" + StorageGe);  //货架名称、排、格
                        WarehouseIndetail.productionDate = Convert.ToDateTime(gr["gridColumndate"].Value.ToString());//采购或者生产日期
                        WarehouseIndetail.qualityDate = Convert.ToDouble(gr["gridColumnbaozhe"].Value.ToString());//保质期
                        WarehouseIndetail.effectiveDate = Convert.ToDateTime(gr["gridColumnyouxiao"].Value.ToString());//有效期至
                        WarehouseIndetail.reserved2 = "";
                        WarehouseIndetail.reserved1 = "";
                        WarehouseIndetail.rfid = "";
                        WarehouseIndetail.updateDate = nowDataTime;
                        WarehouseIndetail.state = 1;
                        WarehouseIndetail.isArrive = 1;
                        WarehouseIndetail.zhujima = "";//暂时为空
                        GridRow dr = superGridControlShangPing.PrimaryGrid.Rows[0] as GridRow;
                        wareHouseInList.Add(WarehouseIndetail);


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2105-尝试创建入库单详商品数据出错,请检查输入" + ex.Message, "入库单温馨提示");
                return;
            }

            //增加一条入库单和入库单详细数据
            object warehouseInResult = warehouseInterface.AddWarehouseOrToDetail(warehouseIn, wareHouseInList);
            this.textBoxid.Text = warehouseInResult.ToString();
            if (warehouseInResult != null)
            {
                MessageBox.Show("新增入库数据成功", "入库单温馨提示");
            }
        }

        /// <summary>
        /// 后单的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripBtnHouDan_Click(object sender, EventArgs e)
        {
            WarehouseInInterface warehouseInterface = new WarehouseInInterface();
            try
            {
                if (textBoxid.Text == "")
                {
                    textBoxid.Text = "0";
                }

                WarehouseIn warehouseIn = warehouseInterface.GetPreAndNext(Convert.ToInt32(textBoxid.Text), 0);
                this.cboInType.Text = XYEEncoding.strHexDecode(warehouseIn.type);
                this.labtextboxTop6.Text = XYEEncoding.strHexDecode(warehouseIn.supplierName);
                this.textBoxOddNumbers.Text = XYEEncoding.strHexDecode(warehouseIn.code);
                this.cboPurchaseCode.Text = XYEEncoding.strHexDecode(warehouseIn.purchaseCode);
                this.labtextboxTop9.Text = XYEEncoding.strHexDecode(warehouseIn.supplierPhone);
                this.ltxtbSalsMan.Text = XYEEncoding.strHexDecode(warehouseIn.operation);
                this.ltxtbMakeMan.Text = XYEEncoding.strHexDecode(warehouseIn.makeMan);
                this.ltxtbShengHeMan.Text = XYEEncoding.strHexDecode(warehouseIn.examine);
                if (warehouseIn.id != 0)
                {
                    textBoxid.Text = warehouseIn.id.ToString();
                    WarehouseInDetailInterface widif = new WarehouseInDetailInterface();
                    superGridControlShangPing.PrimaryGrid.DataSource = null;
                    superGridControlShangPing.PrimaryGrid.Columns.Clear();
                    superGridControlShangPing.PrimaryGrid.AutoGenerateColumns = false;
                    DataTable dt = ch.DataTableReCoding(widif.getListByMainCode(XYEEncoding.strCodeHex(textBoxOddNumbers.Text)).Tables[0]);
                    superGridControlShangPing.PrimaryGrid.DataSource = dt;
                    InitWarehouseDetail();
                    InitForm();
                }
                barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
                _Code.ValueFont = new Font("微软雅黑", 20);
                System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
                pictureBoxBarCode.Image = imgTemp;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2106-尝试点击后单数据错误" + ex.Message, "入库单温馨提示");
            }
        }
        /// <summary>
        /// 前单的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripBtnQianDan_Click(object sender, EventArgs e)
        {
            WarehouseInInterface warehouseInterface = new WarehouseInInterface();
            try
            {
                if (textBoxid.Text == "")
                {
                    textBoxid.Text = "0";
                }
                WarehouseIn warehouseIn = warehouseInterface.GetPreAndNext(Convert.ToInt32(textBoxid.Text), 1);
                this.cboInType.Text = XYEEncoding.strHexDecode(warehouseIn.type);
                this.labtextboxTop6.Text = XYEEncoding.strHexDecode(warehouseIn.supplierName);
                this.textBoxOddNumbers.Text = XYEEncoding.strHexDecode(warehouseIn.code);
                this.cboPurchaseCode.Text = XYEEncoding.strHexDecode(warehouseIn.purchaseCode);
                this.labtextboxTop9.Text = XYEEncoding.strHexDecode(warehouseIn.supplierPhone);
                this.ltxtbSalsMan.Text = XYEEncoding.strHexDecode(warehouseIn.operation);
                this.ltxtbMakeMan.Text = XYEEncoding.strHexDecode(warehouseIn.makeMan);
                this.ltxtbShengHeMan.Text = XYEEncoding.strHexDecode(warehouseIn.examine);
                if (warehouseIn.id != 0)
                {
                    textBoxid.Text = warehouseIn.id.ToString();
                    WarehouseInDetailInterface widif = new WarehouseInDetailInterface();
                    superGridControlShangPing.PrimaryGrid.DataSource = null;
                    superGridControlShangPing.PrimaryGrid.Columns.Clear();
                    superGridControlShangPing.PrimaryGrid.AutoGenerateColumns = false;
                    DataTable dt = ch.DataTableReCoding(widif.getListByMainCode(XYEEncoding.strCodeHex(this.textBoxOddNumbers.Text)).Tables[0]);
                    superGridControlShangPing.PrimaryGrid.DataSource = dt;
                    InitWarehouseDetail();
                    InitForm();

                }
                barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
                _Code.ValueFont = new Font("微软雅黑", 20);
                System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
                pictureBoxBarCode.Image = imgTemp;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2107-查询点击前单数据错误" + ex.Message, "入库单温馨提示");
            }

        }

        #region 小箭头图标和仓库的选择以及两个表格的点击事件

        //供应商
        private void pictureBoxSupply_Click(object sender, EventArgs e)
        {
            if (_Click != 1)
            {
                InitSupply();
            }
            _Click = 3;
        }
        //入库员
        private void pictureBoxEmployee_Click(object sender, EventArgs e)
        {
            if (_Click != 2)
            {
                InitEmployee();
            }
            _Click = 4;
        }

        private void dataGridViewShangPing_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            try
            {
                //是否要新增一行的标记
                bool newAdd = false;
                GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[superGridControlShangPing.PrimaryGrid.Rows.Count - 2];

                GridItemsCollection grs = superGridControl1.PrimaryGrid.Rows;
                //id字段为空 说明是没有数据的行 不是修改而是新增
                if (gr.Cells["gridColumnid"].Value == null)
                {
                    newAdd = true;
                }
                foreach (GridRow g in grs)
                {
                    if (g.Cells["gridColumncode"].Value==null)
                    {
                        break;
                    }
                    if (g.Cells["gridColumncode"].Value.Equals(dataGridView1.Rows[e.RowIndex].Cells["materialCode"].Value))
                    {
                        decimal shuliang = Convert.ToDecimal(g.Cells["gridColumnnumber"].Value);
                        shuliang += 1;
                        g.Cells["gridColumnnumber"].Value = shuliang;
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
                        resizablePanelData.Visible = false;
                        return;
                    }
                    continue;
                }
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
                gr.Cells["gridColumnnumber"].Value = 1.ToString();
                _BeforeNumber = Convert.ToDecimal( gr.Cells["gridColumnnumber"].Value);
                gr.Cells["gridColumndate"].Value = dataGridView1.Rows[e.RowIndex].Cells["productionDate"].Value.ToString();//生产日期
                gr.Cells["gridColumnbaozhe"].Value = dataGridView1.Rows[e.RowIndex].Cells["qualityDate"].Value.ToString();//保质期
                gr.Cells["gridColumnyouxiao"].Value = dataGridView1.Rows[e.RowIndex].Cells["effectiveDate"].Value;//有效期至
                resizablePanelData.Visible = false;
                //新增一行 
                if (newAdd)
                {
                    superGridControlShangPing.PrimaryGrid.NewRow(superGridControlShangPing.PrimaryGrid.Rows.Count);
                    //递增数量和金额 默认为1和单价 
                    gr = (GridRow)superGridControlShangPing.PrimaryGrid.LastSelectableRow;
                    _Materialnumber += _BeforeNumber;
                    gr.Cells["gridColumnnumber"].Value = _Materialnumber;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:2108-尝试连接物料绑定数据错误！" + ex.Message);
            }
            //SendKeys.Send("^{End}{Home}");
        }

        private void dataGridViewFuJia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //供应商
                if (_Click == 1 || _Click == 3)
                {
                    string name = dataGridViewFuJia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                    _SupplierCode = XYEEncoding.strCodeHex(dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString());
                    labtextboxTop6.Text = name;
                    resizablePanel1.Visible = false;
                    DataTable dt = ch.DataTableReCoding(supply.GetPurchaseList(_SupplierCode));
                    this.cboPurchaseCode.DataSource = dt;
                    cboPurchaseCode.ValueMember = "code";
                    cboPurchaseCode.DisplayMember = "name";
                    dataGridViewFuJia.DataSource = _AllSupply;
                    string phone = XYEEncoding.strHexDecode(dataGridViewFuJia.Rows[e.RowIndex].Cells["mobilePhone"].Value.ToString());
                    labtextboxTop9.Text = phone;//联系电话
                }
                //入库员
                if (_Click == 2 || _Click == 4)
                {
                    string name = dataGridViewFuJia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                    ltxtbSalsMan.Text = name;
                    resizablePanel1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2109-双击绑定供应商或者入库员数据错误！请检查：" + ex.Message);
            }
        }

        /// <summary>
        /// 选择仓库和货架的按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gdiec_ButtonCustomClick(object sender, EventArgs e)
        {
            SelectedElementCollection ge = superGridControlShangPing.PrimaryGrid.GetSelectedCells();
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

        #region  初始化数据

        /// <summary>
        /// 初始化商品下拉别表的数据
        /// </summary>
        private void InitMaterialDataGridView()
        {
            dataGridViewShangPing.DataSource = null;
            dataGridViewShangPing.Columns.Clear();
            DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "id";
            dgvc.Visible = false;
            dgvc.HeaderText = "maid";
            dgvc.DataPropertyName = "id";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "isClear";
            dgvc.Visible = false;
            dgvc.HeaderText = "clear";
            dgvc.DataPropertyName = "isClear";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "code";
            dgvc.Visible = false;
            dgvc.HeaderText = "code";
            dgvc.DataPropertyName = "code";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "PurchaseCode";
            dgvc.Visible = false;
            dgvc.HeaderText = "PurchaseCode";
            dgvc.DataPropertyName = "PurchaseCode";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "storageCode";
            dgvc.Visible = false;
            dgvc.HeaderText = "storageCode";
            dgvc.DataPropertyName = "storageCode";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "storageName";
            dgvc.Visible = false;
            dgvc.HeaderText = "storageName";
            dgvc.DataPropertyName = "storageName";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "materialCode";
            dgvc.Visible = false;
            dgvc.HeaderText = "materialCode";
            dgvc.DataPropertyName = "materialCode";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "unit";
            dgvc.Visible = false;
            dgvc.HeaderText = "unit";
            dgvc.DataPropertyName = "unit";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "number";
            dgvc.Visible = false;
            dgvc.HeaderText = "number";
            dgvc.DataPropertyName = "number";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "discountBeforePrice";
            dgvc.Visible = false;
            dgvc.HeaderText = "discountBeforePrice";
            dgvc.DataPropertyName = "discountBeforePrice";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "discount";
            dgvc.Visible = false;
            dgvc.HeaderText = "discount";
            dgvc.DataPropertyName = "discount";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "discountAfterPrice";
            dgvc.Visible = false;
            dgvc.HeaderText = "discountAfterPrice";
            dgvc.DataPropertyName = "discountAfterPrice";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "money";
            dgvc.Visible = false;
            dgvc.HeaderText = "money";
            dgvc.DataPropertyName = "money";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "remark";
            dgvc.Visible = false;
            dgvc.HeaderText = "remark";
            dgvc.DataPropertyName = "remark";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "updateDate";
            dgvc.Visible = false;
            dgvc.HeaderText = "updateDate";
            dgvc.DataPropertyName = "updateDate";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "reserved1";
            dgvc.Visible = false;
            dgvc.HeaderText = "reserved1";
            dgvc.DataPropertyName = "reserved1";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "reserved2";
            dgvc.Visible = false;
            dgvc.HeaderText = "reserved2";
            dgvc.DataPropertyName = "reserved2";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "productionDate";
            dgvc.Visible = false;
            dgvc.HeaderText = "productionDate";
            dgvc.DataPropertyName = "productionDate";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "qualityDate";
            dgvc.Visible = false;
            dgvc.HeaderText = "qualityDate";
            dgvc.DataPropertyName = "qualityDate";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "effectiveDate";
            dgvc.Visible = false;
            dgvc.HeaderText = "effectiveDate";
            dgvc.DataPropertyName = "effectiveDate";
            dataGridViewShangPing.Columns.Add(dgvc);


            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "zhujima";
            dgvc.Visible = true;
            dgvc.HeaderText = "助记码";
            dgvc.DataPropertyName = "zhujima";
            dataGridViewShangPing.Columns.Add(dgvc);


            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "materialName";
            dgvc.Visible = true;
            dgvc.HeaderText = "商品名称";
            dgvc.DataPropertyName = "materialName";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "materialModel";
            dgvc.Visible = true;
            dgvc.HeaderText = "规格型号";
            dgvc.DataPropertyName = "materialModel";
            dataGridViewShangPing.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "barCode";
            dgvc.Visible = true;
            dgvc.HeaderText = "条形码";
            dgvc.DataPropertyName = "barCode";
            dataGridViewShangPing.Columns.Add(dgvc);
        }

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
            gr.Cells["gridColumnnumber"].Value = 0;
            gr.Cells["gridColumnnumber"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnnumber"].CellStyles.Default.Background.Color1 = Color.Orange;
        }

        /// <summary>
        /// 非空验证
        /// </summary>
        private bool isNUllValidate()
        {
            if (cboInType.Text.Trim() == null)
            {
                MessageBox.Show("入货类型不能为空！");
                return false;
            }
            if (labtextboxTop6.Text.Trim() == null || labtextboxTop6.Text == "")
            {
                MessageBox.Show("供应商不能为空！");
                return false;
            }
            GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[0];
            if (gr.Cells["material"].Value == null || gr.Cells["material"].Value.ToString() == "")
            {
                MessageBox.Show("商品代码不能为空！");
                return false;
            }
            if (gr.Cells["griCoulumcangku"].Value == null || gr.Cells["griCoulumcangku"].Value.ToString() == "")
            {
                MessageBox.Show("仓库不能为空！");
                return false;
            }
            if (gr.Cells["griCoulumhuojia"].Value == null || gr.Cells["griCoulumhuojia"].Value.ToString() == "")
            {
                MessageBox.Show("货架不能为空！");
                return false;
            }
            if (ltxtbSalsMan.Text.Trim() == null || ltxtbSalsMan.Text == "")
            {
                MessageBox.Show("入库员不能为空！");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 初始化业务员
        /// </summary>
        private void InitEmployee()
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
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "name";
                dgvc.HeaderText = "姓名";
                dgvc.DataPropertyName = "姓名";
                dataGridViewFuJia.Columns.Add(dgvc);

                resizablePanel1.Location = new Point(234, 440);
                dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllEmployee);
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
        }

        /// <summary>
        /// 初始化供应商
        /// </summary>
        private void InitSupply()
        {
            if (_Click != 1)
            {
                _Click = 1;
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
                dgvc.Visible = false;
                dgvc.HeaderText = "联系手机";
                dgvc.DataPropertyName = "mobilePhone";
                dataGridViewFuJia.Columns.Add(dgvc);

                resizablePanel1.Location = new Point(550, 160);
                dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllSupply);
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
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "materiaName";
            gc.Name = "materiaName";
            gc.HeaderText = "商品名称";
            gc.Width = 140;
            gc.AutoSizeMode = ColumnAutoSizeMode.Fill;
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "materiaModel";
            gc.Name = "materiaModel";
            gc.HeaderText = "规格型号";
            gc.Width = 130;
            gc.AutoSizeMode = ColumnAutoSizeMode.Fill;
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "barcode";
            gc.Name = "barcode";
            gc.HeaderText = "条形码";
            gc.Width = 150;
            gc.AutoSizeMode = ColumnAutoSizeMode.Fill;
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "materiaUnit";
            gc.Name = "materiaUnit";
            gc.HeaderText = "单位";
            gc.Width = 70;
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "number";
            gc.Name = "number";
            gc.HeaderText = "数量";
            gc.Width = 80;
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "warehouseName";
            gc.Name = "warehouseName";
            gc.HeaderText = "仓库";
            gc.Width = 80;
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "storageRackName";
            gc.Name = "storageRackName";
            gc.HeaderText = "区域/排/行/列";
            gc.Width = 80;
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "productionDate";
            gc.Name = "productionDate";
            gc.HeaderText = "采购/生产日期";
            gc.Width = 70;
            gc.HeaderStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "qualityDate";
            gc.Name = "qualityDate";
            gc.HeaderText = "保质期(天)";
            gc.Width = 50;
            gc.HeaderStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "effectiveDate";
            gc.Name = "effectiveDate";
            gc.HeaderText = "有效期至";
            gc.Width = 80;
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "remark";
            gc.Name = "remark";
            gc.HeaderText = "备注";
            gc.Width = 110;
            superGridControlShangPing.PrimaryGrid.Columns.Add(gc);
        }

        /// <summary>
        /// 标示那个控件不可用
        /// </summary>
        private void InitForm()
        {
            this.cboInType.Enabled = false;
            this.labtextboxTop6.ReadOnly = true;
            this.textBoxOddNumbers.ReadOnly = true;
            this.cboPurchaseCode.Enabled = false;
            this.labtextboxTop9.ReadOnly = true;
            this.ltxtbSalsMan.ReadOnly = true;
            this.ltxtbMakeMan.ReadOnly = true;
            this.ltxtbShengHeMan.ReadOnly = true;
            this.resizablePanel1.Visible = false;
            this.dateTimePicker1.Enabled = false;
            this.superGridControlShangPing.PrimaryGrid.ReadOnly = true;
            this.toolStripBtnSave.Enabled = false;
            this.panel2.BackColor = Color.FromArgb(240, 240, 240);
            this.panel5.BackColor = Color.FromArgb(240, 240, 240);
            this.superGridControlShangPing.BackColor = Color.FromArgb(240, 240, 240);
            labtextboxTop9.BackColor = Color.FromArgb(240, 240, 240);
            cboInType.BackColor = Color.FromArgb(240, 240, 240);
            txtbSearch.ReadOnly = true;
            txtbSearch.Enabled = false;
            txtbSearch.BackColor = Color.FromArgb(240, 240, 240);
        }
        #endregion

        /// <summary>
        /// 统计和验证数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControlShangPing_CellValidated(object sender, GridCellValidatedEventArgs e)
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
                for (int i = 0; i < superGridControlShangPing.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControlShangPing.PrimaryGrid.Rows[i] as GridRow;
                    tempAllNumber += Convert.ToDecimal(tempGR["gridColumnnumber"].FormattedValue);
                }
                _MaterialNumber = tempAllNumber;
                gr = (GridRow)superGridControlShangPing.PrimaryGrid.LastSelectableRow;
                gr["gridColumnnumber"].Value = _MaterialNumber.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2110-统计数量出错！请检查：" + ex.Message);
            }
        }

        /// <summary>
        /// 实时摸查询的
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControlShangPing_EditorValueChanged(object sender, GridEditEventArgs e)
        {
            try
            {
                string SS = "";
                GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[ClickRowIndex];
                string materialDaima = XYEEncoding.strCodeHex(e.EditControl.EditorValue.ToString());
                if (SS == "")
                {
                    //模糊查询商品列表
                    _AllMaterial = pdi.GetList("" + XYEEncoding.strCodeHex(this.cboPurchaseCode.Text.Trim() + ""), "" + materialDaima + "");
                    InitMaterialDataGridView();
                    dataGridViewShangPing.DataSource = ch.DataTableReCoding(_AllMaterial);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:2111-表格模糊查询错误，查询数据错误" + ex.Message, "入库单温馨提示");
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
                this.resizablePanelData.Visible = false;
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
        private void labtxtSupply_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (labtextboxTop6.Text.Trim() == "")
                {
                    InitSupply();
                    _Click = 3;
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
                dgvc.Visible = false;
                dgvc.HeaderText = "联系手机";
                dgvc.DataPropertyName = "mobilePhone";
                dataGridViewFuJia.Columns.Add(dgvc);

                resizablePanel1.Location = new Point(550, 160);
                dataGridViewFuJia.DataSource = ch.DataTableReCoding(supply.GetList(0, "" + XYEEncoding.strCodeHex(labtextboxTop6.Text.Trim()) + ""));
                resizablePanel1.Visible = true;
                _Click = 3;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2112-模糊查询供应商数据错误" + ex.Message, "入库单温馨提示");
            }


        }

        /// <summary>
        /// 入库员模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labtxtEmployee_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ltxtbSalsMan.Text.Trim() == "")
                {
                    InitEmployee();
                    _Click = 4;
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

                resizablePanel1.Location = new Point(234, 440);
                dataGridViewFuJia.DataSource = ch.DataTableReCoding(employee.GetList(0, "" + XYEEncoding.strCodeHex(ltxtbSalsMan.Text.Trim()) + ""));
                resizablePanel1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2113-模糊查询入库员数据错误" + ex.Message, "入库单温馨提示");
            }

        }

        private void WareHouseInMain_Activated(object sender, EventArgs e)
        {
            labtextboxTop6.Focus();
        }

        private void superGridControlShangPing_BeginEdit(object sender, GridEditEventArgs e)
        {
            if (this.cboPurchaseCode.Text.Trim() == "")
            {
                resizablePanelData.Visible = false;
                MessageBox.Show("请先选择供应商，显示采购单号!");
                return;
            }
            if (e.GridCell.GridColumn.Name == "material")
            {
                SelectedElementCollection ge = superGridControlShangPing.PrimaryGrid.GetSelectedCells();
                GridCell gc = ge[0] as GridCell;
                if (gc.GridRow.Cells[material].Value != null && (gc.GridRow.Cells[material].Value).ToString() != "")
                {
                    //模糊查询商品列表
                    _AllMaterial = pdi.GetList("" + XYEEncoding.strCodeHex(this.cboPurchaseCode.Text.Trim() + ""), "" + XYEEncoding.strCodeHex(gc.GridRow.Cells[material].Value.ToString()) + "");
                    InitMaterialDataGridView();
                }
                else
                {
                    //绑定商品列表
                    _AllMaterial = pdi.GetList("" + XYEEncoding.strCodeHex(this.cboPurchaseCode.Text.Trim() + ""), "");
                    InitMaterialDataGridView();
                }
                dataGridViewShangPing.DataSource = ch.DataTableReCoding(_AllMaterial);
            }

        }
        /// <summary>
        /// 产品检索商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cboPurchaseCode.Text.Trim() == "")
                {
                    resizablePanelData.Visible = false;
                    MessageBox.Show("请先选择供应商，显示采购单号!");
                    return;
                }
                resizablePanelData.Location = new Point(91, 193);
                resizablePanelData.Visible = true;
                PurchaseDetailInterface pd = new PurchaseDetailInterface();
                if (string.IsNullOrWhiteSpace(txtbSearch.Text.Trim()))
                {
                    //模糊查询商品列表
                    _AllMaterial = pd.GetListAndMaterial("");
                    InitMaterialDataGridView();
                    dataGridViewShangPing.DataSource = ch.DataTableReCoding(_AllMaterial);
                    return;
                }
                //模糊查询商品列表
                _AllMaterial = pd.GetListAndMaterial(XYEEncoding.strCodeHex(txtbSearch.Text.Trim()));
                InitMaterialDataGridView();
                dataGridViewShangPing.DataSource = ch.DataTableReCoding(_AllMaterial);
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2114-产品检索模糊查询数据失败" + ex.Message, "入库单温馨提示");
            }

        }

        private void txtbSearch_Enter(object sender, EventArgs e)
        {
            txtbSearch_TextChanged(sender, e);
        }

        private void txtbSearch_Leave(object sender, EventArgs e)
        {
            if (ActiveControl.Name == "dataGridViewShangPing")//如果当前活动控件是dataGridView1
            {
                resizablePanelData.Visible = true;
                return;
            }
            resizablePanelData.Visible = false;
        }
    }
}
