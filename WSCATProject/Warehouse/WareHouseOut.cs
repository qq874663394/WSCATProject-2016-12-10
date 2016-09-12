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

namespace WSCATProject.WareHouse
{
    public partial class WareHouseOut : TemplateForm
    {
        public WareHouseOut()
        {
            InitializeComponent();
        }

        WarehouseOutDetailInterface warehouseout = new WarehouseOutDetailInterface();
        CodingHelper ch = new CodingHelper();//解密表格
        ClientInterface client = new ClientInterface();
        EmpolyeeInterface employee = new EmpolyeeInterface();
        #region  数据字段    
        /// <summary>
        /// 所有客户
        /// </summary>
        private DataTable _AllClient = null;
        /// <summary>
        /// 所有业务员
        /// </summary>
        private DataTable _AllEmployee = null;
        /// <summary>
        /// 点击的项,1为客户,2业务员
        /// </summary>
        private int _Click = 0;
        /// <summary>
        /// 客户编码
        /// </summary>
        private string _ClientCode = "";
        /// <summary>
        /// 业务员
        /// </summary>
        private string _Operation = "";
        private decimal _MaterialMoney;
        private decimal _MaterialNumber;
        private int _state;//判断状态 0、未出库，1、部分出库，2已出库
        private GridRow _wareHouseoutModel;//出库数据的Model
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
        //出库数据的Model
        public GridRow WareHouseoutModel
        {
            get { return _wareHouseoutModel; }
            set { _wareHouseoutModel = value; }
        }
        private int _chukushu { get; set; }//入库数量

        #endregion

