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

namespace WSCATProject.WareHouse
{
    public partial class WareHouseOut : TemplateForm
    {
        public WareHouseOut()
        {
            InitializeComponent();
        }

        WarehouseOutDetailInterface warehouseout = new WarehouseOutDetailInterface();
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
        private decimal _MaterialMoney;
        private decimal _MaterialNumber;
        private int _state;//判断状态 0、未出库，1、部分出库，2已出库
        private GridRow _wareHouseoutModel;//出库数据的Model
        public int State
        {
            get {  return _state;}
            set {_state = value; }
        }
        //出库数据的Model
        public GridRow WareHouseoutModel 
        {
            get{return _wareHouseoutModel;}
            set { _wareHouseoutModel = value;}
        }

        #endregion

        private void StockOut_Load(object sender, EventArgs e)
        {
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
            ClientInterface client = new ClientInterface();
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


            textBoxOddNumbers.Text = _wareHouseoutModel["code"].Value.ToString();
            this.labtextboxTop5.Text = _wareHouseoutModel["salesCode"].Value.ToString();
            comboBoxEx1.SelectedIndex = 0;
            superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            //待入库进行查看的时候
            if (_state == 0)
            {
                if (_wareHouseoutModel != null)
                {
                    try
                    {
                        //根据条件查询表格里面的数据
                        string code = XYEEncoding.strCodeHex(_wareHouseoutModel["code"].Value.ToString());
                        superGridControl1.PrimaryGrid.DataSource = warehouseout.GetList(" mainCode='"+code+"'");
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
        protected void panel6_Click(object sender, EventArgs e)
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

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }
    }
}
