using DevComponents.DotNetBar.SuperGrid;
using HelperUtility;
using HelperUtility.Encrypt;
using InterfaceLayer.Base;
using InterfaceLayer.Warehouse;
using LogicLayer.Warehouse;
using Model.Warehouse;
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
        WarehouseInventoryDetailInterface warehouseinv = new WarehouseInventoryDetailInterface();
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
        /// <summary>
        /// 仓库code
        /// </summary>
        private string _storageCode;
        public string StorageCode
        {
            get { return _storageCode; }
            set { _storageCode = value; }
        }

        /// <summary>
        /// 仓库code
        /// </summary>
        private string _storageName;
        public string StorageName
        {
            get { return _storageName; }
            set { _storageName = value; }
        }
        #endregion

        private void WareHouseInventoryLossForm_Load(object sender, EventArgs e)
        {
            try
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
                toolStripButtonsave.Click += ToolStripButtonsave_Click;//保存按钮
                toolStripButtonshen.Click += ToolStripButtonshen_Click;//审核按钮

                //生成code 和显示条形码
                _WareHousePanKuiCode = BuildCode.ModuleCode("WIL");
                textBoxOddNumbers.Text = _WareHousePanKuiCode;
                barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
                _Code.ValueFont = new Font("微软雅黑", 20);
                System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
                pictureBox9.Image = imgTemp;
                superGridControl1.PrimaryGrid.AutoGenerateColumns = false;//禁止自动创建列
                superGridControl1.DefaultVisualStyles.CellStyles.Default.Alignment =
                DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;//设置内容居中
                superGridControl1.PrimaryGrid.DataSource = ch.DataTableReCoding(warehouseinv.Search(5, XYEEncoding.strCodeHex(_storageCode)));
                superGridControl1.PrimaryGrid.EnsureVisible();
                //调用合计行数据
                InitDataGridView();
                labtextboxTop7.Text = "由[" + _storageName + "]生成";
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码- ：初始化数据错误，没有仓库code"+ex.Message);
            }
        }
        /// <summary>
        /// 审核按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonshen_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonsave_Click(object sender, EventArgs e)
        {
            //非空验证
            isNUllValidate();
            //获得界面上的数据,准备传给base层新增数据
            WarehouseInventoryLossInterface warehouseinvloss = new WarehouseInventoryLossInterface();
            //盘亏单
            // WarehouseIn warehouseIn = new WarehouseIn();
            WarehouseInventoryLoss warehouseloss = new WarehouseInventoryLoss();
            //盘亏商品列表
            List<WarehouseInventoryLossDetail> wareHouselossList = new List<WarehouseInventoryLossDetail>();
            try
            {
                warehouseloss.checkState = 0;
                warehouseloss.code = textBoxOddNumbers.Text == "" ? "" : XYEEncoding.strCodeHex(textBoxOddNumbers.Text);
                warehouseloss.date = dateTimePicker1.Value;
                warehouseloss.examine = ltxtbShengHeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbShengHeMan.Text);
                warehouseloss.isClear = 1;
                warehouseloss.makeMan = ltxtbMakeMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbMakeMan.Text);
                warehouseloss.operation = ltxtbSalsMan.Text == "" ? "" : XYEEncoding.strCodeHex(ltxtbSalsMan.Text);
                warehouseloss.remark = labtextboxTop7.Text == "" ? "" : XYEEncoding.strCodeHex(labtextboxTop7.Text);
                warehouseloss.reserved1 = "";
                warehouseloss.reserved2 = "";
                warehouseloss.type = cboOutType.Text == "" ? "" : XYEEncoding.strCodeHex(cboOutType.Text);
                warehouseloss.updatetime = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码:;尝试创建盘亏单商品数据出错,请检查输入" + ex.Message, "入库单温馨提示");
                return;
            }

            try
            {
                //获得商品列表数据,准备传给base层新增数据
                GridRow g = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
                GridItemsCollection grs = superGridControl1.PrimaryGrid.Rows;
                int i = 0;
                DateTime nowDataTime = DateTime.Now;
                foreach (GridRow gr in grs)
                {
                    if (gr["name"].FormattedValue != "")
                    {
                        i++;
                        WarehouseInventoryLossDetail warehouselossDetail = new WarehouseInventoryLossDetail();
                        warehouselossDetail.barCode = gr["tiaoxingma"].Value.ToString()==""?"":XYEEncoding.strCodeHex(gr["tiaoxingma"].Value.ToString());
                        warehouselossDetail.code = XYEEncoding.strCodeHex(textBoxOddNumbers.Text) + i.ToString();
                        warehouselossDetail.effectiveDate = Convert.ToDateTime( gr["youxiaoqi"].Value.ToString()==""?null:gr["youxiaoqi"].Value);
                        warehouselossDetail.inventoryNumber = Convert.ToDecimal(gr["pandiannumber"].Value.ToString());
                        warehouselossDetail.isClear = 1;
                        warehouselossDetail.lossMoney = Convert.ToDecimal(gr["pankuimoney"].Value.ToString());
                        warehouselossDetail.lossNumber =  Convert.ToDecimal(gr["pankuinumber"].Value.ToString());
                        warehouselossDetail.mainCode = XYEEncoding.strCodeHex(textBoxOddNumbers.Text);
                        warehouselossDetail.materialCode = gr["materialcode"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["materialcode"].Value.ToString());
                        warehouselossDetail.materialDaima = gr["material"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["material"].Value.ToString());
                        warehouselossDetail.materialModel = gr["model"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["model"].Value.ToString());
                        warehouselossDetail.materialName = gr["name"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["name"].Value.ToString());
                        warehouselossDetail.materiaUnit = gr["unit"].Value.ToString() == "" ? "" : XYEEncoding.strCodeHex(gr["unit"].Value.ToString());
                        warehouselossDetail.number =Convert.ToDecimal(gr["zhangcunnumber"].Value.ToString());
                        warehouselossDetail.price =  Convert.ToDecimal(gr["price"].Value.ToString());
                        warehouselossDetail.productionDate =Convert.ToDateTime(gr["shengchandate"].Value.ToString());
                        warehouselossDetail.qualityDate = Convert.ToDecimal(gr["baozhiqi"].Value.ToString());
                        warehouselossDetail.reserved1 = "";
                        warehouselossDetail.reserved2 = "";
                        warehouselossDetail.updateDate = DateTime.Now;
                        warehouselossDetail.warehouseCode = "";
                        warehouselossDetail.warehouseName = "";
                        GridRow dr = superGridControl1.PrimaryGrid.Rows[0] as GridRow;
                        wareHouselossList.Add(warehouselossDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误代码：2105-尝试创建入库单详商品数据出错,请检查输入" + ex.Message, "入库单温馨提示");
                return;
            }

            //增加一条入库单和入库单详细数据
            object Result = warehouseinvloss.Add(warehouseloss, wareHouselossList);
            // this.textBoxid.Text = warehouseInResult.ToString(); //前单后单
            if (Result != null)
            {
                MessageBox.Show("新盘亏单数据成功", "盘亏单温馨提示");
            }
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