        private void StockOut_Load(object sender, EventArgs e)
        {
            comboBoxEx1.SelectedIndex = 0;
            #region 隐藏控件
            checkBox1.Visible = false;
            labTop4.Visible = false;
            labTop6.Visible = false;
            labTop7.Visible = false;
            labtextboxTop4.Visible = false;
            labtextboxTop5.Visible = false;
            labtextboxTop8.Visible = false;
            labtextboxTop9.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            #endregion

            //客户
            _AllClient = client.SelClient(false);

            //业务员
            EmpolyeeInterface employee = new EmpolyeeInterface();
            _AllEmployee = employee.SelSupplierTable(false);

            //禁用自动创建列
            dataGridView1.AutoGenerateColumns = false;
            dataGridViewFujia.AutoGenerateColumns = false;

            //绑定事件 双击事填充内容并隐藏列表
            dataGridViewFujia.CellDoubleClick += DataGridViewFujia_CellDoubleClick;
            // 将dataGridView中的内容居中显示
            dataGridViewFujia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            try
            {
               // 绑定出库单号
                textBoxOddNumbers.Text = _wareHouseoutModel["code"].Value.ToString();
                this.labtextboxTop7.Text = _wareHouseoutModel["salesCode"].Value.ToString();
                comboBoxEx1.SelectedIndex = 0;
                superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：绑定出库单号错误!"+ex.Message);
            }
          
            //待入库进行查看的时候
            if (_state == 0)
            {
                if (_wareHouseoutModel != null)
                {
                    try
                    {
                        if (_wareHouseoutModel.Cells["transportMathod"].Value.ToString() == "快递")
                        {
                            this.label3.Visible = true;
                            this.textBoxX1.Visible = true;
                            this.textBoxX1.Text = _wareHouseoutModel.Cells["logisticsOddCode"].Value.ToString();
                        }
                        //根据条件查询表格里面的数据
                        string code = XYEEncoding.strCodeHex(_wareHouseoutModel["code"].Value.ToString());
                        superGridControl1.PrimaryGrid.DataSource = ch.DataSetReCoding(warehouseout.GetList(" mainCode='" + code + "'"));
                        superGridControl1.PrimaryGrid.EnsureVisible();
                        //调用统计的方法
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
                if (_wareHouseoutModel != null)
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
                if (_wareHouseoutModel != null)
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
        protected override void panel6_Click(object sender, EventArgs e)
        {
            resizablePanel1.Visible = false;
        }

        #region 初始化数据
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
                dgvc.HeaderText = "客户名称";
                dgvc.DataPropertyName = "name";
                dataGridViewFujia.Columns.Add(dgvc);
                resizablePanel1.Location = new Point(530, 110);
                dataGridViewFujia.DataSource = _AllClient;
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
                resizablePanel1.Location = new Point(208, 270);
                dataGridViewFujia.DataSource = _AllEmployee;
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
            gr.Cells["gridColumn5"].Value = 0;
            gr.Cells["gridColumn5"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumn5"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumn6"].Value = 0;
            gr.Cells["gridColumn6"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumn6"].CellStyles.Default.Background.Color1 = Color.Orange;

            try
            {
                //计算金额
                decimal number = Convert.ToDecimal(gr.Cells["gridColumn5"].FormattedValue);
                decimal price = Convert.ToDecimal(gr.Cells["gridColumn6"].FormattedValue);
                decimal allPrice = number * price;
                gr.Cells["gridColumn6"].Value = allPrice;
                //逐行统计数据总数
                decimal tempAllNumber = 0;
                decimal tempAllMoney = 0;
                for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                    tempAllNumber += Convert.ToDecimal(tempGR["gridColumn5"].FormattedValue);
                    tempAllMoney += Convert.ToDecimal(tempGR["gridColumn6"].FormattedValue);
                }
                _MaterialMoney = tempAllMoney;
                _MaterialNumber = tempAllNumber;
                gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                gr["gridColumn5"].Value = _MaterialNumber.ToString();
                gr["gridColumn6"].Value = _MaterialMoney.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("逐行计算金额或统计数据出错！请检查：" + ex.Message);
                throw;
            }

        }
        #endregion

        #region picture  图标点击事件
        /// <summary>
        /// 客户图标的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (_Click != 1)
            {
                InitClient();
            }
        }
        /// <summary>
        /// 业务员图标的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            //客户
            if (_Click == 1)
            {
                string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                labtextboxTop2.Text = name;
                resizablePanel1.Visible = false;
            }

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
        /// 按下ESC按钮关闭子窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WareHouseOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.resizablePanel1.Visible = false;
            }
        }
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            //非空验证
            isNUllValidate();
            WarehouseOut warehouseOut = new WarehouseOut();
            WarehouseOutInterface woi = new WarehouseOutInterface();
            try
            {
                warehouseOut.type = XYEEncoding.strCodeHex(comboBoxEx1.Text.ToString());
                warehouseOut.ClientCode = XYEEncoding.strCodeHex(_ClientCode == "" ? labtextboxTop2.Text.Trim() : _Operation);
                warehouseOut.salesCode = XYEEncoding.strCodeHex(labtextboxTop7.Text.Trim());
                warehouseOut.operation = XYEEncoding.strCodeHex(_Operation == "" ? labtextboxBotton1.Text.Trim() : _Operation);
                warehouseOut.remark = XYEEncoding.strCodeHex(labtextboxBotton2.Text.Trim());
                warehouseOut.examine = XYEEncoding.strCodeHex(labtextboxBotton4.Text.Trim());
                warehouseOut.date = dateTimePicker1.Value;
                warehouseOut.checkState = 0;
                warehouseOut.code = XYEEncoding.strCodeHex(_wareHouseoutModel.Cells["code"].Value.ToString());
                warehouseOut.DefaultType = "57125B065B4E5141";
                warehouseOut.Delivery = XYEEncoding.strCodeHex(_wareHouseoutModel.Cells["transportMathod"].Value.ToString());
                warehouseOut.ExpressMan = "";//未知快递员
                warehouseOut.ExpressOdd = XYEEncoding.strCodeHex(_wareHouseoutModel.Cells["logisticsOddCode"].Value.ToString());
                warehouseOut.ExpressPhone = XYEEncoding.strCodeHex(_wareHouseoutModel.Cells["logisticsPhone"].Value.ToString());
                warehouseOut.id = 1;
                warehouseOut.isClear = 1;
                warehouseOut.reserved1 = "";
                warehouseOut.reserved2 = "";
                warehouseOut.state = 0;
                warehouseOut.stock = "";
                warehouseOut.updateDate = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("出库单保存失败" + ex.Message);
            }

            int result = woi.update(warehouseOut);

