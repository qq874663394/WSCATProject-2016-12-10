using DevComponents.DotNetBar.SuperGrid;
using HelperUtility;
using HelperUtility.Encrypt;
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
    public partial class WareHouseInventoryLossForm : TestForm
    {
        public WareHouseInventoryLossForm()
        {
            InitializeComponent();
        }

        #region 调用接口以及加密解密方法
        CodingHelper ch = new CodingHelper();
        EmpolyeeInterface employee = new EmpolyeeInterface();
        #endregion

        #region  数据字段
        /// <summary>
        /// 所有业务员
        /// </summary>
        private DataTable _AllEmployee = null;
        /// <summary>
        /// 点击的项 1为业务员
        /// </summary>
        private int _Click = 0;
        /// <summary>
        /// 盘亏单code
        /// </summary>
        private string _WareHousePanKuiCode;
        /// <summary>
        /// 统计贮存数量
        /// </summary>
        private decimal _AllZhuCunShuLiang;
        /// <summary>
        /// 统计盘点数量
        /// </summary>
        private decimal _AllPanDianShuLiang;
        /// <summary>
        /// 统计盘亏数量
        /// </summary>
        private decimal _AllPanKuiShuLiang;
        /// <summary>
        /// 统计盘亏金额
        /// </summary>
        private decimal _AllPanKuiMoney;
        #endregion

        private void WareHouseInventoryLossForm_Load(object sender, EventArgs e)
        {
            //业务员
            _AllEmployee = employee.SelSupplierTable(false);

            //数量
            GridDoubleInputEditControl gdiecNumber = superGridControl1.PrimaryGrid.Columns["pandiannumber"].EditControl as GridDoubleInputEditControl;
            gdiecNumber.MinValue = 0;
            gdiecNumber.MaxValue = 999999999;

            //禁用自动创建列
            dataGridView1.AutoGenerateColumns = false;
            dataGridViewFujia.AutoGenerateColumns = false;
            superGridControl1.HScrollBarVisible = true;
            //绑定事件 双击事填充内容并隐藏列表
            dataGridViewFujia.CellDoubleClick += DataGridViewFujia_CellDoubleClick;
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
            // 将dataGridView中的内容居中显示
            dataGridViewFujia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //调用合计行数据
            InitDataGridView();
            //生成code 和显示条形码
            _WareHousePanKuiCode = BuildCode.ModuleCode("WIL");
            textBoxOddNumbers.Text = _WareHousePanKuiCode;
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
            gr.Cells["zhangcunnumber"].Value = 0;
            gr.Cells["zhangcunnumber"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["zhangcunnumber"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["pandiannumber"].Value = 0;
            gr.Cells["pandiannumber"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["pandiannumber"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["pankuinumber"].Value = 0;
            gr.Cells["pankuinumber"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["pankuinumber"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["pankuimoney"].Value = 0;
            gr.Cells["pankuimoney"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["pankuimoney"].CellStyles.Default.Background.Color1 = Color.Orange;
        }

        /// <summary>
        /// 非空验证
        /// </summary>
        private void isNUllValidate()
        {
            if (cboOutType.Text.Trim() == null)
            {
                MessageBox.Show("出库类别不能为空！");
            }
            if (ltxtbSalsMan.Text.Trim() == null)
            {
                MessageBox.Show("业务员不能为空！");
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

                resizablePanel1.Location = new Point(234, 440);
                dataGridViewFujia.DataSource = ch.DataTableReCoding(_AllEmployee);
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

        #endregion

        #region 小箭头图标和表格数据的点击事件
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (_Click != 1)
            {
                InitEmployee();
            }
            _Click = 2;
        }


        private void DataGridViewFujia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //业务员
                if (_Click == 1 || _Click == 2)
                {
                    string name = dataGridViewFujia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                    ltxtbSalsMan.Text = name;
                    resizablePanel1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("双击绑定业务员员数据错误！请检查：" + ex.Message);
            }
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //表的点击事件
        }
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

        /// <summary>
        /// 业务员模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labtextboxBotton1_TextChanged(object sender, EventArgs e)
        {
            if (ltxtbSalsMan.Text.Trim() == "")
            {
                _Click = 2;
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

            resizablePanel1.Location = new Point(234, 440);
            dataGridViewFujia.DataSource = ch.DataTableReCoding(employee.GetList(0, "" + XYEEncoding.strCodeHex(ltxtbSalsMan.Text.Trim()) + ""));
            resizablePanel1.Visible = true;
        }

        /// <summary>
        /// 按下ESC关闭子窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WareHouseInventoryLossForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.resizablePanel1.Visible = false;
            }
        }

        /// <summary>
        /// 统计和验证数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_CellValidated(object sender, GridCellValidatedEventArgs e)
        {
                //最后一行做统计行
                GridRow gr = e.GridPanel.Rows[e.GridCell.RowIndex] as GridRow;
            //try
            //{
            //    decimal zhucunshu = Convert.ToDecimal(gr.Cells["zhangcunnumber"].FormattedValue);
            //    decimal pandianshu = Convert.ToDecimal(gr.Cells["pandiannumber"].FormattedValue);
            //    decimal price = Convert.ToDecimal(gr.Cells["price"].FormattedValue);
            //    decimal pankuishu = zhucunshu - pandianshu;
            //    gr.Cells["pankuinumber"].Value = pankuishu;
            //    decimal money = pankuishu * price;
            //    gr.Cells["pankuimoney"].Value = money;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("计算盘亏数量和盘亏金额出错！请检查：" + ex.Message);
            //}

            try
            {
                //逐行统计数据总数
                decimal tempAllzhucun = 0;
                decimal tempAllpandian = 0;
                decimal tempAllpankui = 0;
                decimal tempAllpankuimoney = 0;
                for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                    tempAllzhucun += Convert.ToDecimal(tempGR["zhangcunnumber"].FormattedValue);
                    tempAllpandian += Convert.ToDecimal(tempGR["pandiannumber"].FormattedValue);
                    tempAllpankui += Convert.ToDecimal(tempGR["pankuinumber"].FormattedValue);
                    tempAllpankuimoney += Convert.ToDecimal(tempGR["pankuimoney"].FormattedValue);
                }
                _AllZhuCunShuLiang = tempAllzhucun;
                _AllPanDianShuLiang = tempAllpandian;
                _AllPanKuiShuLiang = tempAllpankui;
                _AllPanKuiMoney = tempAllpankuimoney;
                gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                gr["zhangcunnumber"].Value = _AllZhuCunShuLiang.ToString();
                gr["pandiannumber"].Value = _AllPanDianShuLiang.ToString();
                gr["pankuinumber"].Value = _AllPanKuiShuLiang.ToString();
                gr["pankuimoney"].Value = _AllPanKuiMoney.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("统计出错！请检查：" + ex.Message);
            }
        }
    }
}
