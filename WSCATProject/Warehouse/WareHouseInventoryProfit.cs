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
    public partial class WareHouseInventoryProfit : TestForm
    {
        public WareHouseInventoryProfit()
        {
            InitializeComponent();
        }

        #region 调用接口以及加密解密方法
        CodingHelper ch = new CodingHelper();
        InterfaceLayer.Warehouse.WarehouseInDetailInterface waredeta = new InterfaceLayer.Warehouse.WarehouseInDetailInterface();
        SupplierInterface supply = new SupplierInterface();
        EmpolyeeInterface employee = new EmpolyeeInterface();
        InterfaceLayer.Purchase.PurchaseDetailInterface pdi = new InterfaceLayer.Purchase.PurchaseDetailInterface();

        #endregion

        #region 数据字段

        /// <summary>
        /// 所有供应商
        /// </summary>
        private DataTable _AllSupply = null;
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
        private GridRow _wareHouseModel;
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
        /// 保存供应商Code
        /// </summary>
        private string _suppliercode;
        /// <summary>
        /// 入库code
        /// </summary>
        private string _warehouseincode;
        #endregion

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WareHouseInventoryProfit_Load(object sender, EventArgs e)
        {
            ////供应商
            //_AllSupply = supply.SelSupplierTable();

            ////业务员
            //_AllEmployee = employee.SelSupplierTable(false);

            //数量
            GridDoubleInputEditControl gdiecNumber = superGridControl1.PrimaryGrid.Columns["gridColumnzhangmianshu"].EditControl as GridDoubleInputEditControl;
            gdiecNumber.MinValue = 0;
            gdiecNumber.MaxValue = 999999999;

            GridDoubleInputEditControl gdiecpandianNumber = superGridControl1.PrimaryGrid.Columns["gridColumnpandianshu"].EditControl as GridDoubleInputEditControl;
            gdiecpandianNumber.MinValue = 0;
            gdiecpandianNumber.MaxValue = 999999999;

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
            _warehouseincode = BuildCode.ModuleCode("WIP");
            textBoxOddNumbers.Text = _warehouseincode;
            barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
            _Code.ValueFont = new Font("微软雅黑", 20);
            System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
            pictureBox9.Image = imgTemp;

        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //表的点击事件
        }

        private void DataGridViewFujia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //表的点击事件
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
            gr.Cells["gridColumnzhangmianshu"].Value = 0;
            gr.Cells["gridColumnzhangmianshu"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnzhangmianshu"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumnpandianshu"].Value = 0;
            gr.Cells["gridColumnpandianshu"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnpandianshu"].CellStyles.Default.Background.Color1 = Color.Orange;
            gr.Cells["gridColumnmoney"].Value = 0;
            gr.Cells["gridColumnmoney"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumnmoney"].CellStyles.Default.Background.Color1 = Color.Orange;
        }

        /// <summary>
        /// 初始化业务员
        /// </summary>
        private void InitEmployee()
        {
           
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
        #endregion

        private void WareHouseInventoryProfit_Activated(object sender, EventArgs e)
        {
            comboBoxEx1.Focus();
        }
    }
}
