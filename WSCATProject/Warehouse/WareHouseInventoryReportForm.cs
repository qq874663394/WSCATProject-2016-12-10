using DevComponents.DotNetBar.SuperGrid;
using HelperUtility.Encrypt;
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
    public partial class WareHouseInventoryReportForm : Form
    {
        public WareHouseInventoryReportForm()
        {
            InitializeComponent();
        }
        private decimal _MaterialMoney;
        private decimal _MaterialNumber;
        private void WareHouseInventoryReportForm_Load(object sender, EventArgs e)
        {
            //调用表格初始化
            InitDataGridView();
            #region 盘点方案
            WarehouseInventoryInterface iface = new WarehouseInventoryInterface();
            CodingHelper codeh = new CodingHelper();

            DataTable dt = codeh.DataTableReCoding(iface.GetList());
            DataRow dr = dt.NewRow();
            dr["name"] = "请选择";
            dt.Rows.InsertAt(dr, 0);

            comboBoxEx1.DisplayMember = "name";
            comboBoxEx1.ValueMember = "code";
            comboBoxEx1.DataSource = dt;
            #endregion

            //盘点数量
            GridDoubleInputEditControl gdiecNumber = superGridControl1.PrimaryGrid.Columns["gridColumn8"].EditControl as GridDoubleInputEditControl;
            gdiecNumber.MinValue = 0;
            gdiecNumber.MaxValue = 999999999;
          


        }

        private void comboBoxEx1_SelectedValueChanged(object sender, EventArgs e)
        {
            WarehouseInventoryInterface iface = new WarehouseInventoryInterface();
            CodingHelper codeh = new CodingHelper();

            if (comboBoxEx1.SelectedValue == null || comboBoxEx1.SelectedValue.ToString() == "")
            {
                //绑定dgv   查询全部数据
                DataTable dt = codeh.DataTableReCoding(iface.GetTbList(1, ""));

                if (dt == null)
                {
                    superGridControl1.PrimaryGrid.DataSource = null;
                }
                else
                {
                    superGridControl1.PrimaryGrid.DataSource = dt;
                }
            }
            else
            {
                string a = comboBoxEx1.SelectedValue.ToString();
                DataTable dts = codeh.DataTableReCoding(iface.GetTbList(2, XYEEncoding.strCodeHex(comboBoxEx1.SelectedValue.ToString())));
                superGridControl1.PrimaryGrid.DataSource = dts;
            }
        }

        /// <summary>
        /// supergridControl表格初始化
        /// </summary>
        private void InitDataGridView()
        {
            //改为点击可编辑
            superGridControl1.PrimaryGrid.MouseEditMode = MouseEditMode.SingleClick;
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
            //贮存数量
            gr.Cells["gridColumn7"].Value = 0;
            gr.Cells["gridColumn7"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumn7"].CellStyles.Default.Background.Color1 = Color.Orange;
            //盘点数量
            gr.Cells["gridColumn8"].Value = 0;
            gr.Cells["gridColumn8"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumn8"].CellStyles.Default.Background.Color1 = Color.Orange;
            //盘盈数量
            gr.Cells["gridColumn9"].Value = 0;
            gr.Cells["gridColumn9"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumn9"].CellStyles.Default.Background.Color1 = Color.Orange;
            //盘亏数量
            gr.Cells["gridColumn10"].Value = 0;
            gr.Cells["gridColumn10"].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gr.Cells["gridColumn10"].CellStyles.Default.Background.Color1 = Color.Orange;

        }

        private void superGridControl1_CellValidated(object sender, GridCellValidatedEventArgs e)
        {
            GridRow gr = e.GridPanel.Rows[e.GridCell.RowIndex] as GridRow;
            //计算盘盈、盘亏的数量
            try
            {
                decimal tempAllNumber = 0;
                decimal tempAllMoney = 0;
                decimal zhucunnumber = Convert.ToDecimal(gr.Cells["gridColumn7"].FormattedValue);
                decimal pandiannumber = Convert.ToDecimal(gr.Cells["gridColumn8"].FormattedValue);
                decimal panying = pandiannumber - zhucunnumber;
                if (panying > 0)
                {
                    gr.Cells["gridColumn9"].Value = panying;
                    gr.Cells["gridColumn10"].Value = "0.00";
                }
                if (panying < 0)
                {
                    gr.Cells["gridColumn10"].Value = panying;
                    gr.Cells["gridColumn9"].Value = "0.00";
                }

                //逐行统计数据总数
                for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count - 1; i++)
                {
                    GridRow tempGR = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                    tempAllNumber += Convert.ToDecimal(tempGR["gridColumn7"].FormattedValue);
                    tempAllMoney += Convert.ToDecimal(tempGR["gridColumn8"].FormattedValue);
                }
                _MaterialMoney = tempAllMoney;
                _MaterialNumber = tempAllNumber;
                gr = (GridRow)superGridControl1.PrimaryGrid.LastSelectableRow;
                gr["gridColumn7"].Value = _MaterialNumber.ToString();
                gr["gridColumn8"].Value = _MaterialMoney.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex.Message);
            }
        }
    }
}
