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
            StorageInterface sif = new StorageInterface();
            comboBoxEx1.DisplayMember = "name";
            comboBoxEx1.ValueMember = "code";
            comboBoxEx1.DataSource = sif.SelStorage();
        }

        private void comboBoxEx1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxEx1.SelectedValue==null)
            {
                MessageBox.Show("请选择仓库。");
                return;
            }
            StorageRackInterface srif = new StorageRackInterface();
            comboBoxEx2.DisplayMember = "name";
            comboBoxEx2.ValueMember = "code";
            comboBoxEx2.DataSource = srif.SelStorageRackByCode(XYEEncoding.strCodeHex(comboBoxEx1.SelectedValue.ToString()));
        }
    }
}
