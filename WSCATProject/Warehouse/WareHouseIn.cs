﻿using DevComponents.DotNetBar.SuperGrid;
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

        #endregion

        private void StockIn_Load(object sender, EventArgs e)
        {
            //仓库
            GridTextBoxXEditControl gdieccangku = superGridControl1.PrimaryGrid.Columns["griCoulumcangku"].EditControl as GridTextBoxXEditControl;
            gdieccangku.ButtonCustom.Visible = true;
            gdieccangku.ButtonCustomClick += Gdiec_ButtonCustomClick;

            //货架
            GridTextBoxXEditControl gdiehuojia = superGridControl1.PrimaryGrid.Columns["griCoulumhuojia"].EditControl as GridTextBoxXEditControl;
            gdiehuojia.ButtonCustom.Visible = true;
            gdiehuojia.ButtonCustomClick += Gdiec_ButtonCustomClick;

            // SupplierManager supply = new SupplierManager();
            //_AllSupply = supply.SelSupplierTable();//供应商

            //禁用自动创建列
            dataGridView1.AutoGenerateColumns = false;
            dataGridViewFujia.AutoGenerateColumns = false;

            //入库单单号
            textBoxOddNumbers.Text = "OI-2016082516410922";

            //绑定事件 双击事填充内容并隐藏列表
            dataGridViewFujia.CellDoubleClick += DataGridViewFujia_CellDoubleClick;
            // 将dataGridView中的内容居中显示
            dataGridViewFujia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            CodingHelper ch = new CodingHelper();
            try
            {
                WarehouseDetailInterface whdi = new WarehouseDetailInterface();
                superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
                DataSet ds = ch.DataSetReCoding(whdi.getListByMainCode(XYEEncoding.strCodeHex(textBoxOddNumbers.Text.Trim())));
                this.superGridControl1.PrimaryGrid.DataSource = ds;

                WarehouseInterface whi = new WarehouseInterface();
                DataSet DS = ch.DataSetReCoding(whi.getWarehouseInList("code='" + XYEEncoding.strCodeHex(textBoxOddNumbers.Text.Trim()) + "'"));
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                labtextboxTop5.Text = DS.Tables[0].Rows[0].ItemArray[10].ToString();
                //DataTable dt = ch.DataTableReCoding(whdi.getListByMainCode(textBoxOddNumbers.Text.ToString()));
                //this.superGridControl1.PrimaryGrid.DataSource = ds.Tables[0];
                //labtextboxTop5.Text = dt.Rows[0]["code"].ToString();
            }
            catch
            {
                MessageBox.Show("错误代码:4011;销售单数据为空,无法绑定数据,请检查");
                Close();
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
                dgvc.Name = "Su_Code";
                dgvc.HeaderText = "编码";
                dgvc.DataPropertyName = "编码";
                dataGridViewFujia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "Su_Name";
                dgvc.HeaderText = "单位名称";
                dgvc.DataPropertyName = "单位名称";
                dataGridViewFujia.Columns.Add(dgvc);
                resizablePanel1.Location = new Point(449, 115);//供应商的位置
                //dataGridViewFujia.DataSource = _AllSupply;
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
                dgvc.Name = "Su_Code";
                dgvc.HeaderText = "编码";
                dgvc.DataPropertyName = "编码";
                dataGridViewFujia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "Su_Name";
                dgvc.HeaderText = "单位名称";
                dgvc.DataPropertyName = "单位名称";
                dataGridViewFujia.Columns.Add(dgvc);
                resizablePanel1.Location = new Point(449, 115);//供应商的位置
                //dataGridViewFujia.DataSource = _AllSupply;
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
        //业务员图标的点击事件
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
                string name = dataGridViewFujia.Rows[e.RowIndex].Cells["Su_Name"].Value.ToString();
                labtextboxTop3.Text = name;
                resizablePanel1.Visible = false;
            }
        }
        #endregion

        private void buttonSave_Click(object sender, EventArgs e)
        {
            WarehouseDetailInterface wdif = new WarehouseDetailInterface();
            int result = 0;
            string ss = "";
            ss += ss;
            GridRow dr = superGridControl1.PrimaryGrid.Rows[0] as GridRow;
            //MessageBox.Show(dr.Cells["gridColumn11"].Value.ToString());
            List<string> list = new List<string>();
            for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count; i++) //根据DGV的行数循环
            {
                if (Convert.ToBoolean((superGridControl1.PrimaryGrid.Rows[i] as GridRow).Cells["gridColumn11"].Value) ==true)  //判断当前行是否选中
                {
                    list.Add((superGridControl1.PrimaryGrid.Rows[i] as GridRow).Cells["gridColumn1"].Value.ToString());  //将code添加到list中
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                result = wdif.updateStateByCode(XYEEncoding.strCodeHex(list[i]));
            }
            ////获得界面上的数据,准备传给base层新增数据
            //string warehouseIncode = "";
            //WarehouseInterface warehouseInterface = new WarehouseInterface();
            ////入库单
            //WarehouseIn warehouseIn = new WarehouseIn();
            ////入库商品列表
            //List<WarehouseInDetail> wareHouseInList = new List<WarehouseInDetail>();
            //try
            //{
            //    warehouseIncode = XYEEncoding.strCodeHex(this.textBoxOddNumbers.Text);
            //    warehouseIn.code = warehouseIncode;
            //    warehouseIn.isClear = 1;
            //    warehouseIn.purchaseCode = XYEEncoding.strCodeHex(this.labtextboxTop5.Text);
            //    warehouseIn.date = this.dateTimePicker1.Value;
            //    warehouseIn.checkState = 0;
            //    warehouseIn.examine = XYEEncoding.strCodeHex(labtextboxBotton4.Text);
            //    warehouseIn.operation = XYEEncoding.strCodeHex(labtextboxBotton3.Text);
            //    warehouseIn.remark = XYEEncoding.strCodeHex(labtextboxBotton2.Text);
            //   // warehouseIn.stock = XYEEncoding.strCodeHex(labtextboxTop3.Text);

            //    warehouseIn.state = 1;
            //    warehouseIn.reserved1 = "";
            //    warehouseIn.reserved2 = "";
            //    warehouseIn.type = XYEEncoding.strCodeHex(comboBoxEx1.Text);//入货类别
            //    warehouseIn.updateDate = DateTime.Now;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("错误代码:4101;尝试创建入库单商品详情数据出错,请检查输入" + ex.Message);
            //    return;
            //}

            //try
            //{
            //    //获得商品列表数据,准备传给base层新增数据
            //    GridRow g = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
            //    GridItemsCollection grs = superGridControl1.PrimaryGrid.Rows;

            //    int i = 0;
            //    string _wareHouesDetailCode = XYEEncoding.strCodeHex(BuildCode.ModuleCode("WD"));
            //    DateTime nowDataTime = DateTime.Now;
            //    foreach (GridRow gr in grs)
            //    {
            //        i++;
            //        WarehouseInDetail WarehouseIndetail = new WarehouseInDetail();
            //        WarehouseIndetail.barcode = XYEEncoding.strCodeHex(this.textBoxOddNumbers.Text);
            //        WarehouseIndetail.code = _wareHouesDetailCode + i.ToString();
            //        WarehouseIndetail.date = this.dateTimePicker1.Value;
            //        WarehouseIndetail.isClear = 1;
            //        WarehouseIndetail.materiaModel = XYEEncoding.strCodeHex(gr["gridColumn3"].Value.ToString());
            //        WarehouseIndetail.materiaName = XYEEncoding.strCodeHex(gr["gridColumn2"].Value.ToString()); ;
            //        WarehouseIndetail.materiaUnit = XYEEncoding.strCodeHex(gr["gridColumn5"].Value.ToString()); ;
            //        WarehouseIndetail.money = Convert.ToDecimal(gr["gridColumn8"].Value);
            //        WarehouseIndetail.number = Convert.ToDecimal(gr["gridColumn6"].Value);
            //        WarehouseIndetail.price = Convert.ToDecimal(gr["gridColumn7"].Value);
            //        //WarehouseIndetail.remark = gr["gridColumn9"].Value == null ?
            //        // "" : XYEEncoding.strCodeHex(gr["gridColumn9"].Value.ToString());
            //        WarehouseIndetail.WarehouseCode =XYEEncoding.strCodeHex( StorageCode);//仓库code
            //        WarehouseIndetail.WarehouseName = XYEEncoding.strCodeHex(Storage);//仓库名称
            //        WarehouseIndetail.StorageRackCode = XYEEncoding.strCodeHex(StorageRackCode);//货架code
            //        WarehouseIndetail.StorageRackName = XYEEncoding.strCodeHex(StorageRack + "/" + StoragePai + "/" + StorageGe);  //货架名称、排、格
            //        WarehouseIndetail.reserved1 = "";
            //        WarehouseIndetail.reserved2 = "";
            //        WarehouseIndetail.rfid = "";
            //        WarehouseIndetail.updateDate = nowDataTime;
            //        WarehouseIndetail.state = 1;
            //        wareHouseInList.Add(WarehouseIndetail);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("错误代码：4102-尝试创建入库单数据出错,请检查输入" + ex.Message);
            //    return;
            //}

            ////增加一条入库单和入库单详细数据
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
            if (string.IsNullOrWhiteSpace(labtextboxTop3.Text))
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

        private void buttonExamine_Click(object sender, EventArgs e)
        {

        }
    }
}