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
    public partial class WareHouseIn : TemplateForm
    {
        public WareHouseIn()
        {
            InitializeComponent();
        }
        #region 仓库、货架、排、格所需的参数
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
        private decimal _MaterialMoney;
        private decimal _MaterialNumber;
        private int _rukushu;//入库数量
        #endregion

        private void StockIn_Load(object sender, EventArgs e)
        {
            //供应商
            SupplierInterface supply = new SupplierInterface();
            _AllSupply = supply.SelSupplierTable();

            //业务员
            EmpolyeeInterface employee = new EmpolyeeInterface();
            _AllEmployee = employee.SelSupplierTable(false);

            //仓库
            GridTextBoxXEditControl gdieccangku = superGridControl1.PrimaryGrid.Columns["griCoulumcangku"].EditControl as GridTextBoxXEditControl;
            gdieccangku.ButtonCustom.Visible = true;
            gdieccangku.ButtonCustomClick += Gdiec_ButtonCustomClick;

            //货架
            GridTextBoxXEditControl gdiehuojia = superGridControl1.PrimaryGrid.Columns["griCoulumhuojia"].EditControl as GridTextBoxXEditControl;
            gdiehuojia.ButtonCustom.Visible = true;
            gdiehuojia.ButtonCustomClick += Gdiec_ButtonCustomClick;

            //禁用自动创建列
            dataGridView1.AutoGenerateColumns = false;
            dataGridViewFujia.AutoGenerateColumns = false;


            //绑定事件 双击事填充内容并隐藏列表
            dataGridViewFujia.CellDoubleClick += DataGridViewFujia_CellDoubleClick;
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
                        textBoxOddNumbers.Text = _wareHouseModel["code"].Value.ToString();
                        this.labtextboxTop5.Text = _wareHouseModel["purchaseCode"].Value.ToString();
                        comboBoxEx1.SelectedIndex = 0;
                        superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
                        superGridControl1.PrimaryGrid.DataSource = waredeta.getListByMainCode(XYEEncoding.strCodeHex(_wareHouseModel["code"].Value.ToString()));
                        superGridControl1.PrimaryGrid.EnsureVisible();
                        InitDataGridView();
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

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("错误" + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 点击panel隐藏扩展panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void panel6_Click(object sender, EventArgs e)
        {
            resizablePanel1.Visible = false;
        }

        #region 初始化数据
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
                resizablePanel1.Location = new Point(640, 115);
                dataGridViewFujia.DataSource = _AllSupply;
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
                resizablePanel1.Location = new Point(204, 410);
                dataGridViewFujia.DataSource = _AllEmployee;
            }
        }
        #endregion

        #region picture  图标点击事件

        //供应商图标的点击事件
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (_Click != 1)
            {
                InitSupply();
            }
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
            if (_Click == 1)
            {
                // string code = dataGridViewFujia.Rows[e.RowIndex].Cells["Su_Code"].Value.ToString();
                string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                labtextboxTop3.Text = name;
                resizablePanel1.Visible = false;
            }
            //业务员
            if (_Click == 2)
            {
                // string code = dataGridViewFujia.Rows[e.RowIndex].Cells["Su_Code"].Value.ToString();
                string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                labtextboxBotton1.Text = name;
                resizablePanel1.Visible = false;
            }
        }
        #endregion

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //获得界面上的数据,准备传给base层新增数据
            string warehouseIncode = "";
            WarehouseInterface warehouseInterface = new WarehouseInterface();
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
                        WarehouseIndetail.StorageRackName = XYEEncoding.strCodeHex(StorageRack + "/" + StoragePai + "/" + StorageGe);  //货架名称、排、格
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

            if (labtextboxTop3.Text == null)
            {
                MessageBox.Show("请先选择供应商:");
                return;
            }
            SelectedElementCollection ge = superGridControl1.PrimaryGrid.GetSelectedCells();
            if (ge.Count > 0)
            {
                GridCell gc = ge[0] as GridCell;
                WareHuseStorageRack whsr = new WareHuseStorageRack();
                whsr.ShowDialog(this);

                gc.GridRow.Cells[8].Value = Storage;
                gc.GridRow.Cells[9].Value = StorageRack + "/" + StoragePai + "/" + StorageGe;
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
            gr.Cells["gridColumn6"].Value = 0;
            gr.Cells["gridColumn6"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumn6"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumn8"].Value = 0;
            gr.Cells["gridColumn8"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumn8"].CellStyles.Default.Background.Color1 = Color.Orange;

            //计算金额
            decimal number = Convert.ToDecimal(gr.Cells["gridColumn6"].FormattedValue);
            decimal price = Convert.ToDecimal(gr.Cells["gridColumn8"].FormattedValue);
            decimal allPrice = number * price;
            gr.Cells["gridColumn8"].Value = allPrice;
            //逐行统计数据总数
            decimal tempAllNumber = 0;
            decimal tempAllMoney = 0;
            for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
            {
                GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                tempAllNumber += Convert.ToDecimal(tempGR["gridColumn6"].FormattedValue);
                tempAllMoney += Convert.ToDecimal(tempGR["gridColumn8"].FormattedValue);
            }
            _MaterialMoney = tempAllMoney;
            _MaterialNumber = tempAllNumber;
            gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
            gr["gridColumn6"].Value = _MaterialNumber.ToString();
            gr["gridColumn8"].Value = _MaterialMoney.ToString();
        }

        /// <summary>
        /// 审核按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExamine_Click(object sender, EventArgs e)
        {
            int checkResult = 0;
            string wherecode = XYEEncoding.strCodeHex(textBoxOddNumbers.Text.Trim());
            WarehouseInterface warehouseInterface = new WarehouseInterface();
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

       
    }
}