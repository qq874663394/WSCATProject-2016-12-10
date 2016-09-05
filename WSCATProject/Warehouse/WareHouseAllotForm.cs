using DevComponents.DotNetBar.SuperGrid;
using HelperUtility.Encrypt;
using InterfaceLayer.Base;
using InterfaceLayer.Warehouse;
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
    public partial class WareHouseAllotForm : TemplateForm
    {
        public WareHouseAllotForm()
        {
            InitializeComponent();
        }
        #region  数据字段
        /// <summary>
        /// 所有业务员
        /// </summary>
        private DataTable _AllEmployee = null;
        /// <summary>
        /// 点击的项,1业务员
        /// </summary>
        private int _Click = 0;
        /// <summary>
        /// 所有商品列表
        /// </summary>
        private DataTable  _AllMaterial = null;
        /// <summary>
        /// 所有仓库列表
        /// </summary>
        private DataTable  _AllStorage = null;
        /// <summary>
        /// 保存仓库的Code
        /// </summary>
        private string _StorageCode = "";
        /// <summary>
        /// 保存商品Code
        /// </summary>
        private string _MaterialCode = "";
        /// <summary>
        /// 调出仓库
        /// </summary>
        private KeyValuePair<string, string> _ClickStorageout;
        /// <summary>
        /// 调入仓库
        /// </summary>
        private KeyValuePair<string, string> _ClickStoragein;
        //保存调入仓库
        private string _Instorage;
        /// <summary>
        /// 保存调出仓库
        /// </summary>
        private string _Outstorage;
        /// <summary>
        /// 保存仓库状态 1、调出仓库 2、调入仓库 3、商品列表
        /// </summary>
        private int _storageState;
        /// <summary>
        /// 统计数量
        /// </summary>
        private decimal _MaterialNumber;
        /// <summary>
        /// 统计金额
        /// </summary>
        private decimal _MaterialMoney;
        #endregion
        //调用解密方法
        CodingHelper ch = new CodingHelper();
        WarehouseMainInterface waremain = new WarehouseMainInterface();
        private void WareHouseAllotForm_Load(object sender, EventArgs e)
        {
            //业务员
            EmpolyeeInterface employee = new EmpolyeeInterface();
            StorageInterface storage = new StorageInterface();

            _AllEmployee = employee.SelSupplierTable(false);
            _AllStorage = storage.GetList("");
          
            //禁用自动创建列
            dataGridView1.AutoGenerateColumns = false;
            dataGridViewFujia.AutoGenerateColumns = false;

            //绑定事件 双击事填充内容并隐藏列表
            dataGridViewFujia.CellDoubleClick += DataGridViewFujia_CellDoubleClick;
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
            // 将dataGridView中的内容居中显示
            dataGridViewFujia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            //单价  
            GridDoubleInputEditControl gdiecdanjia = superGridControl1.PrimaryGrid.Columns["gridColumnOutprice"].EditControl as GridDoubleInputEditControl;
            gdiecdanjia.MinValue = 0;
            gdiecdanjia.MaxValue = 999999999;
            gdiecdanjia.ButtonCustom.Visible = true;
            //gdiecdanjia.ButtonCustomClick += Gdiec_ButtonCustomClick;

            //需求数量
            GridDoubleInputEditControl gdiecNumber = superGridControl1.PrimaryGrid.Columns["gridColumncurNumber"].EditControl as GridDoubleInputEditControl;
            gdiecNumber.MinValue = 0;
            gdiecNumber.MaxValue = 999999999;
            //调用初始化表格数据
            InitDataGridView();
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
        /// 初始化业务员
        /// </summary>
        private void InitEmployee()
        {
            if (_Click != 1)
            {
                _Click = 1;
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
                resizablePanel1.Location = new Point(204, 300);
                dataGridViewFujia.DataSource = _AllEmployee;
            }
        }
        #endregion

        #region picture  图标点击事件
        //业务员图标点击事件
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (_Click != 1)
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
            //业务员
            if (_Click == 1)
            {
                string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                labtextboxBotton1.Text = name;
                resizablePanel1.Visible = false;
            }
            //仓库信息
            if (_Click == 2)
            {
              
                GridRow gr = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
                //判断是调入还是调出仓库
                if (_storageState==1)
                {
                    string codeout = dataGridViewFujia.Rows[e.RowIndex].Cells["code"].Value.ToString();
                    string OutName = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                    gr.Cells["gridColumnStock"].Value = OutName;
                    gr.Cells["gridColumnoutcode"].Value = codeout;
                    _ClickStorageout = new KeyValuePair<string, string>(codeout, OutName);
                    _Outstorage = codeout;

                }
                else
                {
                    string codein = dataGridViewFujia.Rows[e.RowIndex].Cells["code"].Value.ToString();
                    string Inname = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                    gr.Cells["gridColumnStockIn"].Value = Inname;
                    gr.Cells["gridColumnincode"].Value = codein;
                    _ClickStoragein = new KeyValuePair<string, string>(codein, Inname);
                    _Instorage = codein;
                }
                resizablePanel1.Visible = false;
                //统计库存数
                //if (gr.Cells["gridColumnMaCode"].Value != null)
                //{
                //    if (!string.IsNullOrEmpty(gr.Cells["gridColumnMaCode"].Value.ToString()))
                //    {
                //        DataTable tempDT = sm.searchMaterialStockNumber(_AllStock,
                //            gr.Cells["gridColumnStockCode"].Value.ToString(),
                //            gr.Cells["gridColumnMaCode"].Value.ToString());

                //        materialStockNumber(tempDT, true, gr.Cells["gridColumnName"].Value.ToString(),
                //            gr.Cells["gridColumnStock"].Value.ToString());
                //    }
                //}
                //else
                //{
                //    labelXZongKuCun.Visible = false;
                //    labelXKuCun.Visible = false;
                //}
            }
        }

        #endregion

        /// <summary>
        /// 按下ESC按钮关闭子窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WareHouseAllotForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.resizablePanel1.Visible = false;
            }
        }

        /// <summary>
        /// 第一个表格选择调出出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "gridColumnStock")
            {
                //绑定仓库列表
                InitStorageList();
                _storageState = 1;
                return;
            }
            if (e.GridCell.GridColumn.Name == "gridColumnStockIn")
            {
                //绑定仓库列表
                InitStorageList();
                _storageState = 2;
                return;
            }
            if (e.GridCell.GridColumn.Name== "material")
            {
                _AllMaterial = waremain.GetList("" + XYEEncoding.strCodeHex(_Outstorage) + "");
                InitMaterialDataGridView();
                _storageState = 3;
            }

        }

        #region 初始化数据
        /// <summary>
        /// 初始化仓库列和数据
        /// </summary>
        private void InitStorageList()
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

        /// <summary>
        /// 初始化商品下拉别表的数据
        /// </summary>
        private void InitMaterialDataGridView()
        {
            DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "id";
            dgvc.Visible = false;
            dgvc.HeaderText = "maid";
            dgvc.DataPropertyName = "id";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "picName";
            dgvc.Visible = false;
            dgvc.HeaderText = "pic";
            dgvc.DataPropertyName = "picName";
            dataGridView1.Columns.Add(dgvc);

            //dgvc = new DataGridViewTextBoxColumn();
            //dgvc.Name = "Ma_RFID";
            //dgvc.Visible = false;
            //dgvc.HeaderText = "rfid";
            //dgvc.DataPropertyName = "Ma_RFID";
            //dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "code";
            dgvc.Visible = false;
            dgvc.HeaderText = "code";
            dgvc.DataPropertyName = "code";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "typeID";
            dgvc.Visible = false;
            dgvc.HeaderText = "TypeID";
            dgvc.DataPropertyName = "typeID";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "typeName";
            dgvc.Visible = false;
            dgvc.HeaderText = "TypeName";
            dgvc.DataPropertyName = "typeName";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "price";
            dgvc.Visible = false;
            dgvc.HeaderText = "price";
            dgvc.DataPropertyName = "price";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "peiceA";
            dgvc.Visible = false;
            dgvc.HeaderText = "priceA";
            dgvc.DataPropertyName = "peiceA";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "priceB";
            dgvc.Visible = false;
            dgvc.HeaderText = "priceB";
            dgvc.DataPropertyName = "priceB";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "priceC";
            dgvc.Visible = false;
            dgvc.HeaderText = "priceC";
            dgvc.DataPropertyName = "priceC";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "priceD";
            dgvc.Visible = false;
            dgvc.HeaderText = "priceD";
            dgvc.DataPropertyName = "priceD";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "priceE";
            dgvc.Visible = false;
            dgvc.HeaderText = "priceE";
            dgvc.DataPropertyName = "priceE";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "createDate";
            dgvc.Visible = false;
            dgvc.HeaderText = "CreateDate";
            dgvc.DataPropertyName = "createDate";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "supplierName";
            dgvc.Visible = false;
            dgvc.HeaderText = "Supplier";
            dgvc.DataPropertyName = "supplierName";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "supplierCode";
            dgvc.Visible = false;
            dgvc.HeaderText = "SupID";
            dgvc.DataPropertyName = "supplierCode";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "unit";
            dgvc.Visible = false;
            dgvc.HeaderText = "Unit";
            dgvc.DataPropertyName = "unit";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "inPrice";
            dgvc.Visible = false;
            dgvc.HeaderText = "InPrice";
            dgvc.DataPropertyName = "inPrice";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "inDate";
            dgvc.Visible = false;
            dgvc.HeaderText = "InDate";
            dgvc.DataPropertyName = "inDate";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "remark";
            dgvc.Visible = false;
            dgvc.HeaderText = "Remark";
            dgvc.DataPropertyName = "remark";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "isEnable";
            dgvc.Visible = false;
            dgvc.HeaderText = "Enable";
            dgvc.DataPropertyName = "isEnable";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "isClear";
            dgvc.Visible = false;
            dgvc.HeaderText = "Clear";
            dgvc.DataPropertyName = "isClear";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "reserved1";
            dgvc.Visible = false;
            dgvc.HeaderText = "Safeyone";
            dgvc.DataPropertyName = "reserved1";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "reserved2";
            dgvc.Visible = false;
            dgvc.HeaderText = "Safetytwo";
            dgvc.DataPropertyName = "reserved2";
            dataGridView1.Columns.Add(dgvc);


            //dgvc = new DataGridViewTextBoxColumn();
            //dgvc.Name = "Ma_zhujima";
            //dgvc.Visible = true;
            //dgvc.HeaderText = "助记码";
            //dgvc.DataPropertyName = "Ma_zhujima";
            //dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "name";
            dgvc.Visible = true;
            dgvc.HeaderText = "物料名称";
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

            dataGridView1.DataSource = ch.DataTableReCoding( _AllMaterial);

        }

        /// <summary>
        /// 初始化表格的数据
        /// </summary>
        private void InitDataGridView()
        {
            //改为点击可编辑
            superGridControl1.PrimaryGrid.MouseEditMode = MouseEditMode.SingleClick;
            //新增一行 用于给客户操作
            superGridControl1.PrimaryGrid.NewRow(true);
            //最后一行做统计行
            GridRow gr = (GridRow)superGridControl1.PrimaryGrid.
                Rows[superGridControl1.PrimaryGrid.Rows.Count - 1];
            gr.ReadOnly = true;
            gr.CellStyles.Default.Background.Color1 = Color.SkyBlue;
            gr.Cells["gridColumnStock"].Value = "合计";
            gr.Cells["gridColumnStock"].CellStyles.Default.Alignment =
                DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumncurNumber"].Value = 0;
            gr.Cells["gridColumncurNumber"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumncurNumber"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumnOutMoney"].Value = 0;
            gr.Cells["gridColumnOutMoney"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnOutMoney"].CellStyles.Default.Background.Color1 = Color.Orange;
        }
        #endregion

        /// <summary>
        /// 绑定中间添加销售商品的数据 双击物料信息填写在表格里面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //是否要新增一行的标记
            bool newAdd = false;
            //SellManager sellManager = new SellManager();
            GridRow gr = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
            //id字段为空 说明是没有数据的行 不是修改而是新增
            if (gr.Cells["gridColumnid"].Value == null)
            {
                newAdd = true;
            }
            gr.Cells["material"].Value = dataGridView1.Rows[e.RowIndex].Cells["code"].Value;//编号
            gr.Cells["gridColumnmateriaName"].Value = dataGridView1.Rows[e.RowIndex].Cells["name"].Value;//商品名称
            gr.Cells["gridColumnmateriaModel"].Value = dataGridView1.Rows[e.RowIndex].Cells["model"].Value;//规格型号
            gr.Cells["gridColumnmateriaUnit"].Value = dataGridView1.Rows[e.RowIndex].Cells["unit"].Value;//单位
            //调出金额
            gr.Cells["gridColumncurNumber"].Value = 1;
            decimal price = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["price"].Value.Equals("") ?
                0 : dataGridView1.Rows[e.RowIndex].Cells["price"].Value);
            gr.Cells["gridColumnOutprice"].Value = price;
            gr.Cells["gridColumnOutMoney"].Value = price;
            resizablePanelData.Visible = false;

            //当上一次有选择仓库时 默认本次也为上次选择仓库
            if (!string.IsNullOrEmpty(_ClickStorageout.Value) && !string.IsNullOrEmpty(_ClickStorageout.Key))
            {
                if (!string.IsNullOrEmpty(_ClickStoragein.Value)&&!string.IsNullOrEmpty(_ClickStoragein.Key))
                {
                    gr.Cells["gridColumnoutcode"].Value = _ClickStorageout.Key;
                    gr.Cells["gridColumnStock"].Value = _ClickStorageout.Value;
                    gr.Cells["gridColumnincode"].Value = _ClickStoragein.Key;
                    gr.Cells["gridColumnStockIn"].Value = _ClickStoragein.Value;
                }
            }

            #region 统计库存
            //查询库存数量，
            //if (gr.Cells["gridColumnoutcode"].Value != null)
            //{
            //    //统计库存数
            //    if (!string.IsNullOrEmpty(gr.Cells["gridColumnStockCode"].Value.ToString()))
            //    {
            //        try
            //        {
            //            DataTable tempDT = sellManager.searchMaterialStockNumber(_AllStock,
            //                gr.Cells["gridColumnStockCode"].Value.ToString(),
            //                gr.Cells["gridColumnMaCode"].Value.ToString());

            //            materialStockNumber(tempDT, true, gr.Cells["gridColumnName"].Value.ToString(),
            //                gr.Cells["gridColumnStock"].Value.ToString());
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("错误代码：1203-商品库存查询异常，错误代码=" + ex.Message);
            //        }
            //    }
            //    else
            //    {
            //        //查询所有的库存信息
            //        DataTable tempDT = sellManager.searchMaterialStockNumber(_AllStock,
            //                "",
            //                gr.Cells["gridColumnMaCode"].Value.ToString());
            //        materialStockNumber(tempDT, false, gr.Cells["gridColumnName"].Value.ToString(), "");
            //    }
            //}
            //else
            //{
            //    //查询所有的库存信息
            //    DataTable tempDT = sellManager.searchMaterialStockNumber(_AllStock,
            //                "",
            //                gr.Cells["gridColumnMaCode"].Value.ToString());
            //    materialStockNumber(tempDT, false, gr.Cells["gridColumnName"].Value.ToString(), "");
            //}
            #endregion

            //新增一行 
            if (newAdd)
            {
                superGridControl1.PrimaryGrid.NewRow(superGridControl1.PrimaryGrid.Rows.Count);
                //递增数量和金额 默认为1和单价 
                gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                _MaterialNumber += 1;
                _MaterialMoney += price;
                gr.Cells["gridColumncurNumber"].Value = _MaterialNumber;
                gr.Cells["gridColumnOutMoney"].Value = _MaterialMoney;
            }
            superGridControl1.Focus();
            SendKeys.Send("^{End}{Home}");
        }

        //表格的点击事件 
        private void superGridControl1_Click(object sender, EventArgs e)
        {
            resizablePanelData.Visible = false;
            resizablePanel1.Visible = false;
        }

        //验证完全后,统计单元格数据
        private void superGridControl1_CellValidated(object sender, GridCellValidatedEventArgs e)
        {
            GridRow gr = e.GridPanel.Rows[e.GridCell.RowIndex] as GridRow;
            //若是没数据的行则不做处理
            if (gr.Cells["material"].Value == null)
            {
                return;
            }

            if (e.GridCell.GridColumn.Name == "gridColumncurNumber" ||
                e.GridCell.GridColumn.Name == "gridColumnOutprice")
            {
                //添加对应的单价和总价
                if (e.GridCell.GridColumn.Name == "gridColumncurNumber" &&
                    !string.IsNullOrEmpty(e.GridCell.FormattedValue))
                {
                    _MaterialNumber += decimal.Parse(e.GridCell.FormattedValue);
                }
                if (e.GridCell.GridColumn.Name == "gridColumnOutprice" &&
                    !string.IsNullOrEmpty(e.GridCell.FormattedValue))
                {
                    _MaterialMoney += decimal.Parse(e.GridCell.FormattedValue);
                }
                //计算金额
                decimal number = Convert.ToDecimal(gr.Cells["gridColumncurNumber"].FormattedValue);
                decimal price = Convert.ToDecimal(gr.Cells["gridColumnOutprice"].FormattedValue);
                decimal allPrice = number * price;
                gr.Cells["gridColumnOutMoney"].Value = allPrice;
                //逐行统计数据总数
                decimal tempAllNumber = 0;
                decimal tempAllMoney = 0;
                for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                    tempAllNumber += Convert.ToDecimal(tempGR["gridColumncurNumber"].FormattedValue);
                    tempAllMoney += Convert.ToDecimal(tempGR["gridColumnOutMoney"].FormattedValue);
                }
                _MaterialMoney = tempAllMoney;
                _MaterialNumber = tempAllNumber;
                gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                gr["gridColumncurNumber"].Value = _MaterialNumber.ToString();
                gr["gridColumnOutMoney"].Value = _MaterialMoney.ToString();
            }
            //需求数量大于库存数量显示库存情况
            //GridRow grow = e.GridPanel.Rows[e.GridCell.RowIndex] as GridRow;
            //if (grow["gridColumnStock"].Value.ToString() != "" && grow["gridColumnName"].Value.ToString() != "")
            //{
            //    if (_KuCun == 0)
            //    {
            //        return;
            //    }
            //    if (_KuCun < Convert.ToDouble(grow["gridColumnNumber"].FormattedValue))
            //    {

            //        SelectStockForm select = new SelectStockForm();
            //        SellManager sell = new SellManager();
            //        select.Dt = sell.searchMaterialStockNumber(_AllStock,
            //            _StorageCode,
            //            grow["gridColumnMaCode"].Value.ToString());

            //        select.ShowDialog();
            //    }
            //}
        }
    }

}
