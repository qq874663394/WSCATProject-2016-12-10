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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSCATProject.Sales
{
    public partial class SalesTicketForm : TestForm
    {
        public SalesTicketForm()
        {
            InitializeComponent();
        }

        #region 调用接口以及加密解密方法
        CodingHelper ch = new CodingHelper();
        ClientInterface client = new ClientInterface();//客户
        EmpolyeeInterface employee = new EmpolyeeInterface();//员工  
        StorageInterface storage = new StorageInterface();//仓库
        #endregion

        #region 数据字段

        /// <summary>
        /// 所有客户
        /// </summary>
        private DataTable _AllClient = null;
        /// <summary>
        /// 所有销售员
        /// </summary>
        private DataTable _AllEmployee = null;
        /// <summary>
        /// 所有仓库
        /// </summary>
        private DataTable _AllStorage = null;
        /// <summary>
        /// 点击的项,1客户  2为销售员  3为仓库   
        /// </summary>
        private int _Click = 0;
        /// <summary>
        /// 保存客户Code
        /// </summary>
        private string _clientCode;
        /// <summary>
        /// 保存销售员Code
        /// </summary>
        private string _employeeCode;
        /// <summary>
        /// 保存仓库Code
        /// </summary>
        private string _storgeCode;
        /// <summary>
        /// 所有商品列表
        /// </summary>
        private DataTable _AllMaterial = null;
        /// <summary>
        /// 统计数量
        /// </summary>
        private decimal _Materialnumber;
        /// <summary>
        /// 销售订单code
        /// </summary>
        private string _SalesOrderCode;
        #endregion

        #region 初始化数据
        /// <summary>
        /// 统计行数据
        /// </summary>
        private void InitDataGridView()
        {
            //新增一行 用于给客户操作
            superGridControlShangPing.PrimaryGrid.NewRow(true);
            //最后一行做统计行
            GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.
                Rows[superGridControlShangPing.PrimaryGrid.Rows.Count - 1];
            gr.ReadOnly = true;
            gr.CellStyles.Default.Background.Color1 = Color.SkyBlue;
            gr.Cells["material"].Value = "合计";
            gr.Cells["material"].CellStyles.Default.Alignment =
                DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumndinggoushu"].Value = 0;
            gr.Cells["gridColumndinggoushu"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumndinggoushu"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumnfahuoshu"].Value = 0;
            gr.Cells["gridColumnfahuoshu"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnfahuoshu"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumnmoney"].Value = 0;
            gr.Cells["gridColumnmoney"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnmoney"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumnshuie"].Value = 0;
            gr.Cells["gridColumnshuie"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnshuie"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumnjiashuiheji"].Value = 0;
            gr.Cells["gridColumnjiashuiheji"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnjiashuiheji"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumnchengbenjine"].Value = 0;
            gr.Cells["gridColumnchengbenjine"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnchengbenjine"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumntuihuoshu"].Value = 0;
            gr.Cells["gridColumntuihuoshu"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumntuihuoshu"].CellStyles.Default.Background.Color1 = Color.Orange;
        }

        /// <summary>
        /// 初始化客户
        /// </summary>
        private void InitClient()
        {
            if (_Click != 1)
            {
                _Click = 1;
                dataGridViewFuJia.DataSource = null;
                dataGridViewFuJia.Columns.Clear();

                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "code";
                dgvc.HeaderText = "客户编号";
                dgvc.DataPropertyName = "code";
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "name";
                dgvc.HeaderText = "客户姓名";
                dgvc.DataPropertyName = "name";
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "mobilePhone";
                dgvc.HeaderText = "电话";
                dgvc.DataPropertyName = "mobilePhone";
                dgvc.Visible = false;
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "linkMan";
                dgvc.HeaderText = "联系人";
                dgvc.DataPropertyName = "linkMan";
                dgvc.Visible = false;
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "fax";
                dgvc.HeaderText = "传真";
                dgvc.DataPropertyName = "fax";
                dgvc.Visible = false;
                dataGridViewFuJia.Columns.Add(dgvc);

                resizablePanel1.Location = new Point(460, 160);
                dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllClient);
                resizablePanel1.Visible = true;
            }
        }

        /// <summary>
        /// 初始化销售员
        /// </summary>
        private void InitEmployee()
        {
            if (_Click != 2)
            {
                _Click = 2;
                dataGridViewFuJia.DataSource = null;
                dataGridViewFuJia.Columns.Clear();

                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "code";
                dgvc.HeaderText = "员工工号";
                dgvc.DataPropertyName = "员工工号";
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "name";
                dgvc.HeaderText = "姓名";
                dgvc.DataPropertyName = "姓名";
                dataGridViewFuJia.Columns.Add(dgvc);

                dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllEmployee);
                resizablePanel1.Visible = true;
                if (this.WindowState == FormWindowState.Maximized)
                {
                    resizablePanel1.Location = new Point(220, 670);
                    return;
                }
                if (this.WindowState == FormWindowState.Normal)
                {
                    resizablePanel1.Location = new Point(220, 520);
                    return;
                }
            }
        }

        /// <summary>
        /// 初始化仓库
        /// </summary>
        private void InitStorage()
        {
            if (_Click != 3)
            {
                _Click = 3;
                dataGridViewFuJia.DataSource = null;
                dataGridViewFuJia.Columns.Clear();

                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "code";
                dgvc.Visible = true;
                dgvc.HeaderText = "仓库编号";
                dgvc.DataPropertyName = "code";
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "name";
                dgvc.Visible = true;
                dgvc.HeaderText = "仓库名称";
                dgvc.DataPropertyName = "name";
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "address";
                dgvc.Visible = false;
                dgvc.HeaderText = "仓库地址";
                dgvc.DataPropertyName = "address";
                dataGridViewFuJia.Columns.Add(dgvc);

                //查询仓库的方法
                dataGridViewFuJia.DataSource = ch.DataTableReCoding(_AllStorage);
                resizablePanel1.Visible = true;
            }
        }

        #endregion

        private void SalesTicketForm_Load(object sender, EventArgs e)
        {
            toolStripButtonXuanYuanDan.Visible = true;
            //客户
            _AllClient = client.GetClientByBool(false);
            //销售员
            _AllEmployee = employee.SelSupplierTable(false);
            //仓库
            _AllStorage = storage.GetList(00, "");

            //绑定事件 双击事填充内容并隐藏列表
            dataGridViewFuJia.CellDoubleClick += dataGridViewFuJia_CellDoubleClick;
            dataGridViewShangPing.CellDoubleClick += dataGridViewShangPing_CellDoubleClick;

            #region 初始化窗体

            comboBoxType.SelectedIndex = 0;//单据类型
            comboBoxfapiaotype.SelectedIndex = 0;//发票类型

            //禁用自动创建列
            dataGridViewShangPing.AutoGenerateColumns = false;
            dataGridViewFuJia.AutoGenerateColumns = false;
            superGridControlShangPing.HScrollBarVisible = true;
            // 将dataGridView中的内容居中显示
            dataGridViewFuJia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //显示行号
            superGridControlShangPing.PrimaryGrid.ShowRowGridIndex = true;
            //内容居中
            superGridControlShangPing.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            //调用合计行数据
            InitDataGridView();

            #endregion

            //生成销售单code和显示条形码
            _SalesOrderCode = BuildCode.ModuleCode("ST");
            textBoxOddNumbers.Text = _SalesOrderCode;
            barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
            _Code.ValueFont = new Font("微软雅黑", 20);
            System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
            pictureBoxtiaoxingma.Image = imgTemp;
        }

        private void dataGridViewFuJia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //客户
                if (_Click == 1 || _Click == 4)
                {
                    _clientCode = dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString();//客户code
                    string name = dataGridViewFuJia.Rows[e.RowIndex].Cells["name"].Value.ToString();//客户名称
                    string linkman = dataGridViewFuJia.Rows[e.RowIndex].Cells["linkMan"].Value.ToString();//联系人
                    string phone = dataGridViewFuJia.Rows[e.RowIndex].Cells["mobilePhone"].Value.ToString();//电话
                    string fax = dataGridViewFuJia.Rows[e.RowIndex].Cells["fax"].Value.ToString();//传真
                    labtextboxTop2.Text = name;
                    labtextboxTop8.Text = linkman;
                    labtextboxTop3.Text = phone;
                    labtextboxTop6.Text = fax;
                    resizablePanel1.Visible = false;
                }
                //销售员
                if (_Click == 2 || _Click == 5)
                {
                    _employeeCode = dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString();//销售员code
                    string name = dataGridViewFuJia.Rows[e.RowIndex].Cells["name"].Value.ToString();//销售员
                    ltxtbSalsMan.Text = name;
                    resizablePanel1.Visible = false;
                }
                if (_Click == 3 || _Click == 6)
                {
                    GridRow gr = (GridRow)superGridControlShangPing.PrimaryGrid.Rows[ClickRowIndex];
                    string code = dataGridViewFuJia.Rows[e.RowIndex].Cells["code"].Value.ToString();
                    string Name = dataGridViewFuJia.Rows[e.RowIndex].Cells["name"].Value.ToString();
                    gr.Cells["gridColumnStock"].Value = Name;
                    //gr.Cells["gridColumncode"].Value = code;
                    // _ClickStorageList = new KeyValuePair<string, string>(code, Name);
                    _storgeCode = code;
                    resizablePanel1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("双击绑定数据错误！请检查：" + ex.Message);
            }
        }

        /// <summary>
        /// 双击绑定商品的
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewShangPing_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /// <summary>
        /// 客户的下拉箭头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxClient_Click(object sender, EventArgs e)
        {
            if (_Click != 1)
            {
                InitClient();
            }
            _Click = 4;
        }
        /// <summary>
        /// 销售员的下拉箭头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxEmployee_Click(object sender, EventArgs e)
        {
            if (_Click != 2)
            {
                InitEmployee();
            }
           _Click = 5;
        }

        /// <summary>
        /// 第一行第一列选择仓库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControlShangPing_BeginEdit(object sender, GridEditEventArgs e)
        {
            try
            {
                if (e.GridCell.GridColumn.Name == "gridColumnStock")
                {
                    if (_Click != 3)
                    {
                        InitStorage();
                    }
                    _Click = 6;
                    return;
                }
                if (e.GridCell.GridColumn.Name == "material")
                {
                    //if (_StorageCode != "")
                    //{
                    //    //查询商品列表
                    //    _AllMaterial = waremain.GetWMainAndMaterialByWMCode(999, "", XYEEncoding.strCodeHex(_StorageCode));
                    //    InitMaterialDataGridView();
                    //}
                    //else
                    //{
                    //    resizablePanelData.Visible = false;
                    //    MessageBox.Show("请先选择仓库：");
                    //}
                }
            }
            catch (Exception)
            {
                //if (_StorageCode != "")
                //{
                //    //查询商品列表
                //    _AllMaterial = waremain.GetWMainAndMaterialByWMCode(999, "", XYEEncoding.strCodeHex(_StorageCode));
                //    InitMaterialDataGridView();
                //}
                //else
                //{
                //    this.resizablePanelData.Visible = false;
                //    MessageBox.Show("请先选择仓库！");
                //}
            }

        }

        /// <summary>
        /// 客户的模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labtextboxTopClient_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.labtxtDanJuType.Text.Trim() == "")
                {
                    InitClient();
                    _Click = 4;
                    return;
                }
                dataGridViewFuJia.DataSource = null;
                dataGridViewFuJia.Columns.Clear();

                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "code";
                dgvc.HeaderText = "客户编号";
                dgvc.DataPropertyName = "code";
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "name";
                dgvc.HeaderText = "客户姓名";
                dgvc.DataPropertyName = "name";
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "mobilePhone";
                dgvc.HeaderText = "电话";
                dgvc.DataPropertyName = "mobilePhone";
                dgvc.Visible = false;
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "linkMan";
                dgvc.HeaderText = "联系人";
                dgvc.DataPropertyName = "linkMan";
                dgvc.Visible = false;
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "fax";
                dgvc.HeaderText = "传真";
                dgvc.DataPropertyName = "fax";
                dgvc.Visible = false;
                dataGridViewFuJia.Columns.Add(dgvc);

                resizablePanel1.Location = new Point(230, 160);
                string name = XYEEncoding.strCodeHex(this.labtxtDanJuType.Text.Trim());
                dataGridViewFuJia.DataSource = ch.DataTableReCoding(client.GetList(0, name));
                resizablePanel1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：模糊查询客户数据错误" + ex.Message, "销售订单温馨提示");
            }
        }
        /// <summary>
        /// 销售员的模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ltxtbSalsMan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ltxtbSalsMan.Text.Trim() == "")
                {
                    _Click = 5;
                    InitEmployee();
                    return;
                }
                dataGridViewFuJia.DataSource = null;
                dataGridViewFuJia.Columns.Clear();

                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "code";
                dgvc.HeaderText = "员工工号";
                dgvc.DataPropertyName = "code";
                dataGridViewFuJia.Columns.Add(dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "name";
                dgvc.HeaderText = "姓名";
                dgvc.DataPropertyName = "name";
                dataGridViewFuJia.Columns.Add(dgvc);

                dataGridViewFuJia.DataSource = ch.DataTableReCoding(employee.GetList(0, "" + XYEEncoding.strCodeHex(ltxtbSalsMan.Text.Trim()) + ""));
                resizablePanel1.Visible = true;
                if (this.WindowState == FormWindowState.Maximized)
                {
                    resizablePanel1.Location = new Point(220, 670);
                    return;
                }
                if (this.WindowState == FormWindowState.Normal)
                {
                    resizablePanel1.Location = new Point(234, 460);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-销售员模糊查询数据失败！" + ex.Message);
            }
        }

        /// <summary>
        /// 按ESC关闭副表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SalesTicketForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.resizablePanel1.Visible = false;
                this.resizablePanelData.Visible = false;
            }
        }

        /// <summary>
        /// 单据类型改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (comboBoxType.Text.Trim())
                {
                    case "销售发货":
                        superGridControlShangPing.PrimaryGrid.Columns["gridColumndinggoushu"].Visible = true;
                        superGridControlShangPing.PrimaryGrid.Columns["gridColumntuihuoshu"].Visible = true;
                        break;
                    case "销售退货":
                        superGridControlShangPing.PrimaryGrid.Columns["gridColumndinggoushu"].Visible = false;
                        superGridControlShangPing.PrimaryGrid.Columns["gridColumntuihuoshu"].Visible = false;
                        break;
                    case "委托退货":
                        break;
                    case "委托结算退货":
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：-选择调拨类型数据错误" + ex.Message);
            }
        }

        /// <summary>
        /// 修改边框 的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                             this.panel2.ClientRectangle,
                             Color.FromArgb(85, 177, 238),
                             2,
                             ButtonBorderStyle.Solid,
                             Color.FromArgb(85, 177, 238),
                             1,
                             ButtonBorderStyle.Solid,
                             Color.FromArgb(85, 177, 238),
                             2,
                             ButtonBorderStyle.Solid,
                             Color.White,
                             1,
                             ButtonBorderStyle.Solid);
        }

        private void SalesTicketForm_Activated(object sender, EventArgs e)
        {
            labtextboxTop2.Focus();//焦点在客户上
        }
    }
}
