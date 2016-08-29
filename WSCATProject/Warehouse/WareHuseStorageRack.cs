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
    public partial class WareHuseStorageRack : Form
    {
        public WareHuseStorageRack()
        {
            InitializeComponent();
        }

        private void WareHuseStorageRack_Load(object sender, EventArgs e)
        {
            comboBoxEx2.SelectedIndex = 0;
            comboBoxEx3.SelectedIndex = 0;
            comboBoxEx4.SelectedIndex = 0;
            StorageInterface sif = new StorageInterface();
            DataTable dt = sif.SelStorage();
            DataRow dr = dt.NewRow();
            dr["name"] = "请选择";
            dt.Rows.InsertAt(dr, 0);

            comboBoxEx1.DisplayMember = "name";
            comboBoxEx1.ValueMember = "code";
            comboBoxEx1.DataSource = dt;
        }

        private void comboBoxEx1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxEx1.SelectedValue == null)
            {
                comboBoxEx2.DataSource = null;
                return;
            }
            StorageRackInterface srif = new StorageRackInterface();
            DataTable dt = srif.SelStorageRackByCode(XYEEncoding.strCodeHex(comboBoxEx1.SelectedValue.ToString()));
            DataRow dr = dt.NewRow();
            dr["name"] = "请选择";
            dt.Rows.InsertAt(dr, 0);

            comboBoxEx2.DisplayMember = "name";
            comboBoxEx2.ValueMember = "code";
            comboBoxEx2.DataSource = dt;
        }

        private void comboBoxEx2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEx2.SelectedValue == null)
            {
                comboBoxEx3.DataSource = null;
                return;
            }
            StorageRackInterface srif = new StorageRackInterface();
            DataTable dt = srif.SelStorageRackByCode(XYEEncoding.strCodeHex(comboBoxEx2.SelectedValue.ToString()));
            DataRow dr = dt.NewRow();
            dr["name"] = "请选择";
            dt.Rows.InsertAt(dr, 0);

            comboBoxEx3.DisplayMember = "name";
            comboBoxEx3.ValueMember = "code";
            comboBoxEx3.DataSource = dt;
        }

        private void comboBoxEx3_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxEx3.SelectedValue == null)
            {
                comboBoxEx4.DataSource = null;
                return;
            }
            StorageRackInterface srif = new StorageRackInterface();
            DataTable dt = srif.SelStorageRackByCode(XYEEncoding.strCodeHex(comboBoxEx3.SelectedValue.ToString()));
            DataRow dr = dt.NewRow();
            dr["name"] = "请选择";
            dt.Rows.InsertAt(dr, 0);

            comboBoxEx4.DisplayMember = "name";
            comboBoxEx4.ValueMember = "code";
            comboBoxEx4.DataSource = dt;
        }
    }
}
