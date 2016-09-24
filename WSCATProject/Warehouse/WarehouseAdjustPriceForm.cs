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
    public partial class WareHouseAdjustPriceForm : TestForm
    {
        public WareHouseAdjustPriceForm()
        {
            InitializeComponent();
        }

        #region 调用接口以及加密解密方法
        CodingHelper ch = new CodingHelper();
        EmpolyeeInterface employee = new EmpolyeeInterface();
        StorageInterface storage = new StorageInterface();
        WarehouseMainInterface waremain = new WarehouseMainInterface();
        #endregion

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
        /// 调价单code
        /// </summary>
        private string _WareHouseAdjCode;
        /// <summary>
        /// 所有商品列表
        /// </summary>
        private DataTable _AllMaterial = null;
        /// <summary>
        /// 保存商品Code
        /// </summary>
        private string _MaterialCode = "";
        /// <summary>
        /// 所有仓库
        /// </summary>
        private DataTable _AllStorage = null;
        /// <summary>
        /// 仓库列表
        /// </summary>
        private KeyValuePair<string, string> _ClickStorageList;
        /// <summary>
        /// 保存仓库的Code
        /// </summary>
        private string _StorageCode = "";
        /// <summary>
        /// 保存仓库
        /// </summary>
        private string _Storage;
        /// <summary>
        /// 统计调前金额
        /// </summary>
        private decimal _MaterialBeforeMoney;
        /// <summary>
        /// 统计调后金额
        /// </summary>
        private decimal _MaterialAfterMoney;
        /// <summary>
        /// 统计调价金额
        /// </summary>
        private decimal _MaterialAdjMoney;
        /// <summary>
        /// 统计数量
        /// </summary>
        private decimal _MaterialNumber;
        ///// <summary>
        ///// 保存仓库状态 1、仓库 2、商品列表
        ///// </summary>
        private int _StorageState;
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

        private void WareHouseAdjustPriceForm_Load(object sender, EventArgs e)
        {
            //业务员
            _AllEmployee = employee.SelSupplierTable(false);
            //仓库
            _AllStorage = storage.GetList(00, "");

            //调后单价
            GridDoubleInputEditControl diaoruprice = superGridControl1.PrimaryGrid.Columns["gridColumnafterprice"].EditControl as GridDoubleInputEditControl;
            diaoruprice.MinValue = 0;
            diaoruprice.MaxValue = 999999999;

            //禁用自动创建列
            dataGridView1.AutoGenerateColumns = false;
            dataGridViewFujia.AutoGenerateColumns = false;
            superGridControl1.HScrollBarVisible = true;
            //绑定事件 双击事填充内容并隐藏列表
            dataGridViewFujia.CellDoubleClick += DataGridViewFujia_CellDoubleClick;
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;

            // 将dataGridView中的内容居中显示
            dataGridViewFujia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //显示行号
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            //内容居中
            superGridControl1.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            //调用合计行数据
            InitDataGridView();
            //生成code 和显示条形码
            _WareHouseAdjCode = BuildCode.ModuleCode("WAP");
            textBoxOddNumbers.Text = _WareHouseAdjCode;
            barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
            _Code.ValueFont = new Font("微软雅黑", 20);
            System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
            pictureBox9.Image = imgTemp;
        }

        #region  初始化数据

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
            //数量
            gr.Cells["gridColumnnumber"].Value = 0;
            gr.Cells["gridColumnnumber"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnnumber"].CellStyles.Default.Background.Color1 = Color.Orange;
            //调前金额
            gr.Cells["gridColumnbeforemoney"].Value = 0;
            gr.Cells["gridColumnbeforemoney"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnbeforemoney"].CellStyles.Default.Background.Color1 = Color.Orange;
            //调后金额
            gr.Cells["gridColumnaftermoney"].Value = 0;
            gr.Cells["gridColumnaftermoney"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnaftermoney"].CellStyles.Default.Background.Color1 = Color.Orange;
            //调价金额
            gr.Cells["gridColumnmoneyadj"].Value = 0;
            gr.Cells["gridColumnmoneyadj"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnmoneyadj"].CellStyles.Default.Background.Color1 = Color.Orange;
        }

        /// <summary>
        /// 非空验证
        /// </summary>
        private void isNUllValidate()
        {
            if (comboBoxEx1.Text.Trim() == null)
            {
                MessageBox.Show("调价科目不能为空！");
            }
            if (labtextboxBotton1.Text.Trim() == null)
            {
                MessageBox.Show("业务员不能为空！");
            }
            GridRow gr = (GridRow)superGridControl1.PrimaryGrid.
               Rows[superGridControl1.PrimaryGrid.Rows.Count];
            if (gr.Cells["material"].Value == null)
            {
                MessageBox.Show("商品代码不能为空！");
            }
            if (gr.Cells["gridColumnStock"].Value == null )
            {
                MessageBox.Show("仓库不能为空！");
            }

        }

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

                resizablePanel1.Location = new Point(234, 420);
                dataGridViewFujia.DataSource = ch.DataTableReCoding(_AllEmployee);
                resizablePanel1.Visible = true;

                if (this.WindowState == FormWindowState.Maximized)
                {
                    resizablePanel1.Location = new Point(230, 670);
                    return;
                }
                if (this.WindowState == FormWindowState.Normal)
                {
                    resizablePanel1.Location = new Point(230, 420);
                    return;
                }
            }
        }

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

            dataGridView1.DataSource = ch.DataTableReCoding(_AllMaterial);

        }
        #endregion

        #region 小箭头图标和表格数据的点击事件
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (_Click != 1)
            {
                InitEmployee();
                
            }
            _Click = 3;
        }

        /// <summary>
        /// 双击调价员或者仓库信息填写在表格里面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewFujia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //业务员
                if (_Click == 1 || _Click == 3)
                {
                    string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                    labtextboxBotton1.Text = name;
                    resizablePanel1.Visible = false;
                }
                //仓库信息
                if (_Click == 2)
                {

                    GridRow gr = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
                    string code = dataGridViewFujia.Rows[e.RowIndex].Cells["code"].Value.ToString();
                    string Name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                    gr.Cells["gridColumnStock"].Value = Name;
                    //gr.Cells["gridColumncode"].Value = code;
                    _ClickStorageList = new KeyValuePair<string, string>(code, Name);
                    _StorageCode = code;
                    resizablePanel1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("双击绑定数据错误！请检查：" + ex.Message);
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
            gr.Cells["material"].Value = dataGridView1.Rows[e.RowIndex].Cells["code"].Value;//编号
            gr.Cells["gridColumnname"].Value = dataGridView1.Rows[e.RowIndex].Cells["name"].Value;//商品名称
            gr.Cells["gridColumnmodel"].Value = dataGridView1.Rows[e.RowIndex].Cells["model"].Value;//规格型号
            gr.Cells["gridColumnunit"].Value = dataGridView1.Rows[e.RowIndex].Cells["unit"].Value;//单位
            gr.Cells["gridColumntiaoma"].Value = dataGridView1.Rows[e.RowIndex].Cells["barCode"].Value;//条码
           // gr.Cells["gridColumnnumber"].Value = dataGridView1.Rows[e.RowIndex].Cells["number"].Value;//数量
            gr.Cells["gridColumnbeforeprice"].Value = dataGridView1.Rows[e.RowIndex].Cells["price"].Value;//调前单价
            //调前金额
            decimal number = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["number"].Value);
            decimal beforeprice = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["price"].Value);
            gr.Cells["gridColumnbeforemoney"].Value = number * beforeprice;
   
            //当上一次有选择仓库时 默认本次也为上次选择仓库
            if (!string.IsNullOrEmpty(_ClickStorageList.Value) && !string.IsNullOrEmpty(_ClickStorageList.Key))
            {
                gr.Cells["gridColumnstockcode"].Value = _ClickStorageList.Key;
                gr.Cells["gridColumnStock"].Value = _ClickStorageList.Value;
            }
            //新增一行 
            if (newAdd)
            {
                superGridControl1.PrimaryGrid.NewRow(superGridControl1.PrimaryGrid.Rows.Count);
                //递增数量和金额 默认为1和单价 
                gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                _MaterialNumber += 1;
                _MaterialBeforeMoney += beforeprice;
                gr.Cells["gridColumnnumber"].Value = _MaterialNumber;
                gr.Cells["gridColumnbeforemoney"].Value = _MaterialBeforeMoney;
            }
            superGridControl1.Focus();
            SendKeys.Send("^{End}{Home}");
        }

        #endregion

        /// <summary>
        /// 第一个表格选择仓库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "gridColumnStock")
            {
                InitStorageList();
                _StorageState = 1;
                return;
            }
            if (e.GridCell.GridColumn.Name == "material")
            {
                //绑定商品列表
                _AllMaterial = waremain.GetMaterialDetail(XYEEncoding.strCodeHex(_Storage));
                InitMaterialDataGridView();
                _StorageState = 2;
            }
        }

        /// <summary>
        /// 验证完成后，统计单元格数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_CellValidated(object sender, GridCellValidatedEventArgs e)
        {
            GridRow gr = e.GridPanel.Rows[e.GridCell.RowIndex] as GridRow;
            //计算调后金额以及调价金额
            try
            {
                decimal tempAllBeforemoney = 0;
                decimal tempAllAftermoney = 0;
                decimal tempALLadjmoney = 0;
                decimal tempAllnumber = 0;
                //数量
                decimal number = Convert.ToDecimal(gr.Cells["gridColumnnumber"].FormattedValue);
                //调前单价
                decimal tiaoqianPrice = Convert.ToDecimal(gr.Cells["gridColumnbeforeprice"].FormattedValue);
                //调前金额
                decimal tiaoqianMoney = Convert.ToDecimal(gr.Cells["gridColumnbeforemoney"].FormattedValue);
                //调后单价
                decimal tiaohouPrice = Convert.ToDecimal(gr.Cells["gridColumnafterprice"].FormattedValue);
                //调后金额
                decimal tiaohouMoney = number * tiaohouPrice;
                gr.Cells["gridColumnaftermoney"].Value = tiaohouMoney;
                //调价金额
                decimal ADJmoney = tiaohouMoney - tiaoqianMoney;
                gr.Cells["gridColumnmoneyadj"].Value = ADJmoney;
                labtextboxTop7.Text = ADJmoney.ToString();

                //逐行统计数据总数
                for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                    tempAllBeforemoney += Convert.ToDecimal(tempGR["gridColumnbeforemoney"].FormattedValue);
                    tempAllAftermoney += Convert.ToDecimal(tempGR["gridColumnaftermoney"].FormattedValue);
                    tempALLadjmoney += Convert.ToDecimal(tempGR["gridColumnmoneyadj"].FormattedValue);
                    tempAllnumber += Convert.ToDecimal(tempGR["gridColumnnumber"].FormattedValue);
                }
                _MaterialBeforeMoney = tempAllBeforemoney;
                _MaterialAfterMoney = tempAllAftermoney;
                _MaterialAdjMoney = tempALLadjmoney;
                _MaterialNumber = tempAllnumber;
                gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                gr["gridColumnbeforemoney"].Value = _MaterialBeforeMoney.ToString();
                gr["gridColumnaftermoney"].Value = _MaterialAfterMoney.ToString();
                gr["gridColumnmoneyadj"].Value = _MaterialAdjMoney.ToString();
                gr["gridColumnnumber"].Value = _MaterialNumber.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("统计数量错误" + ex.Message);
            }
        }

        /// <summary>
        /// 业务员模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labtextboxBotton1_TextChanged(object sender, EventArgs e)
        {
            if (labtextboxBotton1.Text.Trim() == "")
            {
                _Click = 3;
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

            resizablePanel1.Location = new Point(234, 420);
            dataGridViewFujia.DataSource = ch.DataTableReCoding(employee.GetList(0, "" + XYEEncoding.strCodeHex(labtextboxBotton1.Text.Trim()) + ""));
            resizablePanel1.Visible = true;
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

        /// <summary>
        /// 按下ESC按钮关闭子窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WareHouseAdjustPriceForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.resizablePanel1.Visible = false;
            }
        }

    }
}
