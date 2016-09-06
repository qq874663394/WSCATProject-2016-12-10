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

        private void WareHouseInventoryForm_Load(object sender, EventArgs e)
        {
            #region 仓库列表
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
    }
}