            List<WarehouseOutDetail> wareHouseOutList = new List<WarehouseOutDetail>();
            //获得商品列表数据,准备传给base层新增数据
            GridRow g = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
            GridItemsCollection grs = superGridControl1.PrimaryGrid.Rows;
            int i = 0;
            // string _wareHouesDetailCode = XYEEncoding.strCodeHex(BuildCode.ModuleCode("WD"));
            DateTime nowDataTime = DateTime.Now;
            try
            {
                foreach (GridRow gr in grs)
                {
                    if (gr["gridColumn2"].Value.ToString() != "")
                    {
                        i++;
                        WarehouseOutDetail WarehouseOutdetail = new WarehouseOutDetail()
                        {
                            barcode = XYEEncoding.strCodeHex(this.textBoxOddNumbers.Text),
                            date = this.dateTimePicker1.Value,
                            isClear = 1,
                            code = XYEEncoding.strCodeHex(gr["gridColumn1"].Value.ToString()),
                            materiaName = XYEEncoding.strCodeHex(gr["gridColumn2"].Value.ToString()),
                            materiaModel = XYEEncoding.strCodeHex(gr["gridColumn3"].Value.ToString()),
                            materiaUnit = XYEEncoding.strCodeHex(gr["gridColumn4"].Value.ToString()),
                            number = Convert.ToDecimal(gr["gridColumn5"].Value),
                            money = Convert.ToDecimal(gr["gridColumn6"].Value),
                            price = Convert.ToDecimal(gr["gridColumn11"].Value),
                            remark = XYEEncoding.strCodeHex(gr["gridColumn7"].Value.ToString()),
                            StorageRackName = XYEEncoding.strCodeHex(gr["gridColumn9"].Value.ToString()),  //货架名称、排、格
                            updateDate = nowDataTime,
                            state = 1,
                            MainCode = XYEEncoding.strCodeHex(this.textBoxOddNumbers.Text),
                            materialCode = ""
                        };
                        int isarrive = Convert.ToBoolean((superGridControl1.PrimaryGrid.Rows[i] as GridRow).Cells["gridColumn10"].Value) == true ? 1 : 0;
                        if (isarrive == 1)
                        {
                            _chukushu++;
                        }
                        WarehouseOutdetail.IsArrive = isarrive;
                        GridRow dr = superGridControl1.PrimaryGrid.Rows[0] as GridRow;
                        //调用修改出库单的方法
                        wareHouseOutList.Add(WarehouseOutdetail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出库详细赋值失败！"+ex.Message);
            }
            //调用方法修改出库单
            woi.update(warehouseOut, wareHouseOutList);
        }
        public string ExSwitch(int ExValue)
        {
            switch (ExValue)
            {
                case -1:
                    return "错误代码:4001;拼接连接字符串时出现异常,请尝试重新插入数据";
                case -2:
                    return "错误代码:4002;建立查询字符串参数时出现异常";
                case -3:
                    return "错误代码:4003;对参数赋值时出现异常,请检查输入";
                case -4:
                    return "错误代码:4004;尝试打开数据库连接时出错,请检查服务器连接";
                case -5:
                    return "错误代码:4005;对数据库新增数据时未能增加任何数据";
                case -6:
                    return "错误代码:4006;对数据库新增数据的方法失效,未能增加任何行";
                case -7:
                    return "错误代码:4007;检查到传入的参数为空,无法进行新增操作";
                default:
                    return "未知错误.";
            }
        }

        private void buttonExamine_Click(object sender, EventArgs e)
        {
            //非空验证
            isNUllValidate();
            WarehouseOutInterface woi = new WarehouseOutInterface();
            int result = woi.update("state", 1, XYEEncoding.strCodeHex(textBoxOddNumbers.Text.Trim()));
            if (result > 0)
            {
                MessageBox.Show("审核成功");
            }
            else
            {
                MessageBox.Show("审核失败");
            }
        }

        /// <summary>
        /// 非空验证
        /// </summary>
        private void isNUllValidate()
        {
            if (comboBoxEx1.Text.Trim() == null)
            {
                MessageBox.Show("出货类型不能为空！");
            }
            if (labtextboxTop2.Text.Trim() == null)
            {
                MessageBox.Show("客户不能为空！");
            }
            if (labtextboxBotton1.Text.Trim() == null)
            {
                MessageBox.Show("业务员不能为空！");
            }
        }
        /// <summary>
        /// 客户的控件改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labtextboxTop2_TextChanged(object sender, EventArgs e)
        {
            string name = XYEEncoding.strCodeHex( this.labtextboxTop2.Text.Trim());
            dataGridViewFujia.DataSource = null;
            dataGridViewFujia.Columns.Clear();

            DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "code";
            dgvc.HeaderText = "客户编号";
            dgvc.DataPropertyName = "code";
            dataGridViewFujia.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "name";
            dgvc.HeaderText = "客户名称";
            dgvc.DataPropertyName = "name";
            dataGridViewFujia.Columns.Add(dgvc);
            resizablePanel1.Location = new Point(530, 110);
            dataGridViewFujia.DataSource =ch.DataTableReCoding( client.GetList(" name like '%"+name+"%'"));
        }
    }
}
