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
            comboBoxEx1.ValueMember = "";
            comboBoxEx1.DisplayMember = "";
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
