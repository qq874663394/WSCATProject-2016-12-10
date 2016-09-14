using DevComponents.DotNetBar.SuperGrid;
using HelperUtility;
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
    public partial class WarehouseAdjustPriceForm : TemplateForm
    {
        public WarehouseAdjustPriceForm()
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
        private DataTable _AllMaterial = null;
        /// <summary>
        /// 所有仓库列表
        /// </summary>
        private DataTable _AllStorage = null;
        /// <summary>
        /// 保存仓库的Code
        /// </summary>
        private string _StorageCode = "";
        /// <summary>
        /// 保存商品Code
        /// </summary>
        private string _MaterialCode = "";
        /// <summary>
        /// 仓库
        /// </summary>
        private KeyValuePair<string, string> _ClickStorage;
        /// <summary>
        /// 保存仓库
        /// </summary>
        private string _storage;
        ///// <summary>
        ///// 保存仓库状态 1、仓库 2、商品列表
        ///// </summary>
        private int _storageState;

        /// <summary>
        /// 统计调前单价
        /// </summary>
        private decimal _MaterialBeforeprice;
        /// <summary>
        /// 统计调前金额
        /// </summary>
        private decimal _MaterialBeforemoney;
        /// <summary>
        /// 统计调后单价
        /// </summary>
        private decimal _MaterialAfterprice;
        /// <summary>
        /// 统计调后金额
        /// </summary>
        private decimal _MaterialAftermoney;
        /// <summary>
        /// 统计调价金额
        /// </summary>
        private decimal _MaterialADJmoney;
        /// <summary>
        /// 统计数量
        /// </summary>
        private decimal _Materialnumber;

        #endregion

        //调用解密方法
        CodingHelper ch = new CodingHelper();
        WarehouseMainInterface waremain = new WarehouseMainInterface();
        private void WarehouseAdjustPriceForm_Load(object sender, EventArgs e)
        {
            //调价单单号
            textBoxOddNumbers.Text = BuildCode.ModuleCode("TJD");
            //业务员
            EmpolyeeInterface employee = new EmpolyeeInterface();
            _AllEmployee = employee.SelSupplierTable(false);
            //仓库
            StorageInterface storage = new StorageInterface();
            _AllStorage = storage.GetList(00,"");

            //禁用自动创建列
            dataGridView1.AutoGenerateColumns = false;
            dataGridViewFujia.AutoGenerateColumns = false;
            superGridControl1.PrimaryGrid.AutoGenerateColumns = false;

            //绑定事件 双击事填充内容并隐藏列表
            dataGridViewFujia.CellDoubleClick += DataGridViewFujia_CellDoubleClick;
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
            // 将dataGridView中的内容居中显示
            dataGridViewFujia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //设置调后单价可输入的最大值和最小值
            GridDoubleInputEditControl gdiecNumber = superGridControl1.PrimaryGrid.Columns["gridColumn9"].EditControl as GridDoubleInputEditControl;
            gdiecNumber.MinValue = 0;
            gdiecNumber.MaxValue = 999999999;

            //调用表格初始化
            superGridControl1.PrimaryGrid.EnsureVisible();
            InitDataGridView();
            dataGridViewFujia.Visible = true;
        }

        /// <summary>
        /// 窗体任意位置隐藏resizablePanel1
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
                resizablePanel1.Location = new Point(204, 250);
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

        #region  绑定表格的数据
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
            //仓库
            if (_Click == 2)
            {
                GridRow gr = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
                string code = dataGridViewFujia.Rows[e.RowIndex].Cells["code"].Value.ToString();
                string Name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                gr.Cells["gridColumnStock"].Value = Name;
                gr.Cells["gridColumncode"].Value = code;
                _ClickStorage = new KeyValuePair<string, string>(code, Name);
                _storage = code;
                resizablePanel1.Visible = false;
            }
        }

        #endregion

        /// <summary>
        /// 按下ESC按钮关闭子窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WarehouseAdjustPriceForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.resizablePanel1.Visible = false;
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

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "number";
            dgvc.Visible = false;
            dgvc.HeaderText = "number";
            dgvc.DataPropertyName = "number";
            dataGridView1.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "price";
            dgvc.Visible = false;
            dgvc.HeaderText = "price";
            dgvc.DataPropertyName = "price";
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

            dataGridView1.DataSource = ch.DataTableReCoding(_AllMaterial);

        }

        /// <summary>
        /// supergridControl表格初始化
        /// </summary>
        private void InitDataGridView()
        {
            //改为点击可编辑
            //  superGridControl1.PrimaryGrid.MouseEditMode = MouseEditMode.SingleClick;
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
            //调前单价
            //gr.Cells["gridColumn7"].Value = 0;
            //gr.Cells["gridColumn7"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            //gr.Cells["gridColumn7"].CellStyles.Default.Background.Color1 = Color.Orange;
            //调前金额
            gr.Cells["gridColumn8"].Value = 0;
            gr.Cells["gridColumn8"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumn8"].CellStyles.Default.Background.Color1 = Color.Orange;
            //调后单价
            //gr.Cells["gridColumn9"].Value = 0;
            //gr.Cells["gridColumn9"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            //gr.Cells["gridColumn9"].CellStyles.Default.Background.Color1 = Color.Orange;
            //调后金额
            gr.Cells["gridColumn10"].Value = 0;
            gr.Cells["gridColumn10"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumn10"].CellStyles.Default.Background.Color1 = Color.Orange;
            //调价金额
            gr.Cells["gridColumn11"].Value = 0;
            gr.Cells["gridColumn11"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumn11"].CellStyles.Default.Background.Color1 = Color.Orange;
            //数量
            gr.Cells["gridColumn13"].Value = 0;
            gr.Cells["gridColumn13"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumn13"].CellStyles.Default.Background.Color1 = Color.Orange;
        }
        #endregion

        /// <summary>
        /// 表格选择仓库和商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "gridColumnStock")
            {
                //resizablePanel1.Visible = true;
                //绑定仓库列表
                InitStorageList();
                _storageState = 1;
                return;
            }
            if (e.GridCell.GridColumn.Name == "material")
            {
                //绑定商品列表
                _AllMaterial = waremain.GetList("" + XYEEncoding.strCodeHex(_storage) + "");
                InitMaterialDataGridView();
                _storageState = 2;
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
            //SellManager sellManager = new SellManager();
            GridRow gr = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
            //id字段为空 说明是没有数据的行 不是修改而是新增
            if (gr.Cells["gridColumnid"].Value == null)
            {
                newAdd = true;
            }
            gr.Cells["material"].Value = dataGridView1.Rows[e.RowIndex].Cells["code"].Value;//编号
            gr.Cells["gridColumn2"].Value = dataGridView1.Rows[e.RowIndex].Cells["name"].Value;//商品名称
            gr.Cells["gridColumn3"].Value = dataGridView1.Rows[e.RowIndex].Cells["model"].Value;//规格型号
            gr.Cells["gridColumn4"].Value = dataGridView1.Rows[e.RowIndex].Cells["unit"].Value;//单位
            gr.Cells["gridColumn5"].Value = dataGridView1.Rows[e.RowIndex].Cells["barCode"].Value;//条码
            gr.Cells["gridColumn13"].Value = dataGridView1.Rows[e.RowIndex].Cells["number"].Value;//数量
            gr.Cells["gridColumn7"].Value = dataGridView1.Rows[e.RowIndex].Cells["price"].Value;//调前单价
            decimal number = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["number"].Value);
            decimal price = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["price"].Value);
            gr.Cells["gridColumn8"].Value = number * price;
            //调出金额
            //gr.Cells["gridColumncurNumber"].Value = 1;
            //decimal price = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["price"].Value.Equals("") ?
            //    0 : dataGridView1.Rows[e.RowIndex].Cells["price"].Value);
            //gr.Cells["gridColumnOutprice"].Value = price;
            //gr.Cells["gridColumnOutMoney"].Value = price;
            resizablePanelData.Visible = false;

            //当上一次有选择仓库时 默认本次也为上次选择仓库
            if (!string.IsNullOrEmpty(_ClickStorage.Value) && !string.IsNullOrEmpty(_ClickStorage.Key))
            {
                gr.Cells["gridColumncode"].Value = _ClickStorage.Key;
                gr.Cells["gridColumnStock"].Value = _ClickStorage.Value;
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
                _Materialnumber += 1;
                _MaterialBeforemoney += price;
                gr.Cells["gridColumn13"].Value = _Materialnumber;
                gr.Cells["gridColumn8"].Value = _MaterialBeforemoney;
            }
            superGridControl1.Focus();
            SendKeys.Send("^{End}{Home}");
        }


        #region superGridControl单元格验证事件
        private void superGridControl1_CellValidated(object sender, GridCellValidatedEventArgs e)
        {
            GridRow gr = e.GridPanel.Rows[e.GridCell.RowIndex] as GridRow;
            //计算调后金额以及调价金额
            try
            {
                //decimal tempAllBeforeprice = 0;
                decimal tempAllBeforemoney = 0;
                //decimal tempAllAfterprice = 0;
                decimal tempAllAftermoney = 0;
                decimal tempALLadjmoney = 0;
                decimal tempAllnumber = 0;
                //数量
                decimal number = Convert.ToDecimal(gr.Cells["gridColumn13"].FormattedValue);
                //调前单价
                decimal tiaoqianPrice = Convert.ToDecimal(gr.Cells["gridColumn7"].FormattedValue);
                //调前金额
                decimal tiaoqianMoney = Convert.ToDecimal(gr.Cells["gridColumn8"].FormattedValue);
                //调后单价
                decimal tiaohouPrice = Convert.ToDecimal(gr.Cells["gridColumn9"].FormattedValue);
                //调后金额
                decimal tiaohouMoney = number * tiaohouPrice;
                gr.Cells["gridColumn10"].Value = tiaohouMoney;
                //调价金额
                decimal ADJmoney = tiaohouMoney - tiaoqianMoney;
                gr.Cells["gridColumn11"].Value = ADJmoney;
                labtextboxTop7.Text = ADJmoney.ToString();

                //逐行统计数据总数
                for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                    //tempAllBeforeprice += Convert.ToDecimal(tempGR["gridColumn7"].FormattedValue);
                    tempAllBeforemoney += Convert.ToDecimal(tempGR["gridColumn8"].FormattedValue);
                   // tempAllAfterprice += Convert.ToDecimal(tempGR["gridColumn9"].FormattedValue);
                    tempAllAftermoney += Convert.ToDecimal(tempGR["gridColumn10"].FormattedValue);
                    tempALLadjmoney += Convert.ToDecimal(tempGR["gridColumn11"].FormattedValue);
                    tempAllnumber += Convert.ToDecimal(tempGR["gridColumn13"].FormattedValue);
                }
              //  _MaterialBeforeprice = tempAllBeforeprice;
                _MaterialBeforemoney = tempAllBeforemoney;
               // _MaterialAfterprice = tempAllAfterprice;
                _MaterialAftermoney = tempAllAftermoney;
                _MaterialADJmoney = tempALLadjmoney;
                _Materialnumber = tempAllnumber;
                gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                //gr["gridColumn7"].Value = _MaterialBeforeprice.ToString();
                gr["gridColumn8"].Value = _MaterialBeforemoney.ToString();
               // gr["gridColumn9"].Value = _MaterialAfterprice.ToString();
                gr["gridColumn10"].Value = _MaterialAftermoney.ToString();
                gr["gridColumn11"].Value = _MaterialADJmoney.ToString();
                gr["gridColumn13"].Value = _Materialnumber.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("统计数量错误" + ex.Message);
            }
        }
        #endregion

       
    }
}
