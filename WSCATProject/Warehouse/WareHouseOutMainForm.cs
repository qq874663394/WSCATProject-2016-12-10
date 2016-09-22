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
        InterfaceLayer.Purchase.PurchaseDetailInterface pdi = new InterfaceLayer.Purchase.PurchaseDetailInterface();
        ClientInterface client = new ClientInterface();//客户
     
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
                //生成出库code 和显示条形码
                _warehouseoutcode = BuildCode.ModuleCode("WHO");
                textBoxOddNumbers.Text = _warehouseoutcode;

                barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
                _Code.ValueFont = new Font("微软雅黑", 20);
                System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
                pictureBox9.Image = imgTemp;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2201-初始化数据错误"+ex.Message,"出库单温馨提示");
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
        /// 初始化商品下拉别表的数据 待修改字段
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

                resizablePanel1.Location = new Point(550, 160);
                dataGridViewFujia.DataSource = ch.DataTableReCoding(_AllClient);
                resizablePanel1.Visible = true;
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
            if (this.labtextboxTop2.Text.Trim() == "")
            {
                MessageBox.Show("请先选择客户，显示销售单号!");
                resizablePanelData.Visible = false;
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
                _AllMaterial = pdi.GetList("" + XYEEncoding.strCodeHex(this.comboBoxEx1.Text.Trim() + ""), "" + materialDaima + "");
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
                //还需要绑定，仓库，货架
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
                //DataTable dt = ch.DataTableReCoding(client.GetList(3,_clientcode));
                //this.comboBoxExxiaos.DataSource = dt;
                //comboBoxExxiaos.ValueMember = "code";
                //comboBoxExxiaos.DisplayMember = "name";

            }
            //业务员
            if (_Click == 2||_Click==4)
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
                MessageBox.Show("错误代码：模糊查询出库员数据错误"+ex.Message,"出库单温馨提示");
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
                if (this.labtextboxTop2.Text.Trim()=="")
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
                dataGridViewFujia.DataSource = ch.DataTableReCoding(client.GetList(0,name));
                resizablePanel1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：模糊查询客户数据错误"+ex.Message,"出库单温馨提示");
            }
        }
    }
}
