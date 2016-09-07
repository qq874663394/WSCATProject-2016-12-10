using DevComponents.DotNetBar.SuperGrid;
using InterfaceLayer.Base;
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

        private void WarehouseAdjustPriceForm_Load(object sender, EventArgs e)
        {
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

            //设置调后单价可输入的最大值和最小值
            GridDoubleInputEditControl gdiecNumber = superGridControl1.PrimaryGrid.Columns["gridColumn9"].EditControl as GridDoubleInputEditControl;
            gdiecNumber.MinValue = 0;
            gdiecNumber.MaxValue = 999999999;

            //调用表格初始化
            superGridControl1.PrimaryGrid.EnsureVisible();
            InitDataGridView();
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

        #region  表格初始化
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
            gr.Cells["gridColumn1"].Value = "合计";
            gr.Cells["gridColumn1"].CellStyles.Default.Alignment =
                DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            //调前单价
            gr.Cells["gridColumn7"].Value = 0;
            gr.Cells["gridColumn7"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumn7"].CellStyles.Default.Background.Color1 = Color.Orange;
            //调前金额
            gr.Cells["gridColumn8"].Value = 0;
            gr.Cells["gridColumn8"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumn8"].CellStyles.Default.Background.Color1 = Color.Orange;
            //调后单价
            gr.Cells["gridColumn9"].Value = 0;
            gr.Cells["gridColumn9"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumn9"].CellStyles.Default.Background.Color1 = Color.Orange;
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

        #region superGridControl单元格验证事件
        private void superGridControl1_CellValidated(object sender, GridCellValidatedEventArgs e)
        {
            GridRow gr = e.GridPanel.Rows[e.GridCell.RowIndex] as GridRow;
            //计算调后金额以及调价金额
            try
            {
                decimal tempAllBeforeprice = 0;
                decimal tempAllBeforemoney = 0;
                decimal tempAllAfterprice = 0;
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
                decimal ADJmoney = tiaoqianMoney - tiaoqianMoney;
                gr.Cells["gridColumn11"].Value = ADJmoney;
                labtextboxTop7.Text = ADJmoney.ToString();

                //逐行统计数据总数
                for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                    tempAllBeforeprice += Convert.ToDecimal(tempGR["gridColumn7"].FormattedValue);
                    tempAllBeforemoney += Convert.ToDecimal(tempGR["gridColumn8"].FormattedValue);
                    tempAllAfterprice += Convert.ToDecimal(tempGR["gridColumn9"].FormattedValue);
                    tempAllAftermoney += Convert.ToDecimal(tempGR["gridColumn10"].FormattedValue);
                    tempALLadjmoney += Convert.ToDecimal(tempGR["gridColumn11"].FormattedValue);
                    tempAllnumber += Convert.ToDecimal(tempGR["gridColumn13"].FormattedValue);
                }
                _MaterialBeforeprice = tempAllBeforeprice;
                _MaterialBeforemoney = tempAllBeforemoney;
                _MaterialAfterprice = tempAllAfterprice;
                _MaterialAftermoney = tempAllAftermoney;
                _MaterialADJmoney = tempALLadjmoney;
                _Materialnumber = tempAllnumber;
                gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                gr["gridColumn7"].Value = _MaterialBeforeprice.ToString();
                gr["gridColumn8"].Value = _MaterialBeforemoney.ToString();
                gr["gridColumn9"].Value = _MaterialAfterprice.ToString();
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
