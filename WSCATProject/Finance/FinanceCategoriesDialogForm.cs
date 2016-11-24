using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSCATProject.Finance
{
    public partial class FinanceCategoriesDialogForm : Form
    {
        public FinanceCategoriesDialogForm()
        {
            InitializeComponent();
        }

        private void FinanceCategoriesDialogForm_Load(object sender, EventArgs e)
        {

        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            string name; //类别名称
            try
            {
                name = this.txtName.Text;
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("请在输入值后再进行添加操作！");
                    return;
                }
                else
                {
                    //传值

                }
            }
            catch
            {
                //执行失败
            }
                this.Close();
                this.Dispose();
        }
    }
}
